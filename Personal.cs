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
    public partial class Personal : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private string surname, name, patronymic, telephone, sex, birthday;
        private int age;
        int Id_personal;
        
        public Personal()
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
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[7].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Personal_create f = new Personal_create();
            f.ShowDialog();

            if (!f.access)
                return;
                
            surname = f.surname;
            name = f.name;
            patronymic = f.patronymic;
            telephone = f.telephone;
            age = f.age;
            sex = f.sex;
            birthday = f.birthday;

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
                string strSql = String.Format(@"INSERT INTO Personal(Surname, Name, Patronymic, Telephone, Age, Sex, Birthday) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", surname, name, patronymic, telephone, age, sex, birthday);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string str = "SELECT Surname, Name, Patronymic, Telephone, Age, Sex, Birthday, Id_personal FROM Personal";
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
                Id_personal = (int)dataGridView1.CurrentRow.Cells[7].Value;
                //DELETE FROM emails WHERE id='2';
                string strSql = String.Format(@"DELETE FROM Personal WHERE Id_personal = '{0}'", Id_personal);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string str = "SELECT Surname, Name, Patronymic, Telephone, Age, Sex, Birthday, Id_personal FROM Personal";
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
            if (dataGridView1.CurrentRow == null)
                return;

            Personal_create f = new Personal_create();
            f.ShowDialog();
            if (!f.access)
                return;
            surname = f.surname;
            name = f.name;
            patronymic = f.patronymic;
            telephone = f.telephone;
            age = f.age;
            sex = f.sex;
            birthday = f.birthday;

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
                Id_personal = (int)dataGridView1.CurrentRow.Cells[7].Value;
                //UPDATE emails SET lname='Ivanov', fname='Ivan' WHERE id='1';
                string strSql = String.Format(@"UPDATE Personal SET 
                Surname = '{0}', Name = '{1}', Patronymic = '{2}', Telephone = '{3}', Age = '{4}', Sex = '{5}', Birthday = '{6}' WHERE Id_personal = '{7}'", surname, name, patronymic,
                telephone, age, sex, birthday, Id_personal);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string str = "SELECT Surname, Name, Patronymic, Telephone, Age, Sex, Birthday, Id_personal FROM Personal";
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
