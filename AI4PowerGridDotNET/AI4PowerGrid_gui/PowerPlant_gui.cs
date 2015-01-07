using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameModel;
using System.Drawing;

namespace AI4PowerGrid_gui
{
    public class PowerPlant_gui : PowerPlant
    {
        int x_;
        int y_;
        List<Resource_gui> resources_;
        PP.PPtype type_;
        bool has_owner_;
        int first_empty_;
        int note_;

        public PowerPlant_gui()
        {
            note_ = 0;
        }

        public PowerPlant_gui(PowerPlant_gui r)
        {
            starting_price_ = r.starting_price_;
            powered_cities_ = r.powered_cities_;
            resource_capacity_ = r.resource_capacity_;
            type_ = r.type_;
            player_id_ = r.player_id_;
            has_owner_ = r.has_owner_;
            resources_ = r.resources_;
        }

        public PowerPlant_gui(int price, PP.PPtype r_type, int capacity, int cities)
        {
            starting_price_ = price;
            powered_cities_ = cities;
            resource_capacity_ = capacity;
            type_ = r_type;

            player_id_ = 0;
            has_owner_ = false;
            first_empty_ = 0;
            resources_ = new List<Resource_gui>();
            AddPotentialResources();
        }

        public PowerPlant_gui(int x, int y, int price, PP.PPtype r_type, int capacity, int cities)
        {
            starting_price_ = price;
            powered_cities_ = cities;
            resource_capacity_ = capacity;
            type_ = r_type;

            x_ = x;
            y_ = y;
            player_id_ = 0;

            resources_ = new List<Resource_gui>();
            AddPotentialResources();
        }

        public int HowManyEmptySlots()
        {
            int empty_sum = 0;
            foreach (Resource_gui r in resources_)
            {
                if (r.GetRType() == R.ResType.POTENTIAL)
                {
                    empty_sum++;
                }
            }
            return empty_sum;
        }


        public List<R.ResType> GetRTypes()
        {
            List<R.ResType> resources = new List<R.ResType>();
            if (type_ == PP.PPtype.CO)
            {
                resources.Add(R.ResType.COAL);
                resources.Add(R.ResType.OIL);
            }
            else
            {
                resources.Add((R.ResType)(type_));
            }
            return resources;
        }
        public Dictionary<R.ResType, int> GetResourcesAsDict()
        {
            Dictionary<R.ResType, int> res_dict = new Dictionary<R.ResType,int>();

            List<R.ResType> res_types = GetRTypes();
            foreach(R.ResType res_t in res_types)
            {
                res_dict.Add(res_t, 0);   
            }

            foreach (Resource_gui res in resources_)
            {
                if (res.GetRType() != R.ResType.POTENTIAL)
                {
                    if (res_dict.ContainsKey(res.GetRType()))
                    {
                        res_dict[res.GetRType()]++;
                    }
                }
            }
            return res_dict;
        }

        public void ApplyManagerChanges(ManagerPP_UI manager)
        {
            CleanResources();

            foreach (R.ResType res_t in manager.res_numerics_.Keys)
            {
                for (int i = 0; i < (int)manager.res_numerics_[res_t].Value; i++)
                {
                    resources_[first_empty_].SetType(res_t);
                    first_empty_++;
                }
            }

        }

        private void CleanResources()
        {
            foreach( Resource_gui res in resources_)
            {
                res.SetType(R.ResType.POTENTIAL);
            }
            first_empty_ = 0;
        }

        public void AddResources(List<Resource_gui> resources, bool to_power = false)
        {
            foreach (Resource_gui res in resources)
            {
                if(type_ == PP.PPtype.CO)
                {
                    if(res.GetRType() == R.ResType.OIL || res.GetRType() == R.ResType.COAL)
                    {
                        if (first_empty_ < resource_capacity_ * 2)
                        {
                            resources_[first_empty_].SetType(res.GetRType());
                            first_empty_++;
                            res.SetType(R.ResType.POTENTIAL);
                        }
                    }
                }
                else
                {
                    if (res.GetRType() != R.ResType.POTENTIAL && res.GetRType() == (R.ResType)type_)
                    {
                        if (first_empty_ < resource_capacity_ * 2)
                        {
                            resources_[first_empty_].SetType(res.GetRType());
                            first_empty_++;
                            res.SetType(R.ResType.POTENTIAL);
                        }
                    }
                }
                if (to_power && HasEnoughResourcesToPower())
                    break;
            }
        }

