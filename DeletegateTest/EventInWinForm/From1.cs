using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeletegateTest.EventInWinForm
{
    public partial class From1 : Form
    {
        public delegate void SendMessage(object obj, EventArgs e);
        public event SendMessage OnSendMessage;

        public From1()
        {
            InitializeComponent();
        }

        Form2 child;
        private void button1_Click(object sender, EventArgs e)
        {
            child = new Form2();
            OnSendMessage += child.MessageReceived;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (OnSendMessage != null)
            {
                Person p = new Person();
                p.Name = textBox1.Text;
                p.Address = textBox2.Text;
                p.CompanyName = textBox3.Text;
                OnSendMessage(this, p);
                child.Show();
            }
        }
    }
}
