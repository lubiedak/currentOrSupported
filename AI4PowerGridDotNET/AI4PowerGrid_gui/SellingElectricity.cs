using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI4PowerGrid_gui
{
    public partial class SellingElectricity : Form
    {
        Dictionary<String, List<int>> power_plants;
        public int profit;
        int cities_count;
        int[] eprices;

        public SellingElectricity(Dictionary<String, List<int>> power_plants, int[] prices, int cities_count)
        {
            profit = 0;
            this.cities_count = cities_count;
            eprices = prices;
            InitializeComponent();
            this.power_plants = power_plants;
            foreach (String pp in this.power_plants.Keys)
            {
                PPList.Items.Add(pp);
            }
            CalculateProfit();
        }

        private void SellButton_Click(object sender, EventArgs e)
        {
            CalculateProfit();
            this.Close();
        }

        private void CalculateProfit()
        {
            int sum = 0;
            for (int i = 0; i < PPList.Items.Count; i++) 
            {
                CheckState st = PPList.GetItemCheckState(i);
                if (st == CheckState.Checked)
                {
                    sum += power_plants[PPList.Items[i].ToString()][1];
                    power_plants[PPList.Items[i].ToString()][2] = 1;
                }
                else
                {
                    power_plants[PPList.Items[i].ToString()][2] = 0;
                }
            }
            profit = eprices[Math.Min(sum, cities_count)];
            EarnedMoney.Text = profit.ToString();
            Invalidate();
        }

        private void PPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateProfit();
        }
    }
}
