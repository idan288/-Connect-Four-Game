using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConnectFour
{
    public enum EState
    {
        Win,
        Draw,
        Continue
    }

    public delegate void UpdateButtonBoardEventHandler(object sender, CoinEventArgs e);

    public partial class MainScreen : Form
    {
        // Const Members
        private const byte k_MinSizeBoard = 4;
        private const byte k_MaxSizeBoard = 10;
        private const char k_Player1Sign = 'X';
        private const char k_Player2Sign = 'O';
        private const int k_NumIterrtationForTickMethod = 2;
        private const int k_CellWidth = 67;

        // Read Only Members
        private readonly GameSettingsForm r_GameSettingFourInRow;
        private readonly Cursor r_RedCoinCursor;
        private readonly Cursor r_YellowCoinCursor;
        private readonly List<ButtonToPress> r_ButtonsToPRess;
        private readonly List<List<BoardCell>> r_BoardImages;
        private readonly GameLogic r_GameLogic;

        // Data Members
        private DialogResult m_DialogResultOfPropeties;
        private Cursor m_CurrentCursorPlayer;
        private Point[] m_ArrWinLine;
        private bool m_Ticks = true;
        private bool m_IsComputer;
        private bool m_GameIsFinish;
        private int m_Depth;
        public MainScreen()
        {
            InitializeComponent();
            r_GameSettingFourInRow = new GameSettingsForm(k_MinSizeBoard, k_MaxSizeBoard);
            r_GameLogic = new GameLogic();
            r_BoardImages = new List<List<BoardCell>>();
            r_ButtonsToPRess = new List<ButtonToPress>();
            r_RedCoinCursor = new Cursor(Properties.Resources.CoinRed.GetHicon());
            r_YellowCoinCursor = new Cursor(Properties.Resources.CoinYellow.GetHicon());
        }

        protected override void OnShown(EventArgs e)
        {
            startNewGame();
        }

        private void startNewGame()
        {
            m_DialogResultOfPropeties = showPropertiesForm();

            if (m_DialogResultOfPropeties == DialogResult.OK)
            {
                initlizeControls(
                    r_GameSettingFourInRow.Player1Name,
                    r_GameSettingFourInRow.Player2Name,
                    r_GameSettingFourInRow.Rows,
                    r_GameSettingFourInRow.Cols);
            }
        }

        public void initlizeControls(string i_Player1Name, string i_Player2Name, int i_Row, int i_Col)
        {
            initLabel(i_Player1Name, i_Player2Name);
            m_IsComputer = i_Player2Name == "Computer" ? true : false;
            if (m_IsComputer)
            {
                updateAIDepth();
            }
            initButtonToPress(i_Col);
            initBoard(i_Row, i_Col);
            this.panel1.Controls.Add(this.pictureBoxFalingCoin);
            Cursor = r_RedCoinCursor;
            m_CurrentCursorPlayer = r_RedCoinCursor;
            r_GameLogic.InitGameLogic(i_Row, i_Col, r_GameLogic_UpdateButtonBoardEventHandler);
            fixLabelAboutSize(i_Col, i_Row);
            CenterToParent();
        }

        private void r_GameLogic_UpdateButtonBoardEventHandler(object sender, CoinEventArgs e)
        {
            if (e.Row == 0)
            {
                r_ButtonsToPRess[e.Col].Enabled = false;
            }
        }

        private void changeToFullCell(CoinEventArgs e)
        {
            if (e.SignPlayer == k_Player1Sign)
            {
                r_BoardImages[e.Col][e.Row].BackgroundImage = Properties.Resources.FullCellRed;
            }
            else
            {
                r_BoardImages[e.Col][e.Row].BackgroundImage = Properties.Resources.FullCellYellow;
            }
        }

        private void initButtonToPress(int i_Col)
        {
            r_ButtonsToPRess.Capacity = i_Col;
            int yPosition = 0;
            int nextXPosition = 0;
            for (int i = 0; i < r_ButtonsToPRess.Capacity; i++)
            {
                ButtonToPress newButtonToPress = new ButtonToPress(nextXPosition, yPosition, i);
                newButtonToPress.Click += ButtonToPress_Click;
                r_ButtonsToPRess.Add(newButtonToPress);
                nextXPosition += Properties.Resources.EmptyCell.Size.Width;
            }
        }

        private void initBoard(int i_Row, int i_Col)
        {
            int nextYPosition;
            int nextXPosition = 0;
            Size panelSizeBeforeChngedBoard = panel1.MaximumSize;

            r_BoardImages.Capacity = i_Col;

            for (int i = 0; i < r_BoardImages.Capacity; ++i)
            {
                r_BoardImages.Add(new List<BoardCell>(i_Row));
                nextYPosition = k_CellWidth;

                for (int j = 0; j < i_Row; ++j)
                {
                    BoardCell newBoardCell = new BoardCell(nextYPosition, nextXPosition);
                    newBoardCell.BackgroundImage = Properties.Resources.EmptyCell;
                    r_BoardImages[i].Add(newBoardCell);
                    panel1.Controls.Add(newBoardCell);
                    nextYPosition += Properties.Resources.EmptyCell.Size.Height;
                }

                nextXPosition += Properties.Resources.EmptyCell.Size.Width;
            }

            changeFormAndPanelSize(i_Col, i_Row);
        }

        private void changeFormAndPanelSize(int i_Col, int i_Row)
        {
            panel1.Controls.AddRange(r_ButtonsToPRess.ToArray());
            panel1.MaximumSize = new Size(i_Col * k_CellWidth, (i_Row + 1) * k_CellWidth);
            Size = new Size(panel1.MaximumSize.Width + 48, panel1.MaximumSize.Height + menuStrip1.Height + statusStripPlayersStatus.Height + 132);
        }

        private void fixLabelAboutSize(int i_Col, int i_Row)
        {
            double getMaxSize = i_Col > i_Row ? i_Col : i_Row;
            getMaxSize = getMaxSize * 3.5;
            labelAbout.Font = new Font("Calibri", (float)getMaxSize, FontStyle.Bold);
        }

        private void changeLabelsVisibileProperty(bool i_Visible)
        {
            toolStripStatusLabelCurrentPlayer.Visible = i_Visible;
            toolStripStatusLabelCurrentPlayerName.Visible = i_Visible;

            toolStripStatusLabelPlayer1Name.Visible = i_Visible;
            toolStripStatusLabelPlayer1Score.Visible = i_Visible;

            toolStripStatusLabelPlayer2Score.Visible = i_Visible;
            toolStripStatusLabelPlayer2Name.Visible = i_Visible;
        }

        private void initLabel(string i_Player1Name, string i_Player2Name)
        {
            toolStripStatusLabelCurrentPlayerName.Text = i_Player1Name;
            updatePlayersLabels(i_Player1Name, i_Player2Name);
            changeLabelsVisibileProperty(true);
        }

        private void updatePlayersLabels(string i_Player1Name, string i_Player2Name)
        {
            string dot = ":";
            toolStripStatusLabelPlayer1Name.Text = i_Player1Name + dot;
            toolStripStatusLabelPlayer2Name.Text = i_Player2Name + dot;
            toolStripStatusLabelCurrentPlayerName.Text = i_Player1Name;
        }

        private DialogResult showPropertiesForm()
        {
            r_GameSettingFourInRow.StartPosition = FormStartPosition.CenterParent;
            DialogResult diagRes = r_GameSettingFourInRow.ShowDialog();

            return diagRes;
        }

        private void resetGame()
        {
            Cursor = r_RedCoinCursor;
            m_CurrentCursorPlayer = r_RedCoinCursor;
            clearBoard();
            clearButtonPressed();
            initButtonToPress(r_GameSettingFourInRow.Cols);
            initBoard(r_GameSettingFourInRow.Rows, r_GameSettingFourInRow.Cols);
            m_IsComputer = r_GameSettingFourInRow.IsHuman;

            if (m_IsComputer)
            {
                updateAIDepth();
            }

            updatePlayersLabels(r_GameSettingFourInRow.Player1Name, r_GameSettingFourInRow.Player2Name);
            r_GameLogic.InitGameLogic(r_GameSettingFourInRow.Rows, r_GameSettingFourInRow.Cols, r_GameLogic_UpdateButtonBoardEventHandler);
            panel1.Controls.Remove(pictureBoxFalingCoin);
            panel1.Controls.Add(pictureBoxFalingCoin);
            r_GameLogic.ResetBoard();
            fixLabelAboutSize(r_GameSettingFourInRow.Cols, r_GameSettingFourInRow.Rows);
            CenterToParent();
        }

        private void updateAIDepth()
        {
            string difficulty = r_GameSettingFourInRow.Difficulty;

            if (difficulty == "Easy")
            {
                m_Depth = 2;
            }
            else if (difficulty == "Hard")
            {
                m_Depth = 8;
            }
            else
            {
                m_Depth = 4;
            }
        }

        private void clearBoard()
        {
            foreach (List<BoardCell> colList in r_BoardImages)
            {
                foreach (BoardCell item in colList)
                {
                    panel1.Controls.Remove(item);
                }
            }

            r_BoardImages.Clear();
        }

        private void clearButtonPressed()
        {
            foreach (ButtonToPress item in r_ButtonsToPRess)
            {
                Controls.Remove(item);
            }

            r_ButtonsToPRess.Clear();
        }

        private void startANewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_DialogResultOfPropeties == DialogResult.OK)
            {
                resetGame();
            }
            else if (m_DialogResultOfPropeties == DialogResult.Cancel)
            {
                startNewGame();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeLabelsVisibileProperty(false);

            if (m_DialogResultOfPropeties == DialogResult.Cancel)
            {
                startNewGame();
            }
            else
            {
                if (showPropertiesForm() == DialogResult.OK)
                {
                    showStartinNewGameMessage();
                }
            }

            changeLabelsVisibileProperty(true);
        }

        private void showStartinNewGameMessage()
        {
            DialogResult result;
            result = MessageBox.Show("Start New Game?", "4 In A row", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                MessageBox.Show("New Board size will take effect\non the next game.", "4 In A row", MessageBoxButtons.OK);
            }
            else
            {
                resetGame();
            }
        }

        private void startANewTournirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_DialogResultOfPropeties == DialogResult.OK)
            {
                resetGame();
                resetScore();
            }
            else if (m_DialogResultOfPropeties == DialogResult.Cancel)
            {
                startNewGame();
            }
        }

        private void resetScore()
        {
            toolStripStatusLabelPlayer1Score.Text = "0";
            toolStripStatusLabelPlayer2Score.Text = "0";
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuStrip1_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void menuStrip1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = m_CurrentCursorPlayer;
        }

        private void changePlayerCoin()
        {
            Cursor = (m_CurrentCursorPlayer == r_RedCoinCursor) ? r_YellowCoinCursor : r_RedCoinCursor;
            m_CurrentCursorPlayer = Cursor;
        }

        private char currentPlayerSign()
        {
            return Cursor == r_RedCoinCursor ? k_Player1Sign : k_Player2Sign;
        }

        private bool checkStatus(EState i_Status, CoinEventArgs i_Coin)
        {
            m_GameIsFinish = true;

            switch (i_Status)
            {
                case EState.Win:
                    winAnimation(i_Coin);
                    updateScore();
                    showWinMessage("wins!", this.Text);
                    break;
                case EState.Draw:
                    showMessage("Tie!!", this.Text);
                    break;
                case EState.Continue:
                    m_GameIsFinish = false;
                    break;
            }

            return m_GameIsFinish;
        }

        private void winAnimation(CoinEventArgs i_Coin)
        {
            if (timerFall.Enabled == false)
            { // if timer is not running.
                m_ArrWinLine = new Point[r_GameLogic.Sequence];
                r_GameLogic.GetWinRow(m_ArrWinLine, i_Coin);
                timerSwitch.Tick += timerSwitch_Tick;
                timerSwitch.Start();
            }
        }

        private void timerSwitch_Tick(object sender, EventArgs e)
        {
            if (m_Ticks)
            {
                switchImageToPink();
                m_Ticks = false;
            }
            else
            {
                switchImageToBack();
                m_Ticks = true;
            }
        }

        private void switchImageToPink()
        {
            foreach (var variable in m_ArrWinLine)
            {
                if (m_CurrentCursorPlayer == r_RedCoinCursor)
                {
                    addNewPictureBoxPink(variable.Y, variable.X, Properties.Resources.CoinRed);
                }
                else
                {
                    addNewPictureBoxPink(variable.Y, variable.X, Properties.Resources.CoinYellow);
                }
            }
        }

        private void addNewPictureBoxPink(int i_Y, int i_X, Image i_setImage)
        {
            r_BoardImages[i_Y][i_X].BackgroundImage = i_setImage;
            r_BoardImages[i_Y][i_X].BackColor = System.Drawing.Color.Fuchsia;
            r_BoardImages[i_Y][i_X].BackgroundImageLayout = ImageLayout.Center;
            r_BoardImages[i_Y][i_X].MaximumSize = new System.Drawing.Size(k_CellWidth, k_CellWidth);
        }

        private void switchImageToBack()
        {
            foreach (var variable in m_ArrWinLine)
            {
                changeToFullCell(new CoinEventArgs(variable.Y, variable.X, currentPlayerSign()));
            }
        }

        private ToolStripStatusLabel getNameLabelOfTheCurrentPlayer()
        {
            return m_CurrentCursorPlayer == r_RedCoinCursor ? toolStripStatusLabelPlayer1Name : toolStripStatusLabelPlayer2Name;
        }

        private ToolStripStatusLabel getScoreLabelOfTheWiningPlayer()
        {
            if (m_CurrentCursorPlayer == r_RedCoinCursor)
            {
                return toolStripStatusLabelPlayer1Score;
            }
            else
            {
                return toolStripStatusLabelPlayer2Score;
            }
        }

        private void updateScore()
        {
            int score;

            ToolStripStatusLabel scoreLabel;
            scoreLabel = getScoreLabelOfTheWiningPlayer();
            score = int.Parse(scoreLabel.Text) + 1;
            scoreLabel.Text = score.ToString();
        }

        private void showWinMessage(string i_Massage, string i_Caption)
        {
            string winPlayer = getCurrentPlayerName();
            winPlayer = winPlayer.Remove(winPlayer.Length - 1);
            string message = string.Format("{0} {1} ", winPlayer, i_Massage);
            showMessage(message, i_Caption);
        }

        private string getCurrentPlayerName()
        {
            return m_CurrentCursorPlayer == r_RedCoinCursor ? toolStripStatusLabelPlayer1Name.Text : toolStripStatusLabelPlayer2Name.Text;
        }

        private void showMessage(string i_Massage, string i_Caption)
        {
            string massage = string.Format("{0}{1}Another Round ?", i_Massage, Environment.NewLine);
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(massage, i_Caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                this.Close();
            }
            else
            {
                timerSwitch.Stop();
                timerSwitch.Tick -= timerSwitch_Tick;
                resetGame();
            }
        }

        // This is the falling coin animation method.
        private void timerFall_Tick(object sender, EventArgs e)
        {
            bool Finished = false;
            CoinAnimationEventArgs coinArgsElement = e as CoinAnimationEventArgs;

            for (int i = 0; i < k_NumIterrtationForTickMethod && !Finished; ++i)
            {
                if (pictureBoxFalingCoin.Bottom >= coinArgsElement.BotoomLimit)
                {
                    Finished = true;
                    doWhenReachedBottom(coinArgsElement);
                }
                else
                {
                    pictureBoxFalingCoin.Top += 5;
                }
            }
        }

        private void onFallingAnimation(int i_Col, int i_Row, EState i_Status)
        {
            if (timerFall.Enabled == false)
            { // if timer is not runing.
                pictureBoxFalingCoin.Image = getCurrentPlayerCoinImage();
                pictureBoxFalingCoin.Visible = true;
                pictureBoxFalingCoin.Location = new Point(k_CellWidth * i_Col, 0);

                // Create coin parameter to send for the Tick method.
                CoinAnimationEventArgs coinArgs = new CoinAnimationEventArgs(
                pictureBoxFalingCoin.Location,
                r_BoardImages[i_Col][i_Row].Bottom,
                new CoinEventArgs(i_Col, i_Row, currentPlayerSign()),
                i_Status);
                EventHandler eventHandlerTickMethod = new EventHandler(delegate (object sender, EventArgs e) { timerFall_Tick(this, coinArgs); });
                coinArgs.EventHandlerMethod = eventHandlerTickMethod;

                timerFall.Tick += eventHandlerTickMethod;
                timerFall.Start();
            }
        }

        private Image getCurrentPlayerCoinImage()
        {
            return m_CurrentCursorPlayer == r_RedCoinCursor ? Properties.Resources.CoinRed : Properties.Resources.CoinYellow;
        }

        private void doWhenReachedBottom(CoinAnimationEventArgs e)
        {
            timerFall.Stop();

            pictureBoxFalingCoin.Visible = false;
            r_BoardImages[e.CoinArgs.Col][e.CoinArgs.Row].ResetRegion(); // Change the region now, because the picture is not in empty cell.          

            timerFall.Tick -= e.EventHandlerMethod;
            changeToFullCell(e.CoinArgs);

            bool isGameFinish = checkStatus(e.State, e.CoinArgs); // TODO:: isGameFinish not needed because adds of m_GameIsFinish member.

            if (!isGameFinish)
            { // do this just if the game is not finish.
                changePlayerCoin();
                changeCurrentPlayerName();

                if (m_IsComputer && currentPlayerSign() == k_Player2Sign)
                {
                    int columnToEnter = r_GameLogic.ReturnComputerMove(m_Depth);
                    int rowToEnter = r_GameLogic.EnterCoin(columnToEnter, currentPlayerSign());
                    EState status = r_GameLogic.CheckBoardStatus(rowToEnter, columnToEnter);
                    onFallingAnimation(columnToEnter, rowToEnter, status);
                }
            }
        }

        private void changeCurrentPlayerName()
        {
            string name = getCurrentPlayerName();
            name = name.Remove(name.Length - 1); // delete the -> ':'
            toolStripStatusLabelCurrentPlayerName.Text = name;
        }

        private void ButtonToPress_Click(object sender, EventArgs e)
        {

            ButtonToPress buttonPressed = sender as ButtonToPress;

            if (timerFall.Enabled == false && buttonPressed != null && !r_GameLogic.CheckIfColumnFull(buttonPressed.Col + 1))
            {
                int rowToEnter = r_GameLogic.EnterCoin(buttonPressed.Col, currentPlayerSign());
                EState status = r_GameLogic.CheckBoardStatus(rowToEnter, buttonPressed.Col);
                onFallingAnimation(buttonPressed.Col, rowToEnter, status);
            }

        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("C:\\FourInARowHelp.txt"))
            {
                HowToPlayForm HowToPlay = new HowToPlayForm();
                HowToPlay.ShowDialog();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
            panel1.Enabled = false;
            menuStrip1.Enabled = false;
            labelAbout.Visible = true;
        }

        private void labelAbout_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            menuStrip1.Enabled = true;
            labelAbout.Visible = false;
            Cursor = m_CurrentCursorPlayer;
        }
    }
}
