using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using GameModel;
using System.Windows.Forms;
using System.Threading;

namespace AI4PowerGrid_gui
{
    class GameUI
    {
        Board board_;
        PlayersTrack players_track_;
        PowerPlantMarket pp_market_;
        public ResourcesMarket res_market_;

        CsvDataProvider data_provider_;

        List<int> already_bought_pp;
        List<int> already_bought_res;
        List<City_gui> cities_selected_to_buy;
        public int cost_of_buying_selected_cities;

        int num_of_players_;
        int passed_;
        int selled_electricity;
        bool transformed_to_stage2;
        bool game_is_ended;

        InvalidateCallbackType invalidateAllDelegate_;

        public GameUI()
        {

        }

        public GameUI(  List<String> players,
                        Bitmap MapBoard,
                        Bitmap PPMarketBoard,
                        Bitmap resource_market,
                        Bitmap order_track,
                        Bitmap players_pp,
                        InvalidateCallbackType invalidateAllDelegate)
        {
            data_provider_ = new CsvDataProvider("../../ModelData/");
            InitPlayersTrack(players, order_track, players_pp);
            data_provider_.LoadAllData(num_of_players_);
            InitBoard(MapBoard);
            InitResourceMarket(resource_market);
            InitPPMarket(PPMarketBoard);

            already_bought_pp = new List<int>();
            already_bought_res = new List<int>();
            cities_selected_to_buy = new List<City_gui>();
            cost_of_buying_selected_cities = 0;
            passed_ = 0;
            selled_electricity = 0;
            transformed_to_stage2 = false;
            game_is_ended = false;
            invalidateAllDelegate_ = invalidateAllDelegate;
        }

        private void InitBoard(Bitmap MapBoard)
        {
            board_ = new Board(data_provider_, MapBoard);
            
            board_.Display();
        }
        private void InitPPMarket(Bitmap pp_market)
        {
            pp_market_ = new PowerPlantMarket(pp_market, data_provider_.GetPowerPlants(),
                            res_market_,
                            pp_market.Width,
                            pp_market.Height);
            pp_market_.PrepareMarket(num_of_players_);
            pp_market_.Display();
        }

        private void InitResourceMarket(Bitmap resource_market)
        {
            res_market_ = new ResourcesMarket(data_provider_.GetResourceStartPositions(),
                                              data_provider_.GetResourceFilling(),
                                              resource_market,
                                              num_of_players_);
            res_market_.Display();
        }

        private void InitPlayersTrack(List<String> players_list, Bitmap order_track, Bitmap players_pp)
        {
            num_of_players_ = 0;
            foreach(String s in players_list)
            {
                if (s != "Closed")
                {
                    num_of_players_++;
                }
            }
            players_track_ = new PlayersTrack(players_list, order_track,
                                              players_pp);
            players_track_.Display();
        }

        private void UpdateOrderTrack()
        {
            players_track_.UpdateOrder();
            players_track_.Display();
        }

        private void PauseAndInvalidate()
        {
            invalidateAllDelegate_();
            Application.DoEvents();
            Thread.Sleep(300);
        }

        //
        // AUCTION
        //

