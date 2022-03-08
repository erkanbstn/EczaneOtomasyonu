using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class BorcGuncelle : DevExpress.XtraEditors.XtraForm
    {
        public BorcGuncelle()
        {
            InitializeComponent();
        }
        bool kapat, ustuneekle;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                kapat = true;
            }
            else
            {
                kapat = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox5.Text = "0,00";
                textBox5.Enabled = true;
                ustuneekle = true;
            }
            else
            {
                textBox5.Text = "0,00";
                textBox5.Enabled = false;
                ustuneekle = false;
            }
        }
        Hastalar f3;
        public void acilis()
        {
            f3 = new Hastalar();
            f3.splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            f3.splashScreenManager1.CloseWaitForm();
        }
        void borcodendi()
        {
            string hasta = textBox1.Text;
            var x = Cache.db.EczaneYarenBorc.Find(hasta);

            if (ustuneekle == true)
            {
                DateTime bugun = DateTime.Now;
                x.Not = richTextBox1.Text.Trim() + "\n" + $"- {bugun} Tarihinde {textBox5.Text} TL Daha Borç Yazdırdı".Trim() + "\n" + $"- {bugun} Tarihinde {textBox3.Text} TL Borcundan Ödedi".Trim();
                x.Kalan = Convert.ToDecimal(textBox4.Text);
                x.Borc = Convert.ToDecimal(textBox4.Text);

            }
            else if (kapat == true)
            {
                x.KapanmaTarih = DateTime.Now.ToString();
                DateTime bugun = DateTime.Now;
                x.Not = richTextBox1.Text.Trim() + "\n" + $"- {bugun} Tarihinde {textBox2.Text} TL Borcunu Kapattım.".Trim() + "\n" + $"- {bugun} Tarihinde {textBox3.Text} TL Borcundan Ödedi".Trim();
                x.Kalan = 0;
                x.BorcDurum = "Borcu Kapandı";
                x.Borc = 0;
            }
            else
            {
                DateTime bugun = DateTime.Now;
                x.Borc = Convert.ToDecimal(textBox4.Text);
                x.Not = richTextBox1.Text + "\n" + $"- {bugun} Tarihinde {textBox3.Text} TL Borcundan Ödedi".Trim();
                x.Kalan = Convert.ToDecimal(textBox4.Text);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" || textBox1.Text != null)
                {
                    string hasta = textBox1.Text;
                    var x = Cache.db.EczaneYarenBorc.Find(hasta);

                    if (textBox3.Text != "" && textBox3.Text != "0")
                    {
                        borcodendi();
                    }
                    else
                    {
                        if (ustuneekle == true)
                        {
                            DateTime bugun = DateTime.Now;
                            x.Not = richTextBox1.Text.Trim() + "\n" + $"- {bugun} Tarihinde {textBox5.Text} TL Daha Borç Yazdırdı".Trim();
                            x.Borc = Convert.ToDecimal(textBox4.Text);

                        }
                        else if (kapat == true)
                        {
                            x.KapanmaTarih = DateTime.Now.ToString();
                            DateTime bugun = DateTime.Now;
                            x.Not = richTextBox1.Text.Trim() + "\n" + $"- {bugun} Tarihinde {textBox2.Text} TL Borcunu Kapattım.".Trim();
                            x.Kalan = 0;
                            x.BorcDurum = "Borcu Kapandı";
                            x.Borc = 0;
                        }
                        else
                        {
                            x.Borc = Convert.ToDecimal(textBox2.Text);
                            x.Not = richTextBox1.Text;
                            x.Kalan = Convert.ToDecimal(textBox4.Text);
                        }
                    }
                    // RAPOR EKLEME
                    EczaneYarenRapor g = new EczaneYarenRapor();
                    Random d = new Random();
                    g.RaporNo = d.Next(10000000, 99999999).ToString();
                    g.Rapor = textBox1.Text.ToUpper() + " " + "İsimli Hasta, Borç Sisteminden Güncellendi.";
                    g.RaporTarih = DateTime.Now.ToString();
                    Cache.db.EczaneYarenRapor.Add(g);
                    Cache.db.SaveChanges();
                    acilis();
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("Sisteme Kayıtlı Böyle Bir Hasta Yok", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Lütfen Girdiğiniz Bilgileri Kontrol Ediniz.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            temizle();
        }
        decimal? a, b, c;

        private void BorcGuncelle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                simpleButton2.PerformClick();
            }
            if (e.KeyCode == Keys.F5)
            {
                simpleButton4.PerformClick();
            }
        }
        decimal? g;
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                a = Convert.ToDecimal(textBox2.Text);
                g = Convert.ToDecimal(textBox3.Text);
                c = a - g;
                textBox4.Text = c.ToString();
            }
            catch (Exception)
            {
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                a = Convert.ToDecimal(textBox2.Text);
                b = Convert.ToDecimal(textBox5.Text);
                c = a + b;
                textBox4.Text = c.ToString();
            }
            catch (Exception)
            {
            }
        }
        void temizle()
        {
            textBox2.Text = "0,00";
            textBox3.Text = "0,00";
            textBox4.Text = "0,00";
            textBox5.Text = "0,00";
            richTextBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
        }
        private void BorcGuncelle_Load(object sender, EventArgs e)
        {

            textBox5.Text = "0,00";
            textBox1.Text = Cache2.Hasta;
            textBox2.Text = Cache2.Borc.ToString();
            textBox4.Text = Cache2.Kalan.ToString();
            richTextBox1.Text = Cache2.Not;
        }
    }
}