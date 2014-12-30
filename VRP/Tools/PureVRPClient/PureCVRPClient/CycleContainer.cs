using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PureCVRPClient
{
    class CycleContainer
    {
        List<Cycle> cycles_;
        public Cycle candidate_;
        Point depot_;
        int id_;
        Permutation perm;
        CheckedListBox cycles_list;
        Color[] cycle_colors;
        public double average_fullfilment;
        public int demands_sum_all;
        public int length_sum_all;
        int adding_tries_count_;


        public CycleContainer(CheckedListBox list)
        {
            perm = new Permutation();
            depot_ = new Point(400, 400, 0);
            id_ = 0;
            cycles_ = new List<Cycle>();
            candidate_ = new Cycle(id_, depot_, Color.Red);
            cycles_list = list;
            cycle_colors = new Color[] { Color.Blue, Color.Orange, Color.Green, Color.Chocolate, Color.Purple };
            adding_tries_count_ = 0;
        }

        public String ResultsToFile()
        {
            String results = "";
            foreach (Cycle cycle in cycles_)
            {
                results += cycle.ToFileFormat();
            }
            results += length_sum_all.ToString() + "\n";
            return results;
        }

        public void LoadFromFile(String data)
        {

        }

        public void ComputeStats()
        {
            demands_sum_all = 0;
            length_sum_all = 0;
            foreach (Cycle c in cycles_)
            {
                demands_sum_all += c.cargo_;
                length_sum_all += c.length_;
            }
            average_fullfilment = 0.1*demands_sum_all/cycles_.Count();
        }

        public void NewCandidate()
        {
            candidate_ = new Cycle(adding_tries_count_, depot_, Color.Red);
            adding_tries_count_++;
        }

        public void AddPoint(Point p)
        {
            if (candidate_.points_.Count() < 5 && candidate_.cargo_ + p.d_ <=1000)
            {
                candidate_.AddPoint(p);
                candidate_.Compute(perm);
            }
        }

        public void RemovePoint(Point p)
        {
            candidate_.RemovePoint(p);
            candidate_.points_.Remove(p);
            candidate_.Compute(perm);
        }

        public void AcceptCandidate()
        {
            candidate_.SetColor(cycle_colors[candidate_.id_ % cycle_colors.Count()]);
            cycles_.Add(candidate_);
            cycles_list.Items.Add(candidate_.ToString());
            ComputeStats();
        }

        public int GetCandidateLength()
        {
            return candidate_.length_;
        }

        public int GetCandidateDemandsSum()
        {
            return candidate_.cargo_;
        }

        public void RemoveCycles()
        {
            CheckedListBox.CheckedItemCollection checked_cycles = cycles_list.CheckedItems;

            for (int i = checked_cycles.Count; i > 0; i--)
            {
                int index = cycles_list.Items.IndexOf(checked_cycles[i - 1]);
                cycles_list.Items.Remove(checked_cycles[i - 1]);
                cycles_[index].RemovePoints();
                cycles_.RemoveAt(index);
            }
        }

        public void Display(Graphics g)
        {
            foreach (Cycle c in cycles_)
            {
                c.Draw(g);
            }
            candidate_.Draw(g);
        }
    }
}
