using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace MyBlackJack
{
    public partial class Logining : Form
    {
        public Logining()
        {
            InitializeComponent();
        }

        public string[] usersdata;
        private void Logining_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Она не знакомится.", "System Error");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLogin fl = this.Owner as FormLogin;
            usersdata = new string[2];
            usersdata[0] = textBox1.Text;
            usersdata[1] = textBox2.Text;
            bool check;
            using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
            {
                baza.Open();
                OleDbCommand VhodVReihstag = baza.CreateCommand();
                VhodVReihstag.CommandText = "SELECT * FROM Userss WHERE login ='" + usersdata[0] + "'AND Password ='" + usersdata[1] + "'";
                OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                if (ReihstagDialog.Read()) check = false;
                else check = true;
                baza.Close();
            }
            if (check == false)
            {
                check = true;
                using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
                {
                    baza.Open();
                    OleDbCommand VhodVReihstag = baza.CreateCommand();
                    VhodVReihstag.CommandText = "SELECT Score FROM Userss WHERE login ='" + usersdata[0] + "'";
                    OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                    while (ReihstagDialog.Read())
                        fl.money = Convert.ToString(ReihstagDialog["Score"]);
                    if (double.Parse(fl.money) > 0) check = false;
                    else check = true;
                    baza.Close();
                }
                if (check == false)
                {
                    fl.Shvisteger = usersdata[0];
                    MessageBox.Show("Вы можете пройти, сэр.", "Ahtung!");
                }
                else MessageBox.Show("Вы должны этому казино, \nможете идти домой, мы сами придём к Вам.");
            }
            else MessageBox.Show("Ваше имя и кодовое слово \nне стыкуются в наших списках, господин.", "Ahtung!");
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