        public void AuctionPhase(object sender, MouseEventArgs e)
        {
            PowerPlant_gui selected_pp = pp_market_.GetBestPP(players_track_.GetActivePlayer().GetMoney());
            if (players_track_.ActiveIsAI())
            {
                if(players_track_.GetActivePlayer().DoINeedToBuyPP_AI(pp_market_,board_,res_market_))
                {
                    selected_pp = pp_market_.GetBestPP(players_track_.GetActivePlayer().GetMoney());
                }
                else
                {
                    AuctionPhasePassed();
                    if (players_track_.ActiveIsAI())
                    {
                        AuctionPhase(sender, e);
                        return;
                    }
                }
            }
            else
            {
                selected_pp = pp_market_.getPPbyXY(e.X, e.Y);
            }

            pp_market_.RemovePP(selected_pp.getIdPrice());

            List<Bidder> bidders = players_track_.CreateBidders(selected_pp.getIdPrice(), already_bought_pp, res_market_, pp_market_, selected_pp);

            AuctionWindow auction_window = new AuctionWindow(selected_pp, bidders);
            if (bidders.Count() != 1)
            {
                auction_window.ShowDialog();
            }
            int winner_id = auction_window.winner_.id_;
            
            int price = auction_window.auction_.last_bid_;
            already_bought_pp.Add(winner_id);
            
            if (players_track_.GetPlayerById(winner_id).getNOfPowerPlants() == players_track_.max_pp)
            {
                ManageResources(winner_id);
                Player winner_player = players_track_.GetPlayerById(winner_id);
                if (winner_player.isAI())
                {
                    winner_player.RemoveWorstPowerPlant(res_market_);
                }
                else
                {
                    int selected_to_remove = RemovePowerPlant(winner_id);
                    winner_player.RemovePowerPlant(selected_to_remove);
                }
            }
            if (winner_id == players_track_.GetActivePlayerId())
            {
                players_track_.NextPlayer(already_bought_pp);
            }
            players_track_.GetPlayerById(winner_id).BuyPowerPlant(auction_window.power_plant_, price);

            players_track_.UpdateNotes(res_market_);
            players_track_.Display();
            
            if (already_bought_pp.Count() == num_of_players_)
            {
                IsEndOfAuction();
                PauseAndInvalidate();
                return;
            }

            IsEndOfAuction();
            if ( players_track_.ActiveIsAI() )
            {
                PauseAndInvalidate();
                
                if (players_track_.GetActivePlayer().DoINeedToBuyPP_AI(pp_market_, board_, res_market_))
                {
                    AuctionPhase(sender, e);
                    return;
                }
                else
                {
                    AuctionPhasePassed();
                    return;
                }
            }
        }

        private int RemovePowerPlant(int winner_id)
        {
            List<String> power_plants = PPtoListString(winner_id);
            RemovingPP removing_pp_window = new RemovingPP(power_plants);
            removing_pp_window.ShowDialog();
            return removing_pp_window.selected_pp;
        }
        private List<String> PPtoListString(int id)
        {
            List<String> power_plants = new List<String>();
            foreach (PowerPlant_gui pp in players_track_.GetPlayerById(id).power_plants_)
            {
                power_plants.Add(pp.ToString());
            }
            return power_plants;
        }

        public void AuctionPhasePassed()
        {
            already_bought_pp.Add(players_track_.GetActivePlayerId());
            players_track_.NextPlayer(already_bought_pp);
            passed_++;
            if (passed_ == num_of_players_)
            {
                passed_ = 0;
                pp_market_.RemoveFirst();
            }
            IsEndOfAuction();
        }

        private void IsEndOfAuction()
        {
            if (already_bought_pp.Count() == num_of_players_)
            {
                already_bought_pp.Clear();
                if (GameFlow.round == 0)
                {
                    UpdateOrderTrack();
                }
                GameFlow.NextPhase();
                if (GameFlow.step == 2 && !transformed_to_stage2)
                {
                    pp_market_.TransformToStage3();
                    transformed_to_stage2 = true;
                }
                pp_market_.Display();
                res_market_.Display();
                players_track_.ActivateLast();
                passed_ = 0;
                if (players_track_.GetActivePlayer().isAI())
                {
                    ResourceBuying();
                    return;
                }
            }
        }


        //
        // RESOURCE BUYING
        //
        public void ResourceBuying()
        {
            if (players_track_.GetActivePlayer().isAI())
            {
                players_track_.GetActivePlayer().BuyResources_AI(board_, res_market_);

                players_track_.PreviousPlayer();
                res_market_.Display();

                already_bought_res.Add(players_track_.GetActivePlayerId());

                PauseAndInvalidate();
                
                if (already_bought_res.Count() != num_of_players_)
                {
                    ResourceBuying();
                    return;
                }
                IsEndOfResourceBuying();
            }
            else
            {
                Dictionary<R.ResType, List<int>> free_places_on_pp = PrepareResourceDict();
                Dictionary<PP.PPtype, int> free_places_on_pp_co = AvailablePlacesForCO();

                ResourceBuying res_window = new ResourceBuying(free_places_on_pp,
                                                                free_places_on_pp_co,
                                                                res_market_.GetAvailableResourcesOnMarket(),
                                                                players_track_.GetActivePlayer().money_);

                res_window.ShowDialog();
                BoughtedResourceManagment(res_window);

                players_track_.PreviousPlayer();
                res_market_.Display();
                
                IsEndOfResourceBuying();
                
                if (players_track_.ActiveIsAI())
                {
                    ResourceBuying();
                }
            }
        }

