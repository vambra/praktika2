using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormLogin formLogin = new FormLogin();
            Application.Run(formLogin);
            //different forms for different users?
            if (formLogin.UserId != 0)
            {
                FormHome formHome = new FormHome(formLogin.UserId, formLogin.UserType);
                Application.Run(formHome);
            }
        }
    }
}
