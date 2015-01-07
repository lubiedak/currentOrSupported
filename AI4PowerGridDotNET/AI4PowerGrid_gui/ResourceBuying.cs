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
    public partial class ResourceBuying : Form
    {
        Dictionary<R.ResType, List<int>> free_places_on_pp_;
        Dictionary<PP.PPtype, int> free_places_on_pp_co_;
        Dictionary<R.ResType, int> available_on_resource_market_;
        int money_;
        public Dictionary<R.ResType, int> boughted_resources;
        public int money_spent;

        public ResourceBuying(Dictionary<R.ResType, List<int>> free_places_on_pp,
            Dictionary<PP.PPtype, int> free_places_on_pp_co, 
            Dictionary<R.ResType, int> available_on_resource_market,
            int money)
        {
            InitializeComponent();
            free_places_on_pp_co_ = free_places_on_pp_co;
            free_places_on_pp_ = free_places_on_pp;
            available_on_resource_market_ = available_on_resource_market;
            money_ = money;
            money_spent = 0;
            boughted_resources = new Dictionary<R.ResType, int>();
            EnableNeeded();
        }

        private void EnableNeeded()
        {
            if (free_places_on_pp_.ContainsKey(R.ResType.COAL))
            {
                numericCoal.Enabled = true;
                numericCoal.Maximum = Math.Min( free_places_on_pp_[R.ResType.COAL].Count(),
                                                available_on_resource_market_[R.ResType.COAL]);
            }
            if(free_places_on_pp_.ContainsKey(R.ResType.OIL))
            {
                numericOil.Enabled = true;
                numericOil.Maximum = Math.Min(  free_places_on_pp_[R.ResType.OIL].Count(),
                                                available_on_resource_market_[R.ResType.OIL]);
            }
            if (free_places_on_pp_.ContainsKey(R.ResType.GARBAGE))
            {
                numericGarbage.Enabled = true;
                numericGarbage.Maximum = Math.Min(  free_places_on_pp_[R.ResType.GARBAGE].Count(),
                                                    available_on_resource_market_[R.ResType.GARBAGE]);
            }
            if (free_places_on_pp_.ContainsKey(R.ResType.URANIUM))
            {
                numericUranium.Enabled = true;
                numericUranium.Maximum = Math.Min(  free_places_on_pp_[R.ResType.URANIUM].Count(),
                                                    available_on_resource_market_[R.ResType.URANIUM]);
            }
        }

        private void numericCoal_ValueChanged(object sender, EventArgs e)
        {
            int request = (int)numericCoal.Value;
            int price = 0;
            for (int i = 0; i < request; i++)
            {
                price += free_places_on_pp_[R.ResType.COAL][i];
            }
            CoalPrice.Text = price.ToString();
            CountSumPrice();
        }

        private void numericOil_ValueChanged(object sender, EventArgs e)
        {
            int request = (int)numericOil.Value;
            int price = 0;
            for (int i = 0; i < request; i++)
            {
                price += free_places_on_pp_[R.ResType.OIL][i];
            }
            OilPrice.Text = price.ToString();
            CountSumPrice();
        }

        private void numericGarbage_ValueChanged(object sender, EventArgs e)
        {
            int request = (int)numericGarbage.Value;
            int price = 0;
            for (int i = 0; i < request; i++)
            {
                price += free_places_on_pp_[R.ResType.GARBAGE][i];
            }
            GarbagePrice.Text = price.ToString();
            CountSumPrice();
        }

        private void numericUranium_ValueChanged(object sender, EventArgs e)
        {
            int request = (int)numericUranium.Value;
            int price = 0;
            for (int i = 0; i < request; i++)
            {
                price += free_places_on_pp_[R.ResType.URANIUM][i];
            }
            UraniumPrice.Text = price.ToString();
            CountSumPrice();
        }

        private void CountSumPrice()
        {
            int sumprice = Convert.ToInt16(CoalPrice.Text);
            sumprice += Convert.ToInt16(OilPrice.Text);
            sumprice += Convert.ToInt16(GarbagePrice.Text);
            sumprice += Convert.ToInt16(UraniumPrice.Text);

            SumPrice.Text = sumprice.ToString();
        }

        private void BuyButt_Click(object sender, EventArgs e)
        {
            if (CheckIfNotToManyResources() && HasEnoughMoney())
            {
                boughted_resources.Add(R.ResType.COAL, (int)numericCoal.Value);
                boughted_resources.Add(R.ResType.OIL, (int)numericOil.Value);
                boughted_resources.Add(R.ResType.GARBAGE, (int)numericGarbage.Value);
                boughted_resources.Add(R.ResType.URANIUM, (int)numericUranium.Value);
                money_spent = Convert.ToInt16(SumPrice.Text);
                this.Close();
            }
        }

        private bool HasEnoughMoney()
        {
            int sumprice = Convert.ToInt16(SumPrice.Text);
            if (sumprice > money_)
            {
                MessageBox.Show("You don't have enough money.",
                    "Important Note",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        private bool CheckIfNotToManyResources()
        {
            if (free_places_on_pp_co_.ContainsKey(PP.PPtype.CO))
            {
                int sum_requested_co = (int)(numericCoal.Value + numericOil.Value);
                if (free_places_on_pp_co_.ContainsKey(PP.PPtype.COAL))
                {
                    sum_requested_co -= free_places_on_pp_co_[PP.PPtype.COAL];
                }
                if (free_places_on_pp_co_.ContainsKey(PP.PPtype.OIL))
                {
                    sum_requested_co -= free_places_on_pp_co_[PP.PPtype.OIL];
                }

                if (sum_requested_co > free_places_on_pp_co_[PP.PPtype.CO])
                {
                    MessageBox.Show("You try to buy more resources than you may contain.",
                        "Important Note",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            return true;
        }

    }
}