        private void BoughtedResourceManagment(ResourceBuying res_window)
        {
            List<Resource_gui> boughted_res = res_market_.GetRequestedResources(res_window.boughted_resources);
            players_track_.GetActivePlayer().money_ -= res_window.money_spent;

            players_track_.GetActivePlayer().AddBoughtedResources(boughted_res);

            players_track_.UpdateNotes(res_market_);
            pp_market_.UpdateNotes();

            players_track_.Display();
            pp_market_.Display();
            already_bought_res.Add(players_track_.GetActivePlayerId());
        }

        public void ResourceBuyingPassed()
        {
            already_bought_res.Add(players_track_.GetActivePlayerId());
            players_track_.PreviousPlayer();
            IsEndOfResourceBuying();
        }

        private void IsEndOfResourceBuying()
        {
            if (already_bought_res.Count() == num_of_players_)
            {
                already_bought_res.Clear();
                GameFlow.NextPhase();
                board_.Display();
                res_market_.Display();
                pp_market_.UpdateMarket();
                pp_market_.Display();
                players_track_.ActivateLast();
                if (players_track_.GetActivePlayer().isAI())
                {
                    BuyingNewHouses(null);
                    return;
                }
            }
        }

        

        private Dictionary<PP.PPtype, int> AvailablePlacesForCO()
        {
            Dictionary<PP.PPtype, int> c_o_co = new Dictionary<PP.PPtype,int>();

            foreach (PowerPlant_gui pp in players_track_.GetActivePlayer().power_plants_)
            {
                PP.PPtype pp_type = pp.GetPPType();
                if (pp_type == PP.PPtype.COAL || pp_type == PP.PPtype.OIL || pp_type == PP.PPtype.CO)
                {
                    if (c_o_co.ContainsKey(pp_type))
                    {
                        c_o_co[pp_type] += pp.HowManyEmptySlots();
                    }
                    else
                    {
                        c_o_co.Add(pp.GetPPType(), pp.HowManyEmptySlots());
                    }
                }
            }
            return c_o_co;
        }

        private Dictionary<R.ResType, List<int>> PrepareResourceDict()
        {
            Dictionary<R.ResType, List<int>> resource_storage = new Dictionary<R.ResType, List<int>>();
            foreach (PowerPlant_gui pp in players_track_.GetActivePlayer().power_plants_)
            {
                if (pp.GetPPType() != PP.PPtype.FREE)
                {
                    List<R.ResType> resources = pp.GetRTypes();
                    foreach (R.ResType res in resources)
                    {
                        if (resource_storage.ContainsKey(res))
                        {
                            //List contains [0] available places in player pp,
                            //[>1] available on market with prices
                            resource_storage[res][0] += pp.HowManyEmptySlots();
                        }
                        else
                        {
                            List<int> avail = new List<int>();
                            avail.Add(pp.HowManyEmptySlots());

                            resource_storage.Add(res, avail);
                        }
                    }
                }
            }

            foreach (R.ResType res in resource_storage.Keys)
            {
                resource_storage[res].AddRange(res_market_.PricesOfAvailableResources(res));
            }

            Dictionary<R.ResType, List<int>> available_resources = new Dictionary<R.ResType, List<int>>();

            foreach (R.ResType res in resource_storage.Keys)
            {
                //putting minimum of (empty slot, avail on market)
                List<int> avail = new List<int>();
                int min = Math.Min(resource_storage[res][0], resource_storage[res].Count() - 1);
                for (int i = 1; i <= min; i++)
                {
                    avail.Add(resource_storage[res][i]);
                }
                available_resources.Add(res, avail);
            }
            return available_resources;
        }


