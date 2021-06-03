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
        PartyDinner partydinner;
        public SpiritKitchen()
        {
            InitializeComponent();
            dinner = new Dinner((int)dinnerNumericUpDown.Value);
            dinner.SetwineOption(dinnerWinerCheckBox.Checked);
            DisplayDinnerCost();

            partydinner = new PartyDinner((int)partyNuMericUpDown.Value);
            partydinner.SetwineOption(partyWineCheckBox.Checked);
            partydinner.SetFinceDecorationsOption(partyfinceCheckBox.Checked);
            DisplayPartyDinnerCost();
        }

        private void dinnerNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            //设计总人数
            dinner = new Dinner((int)dinnerNumericUpDown.Value);
            dinner.SetwineOption(dinnerWinerCheckBox.Checked);
        }

        private void dinnerMemberCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //设置是否是会员
            dinner.customer.SetMemberOption(dinnerMemberCheckBox.Checked);
            DisplayDinnerCost();
        }

        private void dinnerWinerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dinner.SetwineOption(dinnerWinerCheckBox.Checked);
            DisplayDinnerCost();
        }

        private void DisplayDinnerCost()
        {
            decimal Cost = dinner.Calculate();
            Costlabel.Text = Cost.ToString();
        }

        private void DisplayPartyDinnerCost()
        {
            decimal Cost = partydinner.Calculate();
            partyCostlable.Text = Cost.ToString("c");
        }

        private void partyNuMericUpDown_ValueChanged(object sender, EventArgs e)
        {
            partydinner.NumberofPeople = (int)partyNuMericUpDown.Value;
            DisplayPartyDinnerCost();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            partydinner.customer.SetMemberOption(partyMemberCheckBox.Checked);
            DisplayPartyDinnerCost();
        }

        private void partyWineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            partydinner.SetwineOption(partyWineCheckBox.Checked);
            DisplayPartyDinnerCost();
        }

        private void partyfinceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            partydinner.SetFinceDecorationsOption(partyfinceCheckBox.Checked);
            DisplayPartyDinnerCost();
        }
    }
}
