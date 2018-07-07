using System.Drawing;

namespace ConnectFour
{
    public class GameLogic
    {
        private enum eRowOrCol
        {
            Row,
            Col,
        }

        private enum eDiagonalType
        {
            Main,
            Secondary,
        }

        private const byte k_MinSizeBoard = 4;
        private const byte k_MaxSizeBoard = 10;
        private const char k_Computer = 'O';
        private const char k_OpponentSign = 'X';
        private const byte k_SequenceForWin = 4;
        private const char k_EndRow = '@';

        private Board m_GameBoard;
        private Position[] m_Row;

        // AI Consts
        private const byte k_MinDepth = 0;
        private const int k_InitializeColumn = -1;
        private const int k_MinumumValue = int.MinValue + 1;
        private const int k_MaximumValue = int.MaxValue - 1;

        // AI Members
        private char[] m_rowInBoard;                                           // This array will hold the row/diagonal/cloumn from the board as a row.
        private int m_logicSizeArray;                                          // This is a logical size of the m_rowInBoard array.
        private int[] m_ScoreArray = new int[6] { 0, 1, 4, 32, 128, 512 };     // This array contain the ranking score.                   
        private bool m_PlayerWin;                                              // will hold the acknowledgement if the opponent player is win.  
        private bool m_ComputerWin;                                            // will hold the acknowledgement if the computer is win. 

        private event UpdateButtonBoardEventHandler UpdateButtonOnBoardOccured;

        public byte MinSizeBoard
        {
            get { return k_MinSizeBoard; }
        }

        public byte MaxSizeBoard
        {
            get { return k_MaxSizeBoard; }
        }

        public char Player1Sign
        {
            get { return k_OpponentSign; }
        }

        public char Player2Sign
        {
            get { return k_Computer; }
        }

        public int Sequence
        {
            get { return k_SequenceForWin; }
        }

        public void InitGameLogic(int i_Row, int i_Col, UpdateButtonBoardEventHandler i_FuncUpdate)
        {
            m_GameBoard = new Board(i_Row, i_Col);
            m_rowInBoard = new char[k_MaxSizeBoard];
            UpdateButtonOnBoardOccured += i_FuncUpdate;
        }

        public void GetWinRow(Point[] i_arr, CoinEventArgs i_Coin)
        {
            bool isWinHappen;
            int startWinInd, EndWinInd;
            int startRow;
            int startCol;
            m_Row = new Position[k_MaxSizeBoard];

            // check line down
            updateRowLine(eRowOrCol.Col, i_Coin.Col);
            isWinHappen = checkAWinLine(i_Coin.SignPlayer, out startWinInd, out EndWinInd);
            if (isWinHappen)
            {
                CreatePointToReturn(i_arr, startWinInd, EndWinInd);
                return;
            }

            // check line left/right
            updateRowLine(eRowOrCol.Row, i_Coin.Row);
            isWinHappen = checkAWinLine(i_Coin.SignPlayer, out startWinInd, out EndWinInd);
            if (isWinHappen)
            {
                CreatePointToReturn(i_arr, startWinInd, EndWinInd);
                return;
            }

            // check diagonal from up right to down left
            startRow = (i_Coin.Row + i_Coin.Col > m_GameBoard.Row - 1) ? m_GameBoard.Row - 1 : i_Coin.Row + i_Coin.Col;
            startCol = (i_Coin.Col + i_Coin.Row > m_GameBoard.Row - 1) ? (i_Coin.Col - (m_GameBoard.Row - 1 - i_Coin.Row)) : 0;
            updateRowLineDiagonal(eDiagonalType.Secondary, startRow, startCol);
            isWinHappen = checkAWinLine(i_Coin.SignPlayer, out startWinInd, out EndWinInd);
            if (isWinHappen)
            {
                CreatePointToReturn(i_arr, startWinInd, EndWinInd);
                return;
            }

            // check diagonal from up left to down right           
            startRow = (i_Coin.Row - i_Coin.Col < 0) ? 0 : i_Coin.Row - i_Coin.Col;
            startCol = (i_Coin.Col - i_Coin.Row < 0) ? 0 : i_Coin.Col - i_Coin.Row;
            updateRowLineDiagonal(eDiagonalType.Main, startRow, startCol);
            isWinHappen = checkAWinLine(i_Coin.SignPlayer, out startWinInd, out EndWinInd);
            if (isWinHappen)
            {
                CreatePointToReturn(i_arr, startWinInd, EndWinInd);
                return;
            }
        }

