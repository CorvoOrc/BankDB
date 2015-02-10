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
    public partial class Manager : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private string surname, name, patronymic;
        private int age;
        private int Id_bank, Id_manager;
        
        public Manager()
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
                string strSql = @"SELECT t1.Surname as 'Surname', t1.Name as 'Name', t1.Patronymic as 'Patronymic', t1.Age as 'Age', t1.Id_manager as 'Id_manager', t2.Name as 'Name Bank' FROM Bank t2 INNER JOIN Manager t1 ON t1.Id_bank=t2.Id_bank";
                //string strSql = "SELECT Surname, Name, Patronymic, Age, Id_bank, Id_manager FROM Manager";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[4].Visible = false;
                //dataGridView1.Columns[5].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager_create dialog = new Manager_create();
            dialog.ShowDialog();

             if (!dialog.access)
                return;
                
            surname = dialog.surname;
            name = dialog.name;
            patronymic = dialog.pantonymic;
            age = dialog.age;
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
                string strSql = String.Format(@"INSERT INTO Manager(Surname, Name, Patronymic, Age, Id_bank) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", surname, name, patronymic, age, Id_bank);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //string str = "SELECT Surname, Name, Patronymic, Age, Id_bank, Id_manager FROM Manager";
            string str = @"SELECT t1.Surname as 'Surname', t1.Name as 'Name', t1.Patronymic as 'Patronymic', t1.Age as 'Age', t1.Id_manager as 'Id_manager', t2.Name as 'NameBank' FROM Bank t2 INNER JOIN Manager t1 ON t1.Id_bank=t2.Id_bank";
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
            Manager_create dialog = new Manager_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
                
            surname = dialog.surname;
            name = dialog.name;
            patronymic = dialog.pantonymic;
            age = dialog.age;
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
                Id_manager = (int)dataGridView1.CurrentRow.Cells[4].Value;
                string strSql = String.Format(@"UPDATE Manager SET 
                Surname = '{0}', Name = '{1}', Patronymic = '{2}', Age = '{3}' WHERE Id_manager = '{4}'", surname, name, patronymic, age, Id_manager);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //string str = "SELECT Surname, Name, Patronymic, Age, Id_bank, Id_manager FROM Manager";
            string str = @"SELECT t1.Surname as 'Surname', t1.Name as 'Name', t1.Patronymic as 'Patronymic', t1.Age as 'Age', t1.Id_manager as 'Id_manager', t2.Name as 'NameBank' FROM Bank t2 INNER JOIN Manager t1 ON t1.Id_bank=t2.Id_bank";
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
                Id_manager = (int)dataGridView1.CurrentRow.Cells[4].Value;
                string strSql = String.Format(@"DELETE FROM Manager WHERE Id_manager = '{0}'", Id_manager);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //string str = "SELECT Surname, Name, Patronymic, Age, Id_bank, Id_manager FROM Manager";
            string str = @"SELECT t1.Surname as 'Surname', t1.Name as 'Name', t1.Patronymic as 'Patronymic', t1.Age as 'Age', t1.Id_manager as 'Id_manager', t2.Name as 'NameBank' FROM Bank t2 INNER JOIN Manager t1 ON t1.Id_bank=t2.Id_bank";
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
