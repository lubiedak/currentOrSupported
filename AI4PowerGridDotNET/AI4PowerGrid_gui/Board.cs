using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameModel;
using System.Windows.Forms;

namespace AI4PowerGrid_gui
{
    class Board
    {
        List<City_gui> cities_;
        List<Connection_gui> connections_;
        Graphics g;
        Bitmap map;
        public int[][] dist;
        public int[][] paths;

        List<List<int>> good_first_city;
        int first_choice;

        public Board(CsvDataProvider data_provider, Bitmap board)
        {
            cities_ = data_provider.GetCities();
            connections_ = data_provider.GetConnections();
            CalculateShortestPaths();
            map = board;
            g = Graphics.FromImage(map);
            good_first_city = CreateListofGoodCities();
            first_choice = 0;
        }

        private void CalculateShortestPaths() 
        {
            InitializeShortestPathsAndDistances();
            FloydWarshall();
        }

        private void InitializeShortestPathsAndDistances()
        {
            int size = cities_.Count();
            dist = new int[size][];
            paths = new int[size][];
            for (int i = 0; i < size; i++)
            {
                dist[i] = new int[size];
                paths[i] = new int[size];
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        dist[i][j] = 9999;
                    }
                    else
                    {
                        dist[i][j] = 0;
                    }
                    paths[i][j] = 0;
                }
            }

            foreach (Connection_gui c in connections_)
            {
                dist[c.id1_][c.id2_] = c.price_;
                dist[c.id2_][c.id1_] = c.price_;
            }
        }

        private void FloydWarshall()  //Algorithm Floyd-Warshall O(V^3)
        {
            int size = cities_.Count();
            for (int k = 0; k< size; k++)
            {
                for (int i = 0; i< size; i++)
                {
                    for (int j = 0; j< size; j++)
                    {
                        if (dist[i][k]+dist[k][j] < dist[i][j]) 
                        {
                            dist[i][j] = dist[i][k] + dist[k][j];
                            paths[i][j] = k;  
                        } 
                    }
                }
            }
        }

        List<List<int>> CreateListofGoodCities()
        {
            List<List<int>> good_cities = new List<List<int>>();
            good_cities.Add(new List<int> { 15, 16, 17 });
            good_cities.Add(new List<int> { 7, 8, 2 });
            good_cities.Add(new List<int> { 5, 3, 14 });
            good_cities.Add(new List<int> { 21, 22, 23 });

            return good_cities;
        }

        public int FindCheapestConnection(List<City_gui> cities, City_gui new_city)
        {
            if (cities.Count() == 0)
            {
                return 0;
            }
            
            int cheapest = 999;
            foreach (City_gui city in cities)
            {
                if (dist[city.id_][new_city.id_] < cheapest)
                {
                    cheapest = dist[city.id_][new_city.id_];
                }
            }

            return cheapest;
        }

        public int GetGoodFirstCity()
        {
            Random rnd = new Random();
            int city = rnd.Next(0, 3);
            return good_first_city[first_choice++][city];
        }

        public City_gui FindCheapestCity(List<City_gui> cities_owned)
        {
            int cheapest = 999;
            int cheapest_id = cities_owned[0].id_;
            
            foreach (City_gui city_to_buy in cities_)
            {
                if (city_to_buy.CanBuyHouse() && !cities_owned.Any(x => x.id_ == city_to_buy.id_))
                {
                    int cost = city_to_buy.BuyingCost();
                    foreach (City_gui city in cities_owned)
                    {
                        if (dist[city.id_][city_to_buy.id_] + cost < cheapest)
                        {
                            cheapest = dist[city.id_][city_to_buy.id_] + cost;
                            cheapest_id = city_to_buy.id_;
                        }
                    }
                }
            }

            return cities_[cheapest_id];
        }

        private bool CanBuyAnyCity(List<City_gui> cities_owned, int money)
        {
            City_gui city = FindCheapestCity(cities_owned);
            if (city.id_ != cities_owned[0].id_)
            {
                if (money >= CityCost(cities_owned, city))
                {
                    return true;
                }
            }

            return false;
        }

        public int CityCost(List<City_gui> cities_owned, City_gui city)
        {
            return city.BuyingCost() + FindCheapestConnection(cities_owned, city);
        }
        
        public int HowManyCitiesCanBuy_AI(List<City_gui> cities_owned, int money)
        {
            if(!CanBuyAnyCity(cities_owned, money))
            {
                return 0;
            }
            int can_buy = 0;

            List<City_gui> cities_to_buy = new List<City_gui>();
            while(CanBuyAnyCity(cities_owned, money))
            {
                City_gui city = FindCheapestCity(cities_owned);
                money -= CityCost(cities_owned, city);
                cities_to_buy.Add(city);
                cities_owned.Add(city);
                can_buy++;
            }

            foreach (City_gui city in cities_to_buy)
            {
                cities_owned.Remove(city);
            }

            return can_buy;
        }

        

        public void Display()
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(GameFlow.active_phase == phase.BUYING_NEW_HOUSES? Color.LightBlue : SystemColors.Control);
            foreach (Connection_gui con in connections_)
            {
                con.drawConnection(g, cities_);
            }
            foreach (City_gui city in cities_)
            {
                city.drawCity(g);
            }
        }

        public City_gui GetClickedCity(int x, int y)
        {
            City_gui empty_city = new City_gui(); 
            for(int i = 0; i< cities_.Count(); i++)
            {
                if (Math.Sqrt(Math.Pow((cities_[i].getX() - x), 2) +
                             Math.Pow((cities_[i].getY() - y), 2)) < DefaultValues.City_gui_size)
                {
                    return cities_[i];
                }
            }

            return empty_city;
        }

        public void Clean()
        {
            foreach(City_gui city in cities_)
            {
                city.RemoveAll();
            }
        }

        public City_gui GetCityById(int id)
        {
            return cities_[id];
        }
    }
}
