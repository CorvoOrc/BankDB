using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FilingRequestInBank
{
    public partial class Request_create : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        public string job, salary, contact;
        public int  Id_personal, Id_meneger, Id_service;
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private BindingSource bind1 = new BindingSource();
        private DataSet dataset1 = new DataSet();
        private BindingSource bind2 = new BindingSource();
        private DataSet dataset2 = new DataSet();
        public bool access = false;
        
        public Request_create()
        {
            InitializeComponent();
            
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
                
                string strSql = "SELECT Surname, Name, Patronymic, Telephone, Age, Sex, Birthday, Id_personal FROM Personal";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
               // cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[7].Visible = false;

                string strSql1 = "SELECT Name, MinRequirement, Description, Id_bank, Id_service FROM Service";
                SqlCommand cmd1 = new SqlCommand(strSql1, cn);
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                adapter1.Fill(dataset1);
                // cn.Close();
                dataGridView2.AutoGenerateColumns = true;
                bind1.DataSource = dataset1.Tables[0];
                dataGridView2.DataSource = bind1;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Visible = false;

                string strSql2 = "SELECT Surname, Name, Patronymic, Age, Id_bank, Id_manager FROM Manager";
                SqlCommand cmd2 = new SqlCommand(strSql2, cn);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                adapter2.Fill(dataset2);
                cn.Close();
                dataGridView3.AutoGenerateColumns = true;
                bind2.DataSource = dataset2.Tables[0];
                dataGridView3.DataSource = bind2;
                dataGridView3.Columns[4].Visible = false;
                dataGridView3.Columns[5].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            access = true;
            job = textBox1.Text;
            salary = textBox2.Text;
            contact = textBox3.Text;

            if (dataGridView1.CurrentRow == null)
            {
                access = false;
                MessageBox.Show(@"Не выбран Клиент!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            Id_personal = (int)dataGridView1.CurrentRow.Cells[7].Value;
            
            if (dataGridView2.CurrentRow == null)
            {
                access = false;
                MessageBox.Show(@"Не выбрана Усдуга!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            Id_service = (int)dataGridView2.CurrentRow.Cells[4].Value;
            
            if (dataGridView3.CurrentRow == null)
            {
                access = false;
                MessageBox.Show(@"Не выбран Менеджер!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            Id_meneger = (int)dataGridView3.CurrentRow.Cells[5].Value;

            if (job == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Работа'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (salary == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Зарплата'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (contact == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Контакты'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
