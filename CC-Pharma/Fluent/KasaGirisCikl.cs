using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class KasaGirisCikl : DevExpress.XtraEditors.XtraForm
    {
        public KasaGirisCikl()
        {
            InitializeComponent();
        }
        void temizle()
        {
            textBox1.Text = "0,00";
            richTextBox1.Text = "";
        }
        public void acilis()
        {
            Hastalar f3 = new Hastalar();
            f3.splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            f3.splashScreenManager1.CloseWaitForm();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" || textBox1.Text != null)
                {
                    // KASA EKLEME
                    EczaneGunlukKasa g = new EczaneGunlukKasa();
                    Random d = new Random();
                    g.KayıtNo = d.Next(10000000, 99999999).ToString();
                    g.KasaIslemTipi = Cache2.KasaDurum;
                    g.KasaIslemMiktari = Convert.ToDecimal(textBox1.Text);
                    g.IslemTarihi = DateTime.Now.ToString();
                    g.Not = richTextBox1.Text;
                    Cache.db.EczaneGunlukKasa.Add(g);
                    Cache.db.SaveChanges();
                    acilis();

                    // RAPOR EKLEME
                    EczaneYarenRapor f = new EczaneYarenRapor();
                    f.RaporNo = d.Next(10000000, 99999999).ToString();
                    f.Rapor = $"{textBox1.Text.ToUpper()} TL Kasa Giriş - Çıkış Sistemine Yeni {Cache2.KasaDurum} Yapıldı.";
                    f.RaporTarih = DateTime.Now.ToString();
                    Cache.db.EczaneYarenRapor.Add(f);
                    Cache.db.SaveChanges();
                    temizle();
                    XtraMessageBox.Show("İlgili Miktar Kasaya Eklendi !", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Kasa Miktar Bilgilerine Tekrar Bakınız...", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bilinmeyen Bir Sorun Oluştu...", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void KasaGirisCikl_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Cache2.KasaDurum = "Nakit Girişi (Kâr)";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Cache2.KasaDurum = "Nakit Çıkışı (Zarar)";
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}