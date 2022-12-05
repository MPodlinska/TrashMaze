﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrashMaze
{
    public partial class Window : Form
    {
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        bool plastic, glass, paper;
        int playerSpeed = 5;

        Rectangle playerCollison;

        public Window()
        {
            InitializeComponent();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D && noLeft == false)
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;
                goLeft = true;
            }
            if (e.KeyCode == Keys.A && noRight == false)
            {
                goLeft = goUp = goDown = false;
                noLeft = noUp = noDown = false;
                goRight = true;
            }
            if (e.KeyCode == Keys.W && noUp == false)
            {
                goRight = goLeft = goDown = false;
                noRight = noLeft = noDown = false;
                goUp = true;
            }
            if (e.KeyCode == Keys.S && noDown == false)
            {
                goRight = goUp = goLeft = false;
                noRight = noUp = noLeft = false;
                goDown = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.A)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.W)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.S)
            {
                goDown = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            playerMovement();
            playerCollison = new Rectangle(Player.Left, Player.Top, Player.Height, Player.Width);
            wallsCollision(playerCollison);
            trashCollision(playerCollison);
            binCollision(playerCollison);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox85_Click(object sender, EventArgs e)
        {

        }
        private void playerMovement()
        {
            if (goLeft)
            {
                Player.Left += playerSpeed;
            }
            if (goRight)
            {
                Player.Left -= playerSpeed;
            }
            if (goUp)
            {
                Player.Top -= playerSpeed;
            }
            if (goDown)
            {
                Player.Top += playerSpeed;
            }
        }
        private void wallsCollision(Rectangle y)
        {
            foreach (Control x in this.Controls)
            {
                Rectangle wallHit = new Rectangle(x.Left, x.Top, x.Width, x.Height);
                if (x is PictureBox && (string)x.Tag == "wall")
                {
                    if (goLeft && y.IntersectsWith(wallHit))
                    {
                        Player.Left -= 4;
                        noLeft = true;
                        goLeft = false;
                    }
                    if (goRight && y.IntersectsWith(wallHit))
                    {
                        Player.Left += 4;
                        noRight = true;
                        goRight = false;
                    }
                    if (goUp && y.IntersectsWith(wallHit))
                    {
                        Player.Top += 4;
                        noUp = true;
                        goUp = false;
                    }
                    if (goDown && y.IntersectsWith(wallHit))
                    {
                        Player.Top -= 4;
                        noDown = true;
                        goDown = false;
                    }
                }
            }
        }
        private void trashCollision(Rectangle y)
        {
            foreach (Control x in this.Controls)
            {
                Rectangle trashHit = new Rectangle(x.Left, x.Top, x.Width, x.Height);
                if (y.IntersectsWith(trashHit) && plastic == false && glass == false && paper == false)
                {
                    if (x is PictureBox && (string)x.Tag == "plastic")
                    {
                        x.Visible = false;
                        plastic = true;
                    }
                    if (x is PictureBox && (string)x.Tag == "glass")
                    {
                        x.Visible = false;
                        glass = true;
                    }
                    if (x is PictureBox && (string)x.Tag == "paper")
                    {
                        x.Visible = false;
                        paper = true;
                    }
                }
            }
        }
        private void binCollision(Rectangle y)
        {
            foreach (Control x in this.Controls)
            {
                Rectangle binHit = new Rectangle(x.Left, x.Top, x.Width, x.Height);
                if (x is PictureBox && y.IntersectsWith(binHit))
                {
                    if ((string)x.Tag == "paperBin")
                    {
                        paper = false;
                    }
                    if ((string)x.Tag == "glassBin")
                    {
                        glass = false;
                    }
                    if ((string)x.Tag == "plasticBin")
                    {
                        plastic = false;
                    }
                }
            }
        }
        private void RestartLevel()
        {
            goLeft = false;
            goRight = false;
            goUp = false;
            goDown = false;

            Player.Left = 68;
            Player.Top = 66;
        }
        private void GameOver(string message)
        {
            MessageBox.Show(message, "TrashMaze Info");
        }
    }
}
