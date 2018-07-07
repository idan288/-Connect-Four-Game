using System.Collections.Generic;

namespace ConnectFour
{
    public class Board
    {
        private const int k_rowAccess = 0;

        private List<List<char>> m_board;

        public Board(int i_row, int i_col)
        {
            m_board = new List<List<char>>(i_col);

            for (int i = 0; i < i_col; ++i)
            {
                m_board.Add(new List<char>());
                for (int j = 0; j < i_row; ++j)
                {
                    m_board[i].Add(' ');
                }
            }
        }

        public char this[int i_row, int i_col]
        {
            get { return m_board[i_col][i_row]; }
            set { m_board[i_col][i_row] = value; }
        }

        public int Row
        {
            get { return m_board[k_rowAccess].Count; }
        }

        public int Col
        {
            get { return m_board.Count; }
        }

        // this function will reset the board
        public void Reset()
        {
            for (int i = 0; i < Col; i++)
            {
                for (int j = 0; j < Row; j++)
                {
                    m_board[i][j] = ' ';
                }
            }
        }
    }
}