        public void AddResourcesToPower(List<Resource_gui> resources)
        {
            AddResources(resources, true);
        }

        public List<Resource_gui> GetResources()
        {
            List<Resource_gui> resources = new List<Resource_gui>();
            if (first_empty_ > 0)
            {
                foreach (Resource_gui res in resources_)
                {
                    while (first_empty_ > 0)
                    {
                        if (resources_[first_empty_ - 1].GetRType() != R.ResType.POTENTIAL)
                        {
                            resources.Add(new Resource_gui(resources_[first_empty_ - 1]));
                            resources_[first_empty_ - 1].Remove();

                            first_empty_--;
                        }
                    }
                }
            }
            return resources;
        }

        public void setX(int x){ x_ = x;}
        public void setY(int y) { y_ = y; }
        public void setXY(int x, int y) { x_ = x; y_ = y; }
        public PP.PPtype GetPPType() { return type_; }

        private void AddPotentialResources()
        {
            for (int i = 0; i < 2 * resource_capacity_; i++)
            {
                resources_.Add(new Resource_gui());
            }
        }

        public void Buy(int player_id)
        {
            has_owner_ = true;
            player_id_ = player_id;
        }

        public void PutResources(List<Resource_gui> new_resources, bool minimum_fill = true)
        {
            int it = new_resources.Count();
            while (it > 0 && HowManyEmptySlots() > (minimum_fill ? resource_capacity_ : 0))
            {
                it--;
                if (ResourceFits(new_resources[it]))
                {
                    resources_[first_empty_] = new_resources[it];
                    new_resources.RemoveAt(it);
                    first_empty_++;
                }
            }
        }

        public bool HasEnoughResourcesToPower()
        {
            return first_empty_ >= resource_capacity_;
        }

        public int GetResourceNumber()
        {
            return first_empty_;
        }

        public int MinimumToPower()
        {
            return HasEnoughResourcesToPower() ? ( 0 ) : (resource_capacity_ - first_empty_);
        }
        
        public void PowerCities(ResourcesMarket res_market)
        {
            for (int i = 0; i < resource_capacity_; i++ )
            {
                res_market.AddToRefillWarehouse(resources_[first_empty_ - 1].GetRType());
                resources_[first_empty_ - 1].Remove();
                first_empty_--;
            }
        }

        private bool ResourceFits(Resource_gui res)
        {
            bool fits = false;

            PP.PPtype res_as_pp_type = (PP.PPtype)res.GetRType();

            if (res_as_pp_type == type_)
            {
                fits = true;
            }
            if (type_ == PP.PPtype.CO && (res_as_pp_type == PP.PPtype.OIL || res_as_pp_type == PP.PPtype.COAL))
            {
                fits = true;
            }

            return fits;
        }

        public override string ToString()
        {
            return starting_price_.ToString() + ", " + type_.ToString() + ", Cities: " + powered_cities_.ToString();
        }



        public void DrawPowerPlant(Graphics g)
        {
            DrawForm(g);
            DrawPrice(g);
            DrawOwner(g);
            DrawResources(g);
            DrawCityPower(g);
            DrawNote(g);
        }

        private void DrawOwner(Graphics g)
        {
            if (has_owner_)
            {
                int size = DefaultValues.PP_frame_size;
                g.FillRectangle(new SolidBrush(P.color[player_id_]), x_ - size / 2, y_ - size / 2, 10, 10);
            }
        }

