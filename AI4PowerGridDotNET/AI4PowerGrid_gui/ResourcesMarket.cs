using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace AI4PowerGrid_gui
{
    public class ResourcesMarket
    {
        Dictionary<R.ResType, List<Resource_gui>> market;
        Dictionary<R.ResType, List<Resource_gui>> refill_warehouse;
        Dictionary<int, Dictionary<R.ResType, List<int>>> refill_info;
        int not_uranium_max_price;
        int not_uranium_group_size;
        Graphics g;
        int num_of_players_;

        public ResourcesMarket(Dictionary<R.ResType, int> resource_start_position,
                               Dictionary<int, Dictionary<R.ResType, List<int>>> refill_info,
                               Bitmap res_market_board,
                               int num_of_players)
        {
            not_uranium_max_price = 8;
            not_uranium_group_size = 3;
            num_of_players_ = num_of_players;
            refill_warehouse = new Dictionary<R.ResType, List<Resource_gui>>();
            market = new Dictionary<R.ResType, List<Resource_gui>>();
            
            InitRefillWareHouse();
            InitMarket(resource_start_position);

            this.refill_info = refill_info;

            g = Graphics.FromImage(res_market_board);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        private void InitRefillWareHouse()
        {
            for (R.ResType res_t = 0; res_t < R.ResType.POTENTIAL; res_t++)
            {
                refill_warehouse.Add(res_t, CreateEmptyListOfResourceType(res_t));
            }
        }

        public void AddToRefillWarehouse(R.ResType res_t)
        {
            refill_warehouse[res_t].Add(new Resource_gui(res_t, 0));
        }

        private void InitMarket(Dictionary<R.ResType, int> resource_start_position)
        {
            FillWithPotentialResources();
            FillWithStartingResources(resource_start_position);
        }

        private void FillWithPotentialResources()
        {
            for (R.ResType res_t = 0; res_t < R.ResType.POTENTIAL; res_t++)
            {
                market.Add(res_t, CreatePotentialResourceListWithPrices(res_t == R.ResType.URANIUM));
            }
        }

        private void FillWithStartingResources(Dictionary<R.ResType, int> resource_start_position)
        {
            foreach (R.ResType res_t in resource_start_position.Keys)
            {
                if (res_t != R.ResType.URANIUM)
                {
                    for (int i = resource_start_position[res_t]; i < not_uranium_max_price; i++)
                    {
                        market[res_t][not_uranium_group_size * i].SetType(res_t);
                        market[res_t][not_uranium_group_size * i + 1].SetType(res_t);
                        market[res_t][not_uranium_group_size * i + 2].SetType(res_t);
                        refill_warehouse[res_t].RemoveRange(0, 3);
                    }
                }
                else
                {
                    market[res_t][10].SetType(res_t);
                    market[res_t][11].SetType(res_t);
                    refill_warehouse[res_t].RemoveRange(0, 2);
                }
            }
        }

        public List<int> PricesOfAvailableResources(R.ResType res)
        {
            List<int> prices = new List<int>();

            foreach(Resource_gui r in market[res])
            {
                if (r.GetRType() == res)
                {
                    prices.Add(r.GetPrice());
                }
            }
            return prices;
        }

        public Dictionary<R.ResType, int> GetAvailableResourcesOnMarket()
        {
            Dictionary<R.ResType, int> available_on_market = new Dictionary<R.ResType, int>();
            foreach (R.ResType res_t in market.Keys)
            {
                available_on_market.Add(res_t, AvailableResourceTypeOnMarket(res_t));
            }
            return available_on_market;
        }

        private int AvailableResourceTypeOnMarket(R.ResType res_t)
        {
            int sum = 0;
            foreach (Resource_gui r in market[res_t])
            {
                sum += (r.GetRType() == res_t) ? 1 : 0;
            }
            return sum;
        }

        private List<Resource_gui> CreatePotentialResourceListWithPrices( bool uranium = false)
        {
            List<Resource_gui> empty_res_list = new List<Resource_gui>();
            if (uranium)
            {
                for (int i = 0; i < 8; i++)
                {
                    empty_res_list.Add(new Resource_gui(R.ResType.POTENTIAL, i + 1));
                }
                for (int i = 10; i <= 16; i += 2)
                {
                    empty_res_list.Add(new Resource_gui(R.ResType.POTENTIAL, i));
                }
            }
            else
            {
                for (int i = 0; i < not_uranium_group_size * not_uranium_max_price; i++)
                {
                    empty_res_list.Add(new Resource_gui(R.ResType.POTENTIAL, 1 + i / 3));
                }
            }
            return empty_res_list;
        }

        private List<Resource_gui> CreateEmptyListOfResourceType(R.ResType res_t)
        {
            List<Resource_gui> empty_res_list = new List<Resource_gui>();
            int how_big = (res_t == R.ResType.URANIUM) ? 12 : not_uranium_group_size * not_uranium_max_price;

            for (int i = 0; i < how_big; i++)
            {
                empty_res_list.Add(new Resource_gui(res_t, 0));
            }
            
            return empty_res_list;
        }


        public void RefillMarket()
        {
            int stage = GameFlow.step;
            foreach (R.ResType res_t in refill_info[num_of_players_].Keys)
            {
                AddResources(res_t, refill_info[num_of_players_][res_t][stage]);
            }
            Display();
        }

        private void AddResources(R.ResType res_t, int how_much)
        {
            int last = market[res_t].FindLastIndex(x => x.GetRType() == R.ResType.POTENTIAL);
            how_much = Math.Min(how_much, refill_warehouse[res_t].Count());
            for (int i = 0; i < how_much; i++)
            {
                if (last >= i)
                {
                    int price = market[res_t][last - i].GetPrice();
                    market[res_t][last - i] = new Resource_gui(res_t, price);
                    refill_warehouse[res_t].RemoveAt(0);
                }
            }
        }

        public List<Resource_gui> GetRequestedResources(Dictionary<R.ResType, int> resource_request)
        {
            List<Resource_gui> resource_list = new List<Resource_gui>();
            foreach (R.ResType res in resource_request.Keys)
            {
                resource_list.AddRange(GetSpecificResources(res, resource_request[res]));
            }
            Display();
            return resource_list;
        }

        public List<Resource_gui> GetCheapestResources(PP.PPtype pp_type, int how_much)
        {
            List<Resource_gui> resource_list = new List<Resource_gui>();
            
            if (pp_type == PP.PPtype.CO)
            {
                for (int i = 0; i < how_much; i++)
                {
                    if (GetResourceCost(R.ResType.OIL, i) < GetResourceCost(R.ResType.COAL, i))
                    {
                        resource_list.Add(GetFromMarket(R.ResType.OIL));
                    }
                    else
                    {
                        resource_list.Add(GetFromMarket(R.ResType.COAL));
                    }
                }

            }
            else
            {
                for (int i = 0; i < how_much; i++)
                {
                    resource_list.Add(GetFromMarket((R.ResType)pp_type));
                }
            }
            
            Display();
            return resource_list;
        }

        public int GetResourceCost(R.ResType res_t, int position)
        {
            int first_resource = FirstPosition(res_t);
            if (first_resource == -1)
            {
                return 999;
            }
            if (first_resource + position >= market[res_t].Count())
            {
                return -1;
            }
            return market[res_t][first_resource + position].GetPrice();
        }

        public bool RequestCanBeExecuted(Dictionary<R.ResType, int> pp_request)
        {
            foreach (R.ResType res_t in pp_request.Keys)
            {
                if (FirstPosition(res_t) + pp_request[res_t] > market[res_t].Count())
                {
                    return false;
                }
            }

            return true;
        }

        public int CalculateRequestCost(Dictionary<R.ResType, int> pp_request)
        {
            int cost = 0;

            foreach(R.ResType res_t in pp_request.Keys)
            {
                for (int i = 0; i < pp_request[res_t]; i++)
                {
                    cost += GetResourceCost(res_t, i);
                }
            }
            return cost;
        }

        public int CalculateRequest4PPCost(PP.PPtype pp_request, int how_much)
        {
            int cost = 0;

            if (pp_request == PP.PPtype.CO)
            {
                for (int i = 0; i < how_much; i++)
                {
                    if (FirstPosition(R.ResType.OIL) >= 0 && FirstPosition(R.ResType.OIL) >= 0)
                    {
                        if (GetResourceCost(R.ResType.OIL, i) < GetResourceCost(R.ResType.COAL, i))
                        {
                            cost += GetResourceCost(R.ResType.OIL, i);
                        }
                        else
                        {
                            cost += GetResourceCost(R.ResType.COAL, i);
                        }
                    }
                    else
                    {
                        cost = 999;
                    }
                }

            }
            else if (pp_request == PP.PPtype.FREE)
            {
                return 0;
            }
            else
            {
                if (FirstPosition((R.ResType)pp_request) >= 0)
                {
                    for (int i = 0; i < how_much; i++)
                    {
                        cost += GetResourceCost((R.ResType)pp_request, i);
                    }
                }
                else
                {
                    cost = 999;
                }
            }
            
            return cost;
        }

        private int FirstPosition(R.ResType res_t)
        {
            return market[res_t].FindIndex(x => x.GetRType() == res_t); ;
        }

        private List<Resource_gui> GetSpecificResources(R.ResType res_type, int how_much)
        {
            List<Resource_gui> resource_list = new List<Resource_gui>();

            for(int i = 0; i< how_much; i++)
            {
                resource_list.Add(GetFromMarket(res_type));
            }
            return resource_list;
        }

        private Resource_gui GetFromMarket(R.ResType res_t)
        {
            int first_resource = FirstPosition(res_t);
            Resource_gui ret = new Resource_gui(market[res_t][first_resource]);
            market[res_t][first_resource].Remove();
            return ret;
        }

        public void Display()
        {
            g.Clear(GameFlow.active_phase == phase.RESOURCE_BUYING ? Color.LightBlue : SystemColors.Control);
            int i = 1;
            int j = 1;
            int space = DefaultValues.Resource_size + 1;
            int sep = 0;

            Font f = new Font("Arial Black", 8);
            SolidBrush price_sb = new SolidBrush(Color.Black);

            foreach (R.ResType res_t in market.Keys)
            {
                if (res_t != R.ResType.URANIUM)
                {
                    foreach (Resource_gui res in market[res_t])
                    {
                        res.DrawResource(g, (i+2) * space + sep, j * space);
                        i++;
                        sep = i % 3 == 1 ? sep + 5 : sep;
                    }
                    g.DrawString(refill_warehouse[res_t].Count().ToString(), f, price_sb, 0, j * space - 7);
                    g.DrawString(refill_info[num_of_players_][res_t][GameFlow.step].ToString(), f, price_sb, 20, j * space - 7);
                    sep = 0;
                    i = 1;
                    j++;
                }
                else
                {
                    for (int it = 0; it < 8; it++)
                    {
                        market[res_t][it].DrawResource(g,
                                        (3 * (it) + 4) * space + sep, j * space);
                        g.DrawString((it + 1).ToString(), f, price_sb,
                                        (3 * (it) + 4) * space - 5 + sep, -3);
                        sep += 5;

                        
                    }
                    for (int it = 8; it < 12; it++)
                    {
                        market[res_t][it].SetShowPrice(true);
                        market[res_t][it].DrawResource(g, 27 * space + sep,
                                                                  (it-7) * space);
                    }
                    g.DrawString(refill_warehouse[res_t].Count().ToString(), f, price_sb, 0, j * space - 7);
                    g.DrawString(refill_info[num_of_players_][res_t][GameFlow.step].ToString(), f, price_sb, 20, j * space - 7);
                }
            }
        }
    }
}
