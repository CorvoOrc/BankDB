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
    public partial class Request_client : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private BindingSource bind1 = new BindingSource();
        private DataSet dataset1 = new DataSet();
        private BindingSource bind2 = new BindingSource();
        private DataSet dataset2 = new DataSet();
        public int Id_service, Id_manager,Id_personal, Id_family_member, Id_family = -6;
        public string job, salary, contact;
        public string family_member, activities,surname;
        public int age;
        public bool acc = false;
        public Request_client()
        {
            InitializeComponent();
            FileInfo fi1 = new FileInfo("service.txt");
            using (StreamReader sr = fi1.OpenText())
            {
                string s = "";
                s = sr.ReadLine();
                sr.Close();
                Id_service = Convert.ToInt32(s);
            }
            FileInfo fi2 = new FileInfo("manager.txt");
            using (StreamReader sr = fi2.OpenText())
            {
                string s = "";
                s = sr.ReadLine();
                sr.Close();
                Id_manager = Convert.ToInt32(s);
            }
            FileInfo fi3 = new FileInfo("personal.txt");
            using (StreamReader sr = fi3.OpenText())
            {
                string s = "";
                s = sr.ReadLine();
                sr.Close();
                Id_personal = Convert.ToInt32(s);
            }
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
                    string strSql =  String.Format("SELECT Surname, Name, Patronymic, Telephone, Age, Sex, Birthday, Id_personal FROM Personal WHERE Id_personal = '{0}'",Id_personal);
                    SqlCommand cmd = new SqlCommand(strSql, cn);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                    dataset1.Reset();
                    adapter1.Fill(dataset1);
                    bind1.DataSource = dataset1.Tables[0];
                    surname = (string)dataset1.Tables[0].Rows[0].ItemArray[0];
                }
                catch
                {
                    MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    string strSql1 =  String.Format("INSERT INTO Family(Surname) VALUES ('{0}')", surname);
                    SqlCommand cmd1 = new SqlCommand(strSql1, cn);
                    cmd1.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                string str1 = "SELECT Surname,Id_family FROM Family";
                SqlCommand cm = new SqlCommand(str1, cn);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cm);
                dataset2.Reset();
                adapter2.Fill(dataset2);
                bind2.DataSource = dataset2.Tables[0];
                int count = bind2.Count;
                Id_family = (int)dataset2.Tables[0].Rows[count-1].ItemArray[1];
                cn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = false;

            Family f = new Family();
            f.ShowDialog();

            if (!f.access)
                return;
            family_member = f.family_member;
            activities = f.activities;
            age = f.age;

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
                string strSql = String.Format(@"INSERT INTO Family_member(Family_member_field, Activities, Age, Id_family) VALUES ('{0}', '{1}', '{2}', '{3}')", family_member, activities, age, Id_family);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str = String.Format(@"SELECT t1.Family_member_field as 'Family memder', t1.Activities as 'Activities', t1.Id_family_member as 'Id_family_member', t1.Age as 'Age', t2.Surname as 'Surname' FROM Family t2 INNER JOIN Family_member t1 ON t1.Id_family=t2.Id_family WHERE t2.Id_family = '{0}'", Id_family);
            //string str = String.Format(@"SELECT Family_member_field, Activities, Age, Id_family_member FROM Family_member WHERE Id_family = '{0}'", Id_family);
            SqlCommand cm = new SqlCommand(str, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            dataset.Reset();
            adapter.Fill(dataset);
            cn.Close();
            dataGridView1.Update();
            dataGridView1.AutoGenerateColumns = true;
            bind.DataSource = dataset.Tables[0];
            dataGridView1.DataSource = bind;
            dataGridView1.Columns[2].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

            Family f = new Family();
            f.ShowDialog();
            if (!f.access)
                return;
            family_member = f.family_member;
            activities = f.activities;
            age = f.age;

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
                Id_family_member = (int)dataGridView1.CurrentRow.Cells[2].Value;
                //UPDATE emails SET lname='Ivanov', fname='Ivan' WHERE id='1';
                string strSql = String.Format(@"UPDATE Family_member SET 
                Family_member_field = '{0}', Activities = '{1}', Age = '{2}' WHERE Id_family_member = '{3}'", family_member, activities, age, Id_family_member);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str = String.Format(@"SELECT t1.Family_member_field as 'Family memder', t1.Activities as 'Activities', t1.Id_family_member as 'Id_family_member', t1.Age as 'Age', t2.Surname as 'Surname' FROM Family t2 INNER JOIN Family_member t1 ON t1.Id_family=t2.Id_family WHERE t2.Id_family = '{0}'", Id_family);
            //string str = String.Format(@"SELECT Family_member_field, Activities, Age, Id_family_member FROM Family_member WHERE Id_family = '{0}'", Id_family);
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
                Id_family_member = (int)dataGridView1.CurrentRow.Cells[2].Value;
                //DELETE FROM emails WHERE id='2';
                string strSql = String.Format(@"DELETE FROM Family_member WHERE Id_family_member = '{0}'", Id_family_member);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str = String.Format(@"SELECT t1.Family_member_field as 'Family memder', t1.Activities as 'Activities', t1.Id_family_member as 'Id_family_member', t1.Age as 'Age', t2.Surname as 'Surname' FROM Family t2 INNER JOIN Family_member t1 ON t1.Id_family=t2.Id_family WHERE t2.Id_family = '{0}'", Id_family);
            //string str = String.Format(@"SELECT Family_member_field, Activities, Age, Id_family_member FROM Family_member WHERE Id_family = '{0}'", Id_family);
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
            Question f = new Question();
            f.ShowDialog();

            if (!f.access)
                return;

            job = textBox1.Text;
            salary = textBox2.Text;
            contact = textBox3.Text;

            if (job == "")
            {
                MessageBox.Show("Не заполнено поле 'Работа'", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (salary == "")
            {
                MessageBox.Show("Не заполнено поле 'Зарплата'", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (contact == "")
            {
                MessageBox.Show("Не заполнено поле 'Контакты'", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            acc = true;

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
                string strSql = String.Format(@"INSERT INTO Request(Job, Salary, Contact, Id_personal, Id_manager, Id_service, Id_family) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}')", job, salary, contact, Id_personal, Id_manager, Id_service, Id_family);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cn.Close();
            MessageBox.Show("Заявка успешно отправлена на рассмотрение", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            button5.Visible = false;
            label6.Visible = false;
            dataGridView1.Visible = false;
        }

    }
}
