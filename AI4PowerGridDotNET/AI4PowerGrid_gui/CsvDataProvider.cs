using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI4PowerGrid_gui;
using System.IO;

namespace GameModel
{
    class CsvDataProvider
    {
        String file_name_;
        String directory_name_;

        List<City_gui> cities;
        List<Connection_gui> connections;
        List<PowerPlant_gui> all_powerplants;
        int[] electricity_prices;
        Dictionary<R.ResType, int> resource_start_positions;
        Dictionary<int, Dictionary<R.ResType, List<int>>> resource_refilling;

        String[] file_data_;

        public CsvDataProvider(String dir)
        {
            directory_name_ = dir;
        }

        public List<City_gui> GetCities(){ return cities;}
        public List<Connection_gui> GetConnections(){ return connections;}
        public List<PowerPlant_gui> GetPowerPlants(){ return all_powerplants;}
        public int[] GetElectricityPrices(){ return electricity_prices; }
        public Dictionary<R.ResType, int> GetResourceStartPositions(){ return resource_start_positions; }
        public Dictionary<int, Dictionary<R.ResType, List<int>>> GetResourceFilling() { return resource_refilling; }

        public void LoadAllData(int num_of_players)
        {
            ReadCities(num_of_players);
            ReadConnections();
            ReadPowerPlants();
            ReadElectricityPrices();
            ReadResourceMarketPrices();
            ReadResourceRefilling();
        }

