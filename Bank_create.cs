using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FilingRequestInBank
{
    public partial class Bank_create : Form
    {
        public string name, viewbank, history, adds, telephone, website;
        public bool access=false;
        
        public Bank_create()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            access = true;
            name = textBox1.Text;
            viewbank = textBox2.Text;
            history = textBox3.Text;
            adds = textBox4.Text;
            telephone = textBox5.Text;
            website = textBox6.Text;

            if (name == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Название'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (viewbank == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Вид банка'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (history == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'История'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (adds == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Адрес'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (telephone == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Телефон'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (website == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Веб сайт'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
