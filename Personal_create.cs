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
    public partial class Personal_create : Form
    {
        public string surname, name, patronymic, telephone, sex, birthday;
        public int age;
        public bool access=false;
        //public string address = @"Data Source=(local)\SQLEXPRESS;AttachDbFilename=C:\Users\Tex\Documents\database.mdf;Initial Catalog=database;Integrated Security=True;Connect Timeout=50;User Instance=True";
        //public BindingSource bind = new BindingSource();
        //public DataSet dataset = new DataSet();
        
        public Personal_create()
        {
            InitializeComponent();
        }

       private void button1_Click(object sender, EventArgs e)
        {
            access = true;
            surname = textBox1.Text;
            name = textBox2.Text;
            patronymic = textBox3.Text;
            telephone = textBox4.Text;
            birthday = textBox5.Text;
            age = (int)numericUpDown1.Value;
            sex = comboBox1.Text;

            if (surname == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Фамилия'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (name == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Имя'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (patronymic == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Отчество'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (telephone == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Телефон'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (birthday == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'День Рождения'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sex == "")
            {
                access = false;
                MessageBox.Show(@"Не выбран Пол!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
