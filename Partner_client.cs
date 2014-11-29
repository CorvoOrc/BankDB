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
    public partial class Partner_client : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private BindingSource bind1 = new BindingSource();
        private DataSet dataset1 = new DataSet();
        public int Id_bank;
        public Partner_client()
        {
            InitializeComponent();
            FileInfo fi1 = new FileInfo("bank.txt");
            using (StreamReader sr = fi1.OpenText())
            {
                string s = "";
                s = sr.ReadLine();
                sr.Close();
                Id_bank = Convert.ToInt32(s);
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
                string strSql = String.Format(@"SELECT Id_partner FROM ReferBP WHERE Id_bank='{0}'", Id_bank);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                int count = bind.Count;
                for (int i=0; i < count; ++i)
                {
                   int tmp= (int) dataset.Tables[0].Rows[i].ItemArray[0];
                   string strSql1 = String.Format(@"SELECT Name, Adds, Telephone, Website, Description FROM Partner WHERE Id_partner = '{0}'", tmp);
                   cmd.CommandText = strSql1;
                   cmd.Connection = cn;
                   adapter.SelectCommand = cmd;
                   adapter.Fill(dataset1);
                }
                try
                {
                    bind1.DataSource = dataset1.Tables[0];
                    dataGridView1.DataSource = bind1;
                    cn.Close();
                }
                catch
                {
                    MessageBox.Show(@"Выполняйте все действия по порядку!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
