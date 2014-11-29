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
    public partial class Family : Form
    {
        public string family_member, activities;
        public int age;
        public bool access=false;
        public Family()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            access = true;
            family_member = comboBox1.Text;
            activities = textBox1.Text;
            age = (int)numericUpDown1.Value;

            if (family_member == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Член семьи'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (activities == "")
            {
                access = false;
                MessageBox.Show(@"Не заполенено поле 'Род деятельности'!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
