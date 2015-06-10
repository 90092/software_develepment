using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace 黑羊白羊
{
    [Serializable]
    public class Renderer
    {

        public Renderer()
        {

        }
        
        public Bitmap ResizeImage(Image ImageToResize, int Width, int Height)
        {
            Bitmap bitmap = new Bitmap(Width, Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(ImageToResize, 0, 0, Width, Height);
            }
            return bitmap;
        }
    }
}
