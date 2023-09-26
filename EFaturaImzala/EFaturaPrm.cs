using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFaturaImzala
{
    public class EFaturaPrm
    {
        private string _eFaturaKlasor;
        private Imzalayan _imzalayan;
        private string _kartCinsi;
        private string _kartSifresi;
        private string _entegratorKodu;
        private string _webserviceUsername;
        private string _webservicePassword;
        private string _eFaturaAlisKlasor;
        private string _eFaturaAlisEntegratorKlasor;
        private string _eFaturaSatisKlasor;
        private string _eFaturaSatisEntegratorKlasor;
        private string _eFaturaArsivAlisKlasor;
        private string _eFaturaArsivSatisKlasor;
        private string _eFaturaAlisResponseKlasor;
        private string _eFaturaSatisImzasizKlasor;
        private long? _timerEntegratoreGonder;
        private long? _timerEntegratordenAl;
        private string _eFaturaSatisPaketKlasor;
        private string _entegratorAdi;
        private string _tamamlanmamisFaturalarArsivlensinMi;
        private string _kabulRedAsamasindaOnayAl;
        private string _eFaturaDbKlasor;
        private string _dbFilePath;
        private string _gonderenBirim;

        public string GonderenBirim
        {
            get
            {
                return this._gonderenBirim;
            }
            set
            {
                this._gonderenBirim = value;
            }
        }

        public string EFaturaAlisEntegratorKlasor
        {
            get
            {
                return this._eFaturaAlisEntegratorKlasor;
            }
            set
            {
                this._eFaturaAlisEntegratorKlasor = value;
            }
        }

        public string EFaturaSatisEntegratorKlasor
        {
            get
            {
                return this._eFaturaSatisEntegratorKlasor;
            }
            set
            {
                this._eFaturaSatisEntegratorKlasor = value;
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

        public string EFaturaSatisPaketKlasor
        {
            get
            {
                return this._eFaturaSatisPaketKlasor;
            }
            set
            {
                this._eFaturaSatisPaketKlasor = value;
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

        public string EFaturaSatisImzasizKlasor
        {
            get
            {
                return this._eFaturaSatisImzasizKlasor;
            }
            set
            {
                this._eFaturaSatisImzasizKlasor = value;
            }
        }

        public string EFaturaAlisResponseKlasor
        {
            get
            {
                return this._eFaturaAlisResponseKlasor;
            }
            set
            {
                this._eFaturaAlisResponseKlasor = value;
            }
        }

        public string EFaturaArsivSatisKlasor
        {
            get
            {
                return this._eFaturaArsivSatisKlasor;
            }
        }

        public string EFaturaArsivAlisKlasor
        {
            get
            {
                return this._eFaturaArsivAlisKlasor;
            }
        }

        public string EFaturaSatisKlasor
        {
            get
            {
                return this._eFaturaSatisKlasor;
            }
            set
            {
                this._eFaturaSatisKlasor = value;
                this._eFaturaSatisImzasizKlasor = Path.Combine(this._eFaturaSatisKlasor, "IMZASIZ");
                this._eFaturaArsivSatisKlasor = Path.Combine(this._eFaturaSatisKlasor, "ARSIV_SATIS");
                this._eFaturaSatisPaketKlasor = Path.Combine(this._eFaturaSatisKlasor, "PAKET");
                this._eFaturaSatisEntegratorKlasor = Path.Combine(this._eFaturaSatisKlasor, "ENTEGRATOR");
            }
        }

        public string EFaturaAlisKlasor
        {
            get
            {
                return this._eFaturaAlisKlasor;
            }
            set
            {
                this._eFaturaAlisKlasor = value;
                this._eFaturaArsivAlisKlasor = Path.Combine(this._eFaturaAlisKlasor, "ARSIV_ALIS");
                this._eFaturaAlisResponseKlasor = Path.Combine(this._eFaturaAlisKlasor, "APP_RESPONSE");
                this._eFaturaAlisEntegratorKlasor = Path.Combine(this._eFaturaAlisKlasor, "ENTEGRATOR");
            }
        }

        public string EFaturaKlasor
        {
            get
            {
                return this._eFaturaKlasor;
            }
            set
            {
                this._eFaturaKlasor = value;
                this._eFaturaArsivAlisKlasor = Path.Combine(this._eFaturaAlisKlasor, "ARSIV_ALIS");
                this._eFaturaAlisResponseKlasor = Path.Combine(this._eFaturaAlisKlasor, "APP_RESPONSE");
                this._eFaturaArsivSatisKlasor = Path.Combine(this._eFaturaSatisKlasor, "ARSIV_SATIS");
                this._eFaturaSatisImzasizKlasor = Path.Combine(this._eFaturaSatisKlasor, "IMZASIZ");
                this._eFaturaDbKlasor = Path.Combine(this._eFaturaKlasor, "DB");
                this._dbFilePath = Path.Combine(this._eFaturaDbKlasor, "data.db");
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

        public string EFaturaDbKlasor
        {
            get
            {
                return this._eFaturaDbKlasor;
            }
            set
            {
                this._eFaturaDbKlasor = value;
            }
        }

    }
}
