using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace FilingRequestInBank
{
    public partial class Manager_client : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        public int Id_bank, Id_manager;
        public string surname,name;
        
        public Manager_client()
        {
            InitializeComponent();
            
            FileInfo fi1 = new FileInfo("bank.txt");
            using (StreamReader sr = fi1.OpenText())
            {
                string s = "";
                s = sr.ReadLine();
                sr.Close();
                Id_bank = Convert.ToInt32(s);
            }
            
            using (SqlConnection cn = new System.Data.SqlClient.SqlConnection())
            {
                cn.ConnectionString = address;
                try
                {
                    cn.Open();
                }
                catch
                {
                    MessageBox.Show(@"Нет соединения с базой данных. Повторите запрос позднее!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //string strSql = "SELECT Surname, Name, Patronymic, Age, Id_bank, Id_manager FROM Manager"
                string strSql = String.Format(@"SELECT Surname, Name, Patronymic, Age, Id_manager FROM Manager WHERE Id_bank = '{0}'", Id_bank);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                cn.Close();
                dataGridView1.Columns[4].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = address;
            try
            {
                cn.Open();
            }
            catch
            {
                MessageBox.Show(@"Нет соединения с базой данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show(@"Нет Менеджеров для выбора!Обратитесь к администрации за помощью", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            button2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;

            Id_manager = (int)dataGridView1.CurrentRow.Cells[4].Value;
            surname = (string)dataGridView1.CurrentRow.Cells[0].Value;
            name = (string)dataGridView1.CurrentRow.Cells[1].Value;
            label3.Text = "Выбран менеджер: " + surname + " "+ name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
