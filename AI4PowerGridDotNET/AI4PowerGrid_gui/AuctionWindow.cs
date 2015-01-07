using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameModel;

namespace AI4PowerGrid_gui
{
    partial class AuctionWindow : Form
    {
        public Auction auction_;
        Graphics pp_graphics_;
        Graphics bidders_track_graphics_;
        public PowerPlant_gui power_plant_;
        Bitmap pp_bitmap_;
        Bitmap bidders_track_;
        public Player winner_;


        public AuctionWindow( PowerPlant_gui pp, List<Bidder> bidders)
        {
            InitializeComponent();

            power_plant_ = new PowerPlant_gui(pp);

            pp_bitmap_ = new Bitmap(PPlant.ClientSize.Width, PPlant.ClientSize.Height);
            bidders_track_ = new Bitmap(BiddersTrack.ClientSize.Width, BiddersTrack.ClientSize.Height);

            pp_graphics_ = Graphics.FromImage(pp_bitmap_);
            bidders_track_graphics_ = Graphics.FromImage(bidders_track_);

            auction_ = new Auction(bidders,
                                    bidders_track_graphics_,
                                    BiddersTrack.Height,
                                    power_plant_.getIdPrice());

            
            FillComboBox(bidPrice, auction_.PossibleBids());
            winner_ = auction_.GetBuyer();
            power_plant_.setXY(40, 40);
            power_plant_.DrawPowerPlant(pp_graphics_);
            Actualize();
        }


        private void buttonBid_Click(object sender, EventArgs e)
        {
            auction_.Bid(Convert.ToInt32(bidPrice.Text));
            Actualize();
        }
        public void IsAuctionEnded()
        {
            if (auction_.AuctionEnded())
            {
                winner_ = auction_.GetBuyer();
                this.Close();
            }
        }

        private void Actualize()
        {
            auction_.Display();
            BiddersTrack.Invalidate();
            FillComboBox(bidPrice, auction_.PossibleBids());
        }

        private void FillComboBox(ComboBox combo, List<int> values)
        {
            combo.Items.Clear();
            if (values.Count() == 0)
            {
                buttonPass_Click(null, null);
            }
            foreach (int val in values)
            {
                combo.Items.Add(val);
            }
            combo.SelectedIndex = 0;
        }

        private void buttonPass_Click(object sender, EventArgs e)
        {
            auction_.Pass();
            IsAuctionEnded();
            Actualize();
        }

        private void PPlant_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(pp_bitmap_, 0, 0, pp_bitmap_.Width, pp_bitmap_.Height);
        }

        private void BiddersTrack_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bidders_track_, 0, 0, bidders_track_.Width, bidders_track_.Height);
        }
    }
}
