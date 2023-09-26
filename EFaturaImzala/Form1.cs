using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFaturaImzala
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Params prm = new Params();

            EArsivPrm eprm = new EArsivPrm { KartCinsi = "AKIS", KartSifresi = "1234" };

            EFaturaPrm efprm = new EFaturaPrm
            {
                KartCinsi = "AKIS",
                KartSifresi = "1234",
                EFaturaAlisKlasor = "D:\\E-FaturaAlis",
                EFaturaSatisKlasor = "D:\\E-FaturaSatis",
                EFaturaKlasor = "D:\\E-Fatura"
            };

            eprm.EArsivSatisImzasizKlasor = "D:\\Temp";
            prm.EFaturaPrm = efprm;
            prm.EArsivPrm = eprm;



            var help = new SignHelper(prm, true);
            help.Imzala("d:\\EAR2019000000001.xml", null);
        }
    }
}
