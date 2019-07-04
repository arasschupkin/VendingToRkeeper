using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vending
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //string CodeCard = DbHelper.SocketHelper.HexToDescSort("4D5F31C1");
            //CodeCard = DbHelper.SocketHelper.HexToDec(CodeCard);

            //CryptoVending.data = CryptoVending.Encrypt();
            //CryptoVending.Decrypt();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainVending());
        }
    }
}
