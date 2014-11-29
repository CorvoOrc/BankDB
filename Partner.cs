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
    public partial class Partner : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        private BindingSource bind1 = new BindingSource();
        private DataSet dataset1 = new DataSet();
        private BindingSource bind2 = new BindingSource();
        private DataSet dataset2 = new DataSet();
        private string name, adds, telephone, website, description;
        private int Id_partner;
        private int Id_bank;
        public Partner()
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
                string strSql = "SELECT Name, Adds, Telephone, Website, Description, Id_partner FROM Partner";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[5].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Partner_create dialog = new Partner_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
            name = dialog.name;
            adds = dialog.adds;
            telephone = dialog.adds;
            website = dialog.website;
            description = dialog.description;
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
                string strSql = String.Format(@"INSERT INTO Partner(Name, Adds, Telephone, Website, Description) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", name, adds, telephone, website, description);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str = "SELECT Name, Adds, Telephone, Website, Description, Id_partner FROM Partner";
            SqlCommand cm = new SqlCommand(str, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            dataset.Reset();
            adapter.Fill(dataset);
           // cn.Close();
            dataGridView1.Update();
            bind.DataSource = dataset.Tables[0];
            dataGridView1.DataSource = bind;
            //Id_partner = bind.Count;
            int count = bind.Count;
            Id_partner = (int)dataset.Tables[0].Rows[count-1].ItemArray[5];
                try
                {
                    string strSql2 = String.Format(@"INSERT INTO ReferBP(Id_bank, Id_partner) VALUES ('{0}', '{1}')", Id_bank, Id_partner);
                    SqlCommand cmd = new SqlCommand(strSql2, cn);
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
           /* string str1 = "SELECT Id_bank, Id_partner, Id_bp FROM ReferBP";
            SqlCommand cm1 = new SqlCommand(str1, cn);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cm1);
            dataset1.Reset();
            adapter1.Fill(dataset1);
            // cn.Close();
            dataGridView2.Update();
            bind1.DataSource = dataset1.Tables[0];
            dataGridView2.DataSource = bind1;*/
            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Partner_create dialog = new Partner_create();
            dialog.ShowDialog();

            if (!dialog.access)
                return;
            name = dialog.name;
            adds = dialog.adds;
            telephone = dialog.adds;
            website = dialog.website;
            description = dialog.description;
            //Id_bank = dialog.Id_bank;

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
            {//SELECT Name, Adds, Telephone, Website, Description, Id_partner FROM Partner
                Id_partner = (int)dataGridView1.CurrentRow.Cells[5].Value;
                string strSql = String.Format(@"UPDATE Partner SET 
                Name = '{0}', Adds = '{1}', Telephone = '{2}', Website = '{3}', Description = '{4}' WHERE Id_partner = '{5}'", name, adds, telephone,
                website, description, Id_partner);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str = "SELECT Name, Adds, Telephone, Website, Description, Id_partner FROM Partner";
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

            Id_partner = (int)dataGridView1.CurrentRow.Cells[5].Value;

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
                //DELETE FROM emails WHERE id='2';
                string strSql = String.Format(@"DELETE FROM Partner WHERE Id_partner = '{0}'", Id_partner);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                //DELETE FROM emails WHERE id='2';
                string strSql = String.Format(@"DELETE FROM ReferBP WHERE Id_partner = '{0}'", Id_partner);
                SqlCommand cmd = new SqlCommand(strSql, cn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string str = "SELECT Name, Adds, Telephone, Website, Description, Id_partner FROM Partner";
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
            JoinBP dialog = new JoinBP();
            dialog.ShowDialog();
        }
    }
}
