using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public Main()
        {
            InitializeComponent();
        }
        public void hastaliste()   // Her yerde Kullanılan
        {
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter("Select * from EczaneYaren", Cache.bgl);
                DataTable tb = new DataTable();
                ad.Fill(tb);
                gridControl1.DataSource = tb;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sunucuya Bağlanılamıyor.. Lütfen İnternet Bağlantınızı Kontrol Edin.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            hastaliste();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow r = gridView1.GetDataRow(e.FocusedRowHandle);
            if (r != null)
            {
                textEdit1.Text = r[0].ToString();
                richTextBox1.Text = r[3].ToString();
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
                    var x = Cache.db.EczaneYaren.Find(hasta);
                    Cache.db.EczaneYaren.Remove(x);
                    Cache.db.SaveChanges();
                    hastaliste();

                    EczaneYarenRapor g = new EczaneYarenRapor();
                    Random d = new Random();
                    g.RaporNo = d.Next(10000000, 99999999).ToString();
                    g.Rapor = $"{textEdit1.Text} İsimli Hasta, Hasta Sisteminden Silindi.";
                    g.RaporTarih = DateTime.Now.ToString();
                    Cache.db.EczaneYarenRapor.Add(g);
                    Cache.db.SaveChanges();

                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sisteme Kayıtlı Veya Seçili Bir Hasta Yok ", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            Cache.Hasta = textEdit1.Text;
            Cache.Ilac = Cache.db.EczaneYaren.Where(c => c.Hasta == textEdit1.Text).Select(z => z.IlacDurum).FirstOrDefault();
            Cache.Tarih = Cache.db.EczaneYaren.Where(c => c.Hasta == textEdit1.Text).Select(z => z.Tarih).FirstOrDefault();
            Cache.Not = Cache.db.EczaneYaren.Where(c => c.Hasta == textEdit1.Text).Select(z => z.Not).FirstOrDefault();
        }
    }
}