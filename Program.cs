using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseRacingAutoPurchaser
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //日本時間で行われることなので、一応
            CultureInfo.CurrentCulture = new CultureInfo("ja-JP");
            CultureInfo.CurrentUICulture = new CultureInfo("ja-JP");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
