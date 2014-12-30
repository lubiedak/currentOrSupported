using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PureCVRPClient
{
    class Point
    {
        public int x_;
        public int y_;
        public int d_;
        public Color color_;
        public bool has_owner_;
        public string owner_id_;

        public Point()
        {
            x_ = 0;
            y_ = 0;
            d_ = 0;
            has_owner_ = false;
            color_ = Color.Black;
            owner_id_ = "";
        }

        public Point(int x, int y, int d)
        {
            x_ = x;
            y_ = y;
            d_ = d;
            has_owner_ = false;
            color_ = Color.Black;
            owner_id_ = "";
        }

        public void SetOwner(int owner_id)
        {
            has_owner_ = true;
            owner_id_ = "\n " + owner_id.ToString();
        }

        public void UnsetOwner()
        {
            has_owner_ = false;
            owner_id_ = "";
        }

        public Point(int r, int phi, int dx, int dy)
        {
            double pi = Math.Acos(-1);
            x_ = dx + (int)(r * Math.Cos(pi * (double)phi / (double)180));
            y_ = dy + (int)(r * Math.Sin(pi * (double)phi / (double)180));
            color_ = Color.Black;
            has_owner_ = false;
            owner_id_ = "";
        }

        public void SetBlack() { color_ = Color.Black; }

        public void Draw(Graphics g)
        {
            Font f = new Font("Courier New", 7);
            int r = 11;
            SolidBrush sb = new SolidBrush(color_);
            SolidBrush text_sb = new SolidBrush(Color.White);
            g.FillEllipse(sb, x_ - r, y_ - r, 2 * r, 2 * r);
            g.DrawString(d_.ToString() + owner_id_, f, text_sb, x_ - r+1, y_ - r + 1);
        }

        public override string ToString()
        {
            return (x_.ToString() + "," + y_.ToString() + "," + d_.ToString() + "\r\n");
        }
    }
}
