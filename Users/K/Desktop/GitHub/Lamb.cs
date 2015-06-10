using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 黑羊白羊
{
    class Lamb
    {
        //控制移動規則
        //存放變數來判斷Lamb是屬於哪個Player
        Point Location;
        Bitmap Image;
        bool front;
        bool stack;
        int PlayerNumber;
        Rectangle ImageArea { get { return new Rectangle(new Point(Location.X, Location.Y), new Size(Image.Size.Width, Image.Size.Height)); } }
        bool LambArrived;
        Bitmap ImageBig;
        Bitmap ImageSmall;
        public  Map[] Maps;
        Renderer renderer;
        public Lamb(Point Location,Bitmap ImageSmall,Bitmap ImageBig,bool front,int PlayerNumber,Renderer renderer)
        {
            this.Location = Location;
            this.Image = ImageSmall;
            this.front = front;//true 正面  false 反面
            this.PlayerNumber = PlayerNumber;
            this.ImageBig = ImageBig;
            this.ImageSmall = ImageSmall;
            this.renderer = renderer;
            stack = false;
            LambArrived = false;
            SetMapImage();
            SetMaps();
           
        }

        //移動改寫 加入地圖進行判斷

        //在Game判斷Lamb是否堆疊
        //這邊只判斷能否移動到指定地點，實際移動的部份在Game
        //一旦傳回false，則該地點不能移動
        public bool Move(bool PlayerCanMove)
        {
            if (PlayerCanMove == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        public void DrawClickedLambMap(Graphics g)
        {
            for (int i = 0; i < Maps.Length; i++)
            {
                g.DrawImage(Maps[i].GetImage(), Maps[i].GetLocation());

            }
        }

       /* public void LambCanMoveAndMapLight()
        {
            
            if (Maps[0].GetArea().IntersectsWith(ImageArea))
            {
                Maps[1].ChangeLight();
            }
            else if (Maps[1].GetArea().IntersectsWith(ImageArea))
            {
                Maps[0].ChangeLight();

                Maps[2].ChangeLight();

            }
            else if (Maps[2].GetArea().IntersectsWith(ImageArea))
            {
                Maps[1].ChangeLight();
                Maps[3].ChangeLight();
            }
            else if (Maps[3].GetArea().IntersectsWith(ImageArea))
            {
                Maps[2].ChangeLight();
                Maps[4].ChangeLight();
            }
            else if (Maps[4].GetArea().IntersectsWith(ImageArea))
            {
                Maps[3].ChangeLight();
                Maps[5].ChangeLight();
            }
            else if (Maps[5].GetArea().IntersectsWith(ImageArea))
            {
                Maps[4].ChangeLight();
            }
        }*/

        //正反面判斷寫這邊
        public void LambCanMoveAndMapLight()//test用
        {

            if (Maps[0].GetArea().IntersectsWith(ImageArea))
            {
                if (Maps[1].GetHaveLamb() != true)
                {
                    Maps[1].ChangeLight();
                }

            }
            else if (Maps[1].GetArea().IntersectsWith(ImageArea))
            {
                Maps[0].ChangeLight();
                if (Maps[2].GetHaveLamb() != true)
                {
                    Maps[2].ChangeLight();
                }
            }
            else if (Maps[2].GetArea().IntersectsWith(ImageArea))
            {
                if (Maps[1].GetHaveLamb() != true)
                {
                    Maps[1].ChangeLight();
                }
                if (Maps[3].GetHaveLamb() != true)
                {
                    Maps[3].ChangeLight();
                }
            }
            else if (Maps[3].GetArea().IntersectsWith(ImageArea))
            {
                if (Maps[2].GetHaveLamb() != true)
                {
                    Maps[2].ChangeLight();
                }
                if (Maps[4].GetHaveLamb() != true)
                {
                    Maps[4].ChangeLight();
                }
            }
            else if (Maps[4].GetArea().IntersectsWith(ImageArea))
            {
                if (Maps[3].GetHaveLamb() != true)
                {
                    Maps[3].ChangeLight();
                }

                    Maps[5].ChangeLight();
            }
            else if (Maps[5].GetArea().IntersectsWith(ImageArea))
            {
                if (Maps[4].GetHaveLamb() != true)
                {
                    Maps[4].ChangeLight();
                }
            }
        }


        public void MapNoLight()
        {
            for (int i = 0; i < Maps.Length; i++)
            {
                if (Maps[i].GetHaveLight())
                {
                    Maps[i].ChangeLight();
                }
            }
        }
       /* public void MapHaveorNotHaveLamb(int mapcount)
        {
           // if(Maps[count+1].GetArea().IntersectsWith())
        }*/

        private void SetMapImage()
        {
            PictureMap1 = renderer.ResizeImage(Properties.Resources.白羊起點, 122, 534);
            PictureMap2 = renderer.ResizeImage(Properties.Resources.木版1, 159, 305);
            PictureMap3 = renderer.ResizeImage(Properties.Resources.木版2, 149, 308);
            PictureMap4 = renderer.ResizeImage(Properties.Resources.木版3, 161, 302);
            PictureMap5 = renderer.ResizeImage(Properties.Resources.木版4, 155, 308);
            PictureMap6 = renderer.ResizeImage(Properties.Resources.黑羊起點, 108, 538);

            PictureLightMap1 = renderer.ResizeImage(Properties.Resources.白羊起點亮, 122, 534);
            PictureLightMap2 = renderer.ResizeImage(Properties.Resources.木版1亮, 159, 305);
            PictureLightMap3 = renderer.ResizeImage(Properties.Resources.木版2亮, 149, 308);
            PictureLightMap4 = renderer.ResizeImage(Properties.Resources.木版3亮, 161, 302);
            PictureLightMap5 = renderer.ResizeImage(Properties.Resources.木版4亮, 155, 308);
            PictureLightMap6 = renderer.ResizeImage(Properties.Resources.黑羊起點亮, 108, 538);
        }

        Bitmap PictureMap1, PictureMap2, PictureMap3, PictureMap4, PictureMap5, PictureMap6;
        Bitmap PictureLightMap1, PictureLightMap2, PictureLightMap3, PictureLightMap4, PictureLightMap5, PictureLightMap6;
        private void SetMaps()
        {
            Maps = new Map[6];
            Maps[0] = new Map(new Point(1, 2), PictureMap1, PictureLightMap1, true);
            Maps[1] = new Map(new Point(136, 121), PictureMap2, PictureLightMap2, false);
            Maps[2] = new Map(new Point(316, 119), PictureMap3, PictureLightMap3, false);
            Maps[3] = new Map(new Point(484, 119), PictureMap4, PictureLightMap4, false);
            Maps[4] = new Map(new Point(666, 117), PictureMap5, PictureLightMap5, false);
            Maps[5] = new Map(new Point(831, 0), PictureMap6, PictureLightMap6, true);
        }


        public void ArriveEnd()
        {
            if (PlayerNumber == 1)
            {
                if (Maps[5].GetArea().IntersectsWith(ImageArea) && !Maps[4].GetArea().IntersectsWith(ImageArea))
                {
                    LambArrived = true;
                }
            }
            else
            {
                if (Maps[0].GetArea().IntersectsWith(ImageArea) && !Maps[1].GetArea().IntersectsWith(ImageArea))
                {
                    LambArrived = true;
                }
            }
        }


        public void ChangeLambBig()
        {
           Image = ImageBig;
        }
        public void ChangeLambSmall()
        {

            Image = ImageSmall;
        }

        public Point GetLocation()
        {
            return Location;
        }
        public void ChangeLocation(Point location)
        {
            if (LambArrived == false)
            {
                Location = location;
            }
        }
        public bool GetFront()
        {
            return front;
        }

        public int GetPlayerNumber()
        {
            return PlayerNumber;
        }

        public Image GetImage()
        {
            return Image;
        }

        public bool GetStack()
        {
            return stack;
        }

        public void ChangeStack()
        {
            if (LambArrived == false)
            {
                if (stack == false)
                {
                    stack = true;
                }
                else
                {
                    stack = false;
                }
            }
        }
        public Rectangle GetLambArea()
        {
            return ImageArea;
        }

        public bool GetLambArrived()
        {
            return LambArrived;
        }
    }
}
