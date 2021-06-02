using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spiritkitchen
{
    public partial class SpiritKitchen : Form
    {
        Dinner dinner;
        public SpiritKitchen()
        {
            InitializeComponent();
        }

        private void dinnerNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            dinner = new Dinner((int)dinnerNumericUpDown.Value);
            dinner.SetwineOption(dinnerWinerCheckBox.Checked);
        }

        private void dinnerMemberCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dinner.numberofPeople = (int)dinnerNumericUpDown.Value;
            DisplayDinnerCost();
        }

        private void dinnerWinerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dinner.SetwineOption(dinnerWinerCheckBox.Checked);
        }

        private void DisplayDinnerCost()
        {
            decimal Cost = dinner.Calculate();
            Costlabel.Text = Cost.ToString();
        }
    }
}
