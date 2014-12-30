using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PureCVRPClient
{
    class InputGenerator
    {
        Point depot_;
        public List<Point> points_;
        int size_;


        public InputGenerator(int size)
        {
            size_ = size;
            depot_ = new Point(size_ / 2, size_ / 2, 0);
            points_ = new List<Point>();
        }

        public String ToFile()
        {
            String data = depot_.ToString();
            foreach (Point p in points_)
            {
                data += p.ToString();
            }
            return data;
        }

        public void GeneratePoints(int n)
        {
            points_ = new List<Point>();
            List<int> phis = new List<int>();
            Random rnd = new Random();
            List<int> rs = new List<int>();
            for (int i = 0; i < n; i++)
            {
                phis.Add(rnd.Next(0,360));
                rs.Add(rnd.Next(size_ / 20, size_/2));
            }
            phis.Sort();
            rs.Add(rnd.Next(size_ / 20, size_ / 2));
            int it = 0;
            while(it<n)
            {
                Point candidate = new Point(rnd.Next(size_ / 10, size_ / 2), phis[it], depot_.x_, depot_.y_);
                if (AcceptedPoint(candidate))
                {
                    points_.Add(candidate);
                    it++;
                }
            }
        }

        private bool AcceptedPoint(Point point)
        {
            bool accepted = true;
            foreach (Point p in points_)
            {
                if (Diff(point, p) < 25)
                {
                    accepted = false;
                    break;
                }
            }
            return accepted;
        }

        private int Diff(Point a, Point b)
        {
            return (int)(Math.Sqrt(Math.Pow(a.x_ - b.x_, 2) + Math.Pow(a.y_ - b.y_, 2)));
        }

        public void GenerateDemands(int n, int d_min, int d_max)
        {
            if (points_.Count() == 0)
            {
                GeneratePoints(n);
            }
            else
            {
                Random rnd = new Random();
                foreach (Point p in points_)
                {
                    p.d_ = rnd.Next(d_min, d_max);
                }
            }
        }

        public void Display(Graphics g)
        {
            depot_.Draw(g);
            foreach (Point p in points_)
            {
                p.Draw(g);
            }
        }
    }
}
