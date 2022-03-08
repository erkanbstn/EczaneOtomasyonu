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
    public partial class Personel : DevExpress.XtraEditors.XtraForm
    {
        public Personel()
        {
            InitializeComponent();
        }
        void liste()
        {
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("Select * from EczaneYarenAdmin", Cache.bgl);
                DataTable tb = new DataTable();
                ad.Fill(tb);
                gridControl1.DataSource = tb;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sunucuya Bağlanılamıyor.. Lütfen İnternet Bağlantınızı Kontrol Edin.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       
        public void acilis()
        {
            splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            splashScreenManager1.CloseWaitForm();
        }
        private void Personel_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            liste();
            textBox1.MaxLength = 8;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                EczaneYarenAdmin g = new EczaneYarenAdmin();
                Random d = new Random();
                g.KayitNo = d.Next(10000000, 99999999).ToString();
                g.Personel = textEdit1.Text;
                g.PersonelSifre = textBox1.Text;
                if (radioButton1.Checked == true)
                {
                    g.Yetki = "Personel";
                }
                else if (radioButton2.Checked == true)
                {
                    g.Yetki = "Yetkili";
                }
                else
                {
                    XtraMessageBox.Show("Yetkilendirme İçin En Az Birini Seçmelisiniz.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Cache.db.EczaneYarenAdmin.Add(g); acilis();
                Cache.db.SaveChanges(); liste();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Lütfen Girdiğiniz Değerleri Kontrol Ediniz..", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string kayit = textEdit2.Text;
                var x = Cache.db.EczaneYarenAdmin.Find(kayit);
                Cache.db.EczaneYarenAdmin.Remove(x); acilis();
                Cache.db.SaveChanges(); liste();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Lütfen Girdiğiniz Değerleri Kontrol Ediniz..", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string kayit = textEdit2.Text;
                var x = Cache.db.EczaneYarenAdmin.Find(kayit);
                x.Personel = textEdit1.Text;
                x.PersonelSifre = textBox1.Text;
                if (radioButton1.Checked == true)
                {
                    x.Yetki = "Personel";
                }
                else if (radioButton2.Checked == true)
                {
                    x.Yetki = "Yetkili";
                }
                else
                {
                    XtraMessageBox.Show("Yetkilendirme İçin En Az Birini Seçmelisiniz.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                acilis();
                Cache.db.SaveChanges(); liste();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Lütfen Girdiğiniz Değerleri Kontrol Ediniz..", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView1.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                textEdit2.Text = r[0].ToString();
                textEdit1.Text = r[1].ToString();
                textBox1.Text = r[2].ToString();
                if (r[3].ToString() == "Personel")
                {
                    radioButton1.Checked = true;
                }
                else if(r[3].ToString() == "Yetkili")
                {
                    radioButton2.Checked = true;
                }
            }
        }

        private void Personel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                simpleButton1.PerformClick();
            }
            if (e.KeyCode == Keys.F3)
            {
                simpleButton2.PerformClick();
            }
        }
    }
}