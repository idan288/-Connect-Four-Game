namespace ConnectFour
{
    public partial class GameSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxPlayer1Name = new System.Windows.Forms.TextBox();
            this.textboxPlayer2Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCols = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBoxComputer = new System.Windows.Forms.CheckBox();
            this.comboBoxDiff = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCols)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 22);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Player 1:";
            // 
            // textboxPlayer1Name
            // 
            this.textboxPlayer1Name.Location = new System.Drawing.Point(109, 29);
            this.textboxPlayer1Name.Name = "textboxPlayer1Name";
            this.textboxPlayer1Name.Size = new System.Drawing.Size(110, 20);
            this.textboxPlayer1Name.TabIndex = 0;
            // 
            // textboxPlayer2Name
            // 
            this.textboxPlayer2Name.Location = new System.Drawing.Point(109, 55);
            this.textboxPlayer2Name.Name = "textboxPlayer2Name";
            this.textboxPlayer2Name.Size = new System.Drawing.Size(110, 20);
            this.textboxPlayer2Name.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cols:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Rows:";
            // 
            // numericUpDownRows
            // 
            this.numericUpDownRows.Location = new System.Drawing.Point(133, 136);
            this.numericUpDownRows.Name = "numericUpDownRows";
            this.numericUpDownRows.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownRows.TabIndex = 2;
            // 
            // numericUpDownCols
            // 
            this.numericUpDownCols.Location = new System.Drawing.Point(133, 176);
            this.numericUpDownCols.Name = "numericUpDownCols";
            this.numericUpDownCols.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownCols.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(133, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 22);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Player 2:";
            // 
            // CheckBoxComputer
            // 
            this.CheckBoxComputer.AutoSize = true;
            this.CheckBoxComputer.Location = new System.Drawing.Point(12, 98);
            this.CheckBoxComputer.Name = "CheckBoxComputer";
            this.CheckBoxComputer.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxComputer.TabIndex = 14;
            this.CheckBoxComputer.UseVisualStyleBackColor = true;
            this.CheckBoxComputer.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBoxDiff
            // 
            this.comboBoxDiff.FormattingEnabled = true;
            this.comboBoxDiff.Items.AddRange(new object[] {
            "Easy",
            "Mideum",
            "Hard"});
            this.comboBoxDiff.Location = new System.Drawing.Point(109, 95);
            this.comboBoxDiff.Name = "comboBoxDiff";
            this.comboBoxDiff.Size = new System.Drawing.Size(110, 21);
            this.comboBoxDiff.TabIndex = 15;
            this.comboBoxDiff.Text = "Choose Difficulty";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "vs Computer";
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 249);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxDiff);
            this.Controls.Add(this.CheckBoxComputer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.numericUpDownCols);
            this.Controls.Add(this.numericUpDownRows);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textboxPlayer2Name);
            this.Controls.Add(this.textboxPlayer1Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxPlayer1Name;
        private System.Windows.Forms.TextBox textboxPlayer2Name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownRows;
        private System.Windows.Forms.NumericUpDown numericUpDownCols;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CheckBoxComputer;
        private System.Windows.Forms.ComboBox comboBoxDiff;
        private System.Windows.Forms.Label label3;
    }
}