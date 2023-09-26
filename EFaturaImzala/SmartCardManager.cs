using iaik.pkcs.pkcs11.wrapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tr.gov.tubitak.uekae.esya.api.asn.x509;
using tr.gov.tubitak.uekae.esya.api.common;
using tr.gov.tubitak.uekae.esya.api.common.crypto;
using tr.gov.tubitak.uekae.esya.api.common.util;
using tr.gov.tubitak.uekae.esya.api.common.util.bag;
using tr.gov.tubitak.uekae.esya.api.smartcard.config;
using tr.gov.tubitak.uekae.esya.api.smartcard.gui;
using tr.gov.tubitak.uekae.esya.api.smartcard.pkcs11;

namespace EFaturaImzala
{
    public class SmartCardManager
    {
        private static readonly ILog LOGGER = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int mSlotCount = 0;
        private static SmartCardManager mSCManager;
        private readonly string mSerialNumber;
        private ECertificate mSignatureCert;
        private ECertificate mEncryptionCert;
        protected IBaseSmartCard bsc;
        protected BaseSigner mSigner;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SmartCardManager getInstance()
        {
            if (SmartCardManager.mSCManager == null)
            {
                SmartCardManager.mSCManager = new SmartCardManager();
                return SmartCardManager.mSCManager;
            }
            try
            {
                if (SmartCardManager.mSCManager.getSlotCount() < SmartOp.getCardTerminals().Length)
                {
                    SmartCardManager.LOGGER.Debug((object)"New card pluged in to system");
                    SmartCardManager.mSCManager = (SmartCardManager)null;
                    return SmartCardManager.getInstance();
                }
                string str;
                try
                {
                    str = StringUtil.ToString(SmartCardManager.mSCManager.getBasicSmartCard().getSerial());
                }
                catch (SmartCardException ex)
                {
                    SmartCardManager.LOGGER.Debug((object)"Card removed");
                    SmartCardManager.mSCManager = (SmartCardManager)null;
                    return SmartCardManager.getInstance();
                }
                if (SmartCardManager.mSCManager.getSelectedSerialNumber().Equals(str))
                    return SmartCardManager.mSCManager;
                SmartCardManager.LOGGER.Debug((object)"Serial number changed. New card is placed to system");
                SmartCardManager.mSCManager = (SmartCardManager)null;
                return SmartCardManager.getInstance();
            }
            catch (SmartCardException ex)
            {
                SmartCardManager.mSCManager = (SmartCardManager)null;
                throw;
            }
        }

