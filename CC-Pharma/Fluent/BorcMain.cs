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
    public partial class BorcMain : DevExpress.XtraEditors.XtraForm
    {
        public BorcMain()
        {
            InitializeComponent();
        }
        public void borcliste() // Her yerde kullanılacak
        {
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("Select * from EczaneYarenBorc", Cache.bgl);
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
            Hastalar f = new Hastalar();
            f.splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            f.splashScreenManager1.CloseWaitForm();
        }
        private void BorcMain_Load(object sender, EventArgs e)
        {
            borcliste();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView1.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                textEdit1.Text = r[0].ToString();
                richTextBox1.Text = r[6].ToString();
            }
        }
        public void sil()
        {
            DialogResult a = XtraMessageBox.Show("Seçili Hastayı Silmek Emin misiniz?", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            try
            {
                if (a == DialogResult.Yes)
                {
                    string hasta = textEdit1.Text;
                    var x = Cache.db.EczaneYarenBorc.Find(hasta);
                    Cache.db.EczaneYarenBorc.Remove(x);
                    Cache.db.SaveChanges();
                    // RAPOR EKLEME
                    EczaneYarenRapor g = new EczaneYarenRapor();
                    Random d = new Random();
                    g.RaporNo = d.Next(10000000, 99999999).ToString();
                    g.Rapor = textEdit1.Text.ToUpper() + " " + "İsimli Hasta, Borç Sisteminden Silindi.";
                    g.RaporTarih = DateTime.Now.ToString();
                    Cache.db.EczaneYarenRapor.Add(g);
                    Cache.db.SaveChanges();
                    borcliste();
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sisteme Kayıtlı Veya Seçili Bir Hasta Yok ", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            Cache2.Hasta = textEdit1.Text;
            Cache2.Borc = Convert.ToDecimal(Cache.db.EczaneYarenBorc.Where(c => c.Hasta == textEdit1.Text).Select(z => z.Borc).FirstOrDefault());
            Cache2.BorcDurum = Cache.db.EczaneYarenBorc.Where(c => c.Hasta == textEdit1.Text).Select(z => z.BorcDurum).FirstOrDefault();
            Cache2.Not = Cache.db.EczaneYarenBorc.Where(c => c.Hasta == textEdit1.Text).Select(z => z.Not).FirstOrDefault();
            Cache2.Kalan = Convert.ToDecimal(Cache.db.EczaneYarenBorc.Where(c => c.Hasta == textEdit1.Text).Select(z => z.Kalan).FirstOrDefault().ToString());
            Cache2.BorcTarih = Cache.db.EczaneYarenBorc.Where(c => c.Hasta == textEdit1.Text).Select(z => z.BorcTarih).FirstOrDefault();
        }
    }
}