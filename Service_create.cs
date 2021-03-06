﻿using System;
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
    public partial class Service_create : Form
    {
        private string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        public string name, minrequi, description;
        public int Id_bank;
        private BindingSource bind = new BindingSource();
        private DataSet dataset = new DataSet();
        public bool access = false;
        
        public Service_create()
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
                
                string strSql = "SELECT Name, ViewBank, History, Adds, Telephone, Website, Id_bank FROM Bank";
                SqlCommand cmd = new SqlCommand(strSql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataset);
                cn.Close();
                dataGridView1.AutoGenerateColumns = true;
                bind.DataSource = dataset.Tables[0];
                dataGridView1.DataSource = bind;
                dataGridView1.Columns[6].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            access = true;
            name = textBox1.Text;
            minrequi= textBox2.Text;
            description = textBox3.Text;

            if (dataGridView1.CurrentRow == null)
            {
                access = false;
                MessageBox.Show(@"Не выбран Банк!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Id_bank = (int)dataGridView1.CurrentRow.Cells[6].Value;

            if (name == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Название'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (minrequi == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Мин требования'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (description == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Описание'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
