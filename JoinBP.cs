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
    public partial class JoinBP : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private BindingSource bind1 = new BindingSource();
        private DataSet dataset1 = new DataSet();
        public int Id_bank, Id_partner;
        
        public JoinBP()
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
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[6].Visible = false;

                string strSql1 = "SELECT Name, Adds, Telephone, Website, Description, Id_partner FROM Partner";
                SqlCommand cmd1 = new SqlCommand(strSql1, cn);
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                adapter1.Fill(dataset1);
                cn.Close();
                dataGridView2.AutoGenerateColumns = true;
                bind1.DataSource = dataset1.Tables[0];
                dataGridView2.DataSource = bind1;
                dataGridView2.Columns[5].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show(@"Выберите Банк для привязки!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show(@"Выберите Партнера для привязки!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            Id_bank = (int)dataGridView1.CurrentRow.Cells[6].Value;
            Id_partner = (int)dataGridView2.CurrentRow.Cells[5].Value;
            
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
                try
                {
                    string strSql = String.Format(@"INSERT INTO ReferBP(Id_bank, Id_partner) VALUES ('{0}', '{1}')", Id_bank, Id_partner);
                    SqlCommand cmd = new SqlCommand(strSql, cn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
