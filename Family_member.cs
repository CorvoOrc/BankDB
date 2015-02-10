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
    public partial class Family_member : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        public int Id_family, Id_family_member;
        
        public Family_member()
        {
            InitializeComponent();
            
            FileInfo fi1 = new FileInfo("cur_req.txt");
            using (StreamReader sr = fi1.OpenText())
            {
                string s = "";
                s = sr.ReadLine();
                sr.Close();
                Id_family = Convert.ToInt32(s);
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

                string str = String.Format(@"SELECT t1.Family_member_field as 'Family memder', t1.Activities as 'Activities', t1.Age as 'Age', t2.Surname as 'Surname' FROM Family t2 INNER JOIN Family_member t1 ON t1.Id_family=t2.Id_family WHERE t2.Id_family = '{0}'", Id_family);
                SqlCommand cm = new SqlCommand(str, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                dataset.Reset();
                adapter.Fill(dataset);
                bind.DataSource = dataset.Tables[0];
                dataGridView1.Update();
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = bind;
                cn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
