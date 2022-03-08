using CC_Pharma.Fluent;
using System;
using System.Windows.Forms;

namespace CC_Pharma
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainGiris());
        }
    }
}
