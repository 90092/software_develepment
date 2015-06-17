using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace 黑羊白羊
{
    class Map
    {
        Point Location;
        Bitmap Image;
        Bitmap LightImage;
        Bitmap NoLightImage;
        Rectangle ImageArea { get { return new Rectangle(new Point(Location.X, Location.Y), new Size(Image.Size.Width, Image.Size.Height)); } }
        bool HaveLamb;
        bool HaveLight;
        public Lamb lambonthemap;
        public Map(Point location,Bitmap image,Bitmap imagelight,bool HaveLamb)
        {
            this.Image = image;
            this.NoLightImage = image;
            this.Location = location;
            this.LightImage = imagelight;
            this.HaveLamb = HaveLamb;
            HaveLight = false;
        }
        public void ChangeHaveLamb(Lamb lamb)
        {
            if (HaveLamb)
            {
                HaveLamb = false;
                lambonthemap = null;
            }
            else
            {
                HaveLamb = true;
                lambonthemap = lamb;
            }
        }
        public bool GetHaveLamb()
        {
            return HaveLamb;
        }

        public Bitmap GetImage()
        {
            return Image;
        }
        public Point GetLocation()
        {
            return Location;
        }
        public Rectangle GetArea()
        {
            return ImageArea;
        }
        public bool GetHaveLight()
        {
            return HaveLight;
        }

        public void ChangeLight()
        {
            if (HaveLight)
            {
                Image = NoLightImage;
                HaveLight = false;
            }
            else
            {
                Image = LightImage;
                HaveLight = true;
            }
        }
        public void ChangeOnTheMapLamb(Lamb lamb)
        {
            lambonthemap = lamb;
        }
    }
}
