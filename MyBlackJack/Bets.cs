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
    public partial class Bets : Form
    {
        public Bets()
        {
            InitializeComponent();
        }

        MainForm b;
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(b.Cash) >= Convert.ToDouble(textBox1.Text) && textBox1.Text != string.Empty)
            {
                b.UserBet = textBox1.Text;
                double sum = Convert.ToDouble(b.Cash) - Convert.ToDouble(textBox1.Text);
                using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
                {
                    baza.Open();
                    OleDbCommand VhodVReihstag = baza.CreateCommand();
                    VhodVReihstag.CommandText = "UPDATE Userss SET Score ='" + sum + "' WHERE login ='" + b.Login + "'";
                    OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                    b.Cash = Convert.ToString(sum);
                    baza.Close();
                }
                this.Close();
            }   
            else
            {
                textBox1.Text = string.Empty;
                MessageBox.Show("Ставка привышает Ваши наличные! \nБудьте внимательнее ;) \nЛибо вообще не заполнена...", "Ahtung!");
            }
        }

        private void Bets_Load(object sender, EventArgs e)
        {
           b  = this.Owner as MainForm;
           using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
           {
               baza.Open();
               OleDbCommand VhodVReihstag = baza.CreateCommand();
               VhodVReihstag.CommandText = "SELECT Score FROM Userss WHERE login ='" + b.Login + "'";
               OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
               while (ReihstagDialog.Read())
                   b.Cash = Convert.ToString(ReihstagDialog["Score"]);
               label1.Text = "Ваш банк: " + b.Cash + " $";
               baza.Close();
           }
        }
    }
}
