using DevExpress.XtraEditors;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class KasaTakip : DevExpress.XtraEditors.XtraForm
    {
        public KasaTakip()
        {
            InitializeComponent();
        }
        void liste()
        {
            try
            {
                if (gridControl2.Visible == true)
                {
                    gridControl2.DataSource = Cache.db.EczaneYarenKasaCikis.OrderByDescending(c => c.Tarih).ToList();
                    Cache.bgl.Open();
                    SqlCommand komut3 = new SqlCommand("Select Sum(KasaIslemMiktari) from EczaneGunlukKasa where convert(date,IslemTarihi,103) = convert(date, getdate(), 103) and KasaIslemTipi='Nakit Girişi (Kâr)'", Cache.bgl);
                    SqlDataReader dr2 = komut3.ExecuteReader();
                    while (dr2.Read())
                    {
                        try
                        {
                            if (dr2[0] != null)
                            {
                                Cache2.KasaMiktar = Convert.ToDecimal(dr2[0]);
                            }
                            else
                            {
                                Cache2.KasaMiktar = 0;
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
                                Cache2.KasaMiktar2 = Convert.ToDecimal(dr3[0]);
                            }
                            else
                            {
                                Cache2.KasaMiktar2 = 0;
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    dr3.Close();
                    Cache.bgl.Close();
                }
                gridControl1.DataSource = Cache.db.EczaneGunlukKasa.OrderByDescending(c => c.IslemTarihi).ToList();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sunucuya Bağlanılamıyor.. Lütfen İnternet Bağlantınızı Kontrol Edin.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void KasaTakip_Load(object sender, EventArgs e)
        {
            if (Cache.PK == true)
            {
                simpleButton1.Enabled = false;
                simpleButton2.Enabled = false;
                gridControl2.Visible = false;
            }
            sayac.Start();
            liste();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                string deskPaath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                FileInfo template = new FileInfo(Path.Combine(Application.StartupPath, "Templates", "Kasa Günlük İşlem.xlsx"));
                using (var excel = new ExcelPackage(template))
                {
                    int index2 = 2;
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.FirstOrDefault();
                    for (int index = 0; index < gridView1.DataRowCount; index++)
                    {
                        var x = gridView1.GetRowCellValue(index, "KayıtNo").ToString();
                        var x2 = gridView1.GetRowCellValue(index, "KasaIslemTipi").ToString();
                        var x3 = gridView1.GetRowCellValue(index, "KasaIslemMiktari").ToString();
                        var x4 = gridView1.GetRowCellValue(index, "Not").ToString();
                        var x5 = gridView1.GetRowCellValue(index, "IslemTarihi").ToString();
                        worksheet.Cells[index2, 1].Value = x;
                        worksheet.Cells[index2, 2].Value = x2;
                        worksheet.Cells[index2, 3].Value = x3;
                        worksheet.Cells[index2, 4].Value = x4;
                        worksheet.Cells[index2, 5].Value = x5;
                        index2++;
                    }

                    string fileName = "CC - Pharma Genel Rapor";
                    bool isFileExist = true;
                    int count = 1;
                    while (isFileExist)
                    {
                        string file = Path.Combine(deskPaath, $"{fileName}.xlsx");
                        if (File.Exists(file))
                        {
                            count++;
                            fileName = $"CC - Pharma Kasa Genel Rapor-{count}";
                        }
                        else
                        {
                            isFileExist = false;
                        }
                    }
                    string filePath = Path.Combine(deskPaath, $"{fileName}.xlsx"); ;

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    FileInfo destination = new FileInfo(filePath);
                    excel.SaveAs(destination);
                    XtraMessageBox.Show("Excel Dosyası Başarıyla Oluşturuldu..!", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception)
            {
                XtraMessageBox.Show("Excel Dosyası Oluşturulurken Bir Hata Oluştu Lütfen Daha Sonra Tekrar Deneyiniz!", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            DialogResult a = XtraMessageBox.Show("Excel Çıktısını Aldıysanız Günlük İşlem Raporlarını Tamamen Sileyim mi?", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (a == DialogResult.Yes)
            {
                kasa();
            }
        }
        int sayi = 0;
        private void timer1_Tick(object sender, EventArgs e)
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac.Start();
        }
        void kasa()
        {
            DialogResult a = XtraMessageBox.Show("Günlük İşlem Raporlarını Tamamen Sileyim mi?", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (a == DialogResult.Yes)
            {
                var x = Cache.db.EczaneGunlukKasa.ToList();
                Cache.db.EczaneGunlukKasa.RemoveRange(x);
                Cache.db.SaveChanges();

                EczaneYarenKasaCikis h = new EczaneYarenKasaCikis();
                h.Tarih = DateTime.Now.ToString();
                h.Kasa = (Cache2.KasaMiktar - Cache2.KasaMiktar2).ToString();
                Cache.db.EczaneYarenKasaCikis.Add(h);
                Cache.db.SaveChanges();
                liste();
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            kasa();
        }
    }
}