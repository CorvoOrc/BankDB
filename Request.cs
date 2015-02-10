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
    public partial class Request : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private string job, salary, contact;
        private int Id_request, Id_personal, Id_meneger, Id_service;
        
        public Request()
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
                
                string strSql = @"SELECT t1.Job as 'Job', t1.Salary as 'Salary', t1.Contact as 'Contact',  t1.Id_personal as 'Id_personal', t1.Id_manager as 'Id_manager', 
                t1.Id_service as 'Id_service', t1.Id_request as 'Id_request', t2.Surname as 'Surname' FROM Personal t2 INNER JOIN Request t1 ON t1.Id_personal=t2.Id_personal";
                //string strSql = "SELECT Job, Salary, Contact, Id_personal, Id_manager, Id_service, Id_request FROM Request";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Request_create dialog = new Request_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
                
            job = dialog.job;
            salary = dialog.salary;
            contact = dialog.contact;
            Id_personal = dialog.Id_personal;
            Id_service = dialog.Id_service;
            Id_meneger = dialog.Id_meneger;

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
                //"SELECT Job, Salary, Contact, Family, Id_personal, Id_manager, Id_service, Id_request FROM Request"
                string strSql = String.Format(@"INSERT INTO Request(Job, Salary, Contact, Id_personal, Id_service, Id_manager) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", job, salary, contact, Id_personal, Id_service, Id_meneger);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string str = @"SELECT t1.Job as 'Job', t1.Salary as 'Salary', t1.Contact as 'Contact',  t1.Id_personal as 'Id_personal', t1.Id_manager as 'Id_manager', 
                t1.Id_service as 'Id_service', t1.Id_request as 'Id_request', t2.Surname as 'Surname' FROM Personal t2 INNER JOIN Request t1 ON t1.Id_personal=t2.Id_personal";
            //string str = "SELECT Job, Salary, Contact, Id_personal, Id_manager, Id_service, Id_request FROM Request";
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
                Id_request = (int)dataGridView1.CurrentRow.Cells[6].Value;
                string strSql = String.Format(@"DELETE FROM Request WHERE Id_request = '{0}'", Id_request);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string str = @"SELECT t1.Job as 'Job', t1.Salary as 'Salary', t1.Contact as 'Contact',  t1.Id_personal as 'Id_personal', t1.Id_manager as 'Id_manager', 
                t1.Id_service as 'Id_service', t1.Id_request as 'Id_request', t2.Surname as 'Surname' FROM Personal t2 INNER JOIN Request t1 ON t1.Id_personal=t2.Id_personal";
            //string str = "SELECT Job, Salary, Contact, Id_personal, Id_manager, Id_service, Id_request FROM Request";
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
            Request_create dialog = new Request_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
                
            job = dialog.job;
            salary = dialog.salary;
            contact = dialog.contact;
            Id_personal = dialog.Id_personal;
            Id_service = dialog.Id_service;
            Id_meneger = dialog.Id_meneger;

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
                Id_request = (int)dataGridView1.CurrentRow.Cells[6].Value;
                string strSql = String.Format(@"UPDATE Request SET 
                Job = '{0}', Salary = '{1}', Contact = '{2}' WHERE Id_request = '{3}'", job, salary, contact, Id_request);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string str = @"SELECT t1.Job as 'Job', t1.Salary as 'Salary', t1.Contact as 'Contact',  t1.Id_personal as 'Id_personal', t1.Id_manager as 'Id_manager', 
                t1.Id_service as 'Id_service', t1.Id_request as 'Id_request', t2.Surname as 'Surname' FROM Personal t2 INNER JOIN Request t1 ON t1.Id_personal=t2.Id_personal";
            //string str = "SELECT Job, Salary, Contact, Id_personal, Id_manager, Id_service, Id_request FROM Request";
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
            Id_request = (int)dataGridView1.CurrentRow.Cells[6].Value;
            FileInfo fi2 = new FileInfo("cur_req.txt");
            using (StreamWriter sw = fi2.CreateText())
            {
                sw.WriteLine(Id_request.ToString());
                sw.Close();
            }
            
            Family_member dialog = new Family_member();
            dialog.ShowDialog();
        }
    }
}
