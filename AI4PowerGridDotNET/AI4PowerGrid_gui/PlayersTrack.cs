using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace AI4PowerGrid_gui
{
    class PlayersTrack
    {
        List<Player> players_;
        int active;
        public int max_pp;
        Graphics g_track;
        Graphics g_players;

        public PlayersTrack(List<String> players_list, Bitmap order_track, Bitmap players_pp)
        {
            players_ = CreatePlayers(players_list);
            active = 0;
            players_[active].Activate();
            g_track = Graphics.FromImage(order_track);
            g_players = Graphics.FromImage(players_pp);
            g_players.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        private List<Player> CreatePlayers(List<String> players_list)
        {
            List<Player> players = new List<Player>();
            int pl_n = 0;
            for (int i = 0; i < players_list.Count(); i++)
            {
                if (players_list[i] != "Closed")
                {
                    players.Add(new Player(pl_n, 3, players_list[i]));
                    pl_n++;
                }
            }
            max_pp = (pl_n == 2) ? 4 : 3;
            foreach (Player p in players)
            {
                p.max_n_of_pp_ = max_pp;
            }
            return players;
        }

        public List<Bidder> CreateBidders(int min_bid, List<int> already_bought_pp, ResourcesMarket res_market, PowerPlantMarket pp_market, PowerPlant_gui selected_pp)
        {
            List<Bidder> bidders = new List<Bidder>();
            int max_bid = CalculateMaxBid_AI(pp_market, res_market, selected_pp);
            foreach(Player player in players_)
            {
                if (!already_bought_pp.Exists(x => x == player.id_))
                {
                    bidders.Add(new Bidder(player, max_bid));
                }
            }
            return bidders;
        }

        private int CalculateMaxBid_AI(PowerPlantMarket pp_market, ResourcesMarket res_market, PowerPlant_gui selected_pp)
        {
            int price = selected_pp.getIdPrice();
            
            int best_note = pp_market.GetBestNote();
            while(selected_pp.CalculateNote(res_market, price) > best_note)
            {
                price++;
            }

            return Math.Min(price, (int)(1.8*selected_pp.getIdPrice()));
        }

        public void NextPlayer(List<int> already_bought_pp)
        {
            players_[active].Deactivate();

            while (active < players_.Count && already_bought_pp.Contains(players_[active].id_))
                active++;


            if (active == players_.Count)
            {
                players_[active - 1].Deactivate();
                active = 0;
                players_[active].Activate();
            }
            else
            {
                players_[active].Activate();
            }
            Display();
        }

        public void PreviousPlayer()
        {
            players_[active--].Deactivate();
            if (active == -1)
            {
                active = players_.Count()-1;
                players_[active].Activate();
            }
            else
            {
                players_[active].Activate();
            }
            Display();
        }

        public int GetBestPlayerNetSize()
        {
            int best = 0;
            foreach (Player p in players_)
            {
                if (best < p.getNOfCities())
                {
                    best = p.getNOfCities();
                }
            }
            return best;
        }

        public void ActivateLast()
        {
            players_[active].Deactivate();
            active = players_.Count() - 1;
            players_[active].Activate();
            Display();
        }

        public bool AnyoneEnded()
        {
            return players_.Any(p => p.getNOfCities() >= (players_.Count == 2 ? 21 : 17));
        }

        public void ActiveFirst()
        {
            players_[active].Deactivate();
            active = 0;
            players_[active].Activate();
            Display();
        }

        public void Deactivate()
        {
            players_[active].Deactivate();
            Display();
        }

        public void UpdateOrder()
        {
            Deactivate();
            List<Player> temp = players_.OrderByDescending(x => x.getNOfCities()).ThenByDescending(x => x.GetBestPowerPlant()).ToList();
            players_ = temp;
        }

        public void WinnerOrder()
        {
            Deactivate();
            List<Player> temp = players_.OrderByDescending(x => x.LastPowerSupply()).ThenByDescending(x => x.GetMoney()).ToList();
            players_ = temp;
        }

        public void UpdateOrderNoPP()
        {
            Deactivate();
            List<Player> temp = players_.OrderByDescending(x => x.getNOfCities()).ThenByDescending(x => x.id_).ToList();
            players_ = temp;
        }

        public Player GetActivePlayer()
        {
            return players_[active];
        }
        public int GetActivePlayerId()
        {
            return players_[active].id_;
        }
        public int GetActivePlayerPosition()
        {
            return active;
        }

        public Player GetPlayerById(int id)
        {
            return players_.Find(x => x.id_ == id);
        }

        public Player GetLastPlayer()
        {
            return players_[players_.Count()-1];
        }

        public bool ActiveIsAI()
        {
            return GetActivePlayer().isAI();
        }

        public void Display()
        {
            g_track.Clear(SystemColors.Control);
            int size = DefaultValues.Player_size;
            for (int i = 0; i < players_.Count; i++)
            {
                players_[i].Draw(g_track, 3 * i * size / 2 + size, size / 2);
            }
            DisplayPlayersBoard();
        }

        public void DisplayPlayersBoard()
        {
            int size = DefaultValues.PP_frame_size;
            g_players.Clear(GameFlow.active_phase == phase.SELLING_ELECTRICITY ? Color.LightBlue : SystemColors.Control);
            for (int i = 0; i < players_.Count(); i++)
            {
                int x = size / 2 + 3 * i * size / 2;
                players_[i].DrawPowerPlants(g_players, x, players_.Count());
            }
        }

        public void UpdateNotes(ResourcesMarket res_market)
        {
            foreach (Player p in players_)
            {
                p.UpdateNotes_AI(res_market);
            }
        }

        public void AddMoney(int money)
        {
            foreach (Player p in players_)
            {
                p.money_ += money;
            }
        }

        public void GiveMoney(int money)
        {
            foreach (Player p in players_)
            {
                p.money_ = money;
            }
        }

        public void Clean()
        {
            foreach (Player p in players_)
            {
                p.owned_cities_ = new List<City_gui>();
                p.money_ = 0;
                p.money_spended_ = 0;
            }
        }

        public void CheckForNextStage()
        {
            if (GameFlow.step == 0 && players_.Any(x => x.getNOfCities() >= (players_.Count() == 2 ? 10 : 7)))
            {
                GameFlow.step++;
            }
            if (GameFlow.step == 1 && players_.Any(x => x.getNOfCities() >= (players_.Count() == 2 ? 16 : 12)))
            {
                GameFlow.step++;
            }
        }

        public List<Player> GetPlayersById()
        {
            return players_.OrderByDescending(x => x.id_).ToList();
        }

        public bool AreAllAI()
        {
            return players_.All(x => x.type_ == "AI");
        }

        public bool GameEnded()
        {
            return players_.Any(x => x.owned_cities_.Count() >= (players_.Count() == 2 ? 21 : 17));
        }
    }
}
