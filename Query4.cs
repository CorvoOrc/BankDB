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
    public partial class Query4 : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private string job;
        
        public Query4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            job = textBox1.Text;
            if (job == "")
            {
                MessageBox.Show(@"Не заполнено поле 'Работа'!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label3.Visible = false;
                dataGridView1.Visible = false;
                return;
            }
            
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
                
                string strSql = String.Format(@"SELECT t1.Job as 'Job', t2.Surname as 'Surname', t2.Name as 'Name', t2.Patronymic as 'Patronymic' FROM Personal t2 INNER JOIN Request t1 ON t1.Id_personal=t2.Id_personal WHERE t1.Job = '{0}'", job);
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
