using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashMaze
{
    public class PlayerMove
    {
        /// <summary>
        /// zmienna określająca położenie X gracza
        /// </summary>
        int playerX;
        /// <summary>
        /// zmienna określająca położenie Y gracza
        /// </summary>
        int playerY;
        /// <summary>
        /// prędkość gracza
        /// </summary>
        static int speed = 5;
        /// <summary>
        /// zmienne potwierdzające wybór kierunku
        /// </summary>
        bool goRight, goLeft, goUp, goDown;
        /// <summary>
        /// konstruktor gracza
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="R"></param>
        /// <param name="L"></param>
        /// <param name="U"></param>
        /// <param name="D"></param>
        public PlayerMove(int pX, int pY, bool R, bool L, bool U, bool D)
        {
            this.playerX = pX;
            this.playerY = pY;
            this.goRight = R;
            this.goLeft = L;
            this.goUp = U;
            this.goDown = D;
        }
        /// <summary>
        /// metoda zwracająca nowe położenie X gracza
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// metoda zwracajaca nowe położenie Y gracza
        /// </summary>
        /// <returns></returns>
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