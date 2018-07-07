using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class HowToPlayForm : Form
    {
        public HowToPlayForm()
        {
            InitializeComponent();
            initText();
        }

        private void initText()
        {
            if (File.Exists("C:\\FourInARowHelp.txt"))
            {
                string content = File.ReadAllText("C:\\FourInARowHelp.txt");
                richTextBox1.Text = content;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
