using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using SystemEx.Windows.Forms;
using SystemEx.Windows.Forms.Internal;

namespace SystemEx.DemoApp
{
    static class Program
    {
        public static ResourceManager NeutralResources = new ResourceManager("SystemEx.DemoApp.NeutralResources", typeof(Program).Assembly);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FormHelper.DefaultFixControl += (s, e) => ControlUtil.FixControlScaling(e.Control);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
