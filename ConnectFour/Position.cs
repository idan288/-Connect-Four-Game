using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConnectFour
{
    public class Position
    {
        private Point m_pos;
        private char m_value;

        public Point Pos
        {
            get { return m_pos; }
            set { m_pos = value; }
        }

        public char Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }
}
