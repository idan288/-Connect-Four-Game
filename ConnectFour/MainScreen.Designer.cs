using System;

namespace ConnectFour
{
    public partial class MainScreen
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
            this.components = new System.ComponentModel.Container();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startANewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startANewTournirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStripPlayersStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCurrentPlayer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCurrentPlayerName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPlayer1Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPlayer1Score = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPlayer2Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPlayer2Score = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerFall = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxFalingCoin = new System.Windows.Forms.PictureBox();
            this.timerSwitch = new System.Windows.Forms.Timer(this.components);
            this.labelAbout = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStripPlayersStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFalingCoin)).BeginInit();
            this.SuspendLayout();
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startANewGameToolStripMenuItem,
            this.startANewTournirToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // startANewGameToolStripMenuItem
            // 
            this.startANewGameToolStripMenuItem.Name = "startANewGameToolStripMenuItem";
            this.startANewGameToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.startANewGameToolStripMenuItem.Text = "Start a New Game";
            this.startANewGameToolStripMenuItem.Click += new System.EventHandler(this.startANewGameToolStripMenuItem_Click);
            // 
            // startANewTournirToolStripMenuItem
            // 
            this.startANewTournirToolStripMenuItem.Name = "startANewTournirToolStripMenuItem";
            this.startANewTournirToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.startANewTournirToolStripMenuItem.Text = "Start a New Tournir";
            this.startANewTournirToolStripMenuItem.Click += new System.EventHandler(this.startANewTournirToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(211, 26);
            this.propertiesToolStripMenuItem.Text = "Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(208, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(211, 26);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToPlayToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToPlayToolStripMenuItem
            // 
            this.howToPlayToolStripMenuItem.Name = "howToPlayToolStripMenuItem";
            this.howToPlayToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.howToPlayToolStripMenuItem.Text = "How to play?";
            this.howToPlayToolStripMenuItem.Click += new System.EventHandler(this.howToPlayToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(467, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseLeave += new System.EventHandler(this.menuStrip1_MouseLeave);
            this.menuStrip1.MouseHover += new System.EventHandler(this.menuStrip1_MouseHover);
            // 
            // statusStripPlayersStatus
            // 
            this.statusStripPlayersStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripPlayersStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCurrentPlayer,
            this.toolStripStatusLabelCurrentPlayerName,
            this.toolStripStatusLabelPlayer1Name,
            this.toolStripStatusLabelPlayer1Score,
            this.toolStripStatusLabelPlayer2Name,
            this.toolStripStatusLabelPlayer2Score});
            this.statusStripPlayersStatus.Location = new System.Drawing.Point(0, 444);
            this.statusStripPlayersStatus.Name = "statusStripPlayersStatus";
            this.statusStripPlayersStatus.Size = new System.Drawing.Size(467, 25);
            this.statusStripPlayersStatus.Stretch = false;
            this.statusStripPlayersStatus.TabIndex = 1;
            this.statusStripPlayersStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCurrentPlayer
            // 
            this.toolStripStatusLabelCurrentPlayer.Name = "toolStripStatusLabelCurrentPlayer";
            this.toolStripStatusLabelCurrentPlayer.Size = new System.Drawing.Size(108, 20);
            this.toolStripStatusLabelCurrentPlayer.Text = "Current Player :";
            this.toolStripStatusLabelCurrentPlayer.Visible = false;
            // 
            // toolStripStatusLabelCurrentPlayerName
            // 
            this.toolStripStatusLabelCurrentPlayerName.Name = "toolStripStatusLabelCurrentPlayerName";
            this.toolStripStatusLabelCurrentPlayerName.Size = new System.Drawing.Size(0, 20);
            this.toolStripStatusLabelCurrentPlayerName.Visible = false;
            // 
            // toolStripStatusLabelPlayer1Name
            // 
            this.toolStripStatusLabelPlayer1Name.Margin = new System.Windows.Forms.Padding(20, 3, 0, 2);
            this.toolStripStatusLabelPlayer1Name.Name = "toolStripStatusLabelPlayer1Name";
            this.toolStripStatusLabelPlayer1Name.Size = new System.Drawing.Size(0, 20);
            this.toolStripStatusLabelPlayer1Name.Visible = false;
            // 
            // toolStripStatusLabelPlayer1Score
            // 
            this.toolStripStatusLabelPlayer1Score.Margin = new System.Windows.Forms.Padding(-5, 3, 0, 2);
            this.toolStripStatusLabelPlayer1Score.Name = "toolStripStatusLabelPlayer1Score";
            this.toolStripStatusLabelPlayer1Score.Size = new System.Drawing.Size(17, 20);
            this.toolStripStatusLabelPlayer1Score.Text = "0";
            this.toolStripStatusLabelPlayer1Score.Visible = false;
            // 
            // toolStripStatusLabelPlayer2Name
            // 
            this.toolStripStatusLabelPlayer2Name.Name = "toolStripStatusLabelPlayer2Name";
            this.toolStripStatusLabelPlayer2Name.Size = new System.Drawing.Size(0, 20);
            this.toolStripStatusLabelPlayer2Name.Visible = false;
            // 
            // toolStripStatusLabelPlayer2Score
            // 
            this.toolStripStatusLabelPlayer2Score.Margin = new System.Windows.Forms.Padding(-5, 3, 0, 2);
            this.toolStripStatusLabelPlayer2Score.Name = "toolStripStatusLabelPlayer2Score";
            this.toolStripStatusLabelPlayer2Score.Size = new System.Drawing.Size(17, 20);
            this.toolStripStatusLabelPlayer2Score.Text = "0";
            this.toolStripStatusLabelPlayer2Score.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Plum;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 372);
            this.panel1.TabIndex = 2;
            // 
            // timerFall
            // 
            this.timerFall.Interval = 1;
            // 
            // pictureBoxFalingCoin
            // 
            this.pictureBoxFalingCoin.Location = new System.Drawing.Point(-1, -1);
            this.pictureBoxFalingCoin.Name = "pictureBoxFalingCoin";
            this.pictureBoxFalingCoin.Size = new System.Drawing.Size(67, 67);
            this.pictureBoxFalingCoin.TabIndex = 0;
            this.pictureBoxFalingCoin.TabStop = false;
            // 
            // timerSwitch
            // 
            this.timerSwitch.Interval = 250;
            // 
            // labelAbout
            // 
            this.labelAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAbout.BackColor = System.Drawing.Color.GhostWhite;
            this.labelAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelAbout.Location = new System.Drawing.Point(67, 162);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(333, 145);
            this.labelAbout.TabIndex = 5;
            this.labelAbout.Text = "4 In A Row !! :)";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAbout.Visible = false;
            this.labelAbout.Click += new System.EventHandler(this.labelAbout_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(467, 469);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStripPlayersStatus);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "4 In a Row!!";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStripPlayersStatus.ResumeLayout(false);
            this.statusStripPlayersStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFalingCoin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem howToPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startANewTournirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startANewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripPlayersStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlayer1Name;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentPlayerName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCurrentPlayer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlayer1Score;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlayer2Name;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPlayer2Score;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timerFall;
        private System.Windows.Forms.PictureBox pictureBoxFalingCoin;
        private System.Windows.Forms.Timer timerSwitch;
        private System.Windows.Forms.Label labelAbout;
    }
}