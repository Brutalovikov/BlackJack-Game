using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace MyBlackJack
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        public string Shvisteger = "";
        public string money = "";
        public string soundfile = @"D:\Study\7 sem\ППП\MyBlackJack\MyBlackJack\music\gramatik-hit-that-jive-original-mix-smotra-fm (online-audio-converter.com).wav";
        SoundPlayer sound;

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {
           // if (e.KeyCode == Keys.F1)
             //   DialogResult = DialogResult.OK;
        }

		private void FormLogin_Load(object sender, EventArgs e)
		{
			button1.BackColor = Color.Black;
            sound = new SoundPlayer(soundfile);
            sound.PlayLooping();
            this.ControlBox = false;
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
           // DialogResult = DialogResult.OK;
            if (Shvisteger != string.Empty)
            {
                sound.Stop();
                MainForm mf = new MainForm();
                mf.Owner = this;
                mf.Show();
                this.Hide();
            }
            else MessageBox.Show("Перед входом в казино \nнапомните своё имя, джентльмен.", "Ahtung!");
		}

		private void button1_MouseHover(object sender, EventArgs e)
		{
			button1.BackColor = Color.Black;
		}

		private void button1_MouseLeave(object sender, EventArgs e)
		{
			button1.BackColor = Color.Black;
		}

        private void button4_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.Owner = this;
            reg.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Logining log = new Logining();
            log.Owner = this;
            log.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
