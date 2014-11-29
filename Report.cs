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
    public partial class Report : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private BindingSource bind1 = new BindingSource();
        private DataSet dataset1 = new DataSet();
        private BindingSource bind2 = new BindingSource();
        private DataSet dataset2 = new DataSet();
        private BindingSource bind3 = new BindingSource();
        private DataSet dataset3 = new DataSet();
        private BindingSource bind4 = new BindingSource();
        private DataSet dataset4 = new DataSet();
        private BindingSource bind5 = new BindingSource();
        private DataSet dataset5 = new DataSet();
        private BindingSource bind6 = new BindingSource();
        private DataSet dataset6 = new DataSet();

        public Report()
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
                string strSql = "SELECT Surname, Name, Patronymic, Telephone, Age, Sex, Birthday FROM Personal";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;

                string strSql1 = "SELECT Name, ViewBank, History, Adds, Telephone, Website FROM Bank";
                SqlCommand cmd1 = new SqlCommand(strSql1, cn);
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                adapter1.Fill(dataset1);
                dataGridView2.AutoGenerateColumns = true;
                bind1.DataSource = dataset1.Tables[0];
                dataGridView2.DataSource = bind1;

                string strSql2 = "SELECT Name, Adds, Telephone, Website, Description FROM Partner";
                SqlCommand cmd2 = new SqlCommand(strSql2, cn);
                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                adapter2.Fill(dataset2);
                dataGridView3.AutoGenerateColumns = true;
                bind2.DataSource = dataset2.Tables[0];
                dataGridView3.DataSource = bind2;

                //string strSql3 = "SELECT Name, MinRequirement, Description FROM Service";
                string strSql3 = @"SELECT t1.Name as 'Name', t1.MinRequirement as 'Min Requirement', t1.Description as 'Description', t2.Name as 'Name Bank' FROM Bank t2 INNER JOIN Service t1 ON t1.Id_bank=t2.Id_bank";
                SqlCommand cmd3 = new SqlCommand(strSql3, cn);
                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                adapter3.Fill(dataset3);
                dataGridView4.AutoGenerateColumns = true;
                bind3.DataSource = dataset3.Tables[0];
                dataGridView4.DataSource = bind3;

                //string strSql4 = "SELECT Surname, Name, Patronymic, Age FROM Manager";
                string strSql4 = @"SELECT t1.Surname as 'Surname', t1.Name as 'Name', t1.Patronymic as 'Patronymic', t1.Age as 'Age',  t2.Name as 'Name Bank' FROM Bank t2 INNER JOIN Manager t1 ON t1.Id_bank=t2.Id_bank";
                SqlCommand cmd4 = new SqlCommand(strSql4, cn);
                SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4);
                adapter4.Fill(dataset4);
                dataGridView5.AutoGenerateColumns = true;
                bind4.DataSource = dataset4.Tables[0];
                dataGridView5.DataSource = bind4;

                //string strSql5 = "SELECT Family_member_field, Activities, Age FROM Family_member";
                string strSql5 = @"SELECT t1.Family_member_field as 'Family memder', t1.Activities as 'Activities', t1.Age as 'Age', t2.Surname as 'Surname' FROM Family t2 INNER JOIN Family_member t1 ON t1.Id_family=t2.Id_family";
                SqlCommand cmd5 = new SqlCommand(strSql5, cn);
                SqlDataAdapter adapter5 = new SqlDataAdapter(cmd5);
                adapter5.Fill(dataset5);
                dataGridView6.AutoGenerateColumns = true;
                bind5.DataSource = dataset5.Tables[0];
                dataGridView6.DataSource = bind5;

                //string strSql6 = "SELECT Job, Salary, Contact FROM Request";
                string strSql6 = @"SELECT t1.Job as 'Job', t1.Salary as 'Salary', t1.Contact as 'Contact', t2.Surname as 'Surname' FROM Personal t2 INNER JOIN Request t1 ON t1.Id_personal=t2.Id_personal";
                SqlCommand cmd6 = new SqlCommand(strSql6, cn);
                SqlDataAdapter adapter6 = new SqlDataAdapter(cmd6);
                adapter6.Fill(dataset6);
                dataGridView7.AutoGenerateColumns = true;
                bind6.DataSource = dataset6.Tables[0];
                dataGridView7.DataSource = bind6;
            }
        }
    }
}
