using System;

namespace ConnectFour
{
    public class CoinEventArgs : EventArgs
    {
        private readonly int r_Col;
        private readonly int r_Row;
        private readonly char r_SignPlayer;

        public CoinEventArgs(int i_Col, int i_Row, char i_SignPlayer)
        {
            r_Col = i_Col;
            r_Row = i_Row;
            r_SignPlayer = i_SignPlayer;
        }

        public int Col
        {
            get { return r_Col; }
        }

        public int Row
        {
            get { return r_Row; }
        }

        public char SignPlayer
        {
            get { return r_SignPlayer; }
        }
    }
}
