using DevExpress.XtraEditors;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class Toplam : DevExpress.XtraEditors.XtraForm
    {
        public Toplam()
        {
            InitializeComponent();
        }
        int sayi = 0;
        private void sayac_Tick(object sender, EventArgs e)
        {
            sayi++;
            if (sayi == 11)
            {
                labelControl5.Visible = true;

            }
            else if (sayi == 20)
            {
                sayi = 0;
                sayac2.Start();
                labelControl5.Visible = false;
                sayac.Stop();
            }
        }

        private void sayac2_Tick(object sender, EventArgs e)
        {
            sayac.Start();
        }
        decimal a, b;
        void liste()
        {
            try
            {
                if (Cache.db.EczaneYarenBorc.Sum(c => c.Borc).ToString() != null || Cache.db.EczaneYarenBorc.Sum(c => c.Borc).ToString() != "")
                {
                    textEdit1.Text = Cache.db.EczaneYarenBorc.Sum(c => c.Borc).ToString();
                }
                else
                {
                    textEdit1.Text = "0,00";
                }

                Cache.bgl.Open();
                SqlCommand komut2 = new SqlCommand("Select Sum(Borc) from EczaneYarenBorc where convert(date,BorcTarih,103) = convert(date, getdate(), 103)", Cache.bgl);
                SqlDataReader dr = komut2.ExecuteReader();
                while (dr.Read())
                {
                    try
                    {
                        if (dr[0] != null)
                        {
                            textEdit2.Text = dr[0].ToString();
                        }
                        else
                        {
                            textEdit2.Text = "0,00";
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                dr.Close();
                Cache.bgl.Close();



                Cache.bgl.Open();
                SqlCommand komut3 = new SqlCommand("Select Sum(KasaIslemMiktari) from EczaneGunlukKasa where convert(date,IslemTarihi,103) = convert(date, getdate(), 103) and KasaIslemTipi='Nakit Girişi (Kâr)'", Cache.bgl);
                SqlDataReader dr2 = komut3.ExecuteReader();
                while (dr2.Read())
                {
                    try
                    {
                        if (dr2[0] != null)
                        {
                            textEdit4.Text = dr2[0].ToString();
                        }
                        else
                        {
                            textEdit4.Text = "0,00";
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                dr2.Close();
                Cache.bgl.Close();

                Cache.bgl.Open();
                SqlCommand komut4 = new SqlCommand("Select Sum(KasaIslemMiktari) from EczaneGunlukKasa where convert(date,IslemTarihi,103) = convert(date, getdate(), 103) and KasaIslemTipi='Nakit Çıkışı (Zarar)'", Cache.bgl);
                SqlDataReader dr3 = komut4.ExecuteReader();
                while (dr3.Read())
                {
                    try
                    {
                        if (dr3[0] != null)
                        {
                            textEdit3.Text = dr3[0].ToString();
                        }
                        else
                        {
                            textEdit3.Text = "0,00";
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                dr3.Close();
                Cache.bgl.Close();

                try
                {
                    if (textEdit4.Text != "" && textEdit4.Text != null || textEdit5.Text != "" && textEdit5.Text != null)
                    {
                        textEdit5.Text = (Convert.ToDecimal(textEdit4.Text) - Convert.ToDecimal(textEdit3.Text)).ToString();
                    }
                    else
                    {
                        textEdit5.Text = "0,00";
                    }
                }
                catch (Exception)
                {
                    
                }


            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sunucuya Bağlanılamıyor.. Lütfen İnternet Bağlantınızı Kontrol Edin.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Toplam_Load(object sender, EventArgs e)
        {
            liste();
            sayac.Start();
        }
    }
}