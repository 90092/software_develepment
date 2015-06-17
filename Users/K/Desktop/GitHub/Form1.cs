using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 黑羊白羊
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();
        
        private Game game;
        public Form1()
        {
            InitializeComponent();
            renderer = new Renderer();
            SetImage();
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game = new Game(rnd);
            Drawtimer.Enabled = true;
            LabelShow();
            label3.Visible = false;
            button1.Visible = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (game != null)
            {
                game.DrawGame(e.Graphics);
            }
            else
            {
                e.Graphics.DrawImage(GameStartPicture,0,0);
            }
        }
        private Renderer renderer;
        Bitmap GameStartPicture;
        private void SetImage()
        {
            GameStartPicture = renderer.ResizeImage(Properties.Resources.開始畫面, 938, 536);
        }


        

        private void Drawtimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (game != null)
            {
                if (game.GetLambMoving() == true)
                {

                    game.Move(new Point(e.X, e.Y), game.GetClickedLamb());
                    if (game.GetEndGame())
                    {
                        if (game.GetplayerString().Equals("p1"))
                        {
                            label3.Text = "遊戲結束，白方勝";
                        }
                        else if (game.GetplayerString().Equals("p2"))
                        {
                            label3.Text = "遊戲結束，黑方勝";
                        }
                        label3.Visible = true;
                        label1.Visible = false;
                        label2.Visible = false;
                        button1.Visible = true;
                    }
                    else
                    {
                        LabelShow();
                       /* 
                        label4.Visible = true;
                        label5.Visible = true;
                        label4.Text = "player1" + Environment.NewLine + "lamb1 stack:" + game.testplayer1Lamb()[0].GetStack().ToString() + Environment.NewLine + "lamb2 stack:" + game.testplayer1Lamb()[1].GetStack().ToString() + Environment.NewLine + "lamb3 stack:" + game.testplayer1Lamb()[2].GetStack().ToString() + Environment.NewLine + "lamb4 stack:" + game.testplayer1Lamb()[3].GetStack().ToString() + Environment.NewLine + "lamb5 stack:" + game.testplayer1Lamb()[4].GetStack().ToString();
                        label5.Text = "player2" + Environment.NewLine + "lamb1 stack:" + game.testplayer2Lamb()[0].GetStack().ToString() + Environment.NewLine + "lamb2 stack:" + game.testplayer2Lamb()[1].GetStack().ToString() + Environment.NewLine + "lamb3 stack:" + game.testplayer2Lamb()[2].GetStack().ToString() + Environment.NewLine + "lamb4 stack:" + game.testplayer2Lamb()[3].GetStack().ToString() + Environment.NewLine + "lamb5 stack:" + game.testplayer2Lamb()[4].GetStack().ToString();
                    
                        */
                    }
                }
                else
                {
                    game.LambClick(new Point(e.X, e.Y));
                }
            }
        }
        private void LabelShow()
        {
            if (game.GetplayerString().Equals("p1"))
            {
                label1.Visible = true;
                label2.Visible = false;
            }
            else if (game.GetplayerString().Equals("p2"))
            {
                label2.Visible = true;
                label1.Visible = false;
            }
        }
    }
}
