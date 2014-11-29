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
    public partial class Query1 : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private BindingSource bind1 = new BindingSource();
        private DataSet dataset1 = new DataSet();
        private int Id_manager;
        public Query1()
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
                string strSql = "SELECT Surname, Name, Patronymic, Age, Id_manager FROM Manager";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
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
                MessageBox.Show(@"Нет соединения с базой данных. Повторите запрос позднее!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Id_manager = (int)dataGridView1.CurrentRow.Cells[4].Value;
                string strSql = String.Format(@"SELECT Job, Salary, Contact FROM Request WHERE Id_manager = '{0}'", Id_manager);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                dataset1.Reset();
                adapter1.Fill(dataset1);
                cn.Close();
                label2.Visible = true;
                dataGridView2.Visible = true;
                dataGridView2.Update();
                bind1.DataSource = dataset1.Tables[0];
                dataGridView2.DataSource = bind1;
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