        private void CreatePointToReturn(Point[] i_arr, int i_StartInd, int i_EndInd)
        {
            for (int i = i_StartInd, j = 0; i <= i_EndInd; i++, j++)
            {
                i_arr[j] = m_Row[i].Pos;
            }
        }

        private void updateRowLine(eRowOrCol i_RowOrColOption, int i_index)
        {
            int size = (i_RowOrColOption == eRowOrCol.Col) ? m_GameBoard.Row : m_GameBoard.Col;
            int i;

            for (i = 0; i < size; i++)
            {
                if (i_RowOrColOption == eRowOrCol.Col)
                {
                    m_Row[i] = new Position();
                    m_Row[i].Pos = new Point(i, i_index);
                    m_Row[i].Value = m_GameBoard[i, i_index];
                }
                else
                {
                    m_Row[i] = new Position();
                    m_Row[i].Pos = new Point(i_index, i);
                    m_Row[i].Value = m_GameBoard[i_index, i];
                }
            }

            if (i < m_Row.Length)
            {
                m_Row[i] = new Position();
                m_Row[i].Value = k_EndRow;
            }
        }

        private bool checkAWinLine(char i_playerToWin, out int o_StartInd, out int o_EndInd)
        {
            o_StartInd = o_EndInd = -1;
            int count = 0;
            bool result = false;
            for (int i = 0; i < m_Row.Length; i++)
            {
                if (m_Row[i].Value == k_EndRow)
                {
                    break;
                }

                if (m_Row[i].Value == i_playerToWin)
                {
                    if (count == 0)
                    {
                        o_StartInd = i;
                    }

                    count++;
                    if (count == k_SequenceForWin)
                    {
                        result = true;
                        o_EndInd = i;                      
                    }
                }
                else
                {
                    count = 0;
                }
            }

            if (!result && count == k_SequenceForWin)
            {
                result = true;
            }

            return result;
        }

        private void updateRowLineDiagonal(eDiagonalType i_Diagonal, int i_row, int i_col)
        {
            int k = 0;
            int addI = (i_Diagonal == eDiagonalType.Main) ? 1 : -1; // main diagonal is up, and secondery diagonal is down
            int i = i_row, j = i_col;
            while ((i_Diagonal == eDiagonalType.Main && i < m_GameBoard.Row && j < m_GameBoard.Col) || (i_Diagonal == eDiagonalType.Secondary && i >= 0 && j < m_GameBoard.Col))
            {
                m_Row[k] = new Position();
                m_Row[k].Pos = new Point(i, j);
                m_Row[k].Value = m_GameBoard[i, j];
                i += addI;
                j++;
                k++;
            }

            if (k < m_Row.Length)
            {
                m_Row[k] = new Position();
                m_Row[k].Value = k_EndRow;
            }

            // get the new logic size of the array.
            m_logicSizeArray = k;
        }

        // this fucntion will enter a new coin to the board and will return the row that was update             
        public int EnterCoin(int i_columnToEnter, char i_sign)
        {
            int row;

            row = firstEmptyRow(i_columnToEnter);
            m_GameBoard[row, i_columnToEnter] = i_sign;

            OnUpdateButtonOnBoardOccured(new CoinEventArgs(i_columnToEnter, row, i_sign));

            return row;
        }
        
        protected virtual void OnUpdateButtonOnBoardOccured(CoinEventArgs e)
        {
            if (UpdateButtonOnBoardOccured != null)
            {
                UpdateButtonOnBoardOccured.Invoke(this, e);
            }
        }

        // this function will return the current state on board: continue,draw,win
        // the function recive the last input on board
        public EState CheckBoardStatus(int i_row, int i_col)
        {
            EState returnState = checkForAWin(i_row, i_col);
            if (returnState == EState.Continue)
            {
                returnState = checkForADraw(i_row);
            }

            return returnState;
        }

