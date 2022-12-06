using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashMaze
{
    public class PlayerMove
    {
        int playerX;
        int playerY;
        float playerWidth;
        float playerHeight;

        static int speed = 5;

        bool goRight, goLeft, goUp, goDown;

        public PlayerMove(int pX, int pY, float pW, float pH, bool R, bool L, bool U, bool D)
        {
            this.playerX = pX;
            this.playerY = pY;
            this.playerWidth = pW;
            this.playerHeight = pH;
            this.goRight = R;
            this.goLeft = L;
            this.goUp = U;
            this.goDown = D;
        }
        public int playerMovementX()
        {
            if (goLeft)
            {
                playerX += speed;
            }
            if (goRight)
            {
                playerX -= speed;
            }
            return playerX;
        }

        public int playerMovementY()
        {
            if (goUp)
            {
                playerY += speed;
            }
            if (goDown)
            {
                playerY -= speed;
            }
            return playerY;
        }

    }
}