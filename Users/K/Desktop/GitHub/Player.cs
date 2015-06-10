using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 黑羊白羊
{
    class Player
    {
        //控制是否輪到該玩家移動及勝負
        bool PlayerMove;
        int EndCount = 0;//記錄幾隻Lamb到達終點
        public Player(bool PlayerMove)
        {
            this.PlayerMove = PlayerMove;
        }

        public void ArrivedCount()
        {
            EndCount++;
        }

        public bool WinGame()
        {
            if (EndCount == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetPlayerMover()
        {
            return PlayerMove;
        }

        public void ChangeMove()
        {
            if (PlayerMove == true)
            {
                PlayerMove = false;
            }
            else
            {
                PlayerMove = true;
            }
        }

    }
}
