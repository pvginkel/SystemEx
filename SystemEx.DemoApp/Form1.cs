using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemEx.Windows.Forms;

namespace SystemEx.DemoApp
{
    public partial class Form1 : SystemEx.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnFixControl(ControlEventArgs e)
        {
            ControlUtil.FixControlScaling(e.Control);
        }
    }
}
