using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_Pharma
{
    public static class Cache
    {
        public static SqlConnection bgl = new SqlConnection("Data Source=94.73.145.8;Initial Catalog=u0241506_EczCV; User Id=u0241506_Geo2;Password=MAmh01F2YDqc74U;MultipleActiveResultSets=True");
        public static string Ilac { get; set; }
        public static string Isim { get; set; }
        public static bool YK { get; set; }
        public static bool PK { get; set; }

        public static EczCVEntities db = new EczCVEntities();
        public static string Hasta { get; set; }
        public static string Tarih { get; set; }
        public static string Not { get; set; }

    }
}
