using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tr.gov.tubitak.uekae.esya.api.common.util;

namespace EFaturaImzala
{
    class LisansHelper
    {
        private static bool besLicenseLoaded = false;

        public static void loadBesLicense()
        {
            if (LisansHelper.besLicenseLoaded)
                return;
            Directory.GetCurrentDirectory();
            LicenseUtil.setLicenseXml((Stream) new FileStream(Path.Combine(Application.StartupPath, "e-imza\\BES_lisans.xml"), FileMode.Open, FileAccess.Read));
            LisansHelper.besLicenseLoaded = true;
        }
    }
}
