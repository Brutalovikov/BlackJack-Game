using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Data;
using System.Data.OleDb;

namespace MyBlackJack
{
    public partial class MainForm : Form
	{
        public string Cash = "";
        public string Login = "";
        public string UserBet = "";
        public double DoubleBet;
        public bool dab = true;
        public string soundfile = @"D:\Study\7 sem\ППП\MyBlackJack\MyBlackJack\music\Genesis – I Cant Dance (online-audio-converter.com).wav";
        SoundPlayer sound;
		GameController game;

        //Инициализация
        public void StartGame()
        {
            game = new GameController();
            game.Start();

            fillListBoxes();
        }

        //Спискок для img
        List<PictureBox> playerList, dealerList;

        void fillListBoxes()
        {
            int width = 86,
                height = 120,
                space = 20;

            //PLAYER

            if (playerList != null)
                foreach (PictureBox aa in playerList)
                    Controls.Remove(aa);

            playerList = new List<PictureBox>();

            for (int i = 0; i < game.h.Count; i++)
            {
                PictureBox pb = new PictureBox();
                playerList.Add(pb);
                pb.Image = GetCardImage(game.h[i]);
				pb.Width = width;
                pb.Height = height;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Left = 350 + i * (width + space);
                pb.Top = 350;
                Controls.Add(pb);
            }

            //DEALER

            if (dealerList != null)
                foreach (PictureBox pb in dealerList)
                    Controls.Remove(pb);

            dealerList = new List<PictureBox>();
            for (int i = 0; i < game.dealer.Count; i++)  
			{
                PictureBox pb = new PictureBox();
                playerList.Add(pb);
                pb.Image = GetCardImage(game.dealer[i]);
                pb.Width = width;
                pb.Height = height;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Left = 400 + i * (width + space);
                pb.Top = 50;
                Controls.Add(pb);
            }
        }

        Image GetCardImage(Card crd)
        {
            string num = "", mast = "";

            switch (crd.value)
            {
                case 0: num = "A"; break;
                case 2: num = "2"; break;
                case 3: num = "3"; break;
                case 4: num = "4"; break;
                case 5: num = "5"; break;
                case 6: num = "6"; break;
                case 7: num = "7"; break;
                case 8: num = "8"; break;
                case 9: num = "9"; break;
                case 10: num = "10"; break;
                case 11: num = "J"; break;
                case 12: num = "Q"; break;
                case 13: num = "K"; break;
            }
            switch (crd.colour)
            {
                case 0: mast = "c"; break;
                case 1: mast = "s"; break;
                case 2: mast = "h"; break;
                case 3: mast = "d"; break;
            }

            return Image.FromFile("images/" + num + mast + ".jpg");
        }

        public MainForm()
        {
            InitializeComponent();
        }

        void Button3Click(object sender, EventArgs e)
        {
            StartGame();
            label1.Text = UserBet + " $";
            button2.Enabled = true;
            button1.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button3.Enabled = false;
            if (game.Finish() == 21)
            {
                fillListBoxes();
                string result = "";
                int score = game.Finish(), scoreDealer = game.FinishDealer();
                double back = double.Parse(Cash) + ((double.Parse(UserBet)) * 1.5) + double.Parse(UserBet);
                result = "Выигрыш";
                using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
                {
                    baza.Open();
                    OleDbCommand VhodVReihstag = baza.CreateCommand();
                    VhodVReihstag.CommandText = "UPDATE Userss SET Score ='" + back + "' WHERE login ='" + Login + "'";
                    OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                    Cash = Convert.ToString(back);
                    baza.Close();
                }
                MessageBox.Show("Ваши очки: " + game.Finish() + "\nОчки дилера:" + game.FinishDealer() + "\nРезультат: " + result + "\nПоздравляем! Вот Ваш выигрыш: " + (((double.Parse(UserBet)) * 1.5) + double.Parse(UserBet)));
                button6.Enabled = true;
                button5.Enabled = true;
                button2.Enabled = false;
                button1.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button3.Enabled = false;
            }
        }

        void GetDealerCard()
        {
            if (game.FinishDealer() < 17)
                game.getCardDealer();
        }