        // this function will check if a win happen
        private EState checkForAWin(int i_row, int i_col)
        {
            char playerToWin = m_GameBoard[i_row, i_col];
            bool isWinHappen;
            int startRow;
            int startCol;

            // check line down
            updateRowInBoardArrayFromLine(eRowOrCol.Col, i_col);
            isWinHappen = checkASequanceOfFour(playerToWin);
            if (isWinHappen)
            {
                return EState.Win;
            }

            // check line left/right
            updateRowInBoardArrayFromLine(eRowOrCol.Row, i_row);
            isWinHappen = checkASequanceOfFour(playerToWin);
            if (isWinHappen)
            {
                return EState.Win;
            }

            // check diagonal from up right to down left
            startRow = (i_row + i_col > m_GameBoard.Row - 1) ? m_GameBoard.Row - 1 : i_row + i_col;
            startCol = (i_col + i_row > m_GameBoard.Row - 1) ? (i_col - (m_GameBoard.Row - 1 - i_row)) : 0;
            updateRowInBoardArrayFromDiagonal(eDiagonalType.Secondary, startRow, startCol);
            isWinHappen = checkASequanceOfFour(playerToWin);
            if (isWinHappen)
            {
                return EState.Win;
            }

            // check diagonal from up left to down right           
            startRow = (i_row - i_col < 0) ? 0 : i_row - i_col;
            startCol = (i_col - i_row < 0) ? 0 : i_col - i_row;
            updateRowInBoardArrayFromDiagonal(eDiagonalType.Main, startRow, startCol);
            isWinHappen = checkASequanceOfFour(playerToWin);
            if (isWinHappen)
            {
                return EState.Win;
            }

            return EState.Continue;
        }

        // copy the row we want to check to the member array (this row is a line)
        private void updateRowInBoardArrayFromLine(eRowOrCol i_RowOrColOption, int i_index)
        {
            int size = (i_RowOrColOption == eRowOrCol.Col) ? m_GameBoard.Row : m_GameBoard.Col;
            int i;

            for (i = 0; i < size; i++)
            {
                if (i_RowOrColOption == eRowOrCol.Col)
                {
                    m_rowInBoard[i] = m_GameBoard[i, i_index];
                }
                else
                {
                    m_rowInBoard[i] = m_GameBoard[i_index, i];
                }
            }

            if (i < m_rowInBoard.Length)
            {
                m_rowInBoard[i] = k_EndRow;
            }

            m_logicSizeArray = i;
        }

        // copy the row we want to check to the member array (this row is a diagonal)
        private void updateRowInBoardArrayFromDiagonal(eDiagonalType i_Diagonal, int i_row, int i_col)
        {
            int k = 0;
            int addI = (i_Diagonal == eDiagonalType.Main) ? 1 : -1; // main diagonal is up, and secondery diagonal is down
            int i = i_row, j = i_col;
            while ((i_Diagonal == eDiagonalType.Main && i < m_GameBoard.Row && j < m_GameBoard.Col) || (i_Diagonal == eDiagonalType.Secondary && i >= 0 && j < m_GameBoard.Col))
            {
                m_rowInBoard[k] = m_GameBoard[i, j];
                i += addI;
                j++;
                k++;
            }

            if (k < m_rowInBoard.Length)
            {
                m_rowInBoard[k] = k_EndRow;
            }

            // get the new logic size of the array.
            m_logicSizeArray = k;
        }

