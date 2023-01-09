using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashMaze
{
    /// <summary>
    /// Klasa opisująca ruch gracza
    /// </summary>
    public class PlayerMove
    {
        /// <summary>
        /// Zmienna określająca położenie X gracza
        /// </summary>
        int playerX;
        /// <summary>
        /// Zmienna określająca położenie Y gracza
        /// </summary>
        int playerY;
        /// <summary>
        /// Prędkość gracza
        /// </summary>
        static int speed = 5;
        /// <summary>
        /// Zmienne potwierdzające wybór kierunku
        /// </summary>
        bool goRight, goLeft, goUp, goDown;
        /// <summary>
        /// Konstruktor gracza
        /// </summary>
        /// <param name="pX">Zmienna przechowująca połozenie X gracza</param>
        /// <param name="pY">Zmienna przechowująca połozenie Y gracza</param>
        /// <param name="R">Zmienna przechowująca czy ruch w prawo</param>
        /// <param name="L">Zmienna przechowująca czy ruch w lewo</param>
        /// <param name="U">Zmienna przechowująca czy ruch w górę</param>
        /// <param name="D">Zmienna przechowująca czy ruch w dół</param>
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
        /// Metoda zwracająca nowe położenie X gracza
        /// </summary>
        /// <returns>Zwraca położenie X gracza </returns>
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
        /// Metoda zwracajaca nowe położenie Y gracza
        /// </summary>
        /// <returns>Zwraca połozenie Y gracza</returns>
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