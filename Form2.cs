using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public static string connectionString = "Server=DESKTOP-QIN1VF9\\SQLEXPRESS;Database=USERS;Trusted_Connection=True; encrypt=false;"; //VEMZ
        public static string sql = "SELECT * FROM dbo.USER_LOGINS"; //VEMZ
        public static string id_f = "SELECT pass FROM dbo.USER_LOGINS where id=@id_ch";//VEMZ
        public static int user_acc;

        //public static string connectionString = "Server=DESKTOP-KTETICJ\\SQLEXPRESS;Database=fileser;Trusted_Connection=True; encrypt=false;";//home
        //public static string sql = "SELECT * FROM dbo.USER_LOGIN";//home
        //public static string id_f = "SELECT pass FROM dbo.USER_LOGIN where id=@id_ch";//home

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                comboBox1.DataSource = ds.Tables[0].DefaultView;
                comboBox1.DisplayMember = "login";
                comboBox1.ValueMember = "id";
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pass = textBox2.Text.ToString();

            int id_sql = (int)comboBox1.SelectedValue;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                //Поиск строки по id
                DataRow[] foundRows;
                foundRows = dt.Select("id="+id_sql);

                //проверка соответствия pass at home
                /*if ((string)foundRows[0][2]== pass)
                {
                    //проверка соответствия асс
                    if ((int)foundRows[0][3] == 1)
                    {
                        Form1 admin_form = new Form1();
                        admin_form.Show();
                        this.Visible = false;
                    }
                    else
                    //проверка соответствия асс
                    if ((int)foundRows[0][3] == 0)
                    {
                        filseSer fs_form = new filseSer();
                        fs_form.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show(
                   "У вас нет доступа к просмотру данных",
                   "Ошибка",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show(
                    "Вы неверно ввели пароль",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    textBox2.Text = "";
                }*/

                //проверка соответствия pass at VEMZ
                if ((string)foundRows[0][1]== pass)
                {
                    //проверка соответствия асс
                    if ((int)foundRows[0][3] == 1)
                    {
                        Form1 admin_form = new Form1();
                        admin_form.Show();
                        this.Visible = false;
                        user_acc=1;
                    }
                    else
                    //проверка соответствия асс
                    if ((int)foundRows[0][3] == 0)
                    {
                        filseSer fs_form = new filseSer();
                        fs_form.Show();
                        this.Visible = false;
                        user_acc = 0;

                    }
                    else
                    {
                        MessageBox.Show(
                   "У вас нет доступа к просмотру данных",
                   "Ошибка",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show(
                    "Вы неверно ввели пароль",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    textBox2.Text = "";
                }
            }
        }

                private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
                textBox2.PasswordChar = '*';
        }
    }
}
