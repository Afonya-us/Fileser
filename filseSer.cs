using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Data.Sqlite;
using static WindowsFormsApp1.filseSer;

namespace WindowsFormsApp1
{
    public partial class filseSer : Form
    {
        public static string connectionString = "Server=DESKTOP-QIN1VF9\\SQLEXPRESS;Database=USERS;Trusted_Connection=True; encrypt=false;"; //VEMZ
        public static string sql = "SELECT * FROM dbo.file_brow"; //VEMZ      

        //public static string connectionString = "Server=DESKTOP-KTETICJ\\SQLEXPRESS;Database=fileser;Trusted_Connection=True; encrypt=false;";//home
        //public static string sql = "SELECT * FROM dbo.USER_LOGIN";//home
        public filseSer()
        {
            InitializeComponent();
        }




        private void filseSer_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if ((int)dt.Rows[i][3] == Form2.user_acc && Form2.user_acc == 0)
                    {
                        treeView1.Nodes[0].Nodes[1].Nodes.Add((string)dt.Rows[i][1]);
                    }
                    else
                        if (Form2.user_acc == 1)
                    {
                        treeView1.Nodes[0].Nodes[0].Nodes.Add((string)dt.Rows[i][1]);
                    }
                }

            }


        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                string node_sel = treeView1.SelectedNode.Text;
                if (node_sel != "")
                {
                    var sel_f = dt.Select("file_name like'%" + node_sel + "%'");
                    using (MemoryStream ms = new MemoryStream((byte[])sel_f[0][2]))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            string node_sel = treeView1.SelectedNode.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];

                if (node_sel != "")
                {
                    var sel_f = dt.Select("file_name like'%" + node_sel + "%'");
                    saveFileDialog1.InitialDirectory = "C://";
                    saveFileDialog1.FileName = (string)sel_f[0][4];
                    saveFileDialog1.Filter = "all files (*.*)|*.*";
                    byte[] data = (byte[])sel_f[0][2];
                    string title = (string)sel_f[0][4];
                    //string a = (string)sel_f[0][2];
                    // сохранение
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream fs = new FileStream(title, FileMode.Create))
                        {
                          
                            fs.Write(data, 0, data.Length);
                           
                        }
                        
                    }
                    else
                        MessageBox.Show("ошибка", "пустой стрим");
                }
                
            }

                
               
            }
        }
    }


