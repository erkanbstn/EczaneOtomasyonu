using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class MainGiris : DevExpress.XtraEditors.XtraForm
    {
        public MainGiris()
        {
            InitializeComponent();
        }
        Hastalar f;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var s = Cache.db.EczaneYarenAdmin.Where(c => c.PersonelSifre == textBox1.Text).Select(v => v.Yetki).FirstOrDefault();
                Cache.Isim = Cache.db.EczaneYarenAdmin.Where(c => c.PersonelSifre == textBox1.Text).Select(v => v.Personel).FirstOrDefault();
                if (s != null)
                {
                    if (s == "Yetkili")
                    {
                        Cache.YK = true;
                        f = new Hastalar();
                        f.Show();
                        this.Hide();
                    }
                    else
                    {
                        Cache.PK = true;
                        f = new Hastalar();
                        f.Show();
                        this.Hide();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Sisteme Kayıtlı Böyle Bir Kullanıcı Bulunamadı !", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Bir Sorun Oluştu Sisteme Giriş Yapılamıyor. Lütfen İnternet Bağlantınızı Kontrol Ediniz.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MainGiris_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 8;
        }
    }
}