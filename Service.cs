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
    public partial class Service : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private string name, minrequi, description;
        private int Id_bank, Id_service;
        
        public Service()
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
                
                string strSql = @"SELECT t1.Name as 'Name', t1.MinRequirement as 'Min Requirement', t1.Description as 'Description', t1.Id_service as 'Id_service', t2.Name as 'Name Bank' FROM Bank t2 INNER JOIN Service t1 ON t1.Id_bank=t2.Id_bank";
                //string strSql = "SELECT Name, MinRequirement, Description, Id_bank, Id_service FROM Service";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[3].Visible = false;
                //dataGridView1.Columns[4].Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Service_create dialog = new Service_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
                
            name = dialog.name;
            minrequi = dialog.minrequi;
            description = dialog.description;
            Id_bank = dialog.Id_bank;

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
                string strSql = String.Format(@"INSERT INTO Service(Name, MinRequirement, Description, Id_bank) VALUES ('{0}', '{1}', '{2}', '{3}')", name, minrequi, description, Id_bank);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //string str = "SELECT Name, MinRequirement, Description, Id_bank, Id_service FROM Service";
            string str = @"SELECT t1.Name as 'Name', t1.MinRequirement as 'Min Requirement', t1.Description as 'Description', t1.Id_service as 'Id_service', t2.Name as 'Name Bank' FROM Bank t2 INNER JOIN Service t1 ON t1.Id_bank=t2.Id_bank";
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
            Service_create dialog = new Service_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
                
            name = dialog.name;
            minrequi = dialog.minrequi;
            description = dialog.description;
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
                Id_service = (int)dataGridView1.CurrentRow.Cells[3].Value;
                string strSql = String.Format(@"UPDATE Service SET 
                 Name = '{0}', MinRequirement = '{1}', Description = '{2}' WHERE Id_service = '{3}'", name, minrequi, description, Id_service);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //string str = "SELECT Name, MinRequirement, Description, Id_bank, Id_service FROM Service";
            string str = @"SELECT t1.Name as 'Name', t1.MinRequirement as 'Min Requirement', t1.Description as 'Description', t1.Id_service as 'Id_service', t2.Name as 'Name Bank' FROM Bank t2 INNER JOIN Service t1 ON t1.Id_bank=t2.Id_bank";
            SqlCommand cm = new SqlCommand(str, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            dataset.Reset();
            adapter.Fill(dataset);
            cn.Close();
            dataGridView1.Update();
            bind.DataSource = dataset.Tables[0];
            dataGridView1.DataSource = bind;
        }

        private void button4_Click(object sender, EventArgs e)
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
                Id_service = (int)dataGridView1.CurrentRow.Cells[3].Value;
                string strSql = String.Format(@"DELETE FROM Service WHERE Id_service = '{0}'", Id_service);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            //string str = "SELECT Name, MinRequirement, Description, Id_bank, Id_service FROM Service";
            string str = @"SELECT t1.Name as 'Name', t1.MinRequirement as 'Min Requirement', t1.Description as 'Description', t1.Id_service as 'Id_service', t2.Name as 'Name Bank' FROM Bank t2 INNER JOIN Service t1 ON t1.Id_bank=t2.Id_bank";
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
