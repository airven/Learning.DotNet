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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void MessageReceived(object sender, EventArgs e)
        {
            Person person =  (Person)(e);
            label1.Text = $"this name is {person.Name} and address is {person.Address} and company is {person.CompanyName}";
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
