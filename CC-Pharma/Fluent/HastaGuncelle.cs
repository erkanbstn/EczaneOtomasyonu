using DevExpress.XtraEditors;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class HastaGuncelle : DevExpress.XtraEditors.XtraForm
    {
        public HastaGuncelle()
        {
            InitializeComponent();
        }
        private void HastaGuncelle_Load(object sender, EventArgs e)
        {
            if (Cache.Ilac == "İlacı Var")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            textBox1.Text = Cache.Hasta;
            richTextBox1.Text = Cache.Not;
            textBox2.Text = Cache.Tarih;
        }

        private void HastaGuncelle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                simpleButton1.PerformClick();
            }
            if (e.KeyCode == Keys.F5)
            {
                simpleButton4.PerformClick();
            }
        }
        Main f2;
        public void acilis()
        {
            Hastalar f3 = new Hastalar();
            f3.splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            f3.splashScreenManager1.CloseWaitForm();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox1.Text != null)
                {
                    f2 = new Main();
                    string hasta = textBox1.Text;
                    acilis();
                    var x = Cache.db.EczaneYaren.Find(hasta);
                    x.IlacDurum = Cache.Ilac;
                    x.Tarih = textBox2.Text;
                    DateTime bugun = DateTime.Now;
                    if (Cache.Ilac == "İlacı Aldı")
                    {
                        x.Not = richTextBox1.Text.Trim() + "\n" + $"- {bugun} Tarihinde {textBox2.Text} İlacını Aldı.".Trim();
                    }
                    else
                    {
                        x.Not = richTextBox1.Text;
                    }
                    temizle();
                    Cache.db.SaveChanges();


                    // RAPOR EKLEME
                    EczaneYarenRapor g = new EczaneYarenRapor();
                    Random d = new Random();
                    g.RaporNo = d.Next(10000000, 99999999).ToString();
                    g.Rapor = textBox1.Text.ToUpper() + " " + "İsimli Hasta, Hasta Sisteminden Güncellendi.";
                    g.RaporTarih = DateTime.Now.ToString();
                    Cache.db.EczaneYarenRapor.Add(g);
                    Cache.db.SaveChanges();
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("Sisteme Kayıtlı Böyle Bir Hasta Yok", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Lütfen Girdiğiniz Değerleri Kontrol Edin.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void temizle()
        {
            richTextBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox2.Text = "";
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Cache.Ilac = "İlacı Var";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Cache.Ilac = "İlacı Aldı";
        }
    }
}