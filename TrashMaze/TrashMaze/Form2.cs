using System;
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
    public partial class Poziom2 : Form
    {
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        bool plastic, glass, paper;

        bool starts = false;
        bool lastChance = false;

        int pointSeed = 1;
        int pointTree = 1;
        string collectedTrash;

        int i = 7;
        Timer t = new Timer();
        int m = 0;

        int s = 0;

        Rectangle playerCollison;

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            PlayerMove player = new PlayerMove(Player.Left, Player.Top, Player.Width, Player.Height, goRight, goLeft, goDown, goUp);
            Player.Left = player.playerMovementX();
            Player.Top = player.playerMovementY();
            playerCollison = new Rectangle(Player.Left, Player.Top, Player.Height, Player.Width);
            wallsCollision(playerCollison);
            trashCollision(playerCollison);
            txtCollect.Text = "Zebrano: " + collectedTrash;
            txtTrash.Text = "Śmieci: " + i;
            binCollision(playerCollison);
            exitLevel(playerCollison);
        }

        private void startClick(object sender, EventArgs e)
        {
            t.Stop();
            if (starts == false)
            {
                starts = true;
                m = 1;
                s = 20;
                t.Interval = 1000;
                t.Tick += new EventHandler(this.timer_Tick);
                t.Start();
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {

            string timer = m + ":" + s;
            s--;
            if (i > 0)
            {
                if (m == 0 && s == 0 && pointTree > 0 && starts == true)
                {
                    m = 0;
                    s = 20;
                    pointTree--;
                    lastChance = true;
                }
                if (s == 0 && m > 0)
                {
                    s = 60;
                    m--;
                }
            }
            if (lastChance == true && i > 0 && m == 0 && s == 0)
            {
                TimeGame.Text = "Przegrałeś! Restart?";
                t.Stop();
                lose();
            }
            else
            {
                timer = m + ":" + s;
                TimeGame.Text = timer;
            }
            txtPointTree.Text = "Drzewa: " + pointTree;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (starts == true)
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

        private void MouseClick_Restart(object sender, MouseEventArgs e)
        {
            t.Stop();
            if (starts == true)
            {
                RestartLevel();
            }
        }

        private void MouseClick_menu(object sender, MouseEventArgs e)
        {

        }

        public Poziom2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void exitLevel(Rectangle y)
        {
            foreach (Control x in this.Controls)
            {
                Rectangle exit = new Rectangle(x.Left, x.Top, x.Width, x.Height);
                if (i == 0 && x is PictureBox && (string)x.Tag == "exit" && y.IntersectsWith(exit))
                {
                    i = 1;
                    t.Stop();
                    starts = false;
                    if (MessageBox.Show("Wygrałeś! Wychodowałeś: drzew", "Wygrana", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                    {  
                        Application.Exit();
                    }
                }
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
            Trash pickTrash = new Trash();
            foreach (Control x in this.Controls)
            {
                Rectangle trashHit = new Rectangle(x.Left, x.Top, x.Width, x.Height);
                if (y.IntersectsWith(trashHit) && plastic == false && glass == false && paper == false)
                {
                    if (x is PictureBox && (string)x.Tag == "plastic" && x.Visible==true)
                    {
                        x.Visible = false;
                        plastic = true;
                        collectedTrash = pickTrash.plastic(i);
                    }
                    if (x is PictureBox && (string)x.Tag == "glass" && x.Visible == true)
                    {
                        x.Visible = false;
                        glass = true;
                        collectedTrash = pickTrash.glass(i);
                    }
                    if (x is PictureBox && (string)x.Tag == "paper" && x.Visible == true)
                    {
                        x.Visible = false;
                        paper = true;
                        collectedTrash = pickTrash.papers(i);
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
                    if ((string)x.Tag == "paperBin" && paper == true)
                    {
                        paper = false;
                        collectedTrash = "Brak";
                        i--;
                    }
                    if ((string)x.Tag == "glassBin" && glass == true)
                    {
                        glass = false;
                        collectedTrash = "Brak";
                        i--;
                    }
                    if ((string)x.Tag == "plasticBin" && plastic == true)
                    {
                        plastic = false;
                        collectedTrash = "Brak";
                        i--;
                    }
                }
            }
        }
        public void RestartLevel()
        {
            goLeft = false;
            goRight = false;
            goUp = false;
            goDown = false;

            Player.Left = 470;
            Player.Top = 155;

            collectedTrash = "Brak";
            pointSeed = 1;
            pointTree = 2;

            i = 7;

            txtCollect.Text = "Zebrano: " + collectedTrash;
            txtPointSeeds.Text = "Nasiona: " + pointSeed;
            txtPointTree.Text = "Drzewa: " + pointTree;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }
            m = 1;
            s = 21;
            t.Start();
        }
        private void lose()
        {
            for (int j = 1; j > 0; j--)
            {
                if (MessageBox.Show("Przegrałeś! Zacznij jeszcze raz i kliknij tak, lub zakończ i kliknij nie!", "Przegrana", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RestartLevel();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
