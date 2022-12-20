using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Add_User : Form
    {

        public static string connectionString_users = "Server=DESKTOP-QIN1VF9\\SQLEXPRESS;Database=USERS;Trusted_Connection=True; encrypt=false;"; //VEMZ
        public static string sql_users = "SELECT * FROM dbo.USER_LOGINS"; //VEMZ
        public static string sql_acc = "SELECT * FROM dbo.acc_role"; //VEMZ

        //public static string connectionString = "Server=DESKTOP-KTETICJ\\SQLEXPRESS;Database=fileser;Trusted_Connection=True; encrypt=false;";//home
        //public static string sql = "SELECT * FROM dbo.USER_LOGIN";//home

        public Add_User()
        {
            InitializeComponent();
        }

        private void Add_User_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Логин";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Пароль";
            textBox2.ForeColor = Color.Gray;
            comboBox1.Text = "Выберите уровень доступа";
            comboBox1.ForeColor = Color.Gray;
            using (SqlConnection connection = new SqlConnection(connectionString_users))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql_acc, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                comboBox1.DataSource = ds.Tables[0].DefaultView;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "acc_id";

            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString_users))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql_users, connection);
                SqlCommandBuilder command = new SqlCommandBuilder();
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];

                string reg_login = (string)textBox1.Text;
                DataRow[] foundRows;
                foundRows = dt.Select("login=" + reg_login);

                if ((string)foundRows[0][2]!=null)
                {
                    MessageBox.Show(
                   "Данное имя пользователя уже используется",
                   "Ошибка",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    newRow["login"] = textBox1.Text.ToString();
                    newRow["pass"] = textBox2.Text.ToString();
                    newRow["acc"] = (int)comboBox1.SelectedValue;
                    dt.Rows.Add(newRow);

                    //сохранение
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(ds.Tables[0]);
                    ds.Clear();
                    adapter.Fill(ds);

                    textBox1.Text = "Логин";
                    textBox1.ForeColor = Color.Gray;
                    textBox2.Text = "Пароль";
                    textBox2.ForeColor = Color.Gray;
                    comboBox1.Text = "Выберите уровень доступа";
                    comboBox1.ForeColor = Color.Gray;

                    MessageBox.Show(
                       "Вы успешно добавили пользователя",
                       "Ответ",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    Form1 admin_form = new Form1();
                    admin_form.Show();
                    this.Close();
                }                
            }
           
        }
    }
}