        private void ReadCities(int num_of_players)
        {
            file_name_ = directory_name_ + "cities.csv";
            file_data_ = File.ReadAllLines(file_name_);
            cities= new List<City_gui>();

            foreach (String line in file_data_)
            {
                cities.Add(ParseCity(line));
                if (cities.Count() > 20)
                {
                    if (num_of_players == 2 || num_of_players == 3)
                    {
                        break;
                    }
                    else
                    {
                        if (cities.Count() > 27)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private City_gui ParseCity(String city_string)
        {
            City_gui city = new City_gui();
            try
            {
                String[] line = city_string.Split(',');
                if (line.Length == DefaultValues.City_cols)
                {
                    String city_name;
                    int id, x, y;
                    city_name = line[0];
                    id = Convert.ToInt32(line[1]);
                    x = Convert.ToInt32(line[2]);
                    y = Convert.ToInt32(line[3]);
                    city = new City_gui(city_name, id, x, y);
                }
                else
                {
                    throw new FileLoadException();
                }
                Console.WriteLine("File " + file_name_ + " read correctly.");
            }
            catch (Exception e)
            {
                Console.WriteLine("File " + file_name_ + " could not be read.");
                Console.WriteLine(e.Message);
            }
            return city;
        }
        public void ReadConnections()
        {
            file_name_ = directory_name_ + "connections.csv";
            file_data_ = File.ReadAllLines(file_name_);
            connections = new List<Connection_gui>();
            foreach (String line in file_data_)
            {
                Connection_gui candidate = ParseConnection(line, cities);
                if(ConnectionCanBeAccepted(candidate))
                {
                    connections.Add(candidate);
                }
            }
        }

        private bool ConnectionCanBeAccepted(Connection_gui candidate)
        {
            if (!cities.Exists(city => city.id_ == candidate.id1_) || !cities.Exists(city => city.id_ == candidate.id2_))
            {
                return false;
            }
            return true;
        }

        Connection_gui ParseConnection(String connection_string, List<City_gui> cities)
        {
            Connection_gui connection = new Connection_gui();
            try
            {
                String[] line = connection_string.Split(',');
                if (line.Length == DefaultValues.Connections_cols)
                {
                    int city_id1, city_id2, price;
                    city_id1 = Convert.ToInt32(line[0]);
                    city_id2 = Convert.ToInt32(line[1]);
                    price = Convert.ToInt32(line[2]);
                    connection = new Connection_gui(city_id1, city_id2, price);
                }
                else
                {
                    throw new FileLoadException();
                }
                Console.WriteLine("File " + file_name_ + " read correctly.");
            }
            catch (Exception e)
            {
                Console.WriteLine("File " + file_name_ + " could not be read.");
                Console.WriteLine(e.Message);
            }
            return connection;
        }

        private void ReadPowerPlants()
        {
            file_name_ = directory_name_ + "powerplants.csv";
            file_data_ = File.ReadAllLines(file_name_);
            all_powerplants = new List<PowerPlant_gui>();
            foreach (String line in file_data_)
            {
                all_powerplants.Add( ParsePowerPlant(line));
            }
        }

        private PowerPlant_gui ParsePowerPlant(String power_plant_line)
        {
            PowerPlant_gui power_plant = new PowerPlant_gui();
            try
            {
                String[] line = power_plant_line.Split(',');
                if (line.Length == DefaultValues.PP_cols)
                {
                    String res_type;
                    int price, capacity, powered_cities;

                    price = Convert.ToInt32(line[0]);
                    res_type = line[1];
                    capacity = Convert.ToInt32(line[2]);
                    powered_cities = Convert.ToInt32(line[3]);
                    
                    power_plant = new PowerPlant_gui(price, PP.TranslateAcronym(res_type), capacity, powered_cities);
                }
                else
                {
                    throw new FileLoadException();
                }
                Console.WriteLine("File " + file_name_ + " read correctly.");
            }
            catch (Exception e)
            {
                Console.WriteLine("File " + file_name_ + " could not be read.");
                Console.WriteLine(e.Message);
            }
            return power_plant;
        }
        private void ReadElectricityPrices()
        {
            electricity_prices = new int[]{10,22,33,44,54,64,73,82,90,98,105,112,118,124,129,134,138,142,145,148,150};
        }
        private void ReadResourceMarketPrices()
        {
            resource_start_positions = new Dictionary<R.ResType, int>();
            resource_start_positions.Add(R.ResType.COAL, 0);
            resource_start_positions.Add(R.ResType.OIL, 2);
            resource_start_positions.Add(R.ResType.GARBAGE, 6);
            resource_start_positions.Add(R.ResType.URANIUM, 10);
        }

        private void ReadResourceRefilling()
        {
            resource_refilling = new Dictionary<int, Dictionary<R.ResType, List<int>>>();
            Dictionary<R.ResType, List<int>> players2 = new Dictionary<R.ResType, List<int>>();
            Dictionary<R.ResType, List<int>> players3 = new Dictionary<R.ResType, List<int>>();
            Dictionary<R.ResType, List<int>> players4 = new Dictionary<R.ResType, List<int>>();

            players2.Add(R.ResType.COAL, new List<int> { 3, 4, 3 });
            players3.Add(R.ResType.COAL, new List<int> { 4, 5, 3 });
            players4.Add(R.ResType.COAL, new List<int> { 5, 6, 4 });

            players2.Add(R.ResType.OIL, new List<int> { 2, 2, 4 });
            players3.Add(R.ResType.OIL, new List<int> { 2, 3, 4 });
            players4.Add(R.ResType.OIL, new List<int> { 3, 4, 5 });

            players2.Add(R.ResType.GARBAGE, new List<int> { 1, 2, 3 });
            players3.Add(R.ResType.GARBAGE, new List<int> { 1, 2, 3 });
            players4.Add(R.ResType.GARBAGE, new List<int> { 2, 4, 4 });

            players2.Add(R.ResType.URANIUM, new List<int> { 1, 1, 1 });
            players3.Add(R.ResType.URANIUM, new List<int> { 1, 1, 1 });
            players4.Add(R.ResType.URANIUM, new List<int> { 1, 2, 2 });

            resource_refilling.Add(2, players2);
            resource_refilling.Add(3, players3);
            resource_refilling.Add(4, players4);
        }
    }
}
