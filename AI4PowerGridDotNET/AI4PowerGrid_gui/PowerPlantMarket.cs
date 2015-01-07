using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI4PowerGrid_gui
{
    class PowerPlantMarket
    {
        List<PowerPlant_gui> present_market;
        List<PowerPlant_gui> future_market;
        List<PowerPlant_gui> temporary_market;
        List<PowerPlant_gui> hide_market;
        List<PowerPlant_gui> step3_market;
        PowerPlant_gui special_step3;
        ResourcesMarket res_market_;
        public bool transformed_to_stage3;
        Graphics g_market;
        Bitmap PPMarketBoard;
        int max_market_size;
        int width_;
        int height_;
        float prob_;

        public PowerPlantMarket(Bitmap pp_g_market, List<PowerPlant_gui> all_pp, ResourcesMarket res_market, int width, int height)
        {
            present_market = new List<PowerPlant_gui>();
            future_market = new List<PowerPlant_gui>();
            temporary_market = new List<PowerPlant_gui>();
            hide_market = all_pp;
            step3_market = new List<PowerPlant_gui>();
            max_market_size = 4;
            width_ = width;
            height_ = height;
            res_market_ = res_market;
            PPMarketBoard = pp_g_market;
            g_market = Graphics.FromImage(PPMarketBoard);
            g_market.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            special_step3 = new PowerPlant_gui(333, PP.PPtype.FREE, 0, 33);
            transformed_to_stage3 = false;
        }

        public void PrepareMarket(int number_of_players)
        {
            UpdateMarket();
            Shuffle(hide_market);
            CorrectFirstPP(hide_market);
            RemoveRandom(number_of_players);
            hide_market.Add(special_step3);
        }

        private void CorrectFirstPP(List<PowerPlant_gui> market)
        {
            PowerPlant_gui pp = getPPById(13, market);
            market.Remove(pp);
            market.Insert(0, pp);
        }

        private void Shuffle(List<PowerPlant_gui> market)
        {
            Random rng = new Random();
            int n = market.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                PowerPlant_gui value = market[k];
                market[k] = market[n];
                market[n] = value;
            }
        }

        public void UpdateMarket()
        {
            PutAllFromMarketToMarket(present_market, temporary_market);
            PutAllFromMarketToMarket(future_market, temporary_market);
            PutSomePPToMarket(hide_market, temporary_market, 2 * max_market_size - temporary_market.Count());
            UpdatePresentAndFutureMarket();
            CountNextWillEnterProbability();
        }

        private void UpdatePresentAndFutureMarket()
        {
            temporary_market.Sort((pp1, pp2) => pp1.getIdPrice().CompareTo(pp2.getIdPrice()));

            UpdateNotes();

            PutSomePPToMarket(temporary_market, present_market, max_market_size);
            PutSomePPToMarket(temporary_market, future_market, max_market_size);

            present_market.Sort((pp1, pp2) => pp1.getIdPrice().CompareTo(pp2.getIdPrice()));
            future_market.Sort((pp1, pp2) => pp1.getIdPrice().CompareTo(pp2.getIdPrice()));
            
            if (future_market.Contains(special_step3) && !transformed_to_stage3)
            {
                GameFlow.step = 2;
                TransformToStage3();
            }
        }

        public void UpdateNotes()
        {
            foreach (PowerPlant_gui pp in temporary_market)
            {
                pp.UpdateNote(res_market_);
            }
        }

        public int GetBestNote()
        {
            int best_note = 0;
            foreach (PowerPlant_gui pp in present_market)
            {
                if (best_note < pp.GetNote())
                {
                    best_note = pp.GetNote();
                }
            }
            return best_note;
        }

        public PowerPlant_gui GetBestPP(int money)
        {
            PowerPlant_gui best_pp = present_market[0];
            int best_note = best_pp.GetNote();
            foreach (PowerPlant_gui pp in present_market)
            {
                if (best_note < pp.GetNote() && pp.getIdPrice() < money)
                {
                    best_note = pp.GetNote();
                    best_pp = pp;
                }
            }
            return best_pp;
        }

        public void CountNextWillEnterProbability()
        {
            float all = hide_market.Count();

            float smaller = 0;
            if(future_market.Count > 0)
            {
                foreach (PowerPlant_gui pp in hide_market)
                {
                    if (pp.getIdPrice() < future_market[0].getIdPrice())
                    {
                        smaller++;
                    }
                }
            }
            prob_ = (float)((int)(1000*(1 - smaller / all)))/1000;
        }

        private void PutSomePPToMarket(List<PowerPlant_gui> source_market,
                                        List<PowerPlant_gui> dest_market,
                                        int how_much)
        {
            while (how_much > 0 && source_market.Count() > 0)
            {
                dest_market.Add(source_market[0]);
                source_market.RemoveAt(0);
                how_much--;
            }
        }

        private void PutAllFromMarketToMarket(List<PowerPlant_gui> source_market,
                                        List<PowerPlant_gui> dest_market)
        {
            dest_market.AddRange(source_market);
            source_market.Clear();
        }

        public void RemoveBest()
        {
            step3_market.Add(future_market[future_market.Count() - 1]);
            future_market.RemoveAt(future_market.Count() - 1);
            UpdateMarket();
            Display();
        }

        public void RemoveFirst()
        {
            present_market.RemoveAt(0);
            UpdateMarket();
            Display();
        }

        public int GetFirstPPPrice()
        {
            return present_market[0].getIdPrice();
        }

        private bool MarketIsFull(List<PowerPlant_gui> market)
        {
            int full = GameFlow.step == 2 ? 3 : 4;
            return (market.Count() == full);
        }

        public void AddPP(PowerPlant_gui pp)
        {
            
        }

        public void TransformToStage3()
        {

            Shuffle(step3_market);
            PutAllFromMarketToMarket(step3_market, hide_market);
            if (GameFlow.active_phase == phase.AUCTION)
            {
                //no changes in market size
            }
            else
            {
                transformed_to_stage3 = true;
                UpdateMarket();
                RemoveBest();
                RemoveFirst();
                max_market_size = 3;
                UpdateMarket();
            }
        }

        public PowerPlant_gui getPPbyXY(int x, int y)
        {
            int selected = 0;
            for (int i = 0; i < max_market_size; i++)
            {
                if (x > i * width_ / max_market_size && x < (i + 1) * width_ / max_market_size)
                {
                    selected = i;
                }
            }
            if (GameFlow.step == 2 && y > height_ / 2)
            {
                return future_market[selected];
            }

            return present_market[selected];
        }

        public PowerPlant_gui getPPById_AI(int id)
        {
            PowerPlant_gui pp;
            pp = present_market.Find(x => x.getIdPrice() == id);
            return pp;
        }

        private PowerPlant_gui getPPById(int id, List<PowerPlant_gui> market)
        {
            PowerPlant_gui pp;
            pp = market.Find(x => x.getIdPrice() == id);
            return pp;
        }

        public PowerPlant_gui BuyPP(int power_plant_id, int player_id)
        {
            PowerPlant_gui pp = getPPById(power_plant_id, present_market);
            
            pp.Buy(player_id);
            present_market.Remove(pp);
            UpdateMarket();
            return pp;
        }
        public void RemovePP(int id)
        {
            PowerPlant_gui pp = getPPById(id, present_market);
            present_market.Remove(pp);
            UpdateMarket();
            Display();
        }

        private void RemoveRandom(int num_of_players)
        {
            int how_many = (num_of_players == 4) ? 4 : 8;
            for (int i = 1; i <= how_many; i++)
            {
                hide_market.RemoveAt(i);
            }
        }


        public void Display()
        {
            int starting_x = width_/(2*max_market_size);
            int starting_y = height_/4;
            int i = 0;
            g_market.Clear(GameFlow.active_phase == phase.AUCTION ? Color.LightBlue : SystemColors.Control);

            foreach (PowerPlant_gui pp in present_market)
            {
                pp.setX(starting_x + i * width_ / max_market_size);
                pp.setY(starting_y);
                pp.DrawPowerPlant(g_market);
                i++;
            }
            i = 0;
            foreach (PowerPlant_gui pp in future_market)
            {
                pp.setX(starting_x + i * width_ / max_market_size);
                pp.setY(starting_y + height_/2);
                pp.DrawPowerPlant(g_market);
                i++;
            }

            g_market.DrawString(prob_.ToString(), new Font("Arial Black", 10), new SolidBrush(Color.White), -2, -3);
        }
    }
}
