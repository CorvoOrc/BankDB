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
    public partial class Query3 : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private BindingSource bind1 = new BindingSource();
        private DataSet dataset1 = new DataSet();
        private BindingSource bind2 = new BindingSource();
        private DataSet dataset2 = new DataSet();
        private int Id_bank;
        
        public Query3()
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
            label2.Visible = true;
            dataGridView2.Visible = true;
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
            
            Id_bank = (int)dataGridView1.CurrentRow.Cells[6].Value;
            string strSql = String.Format(@"SELECT Id_partner FROM ReferBP WHERE Id_bank='{0}'", Id_bank);
            SqlCommand cmd = new SqlCommand(strSql, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            dataset1.Reset();
            dataset2.Reset();
            adapter.Fill(dataset1);
            dataGridView2.AutoGenerateColumns = true;
            bind1.DataSource = dataset1.Tables[0];
            int count = bind1.Count;
            for (int i=0; i < count; ++i)
            {
               int tmp= (int) dataset1.Tables[0].Rows[i].ItemArray[0];
               string strSql1 = String.Format(@"SELECT Name, Adds, Telephone, Website, Description FROM Partner WHERE Id_partner = '{0}'", tmp);
               cmd.CommandText = strSql1;
               cmd.Connection = cn;
               adapter.SelectCommand = cmd;
               adapter.Fill(dataset2);
            }
            try
            {
                bind2.DataSource = dataset2.Tables[0];
                dataGridView2.DataSource = bind2;
                cn.Close();
            }
            catch
            {
                MessageBox.Show(@"Непредвиденная ошибка!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
