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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        public string[] usersdata;
        private void Registration_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            usersdata = new string[3];
            usersdata[0] = textBox1.Text;
            bool check;
            using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
            {
                baza.Open();
                OleDbCommand VhodVReihstag = baza.CreateCommand();
                VhodVReihstag.CommandText = "SELECT login FROM Userss WHERE login ='" + usersdata[0] + "'";
                OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader();
                if(ReihstagDialog.Read()) check = false;
                else check = true;
                baza.Close();
            }
            if (check)
            {
                usersdata[1] = textBox2.Text;
                usersdata[2] = textBox3.Text;
                using (OleDbConnection baza = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;Data Source=D:/Study/7 sem/ППП/MyBlackJack/MyBlackJack/db.mdb"))
                {
                    baza.Open();
                    OleDbCommand VhodVReihstag = baza.CreateCommand();
                    VhodVReihstag.CommandText = "INSERT INTO Userss (`login`, `Password`, `Score`) VALUES ('" + usersdata[0] + "','" + usersdata[1] + "'," + usersdata[2] + ")";
                    try { OleDbDataReader ReihstagDialog = VhodVReihstag.ExecuteReader(); }
                    catch { }
                    baza.Close();
                }
            }
            else MessageBox.Show("Такой парень уже есть в базе.", "Ahtung!");
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }
    }
}
