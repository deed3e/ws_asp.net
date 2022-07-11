using pallgree_app.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pallgree_app
{
    internal static class Program
    {
        static ApplicationContext MainContext = new ApplicationContext();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //MainContext.MainForm = new Login();
            MainContext.MainForm = new Home_admin();
            Application.Run(MainContext);
        }
        public static void SetMainForm(Form MainForm)
        {
            MainContext.MainForm = MainForm;
        }

        public static void ShowMainForm()
        {
            MainContext.MainForm.Show();
        }

    }
}
