﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace марафон2
{
    public partial class sponsor : Form
    {
        public sponsor()
        {
            InitializeComponent();
        }
        private void check()
        {
            if (textBox1.Text == "" | textBox3.Text == "" | textBox4.Text == "" | textBox6.Text == "" | textBox7.Text == "" | comboBox1.Text == "")
                button4.Enabled = false;
            else
                button4.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan time1;
            DateTime initial_time = Convert.ToDateTime("8.04.2020 6:00");
            DateTime current_time = DateTime.Now;
            time1 = initial_time - current_time;
            time.Text = time1.Days.ToString() + " дней " + time1.Hours.ToString() + " часов и " +
            time1.Minutes.ToString() + " минут до старта марафона!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            main m = new main();
            m.Show();
            this.Hide();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.TextLength == 0) textBox7.Text = "0";
            label12.Text = textBox7.Text;
            if (Convert.ToInt32(textBox7.Text) <= 0)
            {
                MessageBox.Show("Вы должны пожертвовать хотя бы 1$");
                button4.Enabled = false;
            }
            else
            {
                button4.Enabled = true;
            }
        }

        private void sponsor_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(марафон2.Properties.Settings.Default.MaraphonConnectionString))
            {
                using (SqlConnection conn = new SqlConnection
                    (марафон2.Properties.Settings.Default.MaraphonConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = " SELECT [User].FirstName + ' ' + [User].LastName + ' - ' + CONVERT(varchar(20),RegistrationEvent.BibNumber) + ' (' + Runner.CountryCode +')' AS Runner FROM Runner INNER JOIN              [User] ON Runner.Email = [User].Email INNER JOIN                         Registration ON Runner.RunnerId = Registration.RunnerId INNER JOIN RegistrationEvent ON Registration.RegistrationId = RegistrationEvent.RegistrationId WHERE([User].RoleId = N'R')"; SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0]);
                    }
                    conn.Close();

                }
                using (SqlConnection conn = new SqlConnection(марафон2.Properties.Settings.Default.MaraphonConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = " SELECT CharityName  FROM Charity";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0]);
                    }
                    conn.Close();

                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if
               (textBox4.TextLength != 16)
                textBox4.BackColor = Color.Red;
            else
                textBox4.BackColor = Color.White;
            textBox4.MaxLength = 16;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.TextLength != 3)
                textBox6.BackColor = Color.Red;
            else
                textBox6.BackColor = Color.White;
            textBox6.MaxLength = 3;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            fond f = new fond();
            using (SqlConnection con = new SqlConnection
                (марафон2.Properties.Settings.Default.MaraphonConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = " SELECT CharityName,CharityDescription,CharityLogo FROM Charity WHERE CharityName = '" + comboBox1.Text + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    f.label1.Text = reader["CharityName"].ToString();
                    f.textBox1.Text = reader["CharityDescription"].ToString();
                    f.pictureBox1.Image = Image.FromFile("Resources/" + reader["CharityLogo"].ToString());
                }
                con.Close();
            }
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox7.Text) <= 0)
            {
                textBox7.Text = (Convert.ToInt32(textBox7.Text) + 10).ToString();
                label12.Text = textBox7.Text;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox7.Text) >= 10)
            {
                textBox7.Text = (Convert.ToInt32(textBox7.Text) - 10).ToString();
                label12.Text = textBox7.Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            main m = new main();
            m.Show();
            this.Hide();
        }
    }
}
