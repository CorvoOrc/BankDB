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
    public partial class Registration : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        public string login;
        public string password;
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (login == "")
            {
                MessageBox.Show("Не заполнено поле 'Логин'", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (password == "")
            {
                MessageBox.Show("Не заполнено поле 'Пароль'", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                string strSql = "SELECT Login FROM Account";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                bind.DataSource = dataset.Tables[0];
                int count = bind.Count;
                for (int i = 0; i < count; ++i)
                {
                    if ((string)dataset.Tables[0].Rows[i].ItemArray[0] == login)
                    {
                        MessageBox.Show(@"Логин уже существует!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                try
                {
                    string strSql1 = String.Format(@"INSERT INTO Account(Login, Password) VALUES ('{0}', '{1}')", login, password);
                    SqlCommand cmd1 = new SqlCommand(strSql1, cn);
                    cmd1.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                cn.Close();
            }
            MessageBox.Show(@"Регистрация успешно завершена!", @"Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
