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
    /// Klasa instrukcji gry TrashMaze
    /// </summary>
    public partial class Form3 : Form
    {
        /// <summary>
        /// Inicjalizacja komponentu
        /// </summary>
        public Form3()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Przycisk do kontynuuowania gry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