        //
        // BUYING NEW HOUSES   BUYING NEW HOUSES   BUYING NEW HOUSES   BUYING NEW HOUSES
        //
        public void BuyingNewHouses(MouseEventArgs e)
        {
            Player active_player = players_track_.GetActivePlayer();
            if (active_player.isAI())
            {
                active_player.BuyHouses_AI(board_);

                players_track_.Display();
                board_.Display();
                IsEndOfBuyingNewHouses();

                PauseAndInvalidate();

                if(players_track_.GetActivePlayerPosition() == 0)
                {
                    return;
                }

                players_track_.PreviousPlayer();
                if (players_track_.GetActivePlayer().isAI() )
                {
                    BuyingNewHouses(null);
                    return;
                }
            }
            else
            {
                City_gui selected_city = board_.GetClickedCity(e.X, e.Y);
                if (!active_player.IsCityOwner(selected_city) && selected_city.Exist() && selected_city.CanBuyHouse())
                {
                    if (e.Button == MouseButtons.Left && !cities_selected_to_buy.Contains(selected_city))
                    {
                        if (active_player.owned_cities_.Count() == 0 && cities_selected_to_buy.Count() == 0)
                        {
                            cities_selected_to_buy.Add(selected_city);
                            cost_of_buying_selected_cities += 10 + selected_city.active_district * 5;
                            selected_city.BuyHouse(active_player.color_);
                        }
                        else
                        {
                            if (ComputeCitiesCost(selected_city) + cost_of_buying_selected_cities > active_player.money_)
                            {
                                MessageBox.Show("You don't have enough money.",
                                    "Important Note",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);
                            }
                            else
                            {
                                cost_of_buying_selected_cities += ComputeCitiesCost(selected_city);
                                cities_selected_to_buy.Add(selected_city);
                                selected_city.BuyHouse(active_player.color_);
                            }
                        }
                    }
                    board_.Display();
                }
            }
        }

        private void IsEndOfBuyingNewHouses()
        {
            if (players_track_.GetActivePlayerPosition() == 0)
            {
                GameFlow.NextPhase();
                players_track_.Display();
                board_.Display();
                RemoveCheapestPowerPlant();


                game_is_ended = IsGameEnded();
                
                if (players_track_.GetActivePlayer().isAI())
                {
                    SellingElectricity();
                    return;
                }
            }
        }

        public bool IsGameEnded()
        {
            return players_track_.AnyoneEnded();
        }

        private int ComputeCitiesCost(City_gui new_city)
        {
            List<City_gui> base_cities = players_track_.GetActivePlayer().owned_cities_.ToList();
            base_cities.AddRange(cities_selected_to_buy);

            int cost = 0;

            if (base_cities.Count() > 0)
            {
                cost += board_.FindCheapestConnection(base_cities, new_city);
                cost += 10 + new_city.active_district * 5;
            }
            return cost;
        }

        public void ClearAllSelectedCities(bool remove_houses = true)
        {
            if (remove_houses)
            {
                foreach (City_gui city in cities_selected_to_buy)
                {
                    city.RemoveHouse();
                }
            }
            cities_selected_to_buy.Clear();
            cost_of_buying_selected_cities = 0;
            board_.Display();
        }

        public void AcceptSelectedCities()
        {
            players_track_.GetActivePlayer().BuyNewHouses(cities_selected_to_buy, cost_of_buying_selected_cities);
            players_track_.Display();

            IsEndOfBuyingNewHouses();

            players_track_.PreviousPlayer();
            if(players_track_.GetActivePlayer().isAI())
            {
                BuyingNewHouses(null);
            }

            ClearAllSelectedCities(false);
        }

        private void RemoveCheapestPowerPlant()
        {
            while(players_track_.GetBestPlayerNetSize() >= pp_market_.GetFirstPPPrice())
            {
                pp_market_.RemoveFirst();
            }
            if (GameFlow.step == 0 && players_track_.GetBestPlayerNetSize() >= (num_of_players_ == 2 ? 10 : 7))
            {
                pp_market_.RemoveFirst();
                GameFlow.step = 1;
            }
        }
     

