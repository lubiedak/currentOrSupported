using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI4PowerGrid_gui
{
    public class Resource_gui
    {
        R.ResType type_;
       // String name_;
        int price_;
        bool show_price_;

        public Resource_gui()
        {
            type_ = R.ResType.POTENTIAL;
            price_ = 0;
            show_price_ = false;
        }

        public Resource_gui(Resource_gui r)
        {
            type_ = r.type_;
            price_ = r.price_;
            show_price_ = false;
        }

        public Resource_gui(R.ResType type, int price)
        {
            type_ = type;
            price_ = price;
        }

        public R.ResType GetRType()        { return type_; }
        public int GetPrice()           { return price_; }

        public void SetPrice(int price)     { price_ = price; }
        public void SetShowPrice(bool show) { show_price_ = show; }
        public void SetType(R.ResType type)    { type_ = type; }

        public void Remove()
        {
            SetType(R.ResType.POTENTIAL);
        }

        private void finalDraw(Graphics g, int x, int y)
        {
            int r = DefaultValues.Resource_size; ;
            Pen pen = new Pen(DefaultValues.Resource_pen_color, DefaultValues.Resource_pen_size);
            SolidBrush sb = new SolidBrush(R.color[(int)type_]);

            
            g.FillEllipse(sb, x - r / 2, y - r / 2, r, r);
            g.DrawEllipse(pen, x - r / 2, y - r / 2, r, r);

            if (show_price_)
            {
                Font f = new Font("Arial", 6);
                SolidBrush price_sb = new SolidBrush(Color.White);
                g.DrawString(price_.ToString(), f, price_sb, x - r / 3, y - r / 3); 
            }
        }

        public void DrawResource(Graphics g, int x, int y)
        {
            finalDraw(g, x, y);
        }
    }



    public static class R
    {
        public enum ResType
        {
            COAL = 0,
            OIL,
            GARBAGE,
            URANIUM,
            POTENTIAL,
            MAX_RESOURCE_TYPE,
        }
        public static Color[] color =
        {
            Color.Brown,
            Color.Black,
            Color.Yellow,
            Color.Red,
            Color.LightGray,
        };
    }
}
