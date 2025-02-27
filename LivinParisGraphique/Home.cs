using System;
using System.Windows.Forms;

namespace LivinParisGraphique
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($@"Hello {textBox1.Text}");
        }

        private void Home_Load(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}