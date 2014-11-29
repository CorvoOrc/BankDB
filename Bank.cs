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
    public partial class Bank : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private string name, viewbank, history, adds, telephone, website;
        private int Id_bank;
        public Bank()
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
            Bank_create dialog = new Bank_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
            name = dialog.name;
            viewbank = dialog.viewbank;
            history = dialog.history;
            adds = dialog.adds;
            telephone = dialog.telephone;
            website = dialog.website;

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
            try
            {
                string strSql = String.Format(@"INSERT INTO Bank(Name, ViewBank, History, Adds, Telephone, Website) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", name, viewbank, history, adds, telephone, website);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str = "SELECT Name, ViewBank, History, Adds, Telephone, Website, Id_bank FROM Bank";
            SqlCommand cm = new SqlCommand(str, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            dataset.Reset();
            adapter.Fill(dataset);
            cn.Close();
            dataGridView1.Update();
            bind.DataSource = dataset.Tables[0];
            dataGridView1.DataSource = bind;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

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
            try
            {
                Id_bank = (int)dataGridView1.CurrentRow.Cells[6].Value;
                //DELETE FROM emails WHERE id='2';
                string strSql = String.Format(@"DELETE FROM Bank WHERE Id_bank = '{0}'", Id_bank);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                //DELETE FROM emails WHERE id='2';
                string strSql1 = String.Format(@"DELETE FROM ReferBP WHERE Id_bank = '{0}'", Id_bank);
                SqlCommand cmd1 = new SqlCommand(strSql1, cn);
                cmd1.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
               
            string str = "SELECT Name, ViewBank, History, Adds, Telephone, Website, Id_bank FROM Bank";
            SqlCommand cm = new SqlCommand(str, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            dataset.Reset();
            adapter.Fill(dataset);
            cn.Close();
            dataGridView1.Update();
            bind.DataSource = dataset.Tables[0];
            dataGridView1.DataSource = bind;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bank_create dialog = new Bank_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
            name = dialog.name;
            viewbank = dialog.viewbank;
            history = dialog.history;
            adds = dialog.adds;
            telephone = dialog.telephone;
            website = dialog.website;

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
            try
            {
                Id_bank = (int)dataGridView1.CurrentRow.Cells[6].Value;
                string strSql = String.Format(@"UPDATE Bank SET 
                Name = '{0}', ViewBank = '{1}', History = '{2}', Adds = '{3}', Telephone = '{4}', Website = '{5}' WHERE Id_bank = '{6}'", name, viewbank, history,
                adds, telephone, website, Id_bank);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str = "SELECT Name, ViewBank, History, Adds, Telephone, Website, Id_bank FROM Bank";
            SqlCommand cm = new SqlCommand(str, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            dataset.Reset();
            adapter.Fill(dataset);
            cn.Close();
            dataGridView1.Update();
            bind.DataSource = dataset.Tables[0];
            dataGridView1.DataSource = bind;
        }
    }
}
