using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bioPanel.BackColor = Color.FromArgb(0, Color.Black);
            tempPanel.BackColor = Color.FromArgb(0, Color.Black);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files (*.jpg)|*.jpg|PNG files (*.png)|*.png|All files (*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    InputImage.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler untuk event Click
        private void KMP_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tombol KMP ditekan!"); // Nanti lo masukin pemrosesan KMP di sini
        }

        // Event handler untuk event Click
        private void BM_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tombol BM ditekan!"); // Nanti lo masukin pemrosesan BM di sini
        }
    }
}
