using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using tr.gov.tubitak.uekae.esya.api.common;
using tr.gov.tubitak.uekae.esya.api.xmlsignature.config;
using tr.gov.tubitak.uekae.esya.api.xmlsignature;
using tr.gov.tubitak.uekae.esya.api.xmlsignature.model;
using tr.gov.tubitak.uekae.esya.api.asn.x509;
using tr.gov.tubitak.uekae.esya.api.common.crypto;

namespace EFaturaImzala
{
    internal class SignHelper
    {
        private const string ENVELOPE_XML = "<envelope>\n  <data id=\"data1\">\n    <item>Item 1</item>\n    <item>Item 2</item>\n    <item>Item 3</item>\n  </data>\n</envelope>\n";
        private Params _params;
        private bool _isEArsiv;

        public SignHelper(Params prm, bool isEArsiv)
        {
            this._params = prm;
            this._isEArsiv = isEArsiv;
        }

        public XmlDocument NewEnvelope(string fileName)
        {
            try
            {
                XmlDocument xmlDocument = (XmlDocument)null;
                using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (XmlReader reader = XmlReader.Create((Stream)fileStream, new XmlReaderSettings()
                    {
                        CloseInput = true
                    }))
                    {
                        xmlDocument = new XmlDocument();
                        xmlDocument.PreserveWhitespace = true;
                        xmlDocument.Load(reader);
                    }
                }
                return xmlDocument;
            }
            catch (Exception ex)
            {
                SupportClass.WriteStackTrace(ex, Console.Error);
            }
            throw new ESYAException("Cant construct envelope xml ");
        }

        public void Imzala(string sourceFilePath, string contentTag)
        {
            this.CopyConfigFileIfNotExist();
            LisansHelper.loadBesLicense();
            try
            {
                XmlDocument doc = this.NewEnvelope(sourceFilePath);
                Context context = this.CreateContext();
                if (string.IsNullOrEmpty(contentTag))
                    contentTag = "ext:ExtensionContent";
                XmlNode xmlNode = doc.GetElementsByTagName(contentTag).Item(0);
                if (xmlNode == null)
                    throw new Exception("ext:ExtensionContent bulunamadı" + Environment.NewLine + "Dosya adı: " + sourceFilePath);
                context.Document = doc;
                XMLSignature xmlSignature = new XMLSignature(context, false);
                xmlSignature.SigningTime = new DateTime?(DateTime.Now);
                string str1 = doc.DocumentElement.SelectSingleNode("//cac:Signature/cac:DigitalSignatureAttachment/cac:ExternalReference/cbc:URI", XmlHelper.GetXmlNsManager(doc)).FirstChild.Value.Replace("#", (string)null);
                xmlSignature.Id = str1;
                xmlNode.AppendChild((XmlNode)xmlSignature.Element);
                Transforms aTransforms = new Transforms(context);
                aTransforms.addTransform(new Transform(context, TransformType.ENVELOPED.Url));
                xmlSignature.addDocument("", "text/xml", aTransforms, DigestMethod.SHA_256, false);
                SmartCardManager instance = SmartCardManager.getInstance();
                ECertificate signatureCertificate = instance.getSignatureCertificate(false, false);
                string aCardPIN = this._isEArsiv ? this._params.EArsivPrm.KartSifresi : this._params.EFaturaPrm.KartSifresi;
                BaseSigner signer = instance.getSigner(aCardPIN, signatureCertificate);
                xmlSignature.addKeyInfo(signatureCertificate);
                xmlSignature.sign(signer);
                string str2 = Path.Combine(this._isEArsiv ? this._params.EArsivPrm.EArsivSatisImzasizKlasor : this._params.EFaturaPrm.EFaturaSatisImzasizKlasor, Path.GetFileName(sourceFilePath));
                if (File.Exists(str2))
                    File.Delete(str2);
                File.Move(sourceFilePath, str2);
                doc.PreserveWhitespace = false;
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(sourceFilePath, Encoding.UTF8))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlTextWriter.Indentation = 1;
                    xmlTextWriter.IndentChar = '\t';
                    doc.Save((XmlWriter)xmlTextWriter);
                }
            }
            catch (XMLSignatureException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CopyConfigFileIfNotExist()
        {
            try
            {
                string path1 = Path.Combine(Application.StartupPath, "e-imza");
                string path2_1 = "xmlsignature-config.xml";
                string path2_2 = "certval-policy.xml";
                string str1 = Path.Combine(path1, path2_1);
                string str2 = Path.Combine(path1, path2_2);
                string str3 = Path.Combine(Application.UserAppDataPath, path2_1);
                string str4 = Path.Combine(Application.UserAppDataPath, path2_2);
                if (!File.Exists(str3) || File.Exists(str3) && File.GetLastWriteTime(str3) < File.GetLastWriteTime(str1))
                {
                    File.Copy(str1, str3, true);
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(str3);
                    xmlDocument.GetElementsByTagName("certificate-validation-policy-file")[0].ChildNodes[0].Value = str4;
                    xmlDocument.Save(str3);
                }
                if (File.Exists(str4) && (!File.Exists(str4) || !(File.GetLastWriteTime(str4) < File.GetLastWriteTime(str2))))
                    return;
                File.Copy(str2, str4, true);
            }
            catch (Exception ex)
            {
                int num = (int) MessageBox.Show("Config-Policy dosyaları kopyalanamadı: " + ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private Context CreateContext()
        {
            string str = Path.Combine(Application.UserAppDataPath);
            string aPath = Path.Combine(str, "xmlsignature-config.xml");
            return new Context(str) { Config = new Config(aPath) };
        }
    }
}
