using DevExpress.XtraEditors;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CC_Pharma.Fluent
{
    public partial class Rapor : DevExpress.XtraEditors.XtraForm
    {
        public Rapor()
        {
            InitializeComponent();
        }
        void liste()
        {
            try
            {
                gridControl1.DataSource = Cache.db.EczaneYarenRapor.OrderByDescending(c => c.RaporTarih).ToList();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Sunucuya Bağlanılamıyor.. Lütfen İnternet Bağlantınızı Kontrol Edin.", "CC - Pharma Bilgilendirme Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Rapor_Load(object sender, EventArgs e)
        {
            liste();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                string deskPaath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                FileInfo template = new FileInfo(Path.Combine(Application.StartupPath, "Templates", "Günlük İşlem.xlsx"));
                using (var excel = new ExcelPackage(template))
                {
                    int index2 = 2;
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.FirstOrDefault();
                    for (int index = 0; index < gridView1.DataRowCount; index++)
                    {
                        var x = gridView1.GetRowCellValue(index, "RaporNo").ToString();
                        var x2 = gridView1.GetRowCellValue(index, "Rapor").ToString();
                        var x3 = gridView1.GetRowCellValue(index, "RaporTarih").ToString();
                        worksheet.Cells[index2, 1].Value = x;
                        worksheet.Cells[index2, 2].Value = x2;
                        worksheet.Cells[index2, 3].Value = x3;
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
                            fileName = $"CC - Pharma Genel Rapor-{count}";
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
                var x = Cache.db.EczaneYarenRapor.ToList();
                Cache.db.EczaneYarenRapor.RemoveRange(x);
                Cache.db.SaveChanges();
                liste();
            }
        }
    }
}