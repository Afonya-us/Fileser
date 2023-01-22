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
        //public static string connectionString = "Server=DESKTOP-QIN1VF9\\SQLEXPRESS;Database=USERS;Trusted_Connection=True; encrypt=false;"; //VEMZ
        //public static string sql = "SELECT * FROM dbo.file_brow"; //VEMZ      

        public static string connectionString = "Server=DESKTOP-KTETICJ\\SQLEXPRESS;Database=fileser;Trusted_Connection=True; encrypt=false;";//home
        public static string sql = "SELECT * FROM dbo.files";//home
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
                        trackBar1.Visible = true;
                        label2.Visible = true;
                        label2.Text = "Текущий уровень доступа:\n пользователь ";
                    }
                }

            }


        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            



        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value == 0)
            {
                label1.Text = "Текущий уровень доступа:\n пользователь";
            }
            if (trackBar1.Value == 1)
            {
                label1.Text = "Текущий уровень доступа:\n администратор";
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

        }

        private void filseSer_Leave(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_2(object sender, EventArgs e)
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

                    byte[] data = (byte[])sel_f[0][2];
                    string path = (string)sel_f[0][4];
                    string s_name = Path.GetFileName(path);
                    string ext = Path.GetExtension(path);


                    saveFileDialog1.InitialDirectory = "C://";
                    saveFileDialog1.FileName = (string)sel_f[0][4];
                    saveFileDialog1.Filter = "all files (*" + ext + ")|*" + ext;

                    //string a = (string)sel_f[0][2];

                    // сохранение
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog1.FileName, data);
                    }
                }
                else
                    MessageBox.Show("ошибка", "пустой стрим");

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = "C://";
                openFileDialog1.Filter = "all files (*.*)|*.*";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog1.FileName;
                    string sel_name = Path.GetFileNameWithoutExtension(path);
                    string sel_f_name = openFileDialog1.SafeFileName;
                    byte[] sel_data = File.ReadAllBytes(openFileDialog1.FileName);

                    if (Form2.user_acc == 1)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["file_name"] = sel_name;
                        newRow["file_loc"] = sel_data;
                        newRow["type"] = sel_f_name;
                        dt.Rows.Add(newRow);
                        newRow["acc"] = trackBar1.Value;
                    }

                    if (Form2.user_acc == 0)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["file_name"] = sel_name;
                        newRow["file_loc"] = sel_data;
                        newRow["type"] = sel_f_name;
                        newRow["acc"] = 0;
                        dt.Rows.Add(newRow);
                    }

                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.Update(ds.Tables[0]);
                    ds.Clear();
                    adapter.Fill(ds);
                    treeView1.Nodes[0].Nodes[1].Nodes.Clear();
                    treeView1.Nodes[0].Nodes[0].Nodes.Clear();

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
                            trackBar1.Visible = true;
                            label1.Visible = true;
                        }
                    }
                }
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект DataSet
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (treeView1.SelectedNode != null)
                {
                    string node_sel = treeView1.SelectedNode.Text;

                    if (node_sel != "")
                    {
                        var sel_f = dt.Select("file_name like'%" + node_sel + "%'");
                        using (MemoryStream ms = new MemoryStream((byte[])sel_f[0][2]))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                            ms.Close();
                        }
                       
                    }
                }
            }
        }

        private void panel1_Leave(object sender, EventArgs e)
        {

        }

        private void filseSer_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void filseSer_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Application.Exit();
            Form2 login_form = new Form2();
            login_form.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 login_form = new Form2();
            login_form.Show();
            this.Visible=false;
        }
    }
}