        private SmartCardManager()
        {
            try
            {
                using (FileStream fileStream = File.Open(Path.Combine(System.Windows.Forms.Application.StartupPath, "e-imza", "smartcard-config.xml"), FileMode.Open, FileAccess.Read, FileShare.Read))
                    CardType.applyCardTypeConfig(new SmartCardConfigParser().readConfig((Stream)fileStream));
                SmartCardManager.LOGGER.Debug((object)"New SmartCardManager will be created");
                string[] cardTerminals = SmartOp.getCardTerminals();
                if (cardTerminals == null || cardTerminals.Length == 0)
                    throw new SmartCardException("Kart takılı kart okuyucu bulunamadı");
                SmartCardManager.LOGGER.Debug((object)("Kart okuyucu sayısı : " + (object)cardTerminals.Length));
                int index1 = 0;
                string terminal;
                if (cardTerminals.Length == 1)
                {
                    terminal = cardTerminals[index1];
                }
                else
                {
                    int index2 = SmartCardManager.askOption((Control)null, (Icon)null, cardTerminals, "Okuyucu Listesi", new string[1]
                    {
            "Tamam"
                    });
                    terminal = cardTerminals[index2];
                }
                SmartCardManager.LOGGER.Debug((object)"PKCS11 Smartcard will be created");
                try
                {
                    Pair<long, CardType> slotAndCardType = SmartOp.getSlotAndCardType(terminal);
                    this.bsc = (IBaseSmartCard)new P11SmartCard(slotAndCardType.getmObj2());
                    this.bsc.openSession(slotAndCardType.getmObj1());
                }
                catch (Exception ex)
                {
                    long[] tokenPresentSlotList = new SmartCard(CardTypeConverter.AsCardType("")).getTokenPresentSlotList();
                    int index2 = 0;
                    long int64;
                    if (tokenPresentSlotList.Length == 1)
                    {
                        int64 = tokenPresentSlotList[index2];
                    }
                    else
                    {
                        List<string> stringList = new List<string>();
                        foreach (long num in tokenPresentSlotList)
                            stringList.Add(num.ToString());
                        int64 = Convert.ToInt64(SmartCardManager.askOption((Control)null, (Icon)null, stringList.ToArray(), "Slot Listesi", new string[1]
                        {
              "Tamam"
                        }));
                    }
                    this.bsc = (IBaseSmartCard)new P11SmartCard(CardTypeConverter.AsCardType(""));
                    this.bsc.openSession(int64);
                }
                this.mSerialNumber = StringUtil.ToString(this.bsc.getSerial());
                this.mSlotCount = cardTerminals.Length;
            }
            catch (SmartCardException ex)
            {
                throw ex;
            }
            catch (PKCS11Exception ex)
            {
                throw new SmartCardException("Pkcs11 exception", (Exception)ex);
            }
            catch (IOException ex)
            {
                throw new SmartCardException("Smart Card IO exception", (Exception)ex);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseSigner getSigner(string aCardPIN, ECertificate aCert)
        {
            if (this.mSigner == null)
            {
                this.bsc.login(aCardPIN);
                this.mSigner = this.bsc.getSigner(aCert, Algorithms.SIGNATURE_RSA_SHA256);
            }
            return this.mSigner;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void logout()
        {
            this.mSigner = (BaseSigner)null;
            this.bsc.logout();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public ECertificate getSignatureCertificate(
          bool checkIsQualified,
          bool checkBeingNonQualified)
        {
            if (this.mSignatureCert == null)
            {
                List<byte[]> signatureCertificates = this.bsc.getSignatureCertificates();
                this.mSignatureCert = this.selectCertificate(checkIsQualified, checkBeingNonQualified, signatureCertificates);
            }
            return this.mSignatureCert;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public ECertificate getEncryptionCertificate(
          bool checkIsQualified,
          bool checkBeingNonQualified)
        {
            if (this.mEncryptionCert == null)
            {
                List<byte[]> encryptionCertificates = this.bsc.getEncryptionCertificates();
                this.mEncryptionCert = this.selectCertificate(checkIsQualified, checkBeingNonQualified, encryptionCertificates);
            }
            return this.mEncryptionCert;
        }

        private ECertificate selectCertificate(
          bool checkIsQualified,
          bool checkBeingNonQualified,
          List<byte[]> aCerts)
        {
            if (aCerts != null && aCerts.Count == 0)
                throw new ESYAException("Kartta sertifika bulunmuyor");
            if (checkIsQualified && checkBeingNonQualified)
                throw new ESYAException("Bir sertifika ya nitelikli sertifikadir, ya niteliksiz sertifikadir. Hem nitelikli hem niteliksiz olamaz");
            List<ECertificate> ecertificateList = new List<ECertificate>();
            foreach (byte[] aCert in aCerts)
            {
                ECertificate ecertificate = new ECertificate(aCert);
                if (checkIsQualified)
                {
                    if (ecertificate.isQualifiedCertificate())
                        ecertificateList.Add(ecertificate);
                }
                else if (checkBeingNonQualified)
                {
                    if (!ecertificate.isQualifiedCertificate())
                        ecertificateList.Add(ecertificate);
                }
                else
                    ecertificateList.Add(ecertificate);
            }
            ECertificate ecertificate1 = (ECertificate)null;
            if (ecertificateList.Count == 0)
            {
                if (checkIsQualified)
                    throw new ESYAException("Kartta nitelikli sertifika bulunmuyor");
                if (checkBeingNonQualified)
                    throw new ESYAException("Kartta niteliksiz sertifika bulunmuyor");
            }
            else if (ecertificateList.Count == 1)
            {
                ecertificate1 = ecertificateList[0];
            }
            else
            {
                string[] aSecenekList = new string[ecertificateList.Count];
                for (int index = 0; index < ecertificateList.Count; ++index)
                    aSecenekList[index] = ecertificateList[index].getSubject().getCommonNameAttribute();
                int index1 = SmartCardManager.askOption((Control)null, (Icon)null, aSecenekList, "Sertifika Listesi", new string[1]
                {
          "Tamam"
                });
                ecertificate1 = index1 >= 0 ? ecertificateList[index1] : (ECertificate)null;
            }
            return ecertificate1;
        }

        private string getSelectedSerialNumber()
        {
            return this.mSerialNumber;
        }

        private int getSlotCount()
        {
            return this.mSlotCount;
        }

        public IBaseSmartCard getBasicSmartCard()
        {
            return this.bsc;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void reset()
        {
            SmartCardManager.mSCManager = (SmartCardManager)null;
        }

        public static int askOption(
          Control aParent,
          Icon aIcon,
          string[] aSecenekList,
          string aBaslik,
          string[] aOptions)
        {
            SlotList slotList = new SlotList((Control)null, aIcon, aSecenekList, aBaslik);
            if (slotList.ShowDialog() != DialogResult.OK)
                return -1;
            return slotList.getSelectedIndex();
        }
    }

}