        // check if in row we copied has 4 equals signs in a row
        private bool checkASequanceOfFour(char i_playerToWin)
        {
            int count = 0;
            bool result = false;
            for (int i = 0; i < m_rowInBoard.Length; i++)
            {
                if (m_rowInBoard[i] == k_EndRow)
                {
                    break;
                }

                if (m_rowInBoard[i] == i_playerToWin)
                {
                    count++;
                    if (count == k_SequenceForWin)
                    {
                        result = true;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            if (!result && count == k_SequenceForWin)
            {
                result = true;
            }

            return result;
        }

        // this function check if draw happen
        private EState checkForADraw(int i_row)
        {
            bool isDrawHappen = true;
            if (i_row == 0)
            {
                for (int i = 0; i < m_GameBoard.Col; i++)
                {
                    if (m_GameBoard[i_row, i] == ' ')
                    {
                        isDrawHappen = false;
                        break;
                    }
                }

                if (isDrawHappen)
                {
                    return EState.Draw;
                }
            }

            return EState.Continue;
        }

        // this fucntion will check if the user column choise is valid
        public bool CheckRangeColumnNumber(int i_UserColumnChoice)
        {
            return i_UserColumnChoice - 1 < m_GameBoard.Col;
        }

        // this fucntion will return true if a wanted column is full
        public bool CheckIfColumnFull(int i_UserColumnChoice)
        {
            bool res = (m_GameBoard[0, i_UserColumnChoice - 1] != ' ') ? true : false;
            return res;
        }

        public void ResetBoard()
        {
            m_GameBoard.Reset();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                    AI FUNCTIONS                                                                 //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        /// <summary>
        /// This method using minimax algorithm to find the best column choise for the computer move.
        /// </summary>
        /// MiniMax algorithm is a recursive algorithm for choosing the next move in an n-player game.
        /// A value is associated with each position or state of the game. 
        /// This value is computed by means of a position evaluation function and it indicates how good it would be for a player to reach that position.
        /// The player then makes the move that maximizes the minimum value of the position resulting from the opponent's possible following moves. 
        /// 
        /// <param name="i_MaxDepth">  This parameter will deterimine the depth of the searching. </param>   
        /// <returns> This method return int number that present the best column choise.       
        /// </returns>     
        public int ReturnComputerMove(int i_MaxDepth)
        {
            int bestCol;
            m_PlayerWin = false;
            m_ComputerWin = false;

            // one round just to check if the computer can make one move to win in the next round, so need to take this option.
            evaluateMaxMove(k_MinDepth, 1, k_InitializeColumn, k_MinumumValue, k_MaximumValue, out bestCol);
            if (m_ComputerWin)
            {
                return bestCol;
            }

            // one round just to check if the player can make one move to win in the next round so take this option.
            evaluateMinMove(k_MinDepth, 1, k_InitializeColumn, k_MinumumValue, k_MaximumValue, out bestCol);
            if (m_PlayerWin)
            {
                return bestCol;
            }

            // check the best col to chose.
            evaluateMaxMove(k_MinDepth, i_MaxDepth, k_InitializeColumn, k_MinumumValue, k_MaximumValue, out bestCol);

            return bestCol;
        }

        /// ============================================================================================================================================================
        /// <summary>
        /// This method is the Min part of the recursive minimax algorithm.
        /// </summary>
        /// <param name="i_depth">    The current depth of the recursive search.                                                                               </param>
        /// <param name="i_maxDepth"> The maximum search depth.                                                                                                </param>
        /// <param name="i_col">      The current column that being tested.                                                                                    </param>
        /// <param name="i_alpha">    The current alpha value during the recursive minimax.                                                                    </param> 
        /// <param name="i_beta">     The current beta value during the recursive minimax.                                                                     </param>
        /// <param name="o_bestCol">  The current best coulmn for the computer move, this is a output paramter so in the end will save the best column choice. </param>
        /// <returns>
        /// The method return the minimum heuristic score of the opponent's possible following moves.     
        /// </returns>
        /// ============================================================================================================================================================      
        private int evaluateMinMove(int i_depth, int i_maxDepth, int i_col, int i_alpha, int i_beta, out int o_bestCol)
        {
            int min = int.MaxValue;
            int score = 0;
            o_bestCol = k_InitializeColumn;

            // Check if this is not the first call, so there is no move to evaluate. 
            // evaluate the oppnent score.
            if (i_col != k_InitializeColumn)
            {
                score = getHeuristicScore(k_Computer, i_col);

                // Check if the computer win on this move, so return score now.
                if (m_ComputerWin)
                {
                    return score;
                }
            }

            // Check if this is the max depth so no need to search more.
            if (i_depth == i_maxDepth)
            {
                return score;
            }

            int value;
            int unused;
            int cloumnLength = m_GameBoard.Col;

            // This is the loop that simulate all the possible moves.
            for (int columnToSimulate = 0; columnToSimulate < cloumnLength; ++columnToSimulate)
            {
                // Check if the ColumnToSimulate is not full, so it is a possible move.
                if (CheckIfColumnFull(columnToSimulate + 1) == false)
                {
                    EnterCoinAI(columnToSimulate, k_OpponentSign);
                    value = evaluateMaxMove(i_depth + 1, i_maxDepth, columnToSimulate, i_alpha, i_beta, out unused);
                    removeCoin(columnToSimulate);

                    // found new min value, change the minimum, and update the new best column. 
                    if (value < min)
                    {
                        min = value;
                        o_bestCol = columnToSimulate;
                    }

                    if (value < i_beta)
                    {
                        i_beta = value;
                    }

                    if (i_alpha >= i_beta)
                    {
                        return i_beta;
                    }
                }
            }

            if (min == int.MaxValue)
            {
                return 0;
            }

            return min;
        }

        /// =======================================================================================================================================
        /// <summary>
        /// This method is the Max part of the recursive minimax algorithm.
        /// </summary>
        /// <param name="i_depth">    The current depth of the recursive search.                                                          </param>
        /// <param name="i_maxDepth"> The maximum search depth.                                                                           </param>
        /// <param name="i_col">      The current column that being tested.                                                               </param>
        /// <param name="i_alpha">    The current alpha value during the recursive minimax.                                               </param>
        /// <param name="i_beta">     The current beta value during the recursive minimax.                                                </param>
        /// <param name="o_bestCol">  The current best coulmn, this is a output paramter so in the end will save the best column choice.  </param>
        /// <returns>
        /// The method return the maximum heuristic score of the computer following moves. 
        /// </returns>
        /// /// ==================================================================================================================================
        private int evaluateMaxMove(int i_depth, int i_maxDepth, int i_col, int i_alpha, int i_beta, out int o_bestCol)
        {
            int max = int.MinValue;
            int score = 0;
            o_bestCol = k_InitializeColumn;

            // Check if this is not the first call, so there is no move to evaluate.
            // evaluate the oppnent score.
            if (i_col != k_InitializeColumn)
            {
                score = getHeuristicScore(k_OpponentSign, i_col);

                // Check if the player win on this move, so return score now.
                if (m_PlayerWin)
                {
                    return score;
                }
            }

            // Check if this is the max depth so no need to search more.         
            if (i_depth == i_maxDepth)
            {
                return score;
            }

            int value;
            int unused;
            int cloumnLength = m_GameBoard.Col;

            // This is the loop that simulate all the possible moves.
            for (int columnToSimulate = 0; columnToSimulate < cloumnLength; ++columnToSimulate)
            {
                // Check if the ColumnToSimulate is not full, so it is a possible move.
                if (CheckIfColumnFull(columnToSimulate + 1) == false)
                {
                    EnterCoinAI(columnToSimulate, k_Computer);
                    value = evaluateMinMove(i_depth + 1, i_maxDepth, columnToSimulate, i_alpha, i_beta, out unused);
                    removeCoin(columnToSimulate);

                    // found new max value, change the maximum and update the new best column.
                    if (value > max)
                    {
                        max = value;
                        o_bestCol = columnToSimulate;
                    }

                    if (value > i_alpha)
                    {
                        i_alpha = value;
                    }

                    if (i_alpha >= i_beta)
                    {
                        return i_alpha;
                    }
                }
            }

            if (max == int.MinValue)
            {
                return 0;
            }

            return max;
        }

        /// ==========================================================================================================================
        /// <summary>
        /// This method collect the total score of the current move by checking score of row,cloumn,major diagonal and minor diagonal. 
        /// </summary>
        /// <param name="i_PlayerSign"> The sign of the estimated player </param>
        /// <param name="i_col">        The estimate column move.        </param>         
        /// <returns> 
        /// The method return the total evaluated score.
        /// </returns>
        /// ==========================================================================================================================
        private int getHeuristicScore(char i_PlayerSign, int i_col)
        {
            int score = 0;
            int row = firstEmptyRow(i_col) + 1; // Get the row of the estimate move. 
            int sequenceScore = 0;
            m_PlayerWin = false;
            m_ComputerWin = false;

            ///////////////////////////////////////////////////////////////////////
            // Check score for row.
            ///////////////////////////////////////////////////////////////////////
            updateRowInBoardArrayFromLine(eRowOrCol.Row, row);
            sequenceScore = checkSequencesFromArray(i_col, i_PlayerSign);

            // if there was a winner we need to return the score now. 
            if (m_ComputerWin || m_PlayerWin)
            {
                return sequenceScore;
            }

            score += sequenceScore;

            ///////////////////////////////////////////////////////////////////////
            // Check score for column.
            ///////////////////////////////////////////////////////////////////////
            updateRowInBoardArrayFromLine(eRowOrCol.Col, i_col);
            sequenceScore = checkSequencesFromArray(row, i_PlayerSign);

            // if there was a winner we need to return the score now. 
            if (m_ComputerWin || m_PlayerWin)
            {
                return sequenceScore;
            }

            score += sequenceScore;

            ///////////////////////////////////////////////////////////////////////
            // Check score for major diagonal - Up left to down right.
            ///////////////////////////////////////////////////////////////////////                
            int startRow;
            int startCol;
            startRow = (row - i_col < 0) ? 0 : row - i_col;
            startCol = (i_col - row < 0) ? 0 : i_col - row;
            sequenceScore = 0;
            updateRowInBoardArrayFromDiagonal(eDiagonalType.Main, startRow, startCol);

            // Check if there is a possible for 4 in row, else dont consider this sequence. 
            if (m_logicSizeArray >= k_SequenceForWin)
            {
                sequenceScore = checkSequencesFromArray(i_col - startCol, i_PlayerSign);

                // if there was a winner we need to return the score now. 
                if (m_ComputerWin || m_PlayerWin)
                {
                    return sequenceScore;
                }

                score += sequenceScore;
            }

            ///////////////////////////////////////////////////////////////////////
            // Check score for minor diagonal - Up right to down left.
            ///////////////////////////////////////////////////////////////////////
            startRow = (row + i_col > m_GameBoard.Row - 1) ? m_GameBoard.Row - 1 : row + i_col;
            startCol = (i_col + row > m_GameBoard.Row - 1) ? i_col - (m_GameBoard.Row - 1 - row) : 0;
            sequenceScore = 0;
            updateRowInBoardArrayFromDiagonal(eDiagonalType.Secondary, startRow, startCol);

            // Check if there is a possible for 4 in row, else dont consider this sequence. 
            if (m_logicSizeArray >= k_SequenceForWin)
            {
                sequenceScore = checkSequencesFromArray(i_col - startCol, i_PlayerSign);

                // if there was a winner we need to return the score now. 
                if (m_ComputerWin || m_PlayerWin)
                {
                    return sequenceScore;
                }

                score += sequenceScore;
            }

            return score;
        }

        /// ======================================================================================================= 
        /// <summary>
        /// This is the evaluation method that estimate the value goodness of a position in the minimax algorithm.    
        /// </summary>
        /// <param name="i_ComputerSignCount"> The number of the computer sign that found in the sequnce. </param>
        /// <param name="i_PlayerSignCount">   The number of the opponent sign that found in the sequnce. </param>
        /// <param name="i_sign">              The current player sign.                                   </param>
        /// <returns>
        /// The method return the score acroding the i_ComputerSignCount and i_PlayerSignCount parameters.
        /// if this is a good position for the opponent return a negative score.
        /// if this is a good position for the computer return postive score.
        /// </returns>  
        /// =======================================================================================================
        private int getScore(int i_ComputerSignCount, int i_PlayerSignCount, char i_sign)
        {
            if (i_ComputerSignCount == i_PlayerSignCount)
            {
                if (i_sign == k_OpponentSign)
                {
                    return -1;
                }

                return 1;
            }
            else if (i_ComputerSignCount > i_PlayerSignCount)
            {
                if (i_sign == k_OpponentSign)
                {
                    return m_ScoreArray[i_ComputerSignCount] - m_ScoreArray[i_PlayerSignCount];
                }

                return m_ScoreArray[i_ComputerSignCount + 1] - m_ScoreArray[i_PlayerSignCount];
            }
            else
            {
                if (i_sign == k_OpponentSign)
                {
                    return -m_ScoreArray[i_PlayerSignCount + 1] + m_ScoreArray[i_ComputerSignCount];
                }

                return -m_ScoreArray[i_PlayerSignCount] + m_ScoreArray[i_ComputerSignCount];
            }
        }

        /// ========================================================================
        /// <summary>
        /// This method calculate score of sequnce from the m_rowInBoard.  
        /// </summary>
        /// <param name="i_col">        The estimate column move.         </param>
        /// <param name="i_PlayerSign"> The sign of the estimated player. </param>        
        /// <returns>
        /// This method return the score of this sequance.
        /// </returns>
        /// ========================================================================
        private int checkSequencesFromArray(int i_col, char i_PlayerSign)
        {
            int score = 0;
            int computerSignCount;
            int playerSignCount;
            int returnWiningScore;
            int colStart;
            int colEnd;

            calculateColStarAndColEnd(out colStart, out colEnd, i_col);

            for (int col = colStart; col < colEnd; ++col)
            {
                computerSignCount = 0;
                playerSignCount = 0;

                for (int i = 0; i < k_SequenceForWin; ++i)
                {
                    if (m_rowInBoard[col + i] == k_Computer)
                    {
                        ++computerSignCount;
                    }
                    else if (m_rowInBoard[col + i] == k_OpponentSign)
                    {
                        ++playerSignCount;
                    }
                }

                // Check if this is a good sequence, so we have a potential sequence of four.
                if (computerSignCount == 0 || playerSignCount == 0)
                {
                    // Check if there was a winner so return his wining score.                                     
                    if (foundWinner(playerSignCount, computerSignCount, out returnWiningScore) == true)
                    {
                        return returnWiningScore; // if the computer win ReturnWiningScore = k_MaximumValue.
                    }                             // if the player   win ReturnWiningScore = k_MinumumValue.

                    score += getScore(computerSignCount, playerSignCount, i_PlayerSign);
                }
            }

            return score;
        }

        /// ===============================================================================================================================     
        /// <summary>
        /// This method calculated how many sequence the estimed column will include.
        /// </summary>
        /// <param name="o_ColStart"> This output parameter will get the first cloumn of the first sequence.                       </param>
        /// <param name="o_ColEnd">   This output parameter will get the number of sequences that the estimed column will include. </param>
        /// <param name="i_col">      The estimate column move.                                                                    </param>
        /// <returns>
        /// The method return true if there was a winner in the game.
        /// </returns>
        /// ================================================================================================================================
        private void calculateColStarAndColEnd(out int o_ColStart, out int o_ColEnd, int i_col)
        {
            int threeRightFromCheckedCloumn = i_col + 3;
            int threeLeftFromCheckedCloumn = i_col - 3;
            int oneLeftFromCheckedCloumn = i_col - 1;
            int towLeftFromCheckedCloumn = i_col - 2;

            o_ColStart = threeLeftFromCheckedCloumn >= 0 ? threeLeftFromCheckedCloumn : 0;
            o_ColEnd = o_ColStart;

            // If there is a potential sequence of four right from i_col.
            if (m_logicSizeArray > threeRightFromCheckedCloumn)
            {
                o_ColEnd++;
            }

            // If there is a potential sequence of four left from i_col.
            if (threeLeftFromCheckedCloumn >= 0)
            {
                o_ColEnd++;
            }

            // If there is a potential sequence of four one left from i_col.   
            if (oneLeftFromCheckedCloumn >= 0 && oneLeftFromCheckedCloumn + 3 < m_logicSizeArray)
            {
                o_ColEnd++;
            }

            // If there is a potential sequence of four tow left from i_col.  
            if (towLeftFromCheckedCloumn >= 0 && towLeftFromCheckedCloumn + 3 < m_logicSizeArray)
            {
                o_ColEnd++;
            }
        }

        // This method return true if there was a winner in the game.
        // if the opponent win put into m_PlayerWin true and the o_ReturnWiningScore get the maximum value.
        // if the computer win put into m_ComputerWin true and the o_ReturnWiningScore get the minimum value.
        private bool foundWinner(int i_PlayerSignCount, int i_ComputerSignCount, out int o_ReturnWiningScore)
        {
            o_ReturnWiningScore = -1;

            // The player win.
            if (i_PlayerSignCount == k_SequenceForWin)
            {
                m_PlayerWin = true;
                o_ReturnWiningScore = k_MinumumValue;
                return true;
            }
            else
            {
                // The computer win.
                if (i_ComputerSignCount == k_SequenceForWin)
                {
                    m_ComputerWin = true;
                    o_ReturnWiningScore = k_MaximumValue;
                    return true;
                }
            }

            return false;
        }

        private void removeCoin(int i_RemoveColumn)
        {
            bool foundRow = false;
            int numRowsInTheBoard = m_GameBoard.Row;

            for (int row = 0; row < numRowsInTheBoard && !foundRow; ++row)
            {
                if (m_GameBoard[row, i_RemoveColumn] != ' ')
                {
                    m_GameBoard[row, i_RemoveColumn] = ' ';
                    foundRow = true;
                }
            }
        }

        public void EnterCoinAI(int i_columnToEnter, char i_sign)
        {
            int row = firstEmptyRow(i_columnToEnter);
            m_GameBoard[row, i_columnToEnter] = i_sign;
        }

        // this function will return the first empty slot in a column
        // if it's full will return -1
        private int firstEmptyRow(int i_col)
        {
            for (int i = m_GameBoard.Row - 1; i >= 0; i--)
            {
                if (m_GameBoard[i, i_col] == ' ')
                {
                    return i;
                }
            }

            return -1;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                              END AI FUNCTIONS                                                                   //
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////              
    }
}
