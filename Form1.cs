using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Xml;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //public static string connectionString = "Server=DESKTOP-QIN1VF9\\SQLEXPRESS;Database=USERS;Trusted_Connection=True; encrypt=false;"; //VEMZ
        //public static string sql = "SELECT * FROM dbo.USER_LOGINS"; //VEMZ

        public static string connectionString = "Server=DESKTOP-KTETICJ\\SQLEXPRESS;Database=fileser;Trusted_Connection=True; encrypt=false;";//home
        public static string sql = "SELECT * FROM dbo.USER_LOGIN";//home
        public Form1()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {        
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {         
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                DataTable dt = ds.Tables[0];          
            }


        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                SqlCommandBuilder command = new SqlCommandBuilder();
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();
                newRow["login"] = textBox1.Text.ToString();
                newRow["pass"] = textBox2.Text.ToString();
                newRow["acc"] = 0;
                dt.Rows.Add(newRow);
               
                //сохранение
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables[0]);
                ds.Clear();
                adapter.Fill(ds);

                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int row_ind = dataGridView1.CurrentCell.RowIndex;

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            ds.Tables[0].Rows[row_ind].Delete();
            dataGridView1.DataSource = ds.Tables[0];

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
            ds.Clear();
            adapter.Fill(ds);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 login_form = new Form2();
            login_form.Show();
            this.Close();
        }

        private void добавлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_User add_form = new Add_User();
            add_form.Show();
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            filseSer brow = new filseSer();
            brow.Show();
            this.Close();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Справка", "made by Stein");
        }
    }
    }

