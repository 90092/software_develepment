using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace 黑羊白羊
{
    class Game
    {
        private Renderer renderer;
        public Lamb[] Player1Lambs = new Lamb[5];
        public Lamb[] Player2Lambs = new Lamb[5];
        public Player Player1, Player2;
        bool LambMoving;//判斷玩家是否已點擊Lamb
        Lamb ClickedLamb;
        string playerString;
        bool EndGame;
        bool LambCanStack;
        public Game(Random rnd)
        {
            mapcount = 111;
            LambMoving = false;
            LambCanStack = false;
            ClickedLamb = null;
            playerString = "";
            EndGame = false;
            renderer = new Renderer();
            SetImage();
            Random(rnd);
        }

        private void Random(Random rnd)//隨機選擇正反面
        {
            int a = new int();
            for (int i = 0; i < Player1Lambs.Length; i++)
            {
                a = rnd.Next(2);
                if (a == 1)
                {
                    Player1Lambs[i] = new Lamb(new Point(20, 20 + i * 105), White1,WhiteBig1,true,1,renderer);
                }
                else
                {
                    Player1Lambs[i] = new Lamb(new Point(20, 20 + i * 105), White2, WhiteBig2, false, 1, renderer);
                }
            }

            for (int i = 0; i < Player2Lambs.Length; i++)
            {
                a = rnd.Next(2);
                if (a == 1)
                {
                    Player2Lambs[i] = new Lamb(new Point(860, 20 + i * 105), Black1, BlackBig1, true, 2, renderer);
                }
                else
                {
                    Player2Lambs[i] = new Lamb(new Point(860, 20 + i * 105), Black2, BlackBig2, false, 2, renderer);
                }
            }
            a = rnd.Next(2);
            if (a == 0)
            {
                Player1 = new Player(true);
                Player2 = new Player(false);
                playerString = "p1";
            }
            else
            {
                Player1 = new Player(false);
                Player2 = new Player(true);
                playerString = "p2";
            }
        }

        public void LambClick(Point ClickLocation)
        {
            if (Player1.GetPlayerMover())
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Player1Lambs[i].GetLambArea().Contains(ClickLocation) && Player1Lambs[i].GetLambArrived() != true)
                    {
                        LambMoving = true;
                        ClickedLamb = Player1Lambs[i];
                        i = 6;

                    }
                }
            }
            else if (Player2.GetPlayerMover())
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Player2Lambs[i].GetLambArea().Contains(ClickLocation) && Player2Lambs[i].GetLambArrived() != true)
                    {
                        LambMoving = true;
                        ClickedLamb = Player2Lambs[i];
                        i = 6;
                        
                    }
                }
            }
            else
            {
                LambMoving = false;
            }
            if (ClickedLamb != null && LambMoving != false)
            {
                ClickedLamb.ChangeLambBig();

                //將lamb陣列傳入
                ClickedLamb.LambCanMoveAndMapLight();
            }
        }


        int mapcount;
        public void Move(Point ClickLocation, Lamb lamb)//控制遊戲的移動
        {
            LambMoving = false;

            if (lamb.GetLambArea().Contains(ClickLocation))//取消點擊
            {
                lamb.ChangeLambSmall();
                ClickedLamb.MapNoLight();
                ClickedLamb = null;
            }
            else //移動
            {
                Point LambOldLocation = lamb.GetLocation();
                
                if (lamb.Maps[0].GetHaveLight() && lamb.Maps[0].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(25, ClickLocation.Y - 25));
                    //從map1移動到map0
                    MapChangeHaveLamb(1);

                    
                }
                else if (lamb.Maps[1].GetHaveLight() && lamb.Maps[1].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(160, ClickLocation.Y - 25));
                    //從map0移動到map1
                    //從map2移動到map1
                    MapChangeHaveLamb(2);

                    

                    mapcount = 1;
                }
                else if (lamb.Maps[2].GetHaveLight() && lamb.Maps[2].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(340, ClickLocation.Y - 25));
                    //從map1移動到map2
                    //從map3移動到map2
                    MapChangeHaveLamb(1);
                    MapChangeHaveLamb(3);
                    

                    mapcount = 2;
                }
                else if (lamb.Maps[3].GetHaveLight() && lamb.Maps[3].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(510, ClickLocation.Y - 25));
                    MapChangeHaveLamb(2);
                    MapChangeHaveLamb(4);

                    

                    mapcount = 3;
                }
                else if (lamb.Maps[4].GetHaveLight() && lamb.Maps[4].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(685, ClickLocation.Y - 25));
                    MapChangeHaveLamb(3);

                   

                    mapcount = 4;
                }
                else if (lamb.Maps[5].GetHaveLight() && lamb.Maps[5].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(845, ClickLocation.Y - 25));
                    MapChangeHaveLamb(4);

                    
                }



                if (!lamb.GetLocation().Equals(LambOldLocation))
                {
                    //是否要疊起來判斷及疊起來移動寫這邊
                    lamb.ArriveEnd();
                    if(mapcount < 6)
                    {
                      for (int i = 0; i < Player1Lambs.Length; i++)
                      {
                          Player1Lambs[i].Maps[mapcount].ChangeHaveLamb();
                          Player2Lambs[i].Maps[mapcount].ChangeHaveLamb();
                      }
                    }

                    if (lamb.GetLambArrived() == true)
                    {
                        if (lamb.GetPlayerNumber() == 1)
                        {
                            Player1.ArrivedCount();
                        }
                        else
                        {
                            Player2.ArrivedCount();
                        }
                    }

                    if (Player1.WinGame() == true)
                    {
                        playerString = "p1";
                        EndGame = true;
                    }
                    else if (Player2.WinGame() == true)
                    {
                        playerString = "p2";
                        EndGame = true;
                    }
                    else
                    {
                        Player1.ChangeMove();
                        Player2.ChangeMove();
                        if (Player1.GetPlayerMover())
                        {
                            playerString = "p1";
                        }
                        else
                        {
                            playerString = "p2";
                        }
                    }
                }
            }
            lamb.ChangeLambSmall();
            lamb.MapNoLight();
            mapcount = 111;
        }
        bool lambnasi;
        public void MapChangeHaveLamb(int count)
        {








            for (int i = 0; i < Player1Lambs.Length; i++)
            {
                for (int j = 0; j < Player1Lambs.Length; j++)
                {
                    if (Player1Lambs[i].Maps[count].GetArea().IntersectsWith(Player1Lambs[j].GetLambArea()))//判斷傳進來的地圖是否有羊
                    {//如果有羊
                        lambnasi = false;
                        return;
                    }
                    else
                    {
                        lambnasi = true;
                    }
                    if (Player2Lambs[i].Maps[count].GetArea().IntersectsWith(Player2Lambs[j].GetLambArea()))
                    {
                        lambnasi = false;
                        return;
                    }
                    else
                    {
                        lambnasi = true;
                    }
                }
            }
            for (int i = 0; i < Player1Lambs.Length; i++)
            {
                if (lambnasi == true)
                {
                    if (Player2Lambs[i].Maps[count].GetHaveLamb() == true)
                    {
                        Player1Lambs[i].Maps[count].ChangeHaveLamb();
                        Player2Lambs[i].Maps[count].ChangeHaveLamb();
                    }
                }
                else
                {
                    if (Player2Lambs[i].Maps[count].GetHaveLamb() == false)
                    {
                        Player1Lambs[i].Maps[count].ChangeHaveLamb();
                        Player2Lambs[i].Maps[count].ChangeHaveLamb();
                    }
                }
            }
        }




        public void DrawGame(Graphics g)
        {
            //利用有無點選Lamb來判斷該繪製亮或不亮的地圖
            g.DrawImage(AllMap, 0, 0);
            if (ClickedLamb != null)
            {
                ClickedLamb.DrawClickedLambMap(g);
            }
            for (int i = 0; i < Player1Lambs.Length; i++)
            {
                g.DrawImage(Player1Lambs[i].GetImage(), Player1Lambs[i].GetLocation());
                g.DrawImage(Player2Lambs[i].GetImage(), Player2Lambs[i].GetLocation());
            }
            
        }

        Bitmap White1, White2, WhiteBig1, WhiteBig2;
        Bitmap Black1, Black2, BlackBig1, BlackBig2;
        Bitmap AllMap;//整個地圖
       
        private void SetImage()
        {
            White1 = renderer.ResizeImage(Properties.Resources.白正, 70, 70);
            White2 = renderer.ResizeImage(Properties.Resources.白反, 70, 70);
            Black1 = renderer.ResizeImage(Properties.Resources.黑正, 70, 70);
            Black2 = renderer.ResizeImage(Properties.Resources.黑反, 70, 70);

            WhiteBig1 = renderer.ResizeImage(Properties.Resources.白正, 85, 85);
            WhiteBig2 = renderer.ResizeImage(Properties.Resources.白反, 85, 85);
            BlackBig1 = renderer.ResizeImage(Properties.Resources.黑正, 85, 85);
            BlackBig2 = renderer.ResizeImage(Properties.Resources.黑反, 85, 85);

            

            AllMap = renderer.ResizeImage(Properties.Resources.地圖,938,536);
        }

        

        public Lamb GetClickedLamb()
        {
            return ClickedLamb;
        }
        public bool GetLambMoving()
        {
            return LambMoving;
        }
        public string GetplayerString()
        {
            return playerString;
        }
        public bool GetEndGame()
        {
            return EndGame;
        }
    }
}
