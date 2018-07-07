using System.Windows.Forms;
using System.Drawing;
using System;
using System.Drawing.Imaging;

namespace ConnectFour
{
    public class BoardCell : PictureBox
    {
        private readonly int r_CellWidth;
        private readonly int r_CellHeight;
        private readonly Region r_OriganlRegion;

        public BoardCell(int i_YPosition, int i_XPostion)
        {
            r_CellWidth = Properties.Resources.EmptyCell.Width;
            r_CellHeight = Properties.Resources.EmptyCell.Height;
            Location = new Point(i_XPostion, i_YPosition);
            Size = new Size(r_CellWidth, r_CellHeight);
            BackgroundImage = Properties.Resources.EmptyCell;
            r_OriganlRegion = Region;
            BitmapRegion.CreateControlRegion(this);
        }

        public void ResetRegion()
        {
            Region = r_OriganlRegion;
        }
    }
}