        //
        // SELLING_ELECTRICITY   SELLING_ELECTRICITY   SELLING_ELECTRICITY
        //
        public void SellingElectricity()
        {
            Player active_player = players_track_.GetActivePlayer();
            if (active_player.isAI())
            {
                active_player.SellElectricity(data_provider_.GetElectricityPrices(), res_market_);
                PauseAndInvalidate();
                selled_electricity++;
            }
            else
            {
                Dictionary<String, List<int>> pp_able_to_power = CreatePPAbleToPower();
                int[] electricity_prices = data_provider_.GetElectricityPrices();
                int n_of_cities = active_player.owned_cities_.Count();

                if (active_player.getNOfPowerPlants() > pp_able_to_power.Count())
                {
                    ManageResources();
                    pp_able_to_power = CreatePPAbleToPower();
                }

                SellingElectricity sell_elec_window = new SellingElectricity(pp_able_to_power, electricity_prices, n_of_cities);
                sell_elec_window.ShowDialog();
                active_player.last_power_supplied_ = PowerCities(pp_able_to_power);
                active_player.money_ += sell_elec_window.profit;
                selled_electricity++;
            }

            players_track_.Display();
            players_track_.PreviousPlayer();
            if (selled_electricity == num_of_players_)
            {
                EndOfTurnAndBeginOfIt();
                selled_electricity = 0;
            }
            else
            {
                SellingElectricity();
            }
            
        }

        private int PowerCities(Dictionary<String, List<int>> power_plants)
        {
            int powered_cities = 0;
            List<PowerPlant_gui> power_p = players_track_.GetActivePlayer().power_plants_.ToList();
            foreach (PowerPlant_gui pp in power_p)
            {
                if (pp.HasEnoughResourcesToPower())
                {
                    if (power_plants[pp.ToString()][2] == 1)
                    {
                        pp.PowerCities(res_market_);
                        powered_cities += pp.getPower();
                    }
                }
            }
            res_market_.Display();

            return Math.Min(powered_cities, players_track_.GetActivePlayer().getNOfCities());
        }
                
        private Dictionary<String, List<int>> CreatePPAbleToPower()
        {
            Dictionary<String, List<int>> power_plants = new Dictionary<string, List<int>>();
            List<PowerPlant_gui> power_p = players_track_.GetActivePlayer().power_plants_.ToList();
            foreach (PowerPlant_gui pp in power_p)
            {
                if(pp.HasEnoughResourcesToPower())
                {
                    power_plants.Add(pp.ToString(), new List<int>{pp.getIdPrice(), pp.getPower(), 0});
                }
            }
            return power_plants;
        }

        private void Bureaucracy()
        {
            res_market_.RefillMarket();
            players_track_.UpdateNotes(res_market_);
            pp_market_.UpdateNotes();
            if (GameFlow.step < 2)
            {
                pp_market_.RemoveBest();
            }
            else
            {
                pp_market_.RemoveFirst();
            }
        }

        private void EndOfTurnAndBeginOfIt()
        {
            GameFlow.NextPhase();
            Bureaucracy();
            GameFlow.NextPhase();
            UpdateOrderTrack();
            GameFlow.NextPhase();
            players_track_.ActiveFirst();
            pp_market_.Display();
            if (game_is_ended)
            {
                EndGameMessage();
            }
        }

        public void EndGameMessage()
        {
            players_track_.WinnerOrder();
            players_track_.Display();
            PauseAndInvalidate();

            MessageBox.Show("Game is ended. Winner order is presented. Congratulions!!!",
                                    "Important Note",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);
        }

        public void ManageResources(int id =-1)
        {
            Player player;
            if (id == -1)
            {
                player = players_track_.GetActivePlayer();
            }
            else
            {
                player = players_track_.GetPlayerById(id);
            }
            if (player.getNOfPowerPlants() > 0)
            {
                if (player.isAI())
                {
                    player.ManageResources_AI();
                }
                else
                {
                    ResourceManagement res_man_window = new ResourceManagement(player.power_plants_);
                    if (res_man_window.should_be_managed)
                    {
                        res_man_window.ShowDialog();
                    }
                }
            }
            players_track_.Display();
        }
    }
}
