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
    public partial class Form1 : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        public bool acc = false;
        public int Id_personalGl, Id_bankGl, Id_serviceGl, Id_managerGl, Id_requestGl;
        public Form1()
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
                string strSql = @"SELECT * FROM information_schema.tables WHERE Table_type='BASE TABLE' AND TABLE_NAME='Personal'";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                using (SqlDataReader dread = cmd.ExecuteReader())
                {
                    if (!dread.HasRows)
                    {
                        dread.Close();
                        try
                        {
                            string tableCreation = @"CREATE TABLE Personal(
                            Surname varchar(50) NOT NULL, 
                            Name varchar(50) NOT NULL, 
                            Patronymic varchar(50) NOT NULL, 
                            Telephone varchar(50) NOT NULL, 
                            Age int NOT NULL, 
                            Sex varchar(50) NOT NULL, 
                            Birthday varchar(50) NOT NULL, 
                            Id_personal int IDENTITY(1,1) PRIMARY KEY)";
                            SqlCommand command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();
                            tableCreation = @"CREATE TABLE Bank( 
                            Name varchar(50) NOT NULL, 
                            ViewBank varchar(50) NOT NULL, 
                            History varchar(50) NOT NULL, 
                            Adds varchar(50) NOT NULL, 
                            Telephone varchar(50) NOT NULL, 
                            Website varchar(50) NOT NULL, 
                            Id_bank int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();
                            tableCreation = @"CREATE TABLE Partner(
                            Name varchar(50) NOT NULL, 
                            Adds varchar(50) NOT NULL, 
                            Telephone varchar(50) NOT NULL,
                            Website varchar(50) NOT NULL, 
                            Description varchar(50) NOT NULL,
                            Id_partner int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();
                            tableCreation = @"CREATE TABLE Manager(
                            Surname varchar(50) NOT NULL, 
                            Name varchar(50) NOT NULL, 
                            Patronymic varchar(50) NOT NULL, 
                            Age varchar(50) NOT NULL, 
                            Id_bank int NOT NULL, 
                            Id_manager int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();
                            tableCreation = @"CREATE TABLE Service(
                            Name varchar(50) NOT NULL, 
                            MinRequirement varchar(50) NOT NULL, 
                            Description varchar(50) NOT NULL, 
                            id_bank int NOT NULL,
                            id_service int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();
                            tableCreation = @"CREATE TABLE Request(
                            Job varchar(50) NOT NULL, 
                            Salary varchar(50) NOT NULL, 
                            Contact varchar(50) NOT NULL,
                            Id_personal int NOT NULL, 
                            Id_manager int NOT NULL, 
                            Id_service int NOT NULL,
                            Id_family int NOT NULL,
                            Id_request int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();

                            tableCreation = @"CREATE TABLE Family( 
                            Surname varchar(50) NOT NULL,
                            Id_family int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();

                            tableCreation = @"CREATE TABLE Family_member(
                            Family_member_field varchar(50) NOT NULL, 
                            Activities varchar(50) NOT NULL, 
                            Age int NOT NULL,
                            Id_family int NOT NULL,
                            Id_family_member int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();

                            tableCreation = @"CREATE TABLE ReferBP( 
                            Id_bank int NOT NULL, 
                            Id_partner int NOT NULL, 
                            Id_bp int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();

                            tableCreation = @"CREATE TABLE Account(
                            Login varchar(50) NOT NULL, 
                            Password varchar(50) NOT NULL,
                            Id_account int IDENTITY(1,1) PRIMARY KEY)";
                            command = new SqlCommand(tableCreation, cn);
                            command.ExecuteNonQuery();
                        }
                        catch (InvalidOperationException ex)
                        {
                            MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            cn.Close();
                        }

                    }
                }

            }
        }

        private void банковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Personal f = new Personal();
            f.ShowDialog();
        }

        private void банковToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bank dialog = new Bank();
            dialog.ShowDialog();
        }

        private void партнеровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Partner dialog = new Partner();
            dialog.ShowDialog();
        }

        public void оформитьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Personal_client f = new Personal_client();
            f.ShowDialog();

            Id_personalGl = f.Id_personal;

            FileInfo fi = new FileInfo("personal.txt");
            using (StreamWriter sw = fi.CreateText())
            {
                sw.WriteLine(Id_personalGl.ToString());
                sw.Close();
            }

            Bank_client f1 = new Bank_client();
            f1.ShowDialog();

            Id_bankGl = f1.Id_bank;

            FileInfo fi1 = new FileInfo("bank.txt");
            using (StreamWriter sw = fi1.CreateText())
            {
                sw.WriteLine(Id_bankGl.ToString());
                sw.Close();
            }

            Partner_client f2 = new Partner_client();
            f2.ShowDialog();

            Service_client f3 = new Service_client();
            f3.ShowDialog();

            Id_serviceGl = f3.Id_service;

            FileInfo fi2 = new FileInfo("service.txt");
            using (StreamWriter sw = fi2.CreateText())
            {
                sw.WriteLine(Id_serviceGl.ToString());
                sw.Close();
            }

            Manager_client f4 = new Manager_client();
            f4.ShowDialog();

            Id_managerGl = f4.Id_manager;

            FileInfo fi3 = new FileInfo("manager.txt");
            using (StreamWriter sw = fi3.CreateText())
            {
                sw.WriteLine(Id_managerGl.ToString());
                sw.Close();
            }

            while (true)
            {
                Request_client f5 = new Request_client();
                f5.ShowDialog();
                if (f5.acc)
                    break;
            }

        }

        private void менеджеровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager dialog = new Manager();
            dialog.ShowDialog();
        }

        private void услугToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Service dialog = new Service();
            dialog.ShowDialog();
        }

        private void заявокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Request dialog = new Request();
            dialog.ShowDialog();
        }

        private void запросыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Query_menu dialog = new Query_menu();
            dialog.ShowDialog();
        }

        private void запросыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Query_menu dialog = new Query_menu();
            dialog.ShowDialog();
        }

        private void отчетToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Report dialog = new Report();
            dialog.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "quest";
            textBox2.Text = "*****";

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Visible = false;
            menuToolStripMenuItem.Visible = true;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            button4.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show(@"Введите логин!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show(@"Введите пароль!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string login = textBox1.Text;
            string password = textBox2.Text;
            BindingSource bind = new BindingSource();
            DataSet dataset = new DataSet();
            bool acc = false;
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
                string strSql = "SELECT Login, Password FROM Account";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                bind.DataSource = dataset.Tables[0];
                int count = bind.Count;
                for (int i = 0; i < count; ++i)
                {
                    if ((string)dataset.Tables[0].Rows[i].ItemArray[0] == login && (string)dataset.Tables[0].Rows[i].ItemArray[1] == password)
                    {
                        acc = true;
                        break;
                    }
                }
                cn.Close();
            }

            if (acc)
            {
                textBox2.Text = "*****";
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Visible = false;
                администрированиеToolStripMenuItem.Visible = true;
                отчетToolStripMenuItem.Visible = true;
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                button4.Visible = true;
            }
            else
            {
                MessageBox.Show(@"Не правильно введен логин или пароль!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            menuToolStripMenuItem.Visible = false;
            администрированиеToolStripMenuItem.Visible = false;
            отчетToolStripMenuItem.Visible = false;

            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;

            textBox1.Text = "";
            textBox2.Text = "";

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Visible = true;
            button4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration dialog = new Registration();
            dialog.ShowDialog();

        }
    }
}
