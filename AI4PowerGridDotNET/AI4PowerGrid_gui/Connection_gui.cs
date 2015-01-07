using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI4PowerGrid_gui
{
    class Connection_gui
    {
        public int id1_;
        public int id2_;
        public int price_;

        public Connection_gui()
        {
        }
        public Connection_gui(int id1, int id2, int price)
        {
            id1_ = id1;
            id2_ = id2;
            price_ = price;
        }

        public void drawConnection(Graphics g, List<City_gui> cities)
        {
            int x1_ = cities[id1_].getX();
            int x2_ = cities[id2_].getX();
            int y1_ = cities[id1_].getY();
            int y2_ = cities[id2_].getY();
            int r_ = DefaultValues.Connection_gui_r;
            Pen pen_ = new Pen(Color.Black, DefaultValues.Connection_gui_pen_size);
            SolidBrush sb_inside_ellipse_price_ = new SolidBrush(Color.Black);
            SolidBrush sb_inside_elipse_ = new SolidBrush(Color.LightGray);
            Font font_price_ = new Font("Calibri", DefaultValues.Font_size + 1);

            g.DrawLine(pen_, x1_, y1_, x2_, y2_);
            g.FillEllipse(sb_inside_elipse_, (x1_ + x2_) / 2 - r_, (y1_ + y2_) / 2 - r_, 2 * r_, 2 * r_);
            g.DrawEllipse(pen_      , (x1_ + x2_) / 2 - r_, (y1_ + y2_) / 2 - r_, 2 * r_, 2 * r_);
            g.DrawString(price_.ToString(), font_price_, sb_inside_ellipse_price_, (x1_ + x2_) / 2 - r_+1, (y1_ + y2_) / 2 - r_+2);
        }
    }
}
