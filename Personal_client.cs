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
    public partial class Personal_client : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private string surname, name, patronymic, telephone, sex, birthday;
        private int age;
        public int Id_personal;
        
        public Personal_client()
        {
            InitializeComponent();
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
            
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            //label3.Visible = false;
            label1.Text = "Добро пожаловать, " + surname + " " + name + "!";
            label3.Text = "Измените Ваши персональные данные или перейдите к следующему шагу:";
            string str = "SELECT Surname, Name, Patronymic, Telephone, Age, Sex, Birthday, Id_personal FROM Personal";
            SqlCommand cm = new SqlCommand(str, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            dataset.Reset();
            adapter.Fill(dataset);
            cn.Close();
            bind.DataSource = dataset.Tables[0];
            int count = bind.Count;
            Id_personal = (int)dataset.Tables[0].Rows[count - 1].ItemArray[7];
        }

        private void button2_Click(object sender, EventArgs e)
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
            
            label1.Text = "Добро пожаловать, " + surname + " " + name + "!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
