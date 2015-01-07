using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace AI4PowerGrid_gui
{
    class SimulationMode
    {
        Board board_;
        PlayersTrack players_track_;
        CsvDataProvider data_provider_;
        List<int> possible_starts;
        int num_of_players_;
        int num_of_cities_;

        int startingMoney_;
        int moneyIncrease_;
        int timeDelay_;

        public SimulationMode(List<String> players,
                        Bitmap MapBoard,
                        Bitmap order_track,
                        Bitmap money_track)
        {
            data_provider_ = new CsvDataProvider("../../ModelData/");

            InitPlayersTrack(players, order_track, money_track);
            data_provider_.LoadAllData(num_of_players_);

            board_ = new Board(data_provider_, MapBoard);
            board_.Display();

            possible_starts = CreatePossibilietiesList();
        }

        public void SetSimulationParameters(int startingMoney, int moneyIncrease, int timeDelay)
        {
            startingMoney_ = startingMoney;
            moneyIncrease_ = moneyIncrease;
            timeDelay_ = timeDelay;
        }

        private void InitPlayersTrack(List<String> players_list, Bitmap order_track, Bitmap money_track)
        {
            num_of_players_ = 0;
            foreach (String s in players_list)
            {
                if (s != "Closed")
                {
                    num_of_players_++;
                }
            }
            num_of_cities_ = num_of_players_ == 4 ? 28 : 21;
            players_track_ = new PlayersTrack(players_list, order_track,
                                              money_track);

            players_track_.ActivateLast();
            players_track_.Display();
        }

        public bool AllPlayersAreAI()
        {
            return players_track_.AreAllAI();
        }

        public void PreviousPlayer()
        {
            players_track_.PreviousPlayer();
        }

        public void RunFullAI(PictureBox map, PictureBox orderTrack, PictureBox playersTrack)
        {
            List<int> end_list = CreateEndList();
            players_track_.GiveMoney(startingMoney_);

            ChangeStartingPositions(possible_starts);

            SimulationLogger summary = new SimulationLogger();
            while(!end_list.SequenceEqual(possible_starts))
            {
                board_.Clean();
                GameFlow.step = 0;
                String file_name;
                SimulationLogger slogger = new SimulationLogger();

                for (int i = 0; i < num_of_players_; i++)
                {
                    players_track_.GetActivePlayer().BuyHouseById(board_, possible_starts[i]);
                    players_track_.PreviousPlayer();
                }
                players_track_.GiveMoney(startingMoney_);

                int round = 0;
                slogger.AddLog("Round: " + round.ToString());
                slogger.AddPlayerInfo(players_track_);

                while (!GameEnded())
                {
                    round++;
                    slogger.AddLog("Round: " + round.ToString());
                    
                    for (int i = 0; i < num_of_players_; i++)
                    {
                        players_track_.GetActivePlayer().BuyCheapestHouses(board_);
                        players_track_.PreviousPlayer();
                        players_track_.DisplayPlayersBoard();
                        
                        /*
                        board_.Display();
                        map.Invalidate();
                        orderTrack.Invalidate();
                        playersTrack.Invalidate();
                        Application.DoEvents();
                        Thread.Sleep(timeDelay_);
                         */
                    }
                    slogger.AddPlayerInfo(players_track_);
                    players_track_.CheckForNextStage();
                    players_track_.UpdateOrderNoPP();
                    players_track_.ActivateLast();
                    players_track_.AddMoney(startingMoney_ + round*moneyIncrease_);

                    /*
                    board_.Display();
                    map.Invalidate();
                    Application.DoEvents();
                    */
                }
                slogger.SaveToFileParam(players_track_);
                summary.AddSummaryInfo(players_track_);
                
                /*
                Application.DoEvents();
                board_.Display();
                map.Invalidate();*/
                possible_starts = ChangeStartingPositions(possible_starts);
                
                players_track_.Clean();
                
                players_track_.UpdateOrderNoPP();
            }
            summary.SaveToFile("summary_" + num_of_players_.ToString() + "_" + startingMoney_.ToString() + "_" + moneyIncrease_.ToString() + ".csv");
        }

        private List<int> CreatePossibilietiesList()
        {
            List<int> possibilieties = new List<int>();
            for (int i = 0; i < num_of_players_; i++)
            {
                possibilieties.Add(0);
            }
            return possibilieties;
        }

        private List<int> CreateEndList()
        {
            List<int> endlist = new List<int>();
            for (int i = 0; i < num_of_players_; i++)
            {
                endlist.Add(num_of_cities_ - num_of_players_ + i);
            }
            return endlist;
        }

        private List<int> ChangeStartingPositions(List<int> starting_positions)
        {
            HashSet<int> unique_positions;
            
            do
            {
                int it = 0;
                bool try_more = true;
                while (try_more && it <= num_of_players_)
                {
                    if (starting_positions[it] < num_of_cities_-1)
                    {
                        starting_positions[it]++;
                        try_more = false;
                    }
                    else
                    {
                        starting_positions[it] = 0;
                        it++;
                    }
                    if (it == num_of_players_)
                        break;
                }
                unique_positions = new HashSet<int>(starting_positions);

            } while (unique_positions.Count() != num_of_players_);

            return starting_positions;
        }

        private bool AllPossibilitiesChecked(List<int> possible_ids)
        {
            return false;
        }

        private bool GameEnded()
        {
            return players_track_.GameEnded();
        }


        public void SelectFirstCity(MouseEventArgs e)
        {

        }
    }
}
