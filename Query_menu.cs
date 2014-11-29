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
    public partial class Query_menu : Form
    {
        public Query_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Query1 dialog = new Query1();
            dialog.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Query2 dialog = new Query2();
            dialog.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Query3 dialog = new Query3();
            dialog.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Query4 dialog = new Query4();
            dialog.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Query5 dialog = new Query5();
            dialog.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Query6 dialog = new Query6();
            dialog.Show();
        }

    }
}
