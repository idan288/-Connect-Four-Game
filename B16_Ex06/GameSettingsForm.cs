using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm(byte i_MinSizeBoard, byte i_MaxSizeBoard)
        {
            InitializeComponent();
            initializeNumricUpDown(i_MinSizeBoard, i_MaxSizeBoard);
        }

        public string Player1Name
        {
            get { return textboxPlayer1Name.Text; }
        }

        public string Player2Name
        {
            get { return textboxPlayer2Name.Text; }
        }

        public bool IsHuman
        {
            get { return CheckBoxComputer.Checked; }
        }

        public int Rows
        {
            get { return (int)numericUpDownRows.Value; }
        }

        public int Cols
        {
            get { return (int)numericUpDownCols.Value; }
        }

        public string Difficulty
        {
            get { return comboBoxDiff.GetItemText(comboBoxDiff.SelectedItem); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textboxPlayer1Name.Text == string.Empty)
            {
                MessageBox.Show("Please enter player 1 name!");
            }
            else if (textboxPlayer2Name.Text == string.Empty)
            {
                MessageBox.Show("Please enter player 2 name!");
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void initializeNumricUpDown(byte i_MinSizeBoard, byte i_MaxSizeBoard)
        {
            numericUpDownRows.Minimum = i_MinSizeBoard;
            numericUpDownRows.Maximum = i_MaxSizeBoard;
            numericUpDownCols.Minimum = i_MinSizeBoard;
            numericUpDownCols.Maximum = i_MaxSizeBoard;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(CheckBoxComputer.Checked == true)
            {
                textboxPlayer2Name.Enabled = false;
                textboxPlayer2Name.Text = "Computer";
                comboBoxDiff.SelectedIndex =  0;
            }
            else
            {
                textboxPlayer2Name.Enabled = true;
                textboxPlayer2Name.Text = "";
                comboBoxDiff.Text = "Choose Difficulty";
            }
        }
    }
}
