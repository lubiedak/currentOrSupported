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
    class Player
    {
        public int id_;
        public Color color_;
        public List<PowerPlant_gui> power_plants_;
        public List<City_gui> owned_cities_;
        public int max_n_of_pp_;
        public int money_;
        public int money_spended_;
        public bool is_active_;
        public String type_;
        public int last_power_supplied_;

        public Player(int id, int max_pp, String type)
        {
            id_ = id;
            color_ = P.color[id];
            type_ = type;
            owned_cities_ = new List<City_gui>();
            power_plants_ = new List<PowerPlant_gui>();
            max_n_of_pp_ = max_pp;
            is_active_ = false;
            money_ = 50;
            money_spended_ = 0;
            last_power_supplied_ = 0;
        }

        public int getNOfCities() { return owned_cities_.Count(); }
        public int getNOfPowerPlants() { return power_plants_.Count(); }

        public int BidForPowerPlant(PowerPlant_gui pp)
        {
            return 3;
        }

        public bool IsCityOwner(City_gui city)
        {
            return owned_cities_.Contains(city);
        }

        public bool isAI()
        {
            return type_ == "AI";
        }

        public void ManageResources_AI()
        {
            List<Resource_gui> resources = new List<Resource_gui>();
            foreach(PowerPlant_gui pp in power_plants_)
            {
                resources.AddRange(pp.GetResources());
            }
            if (resources.Count() > 0)
            {
                foreach (PowerPlant_gui pp in power_plants_)
                {
                    pp.AddResourcesToPower(resources);
                }
                foreach (PowerPlant_gui pp in power_plants_)
                {
                    pp.AddResources(resources);
                }
            }
        }

        public void BuyResources_AI(Board board, ResourcesMarket res_market)
        {
            int cities_count = CountCitiesSize_AI(board, res_market);

            List<PowerPlant_gui> temp = power_plants_.OrderByDescending(x => x.GetNote()).ThenBy(x => x.getCapacity()).ToList();
            power_plants_ = temp;
            ManageResources_AI();

            foreach (PowerPlant_gui pp in power_plants_)
            {
                if (ShouldBuyMoreResources(cities_count) && pp.GetPPType() != PP.PPtype.FREE)
                {
                    if (money_ > res_market.CalculateRequest4PPCost(pp.GetPPType(), pp.MinimumToPower()))
                    {
                        money_ -= res_market.CalculateRequest4PPCost(pp.GetPPType(), pp.MinimumToPower());
                        List<Resource_gui> resources = res_market.GetCheapestResources(pp.GetPPType(), pp.MinimumToPower());

                        pp.AddResources(resources);
                    }
                }
            }
        }

        private bool ShouldBuyMoreResources(int cities_count)
        {
            int power_count = 0;
            foreach (PowerPlant_gui pp in power_plants_)
            {
                if (pp.HasEnoughResourcesToPower())
                {
                    power_count += pp.getPower();
                }
            }

            return power_count < cities_count; 
        }

        public void AddBoughtedResources(List<Resource_gui> resources)
        {
            foreach (PowerPlant_gui pp in power_plants_)
            {
                pp.PutResources(resources);
            }
            foreach (PowerPlant_gui pp in power_plants_)
            {
                pp.PutResources(resources, false);
            }
        }

        public void BuyNewHouses()
        {
        }

        public void UpdateNotes_AI(ResourcesMarket res_market)
        {
            foreach (PowerPlant_gui pp in power_plants_)
            {
                pp.UpdateNote(res_market);
            }
        }

        public int ChoiceBestPP_ID_AI(PowerPlantMarket pp_market)
        {
            return 0;
        }

        public bool DoINeedToBuyPP_AI(PowerPlantMarket pp_market, Board board, ResourcesMarket res_market)
        {
            if (GameFlow.round == 0)
            {
                return true;
            }
            PowerPlant_gui pp = pp_market.GetBestPP(money_);
            if ( CountPotencialCitiesSize_AI(board, res_market, pp) < PotentialPower() )
            {
                return false;
            }
            else
            {
                if (power_plants_.Count() < max_n_of_pp_)
                {
                    return true;
                }
                if (SelectWorstPP_AI().GetNote() > pp_market.GetBestNote())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public PowerPlant_gui SelectWorstPP_AI()
        {
            PowerPlant_gui worst = power_plants_[0];
            foreach (PowerPlant_gui pp in power_plants_)
            {
                if( worst.GetNote() > pp.GetNote())
                {
                    worst = pp;
                }
            }
            return worst;
        }

        private int CountPotencialCitiesSize_AI(Board board, ResourcesMarket res_market, PowerPlant_gui pp)
        {
            int money_for_cities = money_ - (int)(pp.getIdPrice() + CalculateResourcesCost_AI(res_market) * 1.2);

            int potential_cities = owned_cities_.Count();
            if (money_for_cities > 0 && potential_cities > 0)
            {
                potential_cities += board.HowManyCitiesCanBuy_AI(owned_cities_, money_for_cities);
            }
            else if (potential_cities == 0)
            {
                potential_cities = 2;
            }
            return potential_cities;
        }

        private int CountCitiesSize_AI(Board board, ResourcesMarket res_market)
        {
            int money_for_cities = money_ - CalculateResourcesCost_AI(res_market);

            int potential_cities = owned_cities_.Count();
            if (money_for_cities > 0 && potential_cities > 0)
            {
                potential_cities += board.HowManyCitiesCanBuy_AI(owned_cities_, money_for_cities);
            }
            else if (potential_cities == 0)
            {
                potential_cities = 2;
            }
            return potential_cities;
        }

        private int CalculateResourcesCost_AI(ResourcesMarket res_market)
        {
            int cost = 0;
            foreach (PowerPlant_gui pp in power_plants_)
            {
                if (pp.GetPPType() != PP.PPtype.FREE && pp.MinimumToPower() != 0)
                {
                    cost += res_market.CalculateRequest4PPCost(pp.GetPPType(), pp.MinimumToPower());
                }
            }
            return 0;
        }

        public int PotentialPower()
        {
            int power = 0;
            foreach (PowerPlant_gui pp in power_plants_)
            {
                power += pp.getPower();
            }
            return power;
        }

        public int AvailablePower()
        {
            int power = 0;
            foreach (PowerPlant_gui pp in power_plants_)
            {
                if (pp.HasEnoughResourcesToPower())
                {
                    power += pp.getPower();
                }
            }
            return power;
        }


        public void BuyCheapestHouses(Board board)
        {
            do
            {
                City_gui city = board.FindCheapestCity(owned_cities_);
                if (city.id_ != owned_cities_[0].id_)
                {
                    int cost = board.FindCheapestConnection(owned_cities_, city);
                    cost += city.BuyingCost();

                    if (cost < money_ && city.CanBuyHouse())
                    {
                        money_ -= cost;
                        money_spended_ += cost;
                        city.BuyHouse(color_);
                        owned_cities_.Add(city);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            } while (true);
        }

        public void BuyHouses_AI(Board board)
        {
            if (GameFlow.round == 0)
            {
                BuyHouseById(board, board.GetGoodFirstCity());
            }
            while (NeedMoreHouses())
            {
                if (!BuyCheapestHouse(board))
                {
                    return;
                }
            }
        }

        public bool NeedMoreHouses()
        {
            if (GameFlow.step == 2)
            {
                return true;
            }
            return AvailablePower() > owned_cities_.Count(); 
        }

        public bool BuyCheapestHouse(Board board)
        {
            City_gui city = board.FindCheapestCity(owned_cities_);
            if (city.id_ != owned_cities_[0].id_)
            {
                int cost = board.FindCheapestConnection(owned_cities_, city);
                cost += city.BuyingCost();

                if (cost < money_ && city.CanBuyHouse())
                {
                    money_ -= cost;
                    money_spended_ += cost;
                    city.BuyHouse(color_);
                    owned_cities_.Add(city);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void BuyHouseById(Board board, int id)
        {
            City_gui city = board.GetCityById(id);
            city.BuyHouse(color_);
            owned_cities_.Add(city);
        }

        public void BuyNewHouses(List<City_gui> new_cities, int cost)
        {
            owned_cities_.AddRange(new_cities);
            money_ -= cost;
            money_spended_ += cost;
        }

        public void SellElectricity(int[] electricity_prices, ResourcesMarket res_market)
        {
            int can_be_powered = AvailablePower();
            if (can_be_powered < getNOfCities())
            {
                foreach (PowerPlant_gui pp in power_plants_)
                {
                    if (pp.HasEnoughResourcesToPower())
                    {
                        pp.PowerCities(res_market);
                    }
                }
                if (getNOfCities() > 20)
                {
                    money_ += electricity_prices[20];
                    last_power_supplied_ = 20;
                }
                else
                {
                    money_ += electricity_prices[getNOfCities()];
                    last_power_supplied_ = getNOfCities();
                }
            }
            else
            {
                int powered_cities = 0;
                int pp_should_power = 0;
                
                while (powered_cities < getNOfCities() && pp_should_power < power_plants_.Count())
                {
                    if (power_plants_[pp_should_power].HasEnoughResourcesToPower())
                    {
                        powered_cities += power_plants_[pp_should_power].getPower();
                        pp_should_power++;
                    }
                    else
                    {
                        break;
                    }
                }
                money_ += electricity_prices[powered_cities];
                last_power_supplied_ = powered_cities;
                for (int i = 0; i < pp_should_power; i++)
                {
                    power_plants_[i].PowerCities(res_market);
                }
            }
        }


        public Color GetColor() { return color_;}
        public int GetMoney() { return money_; }
        public int LastPowerSupply() { return last_power_supplied_; }
        public void Activate()  { is_active_ = true;}
        public void Deactivate(){ is_active_ = false;}

        public void BuyPowerPlant(PowerPlant_gui pp, int price)
        {
            if (power_plants_.Count() < max_n_of_pp_)
            {
                money_ -= price;
                pp.Buy(id_);
                power_plants_.Add(pp);
            } 
            List<PowerPlant_gui> temp = power_plants_.OrderByDescending(x => x.GetNote()).ThenBy(x => x.getCapacity()).ToList();
            power_plants_ = temp;
        }

        public void RemovePowerPlant(int position)
        {
            power_plants_.RemoveAt(position);
        }

        public void RemoveWorstPowerPlant(ResourcesMarket res_market)
        {
            PowerPlant_gui pp = power_plants_[max_n_of_pp_ - 1];
            if (pp.GetResourceNumber() > 0)
            {
                List<Resource_gui> resources = pp.GetResources();
                foreach (PowerPlant_gui ppp in power_plants_)
                {
                    ppp.AddResources(resources);
                }
            }
            if (pp.GetResourceNumber() > 0)
            {
                List<Resource_gui> resources = pp.GetResources();
                foreach (Resource_gui res in resources)
                {
                    res_market.AddToRefillWarehouse(res.GetRType());
                }
            }
            power_plants_.Remove(pp);
        }

        public int GetBestPowerPlant()
        {
            int best = power_plants_[0].getIdPrice();
            foreach (PowerPlant_gui pp in power_plants_)
            {
                if (best < pp.getIdPrice())
                {
                    best = pp.getIdPrice();
                }
            }
            return best;
        }

        public void DrawPowerPlants(Graphics g, int x, int n_of_players)
        {
            int i = 0;
            int size = DefaultValues.PP_frame_size;
            if (n_of_players == 2)
            {
                if (is_active_ == true)
                {
                    g.DrawRectangle(new Pen(Color.Red, 2), 1, x - size / 2, 450, size);
                }
                g.DrawString(money_.ToString(),
                    new Font("Arial Black", 14),
                    new SolidBrush(color_), 0, x-10);

                foreach (PowerPlant_gui pp in power_plants_)
                {
                    pp.setY(x);
                    
                    pp.setX(i * (size + 10) + size);
                    pp.DrawPowerPlant(g);
                    i++;
                }
            }
            else
            {
                if (is_active_ == true)
                {
                    g.DrawRectangle(new Pen(Color.Red, 2), x - size / 2, 1, size, 340);
                }
                g.DrawString(money_.ToString(),
                    new Font("Arial Black", 14),
                    new SolidBrush(color_), x - 10, 10);
                foreach (PowerPlant_gui pp in power_plants_)
                {
                    pp.setX(x);
                    pp.setY(i * (size + 10) + size);
                    pp.DrawPowerPlant(g);
                    i++;
                }
            }
        }


        public void Draw(Graphics g, int x, int y, int size = 0)
        {
            SolidBrush sb = new SolidBrush(color_);
            Pen p = new Pen(Color.Black);
            if(size == 0)
                size = DefaultValues.Player_size;
            if (is_active_)
            {
                p = new Pen(Color.Red, 6);
                p.Alignment = PenAlignment.Inset;
            }   

            g.FillRectangle(sb, x - size / 2, y - size / 2, size, size);
            g.DrawRectangle(p, x - size / 2, y - size / 2, size, size);
            g.DrawString(owned_cities_.Count().ToString(),
                new Font("Arial Black", 14),
                new SolidBrush(Color.Black), x - 16, y - 12);
        }
    }


    public static class P
    {
        public enum id
        {
            P1 = 0,
            P2,
            P3,
            P4,
            MAX_PLAYERS,
        }

        public static Color[] color =
        {
            Color.Blue,
            Color.Green,
            Color.Purple,
            Color.Orange,
        };
        public static id UseIntAsEnum(int which)
        {
            id ret = id.P1;
            if (which < (int)id.MAX_PLAYERS)
            {
                ret = (id)which;
            }
            return ret;
        }
    }
    
}
