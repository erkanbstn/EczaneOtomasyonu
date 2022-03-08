using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class HastaEkle : DevExpress.XtraEditors.XtraForm
    {
        public HastaEkle()
        {
            InitializeComponent();
        }
        void temizle()
        {
            richTextBox1.Text = "";
            checkBox1.Checked = false;
        }
        void hastaliste()
        {
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
        Main f2;
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox1.Text != null)
                {
                    f2 = new Main();

                    if (borclu == true)
                    {
                        // Hasta EKLEME
                        EczaneYaren f = new EczaneYaren();
                        acilis();
                        f.Hasta = textBox1.Text.ToUpper();
                        f.IlacDurum = "İlacı Var";
                        f.Tarih = DateTime.Now.ToString();
                        DateTime bugun = DateTime.Now;
                        if (checkBox3.Checked == true)
                        {
                            f.Not = richTextBox1.Text + "\n" + $"- {textBox1.Text} İsimli Hastanın {textBox2.Text} Adet Reçetesi Var".Trim() + "\n" + $"- {bugun} Tarihinde İlaç Yazdırdı..".Trim();
                        }
                        else
                        {
                            f.Not = richTextBox1.Text + "\n" + $"- {bugun} Tarihinde İlaç Yazdırdı.".Trim();
                        }

                        Cache.db.EczaneYaren.Add(f);
                        Cache.db.SaveChanges();
                        hastaliste();

                        // Hasta  + Borç EKLEME
                        EczaneYarenBorc t = new EczaneYarenBorc();
                        t.Hasta = textBox1.Text;
                        t.Borc = 0;
                        t.Kalan = 0; 
                        t.BorcTarih = DateTime.Now.ToString();
                        t.BorcDurum = "Borcu Var";
                        Cache.db.EczaneYarenBorc.Add(t);
                        Cache.db.SaveChanges();
                        borcliste();


                        // RAPOR EKLEME
                        EczaneYarenRapor g = new EczaneYarenRapor();
                        Random d = new Random();
                        g.RaporNo = d.Next(10000000, 99999999).ToString();
                        g.Rapor = textBox1.Text.ToUpper() + " " + "İsimli Hasta, Borç Ve Hasta Sistemine Eklendi.";
                        g.RaporTarih = DateTime.Now.ToString();
                        Cache.db.EczaneYarenRapor.Add(g);
                        Cache.db.SaveChanges();
                        temizle();

                        XtraMessageBox.Show("Hasta Borçlu Olarak Eklendi Borç Listesinden Kontrol Edebilirsiniz.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Hasta EKLEME
                        EczaneYaren f = new EczaneYaren();
                        acilis();
                        f.Hasta = textBox1.Text.ToUpper();
                        f.IlacDurum = "İlacı Var";
                        f.Tarih = DateTime.Now.ToString();
                        DateTime bugun = DateTime.Now;
                        if (checkBox3.Checked == true)
                        {
                            f.Not = richTextBox1.Text + "\n" + $"- {textBox1.Text} İsimli Hastanın {textBox2.Text} Adet Reçetesi Var".Trim() + "\n" + $"- {bugun} Tarihinde İlaç Yazdırdı..".Trim();
                        }
                        else
                        {
                            f.Not = richTextBox1.Text + "\n" + $"- {bugun} Tarihinde İlaç Yazdırdı.".Trim();
                        }
                        Cache.db.EczaneYaren.Add(f);
                        Cache.db.SaveChanges();
                        hastaliste();
                        temizle();

                        // RAPOR EKLEME
                        EczaneYarenRapor g = new EczaneYarenRapor();
                        Random d = new Random();
                        g.RaporNo = d.Next(10000000, 99999999).ToString();
                        g.Rapor = textBox1.Text.ToUpper() + " " + "İsimli Hasta, Hasta Sistemine Eklendi.";
                        g.RaporTarih = DateTime.Now.ToString();
                        Cache.db.EczaneYarenRapor.Add(g);
                        Cache.db.SaveChanges();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Hasta Bilgilerine Tekrar Bakınız...", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sistemde Aynı İsimde Böyle Bir Hasta Zaten Mevcut...", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void borcliste()
        {
            try
            {
                BorcMain f = new BorcMain();
                f.Name = "f";
                if (Application.OpenForms["f"] != null)
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
        public void acilis()
        {
            Hastalar f3 = new Hastalar();
            f3.splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            f3.splashScreenManager1.CloseWaitForm();
        }
        private void HastaEkle_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
        }
        private void HastaEkle_KeyDown(object sender, KeyEventArgs e)
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
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            temizle();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
        bool borclu;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                borclu = true;
            }
            else
            {
                borclu = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
            }

        }
    }
}