        // Stand
        void Button2Click(object sender, EventArgs e)
        {
            while (game.FinishDealer() < 17) 
            {
                if(game.Finish() != 21)
                    GetDealerCard();
            }  
            fillListBoxes();
            double back = 0;
            string result = "";
            int score = game.Finish(), scoreDealer = game.FinishDealer();
            if ((score > 21 && scoreDealer > 21) || (score == scoreDealer))
            {
                back = double.Parse(Cash) + double.Parse(UserBet);
                result = "Ничья";
                using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
                {
                    baza.Open();
                    OleDbCommand VhodVReihstag = baza.CreateCommand();
                    VhodVReihstag.CommandText = "UPDATE Userss SET Score ='" + back + "' WHERE login ='" + Login + "'";
                    OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                    Cash = Convert.ToString(back);
                    baza.Close();
                }
                MessageBox.Show("Ваши очки: " + game.Finish() + "\nОчки дилера:" + game.FinishDealer() + "\nРезультат: " + result + "\nВаша ставка возвращается к Вам.");
            }
            else
            {
                if (score > 21 || ((score < scoreDealer) && scoreDealer <= 21))
                {
                    result = "Проигрыш";
                    MessageBox.Show("Ваши очки: " + game.Finish() + "\nОчки дилера:" + game.FinishDealer() + "\nРезультат: " + result);
                }
                else
                {
                    back = double.Parse(Cash) + ((double.Parse(UserBet)) * 2.0); 
                    result = "Выигрыш";
                    using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
                    {
                        baza.Open();
                        OleDbCommand VhodVReihstag = baza.CreateCommand();
                        VhodVReihstag.CommandText = "UPDATE Userss SET Score ='" + back + "' WHERE login ='" + Login + "'";
                        OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                        Cash = Convert.ToString(back);
                        baza.Close();
                    }
                    MessageBox.Show("Ваши очки: " + game.Finish() + "\nОчки дилера:" + game.FinishDealer() + "\nРезультат: " + result + "\nПоздравляем! Вот Ваш выигрыш: " + (double.Parse(UserBet) * 2.0));
                }
            }
            button6.Enabled = true;
            button5.Enabled = true;
            button2.Enabled = false;
            button1.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button3.Enabled = false;
            back = 0;
                  }

        //Hit
        void Button1Click(object sender, EventArgs e)
        {
            game.Turn();
            GetDealerCard();
            button7.Enabled = false;
            button8.Enabled = false;
            fillListBoxes();
            if (dab == false) button1.Enabled = false;
            if(game.Finish() > 21)
            {
                while (game.FinishDealer() < 17) 
                        GetDealerCard();
                fillListBoxes();
                double back = 0;
                string result = "";
                int score = game.Finish(), scoreDealer = game.FinishDealer();
                if ((score > 21 && scoreDealer > 21) || (score == scoreDealer))
                {
                    back = double.Parse(Cash) + double.Parse(UserBet); 
                    result = "Ничья";
                    using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
                    {
                        baza.Open();
                        OleDbCommand VhodVReihstag = baza.CreateCommand();
                        VhodVReihstag.CommandText = "UPDATE Userss SET Score ='" + back + "' WHERE login ='" + Login + "'";
                        OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                        Cash = Convert.ToString(back);
                        baza.Close();
                    }
                    MessageBox.Show("Ваши очки: " + game.Finish() + "\nОчки дилера:" + game.FinishDealer() + "\nРезультат: " + result + "\nВаша ставка возвращается к Вам.");
                }
                else
                {
                    if (score > 21 || ((score < scoreDealer) && scoreDealer <= 21))
                    {
                        result = "Проигрыш";
                        MessageBox.Show("Ваши очки: " + game.Finish() + "\nОчки дилера:" + game.FinishDealer() + "\nРезультат: " + result);
                    }
                }
                button6.Enabled = true;
                button5.Enabled = true;
                button2.Enabled = false;
                button1.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button3.Enabled = false;
            }
        }

        void Button5Click(object sender, EventArgs e)
        {
            sound.Stop();
            this.Owner.Close();
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            sound = new SoundPlayer(soundfile);
            sound.PlayLooping();
            FormLogin fl = this.Owner as FormLogin;
            Login = fl.Shvisteger;
            Cash = fl.money;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bets bets = new Bets();
            bets.Owner = this;
            bets.Show();
            button3.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = false;
            dab = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(Cash) >= (double.Parse(UserBet) * 2.0))
            {
                DoubleBet = (double.Parse(UserBet) * 2.0);
                double sum = Convert.ToDouble(Cash) - double.Parse(UserBet);
                using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
                {
                    baza.Open();
                    OleDbCommand VhodVReihstag = baza.CreateCommand();
                    VhodVReihstag.CommandText = "UPDATE Userss SET Score ='" + sum + "' WHERE login ='" + Login + "'";
                    OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                    Cash = Convert.ToString(sum);
                    label1.Text = DoubleBet + " $";
                    baza.Close();
                }
                dab = false;
                UserBet = Convert.ToString(DoubleBet);
            }
            else
                MessageBox.Show("Ставка привышает Ваши наличные! \nБудьте внимательнее ;)", "Ahtung!");
            button7.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double back = 0;
            string result = "";
            back = double.Parse(Cash) + ((double.Parse(UserBet)) / 2.0);
            result = "Игрок сдался";
            using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
            {
                baza.Open();
                OleDbCommand VhodVReihstag = baza.CreateCommand();
                VhodVReihstag.CommandText = "UPDATE Userss SET Score ='" + back + "' WHERE login ='" + Login + "'";
                OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                Cash = Convert.ToString(back);
                baza.Close();
            }
            MessageBox.Show("Ваши очки: " + game.Finish() + "\nОчки дилера:" + game.FinishDealer() + "\nРезультат: " + result + "\nВы сдались. Вам возвращается половина ставки");
            button6.Enabled = true;
            button5.Enabled = true;
            button2.Enabled = false;
            button1.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button3.Enabled = false;
        }
    }
}
