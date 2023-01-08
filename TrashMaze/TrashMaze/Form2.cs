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
        /// <summary>
        /// zmienne potwierdzające wybór kierunku
        /// </summary>
        private bool goLeft, goRight, goUp, goDown;
        /// <summary>
        /// zmienne zaprzeczające wyborowi kierunku, zabezpieczenie
        /// </summary>
        private bool noLeft, noRight, noUp, noDown;
        /// <summary>
        /// zmienne okreslające rodzaj odpadu zebranego i aktualnie posiadanego przez gracza
        /// </summary>
        private bool plastic, glass, paper;
        /// <summary>
        /// zmienna okreslająca czy gra została rozpoczęta
        /// </summary>
        private bool starts = false;
        /// <summary>
        /// zmienna określająca czy ostatnia szansa przejścia poziomu
        /// </summary>
        private bool lastChance = false;
        /// <summary>
        /// punkty w postaci nasion
        /// </summary>
        public int pointSeed;
        /// <summary>
        /// punkty w postaci drzew
        /// </summary>
        public int pointTree;
        /// <summary>
        /// zmienna przechowująca odpad danego rodzaju odpadów
        /// </summary>
        private string collectedTrash;
        /// <summary>
        /// ilość odpadów na planszy do zebrania
        /// </summary>
        private int i = 7;
        /// <summary>
        /// konstruktor timera
        /// </summary>
        Timer t = new Timer();
        /// <summary>
        /// zmienna określająca ilość minut
        /// </summary>
        private int m = 0;
        /// <summary>
        /// zmienna okreslająca ilość sekund
        /// </summary>
        private int s = 0;
        /// <summary>
        /// konstruktor gracza przechowujący rozmiar i położenie
        /// </summary>
        Rectangle playerCollison;
        /// <summary>
        /// zmienna sprawdzająca czy koniec
        /// </summary>
        private bool koniec = false;
        /// <summary>
        /// zmienna sprawdzająca czy punkty zostały przyznane
        /// </summary>
        private bool pointsGot = false;
        /// <summary>
        /// główne eventy gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            PlayerMove player = new PlayerMove(Player.Left, Player.Top, goRight, goLeft, goDown, goUp);
            Player.Left = player.playerMovementX();
            Player.Top = player.playerMovementY();
            playerCollison = new Rectangle(Player.Left, Player.Top, Player.Height, Player.Width);
            wallsCollision(playerCollison);
            trashCollision(playerCollison);
            txtCollect.Text = "Zebrano: " + collectedTrash;
            txtTrash.Text = "Śmieci: " + i;
            binCollision(playerCollison);
            if (i == 0 && pointsGot == false)
            {
                pointsGot = true;
                pointTree = pointTree + pointSeed;
                pointSeed = pointSeed - pointSeed + 1;
            }
            exitLevel(playerCollison);
        }
        /// <summary>
        /// naciśnięcie startu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// odliczanie czasu gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                TimeGame.Text = "Przegrałeś!";
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
        /// <summary>
        /// działanie przycisków w,a,s,d - poruszanie gracza odblokowane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// działanie przyciusków w,a,s,d - poruszanie gracza zablokowane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// naciśnięcie restaru gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClick_Restart(object sender, MouseEventArgs e)
        {
            t.Stop();
            if (starts == true)
            {
                RestartLevel();
            }
        }
        /// <summary>
        /// naciśnięcie intrukcji gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClick_menu(object sender, MouseEventArgs e)
        {
            Form3 menu = new Form3();
            menu.Show();
        }
        /// <summary>
        /// zmienna globana dostepu do klasy - przeazywanie wartości pomiędzy poziomami
        /// </summary>
        public static Poziom2 instance;

        /// <summary>
        /// inicjalizacja komponentu - poziom 2
        /// </summary>
        public Poziom2()
        {
            InitializeComponent();
            instance = this;
            pointSeed = Window.instance.pointSeed;
            pointTree = Window.instance.pointTree;
            txtPointTree.Text = "Drzewa: " + pointTree;
            txtPointSeeds.Text = "Nasiona: " + pointSeed;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// zakończenie gry
        /// </summary>
        /// <param name="y"></param>
        private void exitLevel(Rectangle y)
        {
            foreach (Control x in this.Controls)
            {
                Rectangle exit = new Rectangle(x.Left, x.Top, x.Width, x.Height);
                if (i == 0 && x is PictureBox && (string)x.Tag == "exit" && y.IntersectsWith(exit) && koniec==false)
                {
                    koniec = true;
                    t.Stop();
                    starts = false;
                    if (MessageBox.Show("Wygrałeś! Wychodowałeś: "+pointTree+" drzew", "Wygrana", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                    {  
                        Application.Exit();
                    }
                }
            }
        }
        /// <summary>
        /// metoda kolizji ze ścianamu
        /// </summary>
        /// <param name="y"></param>
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
        /// <summary>
        /// metoda kolizji z odpadami
        /// </summary>
        /// <param name="y"></param>
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
        /// <summary>
        /// metoda kolizji z śmietnikami
        /// </summary>
        /// <param name="y"></param>
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
        /// <summary>
        /// metoda restartu
        /// </summary>
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
        /// <summary>
        /// metoda w razie pzegranej poziomu
        /// </summary>
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
