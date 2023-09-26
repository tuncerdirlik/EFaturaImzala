using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFaturaImzala
{
    public class EArsivPrm
    {
        private Imzalayan _imzalayan;
        private string _kartCinsi;
        private string _kartSifresi;
        private string _entegratorKodu;
        private string _webserviceUsername;
        private string _webservicePassword;
        private string _senderAlias;
        private long? _timerEntegratoreGonder;
        private long? _timerEntegratordenAl;
        private string _entegratorAdi;
        private string _tamamlanmamisFaturalarArsivlensinMi;
        private string _kabulRedAsamasindaOnayAl;
        private string _eArsivSatisKlasor;
        private string _eArsivArsivSatisKlasor;
        private string _eArsivEntegratorArsivKlasor;
        private string _eArsivSatisImzasizKlasor;
        private string _eArsivKlasor;
        private string _eArsivEntegratorRaporKlasor;
        private string _eArsivIptalFaturaKlasor;

        public string EArsivIptalFaturaKlasor
        {
            get
            {
                if (this._eArsivIptalFaturaKlasor == null)
                    this._eArsivIptalFaturaKlasor = Path.Combine(this._eArsivSatisKlasor, "IPTAL");
                return this._eArsivIptalFaturaKlasor;
            }
            set
            {
                this._eArsivIptalFaturaKlasor = value;
            }
        }

        public string EArsivEntegratorRaporKlasor
        {
            get
            {
                return this._eArsivEntegratorRaporKlasor;
            }
            set
            {
                this._eArsivEntegratorRaporKlasor = value;
            }
        }

        public string EArsivKlasor
        {
            get
            {
                return this._eArsivKlasor;
            }
            set
            {
                this._eArsivKlasor = value;
                this._eArsivSatisImzasizKlasor = Path.Combine(this._eArsivSatisKlasor, "IMZASIZ");
                this._eArsivArsivSatisKlasor = Path.Combine(this._eArsivSatisKlasor, "ARSIV_SATIS");
                this._eArsivEntegratorArsivKlasor = Path.Combine(this._eArsivSatisKlasor, "ARSIV_ENTEGRATOR");
                this._eArsivEntegratorRaporKlasor = Path.Combine(this._eArsivKlasor, "RAPOR_ENTEGRATOR");
            }
        }

        public string EArsivSatisImzasizKlasor
        {
            get
            {
                return this._eArsivSatisImzasizKlasor;
            }
            set
            {
                this._eArsivSatisImzasizKlasor = value;
            }
        }

        public string EArsivEntegratorArsivKlasor
        {
            get
            {
                return this._eArsivEntegratorArsivKlasor;
            }
            set
            {
                this._eArsivEntegratorArsivKlasor = value;
            }
        }

        public string EArsivArsivSatisKlasor
        {
            get
            {
                return this._eArsivArsivSatisKlasor;
            }
            set
            {
                this._eArsivArsivSatisKlasor = value;
            }
        }

        public string EArsivSatisKlasor
        {
            get
            {
                return this._eArsivSatisKlasor;
            }
            set
            {
                this._eArsivSatisKlasor = value;
            }
        }

        public string KabulRedAsamasindaOnayAl
        {
            get
            {
                return this._kabulRedAsamasindaOnayAl;
            }
            set
            {
                this._kabulRedAsamasindaOnayAl = value;
            }
        }

        public string TamamlanmamisFaturalarArsivlensinMi
        {
            get
            {
                return this._tamamlanmamisFaturalarArsivlensinMi;
            }
            set
            {
                this._tamamlanmamisFaturalarArsivlensinMi = value;
            }
        }

        public string EntegratorAdi
        {
            get
            {
                return this._entegratorAdi;
            }
            set
            {
                this._entegratorAdi = value;
            }
        }

        public long? TimerEntegratordenAl
        {
            get
            {
                return this._timerEntegratordenAl;
            }
            set
            {
                this._timerEntegratordenAl = value;
            }
        }

        public long? TimerEntegratoreGonder
        {
            get
            {
                return this._timerEntegratoreGonder;
            }
            set
            {
                this._timerEntegratoreGonder = value;
            }
        }

        public string SenderAlias
        {
            get
            {
                return this._senderAlias;
            }
            set
            {
                this._senderAlias = value;
            }
        }

        public string WebservicePassword
        {
            get
            {
                return this._webservicePassword;
            }
            set
            {
                this._webservicePassword = value;
            }
        }

        public string WebserviceUsername
        {
            get
            {
                return this._webserviceUsername;
            }
            set
            {
                this._webserviceUsername = value;
            }
        }

        public string EntegratorKodu
        {
            get
            {
                return this._entegratorKodu;
            }
            set
            {
                this._entegratorKodu = value;
            }
        }

        public string KartSifresi
        {
            get
            {
                return this._kartSifresi;
            }
            set
            {
                this._kartSifresi = value;
            }
        }

        public string KartCinsi
        {
            get
            {
                return this._kartCinsi;
            }
            set
            {
                this._kartCinsi = value;
            }
        }

        public Imzalayan Imzalayan
        {
            get
            {
                return this._imzalayan;
            }
            set
            {
                this._imzalayan = value;
            }
        }

    }
}
