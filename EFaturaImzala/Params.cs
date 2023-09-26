using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFaturaImzala
{
    class Params
    {
        private EFaturaPrm _eFaturaPrm;
        private EArsivPrm _eArsivPrm;

        public EArsivPrm EArsivPrm
        {
            get
            {
                return this._eArsivPrm;
            }
            set
            {
                this._eArsivPrm = value;
            }
        }

        public EFaturaPrm EFaturaPrm
        {
            get
            {
                return this._eFaturaPrm;
            }
            set
            {
                this._eFaturaPrm = value;

            }
        }
    }
}