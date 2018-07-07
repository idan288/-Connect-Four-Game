using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ConnectFour
{
    public static class BitmapRegion
    {
        /// <summary>
        /// Create and apply the region on the supplied control
        /// </summary>
        /// <param name="i_Control">The Control object to apply the region to</param>
        public static void CreateControlRegion(Control i_Control)
        {
            // Check if we are dealing with Form here
            Form form = i_Control as Form;
            if (form != null)
            {
                if (form.FormBorderStyle != FormBorderStyle.None)
                {
                    form.FormBorderStyle = FormBorderStyle.None;
                }
            }

            // Set our control's size to be the same as the bitmap
            i_Control.Width = i_Control.BackgroundImage.Width;
            i_Control.Height = i_Control.BackgroundImage.Height;

            Bitmap bitmap = i_Control.BackgroundImage as Bitmap;

            // Calculate the graphics path based on the bitmap supplied
            GraphicsPath graphicsPath = QuickCalculateGraphicsPath(bitmap);

            // Apply new region
            i_Control.Region = new Region(graphicsPath);
        }

        /// <summary>
        /// Calculate the graphics path that representing the figure in the bitmap 
        /// excluding the transparent color which is the top left pixel.
        /// </summary>
        /// <param name="bitmap">The Bitmap object to calculate our graphics path from</param>
        /// <returns>Calculated graphics path</returns>
        /// <remarks>
        /// This method introduces the following performance improvments:
        /// <para>
        /// 1. Faster algorithm
        /// </para>
        /// <para>
        /// 2. Better approarch to a bitmap using LockBits, Scan0 and Stride
        /// </para>
        /// <para>
        /// 3. Working with pointers
        /// </para>
        /// <para>
        /// 4. Using the "unsafe" keyword
        /// </para>
        /// <para>
        /// 5. Using local variables instead of properties in the iterations
        /// </para>
        /// </remarks>
        private static GraphicsPath QuickCalculateGraphicsPath(Bitmap i_Bitmap)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Color transparentColor = i_Bitmap.GetPixel(0, 0);
            int startRegionArea = -1;
            Color pixelColor = Color.Empty;
            BitmapData bitmapData = i_Bitmap.LockBits(new Rectangle(0, 0, i_Bitmap.Width, i_Bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr scanL = bitmapData.Scan0;
            int yOffset;
            yOffset = bitmapData.Stride - (i_Bitmap.Width * 3);

            unsafe
            {
                int bitmapHeight = i_Bitmap.Height;
                int bitmapWidth = i_Bitmap.Width;

                byte* p = (byte*)(void*)scanL;
                for (int y = 0; y <= bitmapHeight - 1; y++)
                {
                    for (int x = 0; x <= bitmapWidth - 1; x++)
                    {
                        int B = (int)p[0];
                        int G = (int)p[1];
                        int R = (int)p[2];
                        pixelColor = Color.FromArgb(R, G, B);
                        if (pixelColor != transparentColor && startRegionArea != -1)
                        {
                            graphicsPath.AddRectangle(new Rectangle(startRegionArea, y, (x - 1) - startRegionArea, 1));
                            startRegionArea = -1;
                        }

                        if (pixelColor == transparentColor && startRegionArea == -1)
                        {
                            startRegionArea = x;
                        }

                        p += 3;
                    }

                    if (startRegionArea != -1)
                    {
                        graphicsPath.AddRectangle(new Rectangle(startRegionArea, y, i_Bitmap.Width - startRegionArea, 1));
                        startRegionArea = -1;
                    }

                    p += yOffset;
                }
            }

            i_Bitmap.UnlockBits(bitmapData);
            return graphicsPath;
        }
    }
}
