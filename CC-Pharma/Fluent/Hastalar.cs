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
    public partial class Hastalar : DevExpress.XtraEditors.XtraForm
    {
        public Hastalar()
        {
            InitializeComponent();
        }
        public void acilis()
        {
            splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            splashScreenManager1.CloseWaitForm();
        }
        private void Hastalar_Load(object sender, EventArgs e)
        {
            if (Cache.PK == true)
            {
                accordionControlElement5.Visible = false;
                accordionControlElement11.Visible = false;
                accordionControlElement14.Visible = false;
                accordionControlElement16.Visible = false;
            }
            this.Text += $" - Hoşgeldiniz. {Cache.Isim}";
            acilis(); f5 = new BorcMain();
            f5.MdiParent = this;
            f5.Show();
            f2 = new Main();
            f2.MdiParent = this;
            f2.Show();

        }
        private void Hastalar_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult a = XtraMessageBox.Show("Sistemi Kapatmak İstediğinize Emin misiniz?", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (a == DialogResult.Yes)
            {
                acilis();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
        Main f2;
        HastaEkle f3;
        HastaGuncelle f4;
        BorcMain f5;
        BorcEkle f6;
        BorcGuncelle f7;
        Toplam f8;
        Rapor f9;
        Personel f10;
        KasaGirisCikl f11;
        KasaTakip f12;
        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            if (f2 == null || f2.IsDisposed)
            {
                f2 = new Main(); acilis();
                f2.MdiParent = this;
                f2.Show();
            }
            else
            {
                f2.Focus();
            }
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            if (f3 == null || f3.IsDisposed)
            {
                f3 = new HastaEkle(); acilis();
                f3.MdiParent = this;
                f3.Show();
            }
            else
            {
                f3.Focus();
            }
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            f2.sil();
            acilis();
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            acilis();
            if (Application.OpenForms["Main"] == null)
            {
                XtraMessageBox.Show("Lütfen Hasta Listesini Açtıktan Sonra Ekleme İşlemini Gerçekleştiriniz. Aksi Takdirde Anlık Değişimleri Göremeyebilirsiniz.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Main f = (Main)Application.OpenForms["Main"];
                    SqlDataAdapter ad = new SqlDataAdapter("Select * from EczaneYaren", Cache.bgl);
                    DataTable tb = new DataTable();
                    ad.Fill(tb);
                    f.gridControl1.DataSource = tb;
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Sunucuya Bağlanılamıyor.. Lütfen İnternet Bağlantınızı Kontrol Edin.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Hastalar_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void accordionControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                acilis();
                f2.sil();
            }
            if (e.KeyCode == Keys.F1)
            {
                if (f2 == null || f2.IsDisposed)
                {
                    f2 = new Main(); acilis();
                    f2.MdiParent = this;
                    f2.Show();
                }
                else
                {
                    f2.Focus();
                }
            }
            if (e.KeyCode == Keys.F2)
            {
                if (f3 == null || f3.IsDisposed)
                {
                    f3 = new HastaEkle(); acilis();
                    f3.MdiParent = this;
                    f3.Show();
                }
                else
                {
                    f3.Focus();
                }
            }
            if (e.KeyCode == Keys.F5)
            {
                acilis(); f2.hastaliste();
            }
            if (e.KeyCode == Keys.F3)
            {
                if (f4 == null || f4.IsDisposed)
                {
                    f4 = new HastaGuncelle(); acilis();
                    f4.MdiParent = this;
                    f4.Show();
                }
                else
                {
                    f4.Focus();
                }
            }
            if (e.KeyCode == Keys.F7)
            {
                if (f6 == null || f6.IsDisposed)
                {
                    f6 = new BorcEkle(); acilis();
                    f6.MdiParent = this;
                    f6.Show();
                }
                else
                {
                    f6.Focus();
                }
            }
            if (e.KeyCode == Keys.F8)
            {
                if (f7 == null || f7.IsDisposed)
                {
                    f7 = new BorcGuncelle(); acilis();
                    f7.MdiParent = this;
                    f7.Show();
                }
                else
                {
                    f7.Focus();
                }
            }
            if (e.KeyCode == Keys.F11)
            {
                if (f8 == null || f8.IsDisposed)
                {
                    f8 = new Toplam(); acilis();
                    f8.MdiParent = this;
                    f8.Show();
                }
                else
                {
                    f8.Focus();
                }
            }
            
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            if (f4 == null || f4.IsDisposed)
            {
                f4 = new HastaGuncelle(); acilis();
                f4.MdiParent = this;
                f4.Show();
            }
            else
            {
                f4.Focus();
            }
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            if (f5 == null || f5.IsDisposed)
            {
                f5 = new BorcMain(); acilis();
                f5.MdiParent = this;
                f5.Show();
            }
            else
            {
                f5.Focus();
            }
        }

        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            if (f6 == null || f6.IsDisposed)
            {
                f6 = new BorcEkle(); acilis();
                f6.MdiParent = this;
                f6.Show();
            }
            else
            {
                f6.Focus();
            }
        }

        private void accordionControlElement12_Click(object sender, EventArgs e)
        {

            try
            {
                if (Application.OpenForms["BorcMain"] == null)
                {
                    XtraMessageBox.Show("Lütfen Borç Listesini Açtıktan Sonra Ekleme İşlemini Gerçekleştiriniz. Aksi Takdirde Anlık Değişimleri Göremeyebilirsiniz.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    acilis();
                    BorcMain f2 = (BorcMain)Application.OpenForms["BorcMain"];
                    SqlDataAdapter ad = new SqlDataAdapter("Select * from EczaneYarenBorc", Cache.bgl);
                    DataTable tb = new DataTable();
                    ad.Fill(tb);
                    f2.gridControl1.DataSource = tb;
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sunucuya Bağlanılamıyor.. Lütfen İnternet Bağlantınızı Kontrol Edin.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void accordionControlElement10_Click(object sender, EventArgs e)
        {
            if (f7 == null || f7.IsDisposed)
            {
                f7 = new BorcGuncelle(); acilis();
                f7.MdiParent = this;
                f7.Show();
            }
            else
            {
                f7.Focus();
            }
        }

        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            f5.sil(); acilis();
        }

        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
          
        }

        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            if (f9 == null || f9.IsDisposed)
            {
                f9 = new Rapor(); acilis();
                f9.MdiParent = this;
                f9.Show();
            }
            else
            {
                f9.Focus();
            }
        }

        private void accordionControlElement17_Click(object sender, EventArgs e)
        {
            if (f10 == null || f10.IsDisposed)
            {
                f10 = new Personel(); acilis();
                f10.MdiParent = this;
                f10.Show();
            }
            else
            {
                f10.Focus();
            }
        }

        private void accordionControlElement19_Click(object sender, EventArgs e)
        {
            if (f11 == null || f11.IsDisposed)
            {
                f11 = new KasaGirisCikl(); acilis();
                f11.MdiParent = this;
                f11.Show();
            }
            else
            {
                f11.Focus();
            }
        }

        private void accordionControlElement20_Click(object sender, EventArgs e)
        {
            
        }

        private void accordionControlElement21_Click(object sender, EventArgs e)
        {
            if (f8 == null || f8.IsDisposed)
            {
                f8 = new Toplam(); acilis();
                f8.MdiParent = this;
                f8.Show();
            }
            else
            {
                f8.Focus();
            }
        }

        private void accordionControlElement13_Click_1(object sender, EventArgs e)
        {
            if (f12 == null || f12.IsDisposed)
            {
                f12 = new KasaTakip(); acilis();
                f12.MdiParent = this;
                f12.Show();
            }
            else
            {
                f12.Focus();
            }
        }

        private void accordionControlElement14_Click(object sender, EventArgs e)
        {

        }
    }
}