        private void DrawResources(Graphics g)
        {
            if (type_ != PP.PPtype.FREE)
            {
                int size = DefaultValues.PP_inside_frame_size;
                for (byte i = 0; i < resource_capacity_; i++)
                {
                    resources_[i].DrawResource(g, x_ - size / 2 + DefaultValues.Resource_size / 2 * (1 + 2 * i),
                        y_ + size / 4);
                    resources_[i + resource_capacity_].DrawResource(g, x_ - size / 2 + DefaultValues.Resource_size / 2 * (1 + 2 * i),
                        y_ + size / 4 + DefaultValues.Resource_size);

                }
            }
        }
        private void DrawForm(Graphics g)
        {
            int size = DefaultValues.PP_frame_size;
            int inside_size = DefaultValues.PP_inside_frame_size;
            SolidBrush frame_brush_ = new SolidBrush(PP.color[(int)type_]);
            SolidBrush inside_frame_brush_ = new SolidBrush(Color.Gray);

            g.FillRectangle(frame_brush_,
                x_ - size / 2, y_ - size / 2,
                size, size);

            if (type_ == PP.PPtype.CO)
            {
                frame_brush_ = new SolidBrush(R.color[(int)R.ResType.OIL]);
                g.FillRectangle(frame_brush_,
                x_ - size / 2, y_ - size / 2,
                size, size/2);
            }

            g.FillRectangle(inside_frame_brush_,
                x_ - (inside_size + size) / 4, y_ - (inside_size + size) / 4,
                (inside_size + size) / 2, (inside_size + size) / 2);
            
        }
        private void DrawPrice(Graphics g)
        {
            int size = DefaultValues.PP_inside_frame_size;
            Font price_font = new Font("Arial Black", DefaultValues.PP_price_font_size);
            
            g.DrawString(Convert.ToString((int)starting_price_), price_font, new SolidBrush(Color.Black),
                x_ - size / 2 - 5, y_ - size / 2);
        }

        private void DrawCityPower(Graphics g)
        {
            int inside_size = DefaultValues.PP_inside_frame_size;
            Font cities_font_ = new Font("Arial Black", DefaultValues.PP_cities_font_size);

            g.DrawString(Convert.ToString((int)powered_cities_), cities_font_, new SolidBrush(Color.Black),
                x_ + inside_size / 6, y_ + inside_size / 6);
        }

        private void DrawNote(Graphics g)
        {
            int size = DefaultValues.PP_inside_frame_size;
            Font price_font = new Font("Arial Black", 9);

            g.DrawString(Convert.ToString((int)note_), price_font, new SolidBrush(Color.Black),
                x_+5 , y_ - size / 4);
        }

        public void UpdateNote(ResourcesMarket res_market)
        {
            note_ = CalculateNote(res_market, starting_price_);
        }

        public int GetNote() { return note_; }

        public int CalculateNote(ResourcesMarket res_market, int price)
        {
            int note = 1000 * powered_cities_;
            if (type_ == PP.PPtype.FREE)
            {
                note /= has_owner_ ? starting_price_*3/2 : price;
            }
            else
            {
                if (has_owner_)
                {
                    note /= (1 + 3 * res_market.CalculateRequest4PPCost(type_, resource_capacity_));
                }
                else
                {
                    note /= (price + 3 * res_market.CalculateRequest4PPCost(type_, resource_capacity_));
                }
            }
            return note;
        }
    }



    public class PowerPlant
    {
        protected int starting_price_;
        protected int purchase_price_;
        protected int powered_cities_;
        protected int resource_capacity_;
        protected int player_id_;

        public PowerPlant()
        {
            this.starting_price_ = 3;
            this.purchase_price_ = 3;
            this.powered_cities_ = 1;
            this.resource_capacity_ = 0;
            this.player_id_ = 0;
        }
        public PowerPlant(PowerPlant right)
        {
            this.powered_cities_ = right.powered_cities_;
            this.purchase_price_ = right.purchase_price_;
            this.starting_price_ = right.starting_price_;
            this.resource_capacity_ = right.resource_capacity_;
            this.player_id_ = right.player_id_;
        }

        public int getIdPrice() { return starting_price_; }
        public int getPower() { return powered_cities_; }
        public int getCapacity() { return resource_capacity_; }
    }




    public static class PP
    {
        public enum PPtype
        {
            COAL = 0,
            OIL,
            GARBAGE,
            URANIUM,
            CO,
            FREE,
            MAX_PP_TYPE,
        }

        public static Color[] color =
        {
            Color.Brown,
            Color.Black,
            Color.Yellow,
            Color.Red,
            Color.Brown,
            Color.Green,
        };
        public static String[] acronym = { "c", "o", "g", "u", "co", "f", "p", };

        public static PPtype UseIntAsEnum(int which)
        {
            PPtype ret = PPtype.COAL;
            if (which < (int)PPtype.MAX_PP_TYPE)
            {
                ret = (PPtype)which;
            }
            return ret;
        }
        public static PPtype TranslateAcronym(string acr)
        {
            PPtype ret = PPtype.COAL;
            for (int i = (int)PPtype.COAL; i < (int)PPtype.MAX_PP_TYPE; i++)
            {
                if (acr == acronym[i])
                {
                    ret = UseIntAsEnum(i);
                }
            }
            return ret;
        }
    }
}
