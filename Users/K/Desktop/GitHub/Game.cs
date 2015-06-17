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
        bool lambstack;
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

                    mapcount = 0;
                    
                }
                else if (lamb.Maps[1].GetHaveLight() && lamb.Maps[1].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(160, ClickLocation.Y - 25));

                    

                    mapcount = 1;
                }
                else if (lamb.Maps[2].GetHaveLight() && lamb.Maps[2].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(340, ClickLocation.Y - 25));
                    

                    mapcount = 2;
                }
                else if (lamb.Maps[3].GetHaveLight() && lamb.Maps[3].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(510, ClickLocation.Y - 25));

                    

                    mapcount = 3;
                }
                else if (lamb.Maps[4].GetHaveLight() && lamb.Maps[4].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(685, ClickLocation.Y - 25));

                   

                    mapcount = 4;
                }
                else if (lamb.Maps[5].GetHaveLight() && lamb.Maps[5].GetArea().Contains(ClickLocation))
                {
                    lamb.ChangeLocation(new Point(845, ClickLocation.Y - 25));

                    mapcount = 5;
                }



                if (!lamb.GetLocation().Equals(LambOldLocation))//不相等=>有移動
                {                                               //相等=>沒移動
                    
                    
                    if(mapcount < 5 && mapcount > 0)
                    {
                      for (int i = 0; i < Player1Lambs.Length; i++)
                      {
                          Player1Lambs[i].Maps[mapcount].ChangeHaveLamb(lamb);
                          Player2Lambs[i].Maps[mapcount].ChangeHaveLamb(lamb);
                      }
                    }


                    for (int i = 0; i < Player1Lambs.Length; i++)//堆疊
                    {
                        if (Player1Lambs[i].GetLocation().X == lamb.GetLocation().X && lamb.GetLocation().X > 100 && lamb.GetLocation().X < 800 && !Player1Lambs[i].Equals(lamb) /*&& Player1Lambs[i].GetStack() != true*/)
                        {
                           if (Player1Lambs[i].GetFront() != lamb.GetFront() && Player1Lambs[i].GetStack() != true)
                           {
                               Player1Lambs[i].ChangeStack();
                               lambstack = true;
                           }
                           else if (Player1Lambs[i].GetStack() == true && !Player2Lambs[i].Equals(lamb) && Player1Lambs[i].GetLocation().X != LambOldLocation.X)
                           {
                               if (lamb.GetStack() != true)
                               {
                                   lambstack = true;
                               }
                           }
                        }
                        if (Player2Lambs[i].GetLocation().X == lamb.GetLocation().X && lamb.GetLocation().X > 100 && lamb.GetLocation().X < 800 && !Player2Lambs[i].Equals(lamb) /*&& Player2Lambs[i].GetStack() != true*/)
                        {
                            
                          if (Player2Lambs[i].GetFront() != lamb.GetFront() && Player2Lambs[i].GetStack() != true)
                          {
                              Player2Lambs[i].ChangeStack();
                              lambstack = true;
                          }
                          else if (Player2Lambs[i].GetStack() == true && !Player1Lambs[i].Equals(lamb) && Player2Lambs[i].GetLocation().X != LambOldLocation.X)
                          {
                              if (lamb.GetStack() != true)
                              {
                                  lambstack = true;
                              }
                          }
                        }
                    }
                    if (lambstack)
                    {
                        lamb.ChangeStack();
                    }
                    
                    for (int i = 0; i < Player1Lambs.Length; i++)//堆疊移動
                    {   //用mapcount判斷player剛剛點選的lamb移到哪個地圖
                        //在判斷是否有堆疊，有的話就一起移到那個地圖
                        if (Player1Lambs[i].GetLocation().X == LambOldLocation.X && Player1Lambs[i].GetStack() == true)
                        {
                            if (mapcount == 0)
                            {//lamb.ChangeLocation(new Point(25, ClickLocation.Y - 25));
                                Player1Lambs[i].ChangeLocation(new Point(25, i * 25));
                            }
                            else if (mapcount == 1)
                            {
                                Player1Lambs[i].ChangeLocation(new Point(160, 100 + i * 15));
                            }
                            else if (mapcount == 2)
                            {
                                Player1Lambs[i].ChangeLocation(new Point(340, 100 + i * 15));
                            }
                            else if (mapcount == 3)
                            {
                                Player1Lambs[i].ChangeLocation(new Point(510, 100 + i * 15));
                            }
                            else if (mapcount == 4)
                            {
                                Player1Lambs[i].ChangeLocation(new Point(685, 100 + i * 15));
                            }
                            else if (mapcount == 5)
                            {
                                Player1Lambs[i].ChangeLocation(new Point(845, i * 25));
                            }
                        }
                        if (Player2Lambs[i].GetLocation().X == LambOldLocation.X && Player2Lambs[i].GetStack())
                        {
                            if (mapcount == 0)
                            {//lamb.ChangeLocation(new Point(25, ClickLocation.Y - 25));
                                Player2Lambs[i].ChangeLocation(new Point(25, i * 35));
                            }
                            else if (mapcount == 1)
                            {
                                Player2Lambs[i].ChangeLocation(new Point(160, 120 + i * 15));
                            }
                            else if (mapcount == 2)
                            {
                                Player2Lambs[i].ChangeLocation(new Point(340, 120 + i * 15));
                            }
                            else if (mapcount == 3)
                            {
                                Player2Lambs[i].ChangeLocation(new Point(510, 120 + i * 15));
                            }
                            else if (mapcount == 4)
                            {
                                Player2Lambs[i].ChangeLocation(new Point(685, 120 + i * 15));
                            }
                            else if (mapcount == 5)
                            {
                                Player2Lambs[i].ChangeLocation(new Point(845, i * 35));
                            }
                        }

                    }





                    for (int i = 1; i <= 4; i++)//lamb如果離開map 則將HaveLamb變數調整
                    {
                        MapChangeHaveLamb(i, lamb);
                    }


                    for (int i = 0; i < Player1Lambs.Length; i++)//判斷lamb是否抵達終點
                    {
                        Player1Lambs[i].ArriveEnd();
                        Player2Lambs[i].ArriveEnd();
                        if (Player1Lambs[i].GetLambArrived() == true && Player1Lambs[i].GetLambArriveENDcounted() != true)
                        {
                            Player1.ArrivedCount();
                            Player1Lambs[i].ChangeLambArriveENDcounted();
                        }
                        if (Player2Lambs[i].GetLambArrived() == true && Player2Lambs[i].GetLambArriveENDcounted() != true)
                        {
                            Player2.ArrivedCount();
                            Player2Lambs[i].ChangeLambArriveENDcounted();
                        }
                    }


                    if (mapcount >= 1 && mapcount <= 4)
                    {
                        for (int i = 0; i < Player1Lambs.Length; i++)
                        {
                            Player1Lambs[i].Maps[mapcount].ChangeOnTheMapLamb(lamb);
                            Player2Lambs[i].Maps[mapcount].ChangeOnTheMapLamb(lamb);
                        }
                    }


                    if (Player1.WinGame() == true)//p1勝利
                    {
                        playerString = "p1";
                        EndGame = true;
                    }
                    else if (Player2.WinGame() == true)//p2勝利
                    {
                        playerString = "p2";
                        EndGame = true;
                    }
                    else//二者皆沒贏，繼續遊戲
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
            lambstack = false;
        }
        bool lambnasi;
        public void MapChangeHaveLamb(int count,Lamb lamb)
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
                        Player1Lambs[i].Maps[count].ChangeHaveLamb(null);
                        Player2Lambs[i].Maps[count].ChangeHaveLamb(null);
                    }
                    else if (Player1Lambs[i].Maps[count].GetHaveLamb() == true)
                    {
                        Player1Lambs[i].Maps[count].ChangeHaveLamb(null);
                        Player2Lambs[i].Maps[count].ChangeHaveLamb(null);
                    }
                }
                else
                {
                    if (Player2Lambs[i].Maps[count].GetHaveLamb() == false)
                    {
                        Player1Lambs[i].Maps[count].ChangeHaveLamb(lamb);
                        Player2Lambs[i].Maps[count].ChangeHaveLamb(lamb);
                    }
                    else if (Player1Lambs[i].Maps[count].GetHaveLamb() == false)
                    {
                        Player1Lambs[i].Maps[count].ChangeHaveLamb(lamb);
                        Player2Lambs[i].Maps[count].ChangeHaveLamb(lamb);
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

        public Lamb[] testplayer1Lamb()
        {
            return Player1Lambs;
        }
        public Lamb[] testplayer2Lamb()
        {
            return Player2Lambs;
        }
    }
}
