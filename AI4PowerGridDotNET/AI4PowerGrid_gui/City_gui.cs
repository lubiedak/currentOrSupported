using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI4PowerGrid_gui
{
    class City_gui
    {
        public int id_;
        int r_;
        int x_;
        int y_;
        float pen_size_;
        float[] x_borders_;
        float[] y_borders_;
        String name_;
        Color[] owners;
        public int active_district;

        public City_gui()
        {
            id_ = -1;
        }
        public bool Exist() { return id_ != -1; }

        public City_gui(String name, int id, int x, int y)
        {
            id_ = id;
            name_ = name;
            r_ = DefaultValues.City_gui_size;
            x_ = x;
            y_ = y;
            pen_size_ = DefaultValues.City_gui_pen_size;
            owners = new Color[]{ Color.LightGray, Color.LightGray, Color.LightGray };
            active_district = 0;
            Compute_borders();
        }
        void Compute_borders()
        {
            x_borders_ = new float[DefaultValues.Nb_of_districts];
            y_borders_ = new float[DefaultValues.Nb_of_districts];
            x_borders_[0] = x_;
            y_borders_[0] = y_-r_;
            x_borders_[1] = (float)(x_ + r_*Math.Cos(Math.PI * 150.0 / 180.0));
            y_borders_[1] = (float)(y_ + r_ * Math.Sin(Math.PI * 150.0 / 180.0));
            x_borders_[2] = (float)(x_ + r_ * Math.Cos(Math.PI * (30.0) / 180.0));
            y_borders_[2] = (float)(y_ + r_ * Math.Sin(Math.PI * (30.0) / 180.0));
        }
        public void drawCity(Graphics g)
        {
            Pen p = new Pen( Color.Black, pen_size_ );
            Font f = new Font("Arial", DefaultValues.Font_size);
            SolidBrush sb = new SolidBrush(Color.Black);
            SolidBrush sb_gray = new SolidBrush(Color.LightGray);

            for (int i = 0; i < owners.Length; i++)
            {
                g.FillPie(new SolidBrush(owners[i]), x_ - r_, y_ - r_, 2 * r_, 2 * r_, -90 + i * 120, 120);
            }
            g.DrawEllipse(p, x_ - r_, y_ - r_, 2 * r_, 2 * r_);
            
            SizeF text_size = g.MeasureString(name_, f);
            g.FillRectangle(sb_gray, (float)x_ - text_size.Width / 2, y_ + r_, text_size.Width,
                DefaultValues.City_gui_height_name_rect);
            g.DrawRectangle(p, (float)x_ - text_size.Width / 2, y_ + r_, text_size.Width,
                DefaultValues.City_gui_height_name_rect);

            g.DrawString(name_, f, sb, (float)(x_+1) - text_size.Width / 2, y_ + r_);

            for (int i = 0; i < 3; i++)
            {
                g.DrawLine(p, x_, y_, x_borders_[i], y_borders_[i]);
            }
            
        }

        public bool CanBuyHouse()
        {
            return active_district <= GameFlow.step;
        }

        public int BuyingCost()
        {
            return 10 + active_district * 5;
        }

        public int BuyHouse(Color c)
        {
            if (active_district < 3)
            {
                owners[active_district] = c;
                active_district++;
            }
            return 10 + active_district * 5;
        }
        public void RemoveHouse()
        {
            if (active_district > 0)
            {
                active_district--;
                owners[active_district] = Color.LightGray;
                
            }
        }

        public void RemoveAll()
        {
            while (active_district > 0)
                RemoveHouse();
        }

        public int getX() { return x_; }
        public int getY() { return y_; }
        public int getR() { return r_; }
        public string getName() { return name_; }
    }
}
