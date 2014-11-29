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
    public partial class Bank_client : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        public int Id_bank;
        public string name;
        public Bank_client()
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
                string strSql = "SELECT Name, ViewBank, History, Adds, Telephone, Website, Id_bank FROM Bank";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[6].Visible = false;
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
                MessageBox.Show(@"Нет Банка для выбора!Обратитесь к администрации за помощью", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            button2.Visible = true;
            label2.Visible = true;
            label4.Visible = true;

            Id_bank = (int)dataGridView1.CurrentRow.Cells[6].Value;
            name = (string)dataGridView1.CurrentRow.Cells[0].Value;
            label2.Text = "Выбран банк '" + name + "'";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
