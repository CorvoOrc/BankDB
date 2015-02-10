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
    public partial class Query6 : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private int age;
        
        public Query6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            age = (int)numericUpDown1.Value;
            label3.Visible = true;
            dataGridView1.Visible = true;

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
                
                string strSql = String.Format(@"SELECT Surname, Name, Patronymic, Telephone, Age, Sex, Birthday FROM Personal WHERE Age>='{0}'", age);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                dataset.Reset();
                adapter.Fill(dataset);
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
