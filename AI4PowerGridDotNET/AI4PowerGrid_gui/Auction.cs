using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AI4PowerGrid_gui
{
    class Auction
    {
        List<Bidder> bidders_;
        Graphics track_;
        int height_;
        int active_;
        public int actual_bid_;
        public int last_bid_;

        public Auction(List<Bidder> bidders, Graphics track, int height, int min_bid)
        {
            bidders_ = bidders;
            track_ = track;
            height_ = height;
            
            active_ = 0;
            actual_bid_ = min_bid;
            last_bid_ = min_bid;
            bidders_[0].Activate(true);

            Bid(min_bid);
        }

        public void Display()
        {
            for (int i = 0; i < bidders_.Count(); i++)
            {
                bidders_[i].Draw(track_, i * (height_ + 5), 0, height_); 
            }
        }
        private void ClearTrack()
        {
            track_.Clear(SystemColors.Control);
        }

        public void Bid(int b)
        {
            bidders_[active_].Bid(b);
            last_bid_ = b;
            actual_bid_ = b+1;
            NextBidder();
        }

        public void Pass()
        {
            if (!AuctionEnded())
            {
                bidders_.RemoveAt(active_);
                if (active_ == bidders_.Count())
                    active_=0;
                bidders_[active_].Activate(true);

                RunAI();
                ClearTrack();
            }
        }

        private void NextBidder()
        {
            bidders_[active_].Activate(false);

            if (active_ == bidders_.Count() - 1)
            {
                active_ = 0;
            }
            else
            {
                active_++;
            }

            bidders_[active_].Activate(true);

            RunAI();
        }

        private void RunAI()
        {
            if (bidders_[active_].IsAI() && bidders_.Count() > 1)
            {
                if (bidders_[active_].BidOrPass_AI(actual_bid_))
                {
                    Bid(actual_bid_);
                }
                else
                {
                    Pass();
                }
            }
        }

        public List<int> PossibleBids()
        {
            List<int> possible_bids = new List<int>();

            if (actual_bid_ >= bidders_[active_].GetMoney() && !AuctionEnded())
            {
                Pass();
                possible_bids = PossibleBids();
            }

            else
            {    //creating list
                for (int i = actual_bid_; i < bidders_[active_].GetMoney(); i++)
                {
                    possible_bids.Add(i);
                }
            }
            return possible_bids;
        }

        public bool AuctionEnded()
        {
            return bidders_.Count() == 1;
        }

        public Player GetBuyer()
        {
            return bidders_[0].GetPlayer();
        }
    }




    class Bidder
    {
        Color color_;
        Player player_;
        int money_;
        int bid_;
        bool is_active_;
        bool is_ai;
        int max_bid_;

        public Bidder(Player p, int max_bid)
        {
            player_ = p;
            color_ = p.GetColor();
            money_ = p.GetMoney();
            bid_ = 0;
            is_active_ = false;
            is_ai = p.type_ == "AI" ? true : false;
            max_bid_ = max_bid;
        }

        public void Activate(bool act) { is_active_ = act; }
        public Player GetPlayer() { return player_; }

        public void Draw(Graphics g, int x, int y, int size)
        {
            g.FillRectangle( new SolidBrush(color_), x, y, size, size);
            g.DrawString(bid_.ToString(), new Font("Arial Black", 11), new SolidBrush(Color.Black), x + 2, y + 5);
            if(is_active_)
            {
                Pen pen = new Pen(Color.Red, 4);
                pen.Alignment = PenAlignment.Inset; 
                g.DrawRectangle(pen, x, y, size, size);
            }
        }

        public bool IsAI(){ return is_ai; }

        public bool BidOrPass_AI(int b)
        {
            if (b >= max_bid_)
            {
                return false;
            }
            else
            {
                bid_ = b;
                return true;
            }
        }
        public void Bid(int b){bid_ = b; }
        public int GetMoney() { return money_; }
    }
}
