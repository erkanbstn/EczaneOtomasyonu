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
    public partial class BorcEkle : DevExpress.XtraEditors.XtraForm
    {
        public BorcEkle()
        {
            InitializeComponent();
        }

        private void BorcEkle_KeyDown(object sender, KeyEventArgs e)
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

        private void BorcEkle_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }
        void borcliste()
        {
            try
            {
                if (Application.OpenForms["BorcMain"] == null)
                {
                    XtraMessageBox.Show("Lütfen Borç Listesini Açtıktan Sonra Ekleme İşlemini Gerçekleştiriniz. Aksi Takdirde Anlık Değişimleri Göremeyebilirsiniz.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
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
        BorcMain f2;
        Hastalar f3;

        public void acilis()
        {
            f3 = new Hastalar();
            f3.splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            f3.splashScreenManager1.CloseWaitForm();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox1.Text != null)
                {
                    f2 = new BorcMain();
                    EczaneYarenBorc f = new EczaneYarenBorc();
                    acilis();
                    f.Hasta = textBox1.Text.ToUpper();
                    f.BorcDurum = "Borcu Var";
                    f.Borc = Convert.ToDecimal(textBox2.Text);
                    f.Kalan = 0;
                    f.BorcTarih = DateTime.Now.ToString();
                    f.KapanmaTarih = "";
                    f.Not = richTextBox1.Text;
                    Cache.db.EczaneYarenBorc.Add(f);
                    Cache.db.SaveChanges();
                    borcliste();  
                    
                    // RAPOR EKLEME
                    EczaneYarenRapor g = new EczaneYarenRapor();
                    Random d = new Random();
                    g.RaporNo = d.Next(10000000, 99999999).ToString();
                    g.Rapor = textBox1.Text.ToUpper() + " " + "İsimli Hasta, Borç Sistemine Eklendi.";
                    g.RaporTarih = DateTime.Now.ToString();
                    Cache.db.EczaneYarenRapor.Add(g);
                    Cache.db.SaveChanges();
                }
                else
                {
                    XtraMessageBox.Show("Hasta Bilgilerine Tekrar Bakınız...", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Hasta Bilgilerine Tekrar Bakınız...", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void temizle()
        {
            textBox1.Text = "";
            richTextBox1.Text = "";
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}