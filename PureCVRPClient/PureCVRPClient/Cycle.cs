using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PureCVRPClient
{
    class Cycle
    {
        public int id_;
        public int length_;
        public int cargo_;
        public List<Point> points_;
        public Color color_;
        Point depot_;

        public Cycle(int id, Point depot)
        {
            id_ = id;
            points_ = new List<Point>();
            depot_ = depot;
            length_ = 0;
            cargo_ = 0;
            color_ = Color.Black;
        }
        public Cycle(int id, Point depot, Color color)
        {
            id_ = id;
            points_ = new List<Point>();
            depot_ = depot;
            length_ = 0;
            cargo_ = 0;
            color_ = color;
        }

        public void RemovePoints()
        {
            foreach (Point p in points_)
            {
                RemovePoint(p);
            }
            points_.Clear();
        }

        public String ToFileFormat()
        {
            String cycle = id_.ToString() + "," + length_.ToString() + "," + cargo_.ToString() + "\n";

            foreach (Point p in points_)
            {
                cycle += p.ToString();
            }

            return cycle;
        }

        public void AddPoint(Point p)
        {
            p.color_ = color_;
            p.SetOwner(id_);
            points_.Add(p);
        }

        public void RemovePoint(Point p)
        {
            p.color_ = Color.Black;
            p.UnsetOwner();
        }

        public void SetColor(Color color)
        {
            color_ = color;
            foreach (Point p in points_)
            {
                p.color_ = color_;
            }
        }

        private int CountDistance(Point a, Point b)
        {
            return (int)(5 * Math.Sqrt(Math.Pow(a.x_ - b.x_, 2) + Math.Pow(a.y_ - b.y_, 2)));
        }

        private int CountCycleDistance(List<Point> cycle)
        {
            int length = 0;
            length = CountDistance(depot_, cycle[0]);
            
            int i;
            
            for (i = 0; i < cycle.Count() - 1; i++)
            {
                length += CountDistance(cycle[i], cycle[i + 1]);
            }
            length += CountDistance(cycle[i], depot_);

            return length;
        }

        private List<Point> SetPerm(int[] perms)
        {
            List<Point> list = new List<Point>();
            for (int i = 0; i < points_.Count(); i++)
            {
                list.Add(points_[perms[i]]);
            }
            return list;
        }

        public void Compute(Permutation perm)
        {
            length_ = 0;
            if (points_.Count() == 1)
            {
                length_ = 2 * CountDistance(depot_, points_[0]);
            }
            else
            {
                int size = perm.PermSize(points_.Count());
                int shortest = 999999;
                int shortest_perm = 0;
                int distance = 0;
                for (int i = 0; i < size; i++)
                {
                    List<Point> t_cycle = SetPerm(perm.tab[i]);
                    distance = CountCycleDistance(t_cycle);
                    if (distance < shortest)
                    {
                        shortest_perm = i;
                        shortest = distance;
                    }
                }
                points_ = SetPerm(perm.tab[shortest_perm]);
                length_ = shortest;
            }
            SumCargo();
        }

        private void SumCargo()
        {
            int cargo = 0;
            foreach (Point p in points_)
            {
                cargo += p.d_;
            }
            cargo_ = cargo;
        }

        public void Draw(Graphics g)
        {
            if (points_.Count() > 0)
            {
                Pen pen = new Pen(color_, 3);
                g.DrawLine(pen, depot_.x_, depot_.y_, points_[0].x_, points_[0].y_);
                int i;
                for (i = 0; i < points_.Count() - 1; i++)
                {
                    g.DrawLine(pen, points_[i].x_, points_[i].y_, points_[i + 1].x_, points_[i + 1].y_);
                }
                g.DrawLine(pen, depot_.x_, depot_.y_, points_[i].x_, points_[i].y_);

                foreach (Point p in points_)
                {
                    p.Draw(g);
                }
            }
        }

        public override String ToString()
        {
            String view = "";
            view += id_.ToString();
            view += (id_ > 9) ? " " : "  ";
            view += cargo_.ToString() + "  ";
            view += length_.ToString();
            return view;
        }

    }
}
