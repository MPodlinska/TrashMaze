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
    public partial class Form2 : Form
    {
        public static Form2 menu;
        Window level1 = new Window();
        public Form2()
        {
            InitializeComponent();
            menu = this;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void MouseClick_start(object sender, MouseEventArgs e)
        {
            level1.Show();
            menu.Hide();
        }

        private void MouseClick_end(object sender, MouseEventArgs e)
        {
            level1.Close();
            menu.Close();
        }
    }
}
