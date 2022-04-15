using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, 64);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        private void CloseButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }
        Point LastPoint;


        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/Dan/source/repos/WindowsFormsApp1/WindowsFormsApp1/BD/Baza.db; Version=3");
            conn.Open();
            
            SQLiteCommand sqliteCommand = conn.CreateCommand();
            sqliteCommand.CommandText = "INSERT INTO Accounts (login, password) VALUES (@login, @password)";
            
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(passField.Text, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 0, 16);

            sqliteCommand.Parameters.Add("@password", System.Data.DbType.String).Value = passField.Text;
            sqliteCommand.Parameters.Add("@login", System.Data.DbType.String).Value = loginField.Text;
            sqliteCommand.ExecuteNonQuery();
            
            
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                if (loginField.Text.Length < 1 || passField.Text.Length < 1)
                {
                    return;
                }
                SQLiteConnection conn = new SQLiteConnection("Data Source=C:/Users/Dan/source/repos/WindowsFormsApp1/WindowsFormsApp1/BD/Baza.db; Version=3");
                conn.Open();

                SQLiteCommand sqliteCommand = conn.CreateCommand();
                sqliteCommand.CommandText = "SELECT password FROM Accounts WHERE login = @login";
                sqliteCommand.Parameters.Add("@login", System.Data.DbType.String).Value = loginField.Text;

                var password = (string)sqliteCommand.ExecuteScalar();

                if (password == passField.Text)
                {
                    MainForm form = new MainForm();
                    form.Show();
                }
                else
                {
                    label2.Text = "логин или пароль неверный";
                    label2.ForeColor = Color.Red;
                    return;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
    }
}

