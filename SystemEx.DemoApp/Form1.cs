using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SystemEx.Windows.Forms;

namespace SystemEx.DemoApp
{
    public partial class Form1 : SystemEx.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();

            toolStrip1.ImageScalingSize = ControlUtil.Scale(toolStrip1.ImageScalingSize);

            toolStripButton1.Image = Program.NeutralResources.GetScaledImage("apple", 16);
            toolStripButton2.Image = Program.NeutralResources.GetScaledImage("banana", 16);
            toolStripButton3.Image = Program.NeutralResources.GetScaledImage("pineapple", 16);

            imageList1.Images.Clear();

            Program.NeutralResources.FillImageList(imageList1, "apple", "banana", "pineapple");
        }
    }
}
