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
    /// <summary>
    /// Poziom 2 gry TrashMaze
    /// </summary>
    public partial class Poziom2 : Form
    {
        /// <summary>
        /// Zmienne potwierdzające wybór kierunku
        /// </summary>
        private bool goLeft, goRight, goUp, goDown;
        /// <summary>
        /// Zmienne zaprzeczające wyborowi kierunku, zabezpieczenie
        /// </summary>
        private bool noLeft, noRight, noUp, noDown;
        /// <summary>
        /// Zmienne okreslające rodzaj odpadu zebranego i aktualnie posiadanego przez gracza
        /// </summary>
        private bool plastic, glass, paper;
        /// <summary>
        /// Zmienna okreslająca czy gra została rozpoczęta
        /// </summary>
        private bool starts = false;
        /// <summary>
        /// Zmienna określająca czy ostatnia szansa przejścia poziomu
        /// </summary>
        private bool lastChance = false;
        /// <summary>
        /// Punkty w postaci nasion
        /// </summary>
        public int pointSeed;
        /// <summary>
        /// Punkty w postaci drzew
        /// </summary>
        public int pointTree;
        /// <summary>
        /// Zmienna przechowująca odpad danego rodzaju odpadów
        /// </summary>
        private string collectedTrash;
        /// <summary>
        /// Ilość odpadów na planszy do zebrania
        /// </summary>
        private int i = 7;
        /// <summary>
        /// Konstruktor timera
        /// </summary>
        Timer t = new Timer();
        /// <summary>
        /// Zmienna określająca ilość minut
        /// </summary>
        private int m = 0;
        /// <summary>
        /// Zmienna okreslająca ilość sekund
        /// </summary>
        private int s = 0;
        /// <summary>
        /// Konstruktor gracza przechowujący jego rozmiar i położenie
        /// </summary>
        Rectangle playerCollison;
        /// <summary>
        /// Zmienna sprawdzająca czy koniec
        /// </summary>
        private bool koniec = false;
        /// <summary>
        /// Zmienna sprawdzająca czy punkty zostały przyznane
        /// </summary>
        private bool pointsGot = false;
        /// <summary>
        /// Zmienna sprawdzajáca czy restart
        /// </summary>
        private bool restart = false;
        /// <summary>
        /// Główne eventy gry
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
            if (restart == true)
            {
                restart = false;
                RestartLevel();
            }
            if (i == 0 && pointsGot == false)
            {
                pointsGot = true;
                pointTree = pointTree + pointSeed;
                pointSeed = pointSeed - pointSeed + 1;
            }
            exitLevel(playerCollison);
        }
        /// <summary>
        /// Naciśnięcie startu
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
        /// Odliczanie czasu gry
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
        /// Działanie przycisków w,a,s,d - poruszanie gracza odblokowane
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
        /// Działanie przycisków w,a,s,d - poruszanie gracza zablokowane
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
        /// Naciśnięcie restaru gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClick_Restart(object sender, MouseEventArgs e)
        {
            t.Stop();
            if (starts == true)
            {
                restart = true;
            }
        }
        /// <summary>
        /// Naciśnięcie intrukcji gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClick_menu(object sender, MouseEventArgs e)
        {
            Form3 menu = new Form3();
            menu.Show();
        }
        /// <summary>
        /// Zmienna globana dostepu do klasy - przekazywanie wartości pomiędzy poziomami
        /// </summary>
        public static Poziom2 instance;

        /// <summary>
        /// Inicjalizacja komponentu - poziom 2
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
        /// Metoda zakończenia gry
        /// </summary>
        /// <param name="y">Konstruktor gracza przechowujacy jego połozenie i wielkość</param>
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
        /// Metoda kolizji ze ścianamu
        /// </summary>
        /// <param name="y">Konstruktor gracza przechowujacy jego połozenie i wielkość</param>
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
        /// Metoda kolizji z odpadami
        /// </summary>
        /// <param name="y">Konstruktor gracza przechowujacy jego połozenie i wielkość</param>
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
        /// Metoda kolizji z śmietnikami
        /// </summary>
        /// <param name="y">Konstruktor gracza przechowujacy jego połozenie i wielkość</param>
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
        /// Metoda restartu
        /// </summary>
        public void RestartLevel()
        {
            goLeft = false;
            goRight = false;
            goUp = false;
            goDown = false;

            plastic = false;
            glass = false;
            paper = false;

            Player.Left = 470;
            Player.Top = 155;

            collectedTrash = "Brak";
            pointSeed = Window.instance.pointSeed;
            pointTree = Window.instance.pointTree;

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
        /// Metoda w razie pzegranej poziomu
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
