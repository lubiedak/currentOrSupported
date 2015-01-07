using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI4PowerGrid_gui
{
    class SimulationLogger
    {
        List<string> logs;

        public SimulationLogger()
        {
            logs = new List<string>();
        }

        public void AddLog(string log)
        {
            logs.Add(log);
        }

        public void RemoveAll()
        {
            logs.Clear();
        }

        public void SaveToFile(String path)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(path, true);
            foreach (String log in logs)
            {
                file.WriteLine(log);
            }
            file.Close();
        }

        public void SaveToFileParam(PlayersTrack track)
        {
            List<Player> players = track.GetPlayersById();
            int sum = players.Sum(x => x.money_spended_);
            string path = sum.ToString() + "_";
            /*foreach (Player p in players)
            {
                path += p.owned_cities_[0].id_.ToString() + "_";
            }*/
            path += ".csv";
            SaveToFile(path);
        }

        public void AddPlayerInfo(PlayersTrack track)
        {
            List<Player> players = track.GetPlayersById();
            foreach (Player p in players)
            {
                String log = p.color_.ToString() + ",";
                log += p.id_.ToString() + ",";
                log += p.money_.ToString() + ",";
                log += p.money_spended_.ToString() + ",";
                log += p.owned_cities_.Count().ToString();
                
                foreach (City_gui city in p.owned_cities_)
                {
                    log += "," + city.id_.ToString();
                }

                foreach (City_gui city in p.owned_cities_)
                {
                    log += "," + city.getName();
                }

                logs.Add(log);
            }
        }

        public void AddSummaryInfo(PlayersTrack track)
        {
            string summary = "StartingCity,";
            List<Player> players = track.GetPlayersById();
            foreach (Player p in players)
            {
                summary += p.owned_cities_[0].id_.ToString() + ",";
            }
            int sum = 0;
            summary += "Builded,";
            foreach (Player p in players)
            {
                sum += p.getNOfCities();
                summary += p.getNOfCities().ToString() + ",";
            }
            sum = 0;
            summary += "MoneySpended,";
            foreach (Player p in players)
            {
                sum += p.money_spended_;
                summary += p.money_spended_.ToString() + ",";
            }

            logs.Add(summary);
        }
    }
}
