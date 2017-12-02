using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte process = 0;
        PictureBox previousPicture;
        byte remaining = 8;
        void ResetPictures()
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    (x as PictureBox).Image = Properties.Resources._0;
                }
            }
        }

        void RemovePictures()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Tag = "0";
                }
            }
        }

        void tagSpreader()
        {
            int[] number = new int[16];
            Random random = new Random();

            byte i = 0;
            while(i<16)
            {
                int rand = random.Next(1, 17);
                if(Array.IndexOf(number, rand) == -1)
                {
                    number[i] = rand;
                    i ++;
                }
            }

            for(byte a = 0; a < 16; a++)
            {
                if(number[a] > 8)
                {
                    number[a] -= 8;
                }
            }

            byte b = 0;
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    x.Tag = number[b].ToString();
                    b++;
                }
            }
        }

        void Compare(PictureBox previous, PictureBox next)
        {
            if(previous.Tag.ToString() == next.Tag.ToString())
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                previous.Visible = false;
                next.Visible = false;
                remaining--;
                if (remaining == 0) left.Text = "Congratulations !";
                left.Text = "Remaining - " + remaining;
            }
            else
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                previous.Image = Image.FromFile("0.jpg)");
                next.Image = Image.FromFile("0.jpg");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetPictures();
            RemovePictures();
            tagSpreader();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            PictureBox currentPicture = (sender as PictureBox);
            currentPicture.Image = Image.FromFile((sender as PictureBox).Tag.ToString() + ".jpg");
            if (process == 0)
            {
                previousPicture = currentPicture;
                process++;
            }
            else
            {
                if (previousPicture == currentPicture)
                {
                    MessageBox.Show("This image already selected");
                    process = 0;
                    previousPicture.Image = Image.FromFile("0.jpg");
                }
                else
                {
                    Compare(previousPicture, currentPicture);
                    process = 0;
                }
            }
        }


        void Show()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Image = Image.FromFile(x.Tag.ToString() + ".jpg"); 
                }
            }
        }

        void Hide()
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    (x as PictureBox).Image = Image.FromFile("0.jpg");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Show();
            System.Threading.Thread.Sleep(500);
            Hide();
            process = 0;
        }

        void VisibleAc()
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    (x as PictureBox).Visible = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetPictures();
            RemovePictures();
            tagSpreader();
            VisibleAc();
            remaining = 8;
            process = 0;
        }
    }
}
