using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PureCVRPClient
{
    public partial class pureCVRPClient : Form
    {
        InputGenerator input;
        CycleContainer cycle_container;
        Bitmap map;

        bool can_add_points;

        public pureCVRPClient()
        {
            InitializeComponent();

            map = new Bitmap(Map.ClientSize.Width, Map.ClientSize.Height);
            input = new InputGenerator(Map.ClientSize.Width);
            cycle_container = new CycleContainer(ResultList);
            can_add_points = false;
        }

        private void CoordinatesGenButt_Click(object sender, EventArgs e)
        {
            input.GeneratePoints((int)NumberOfPoints.Value);
            AddCycleButt.Enabled = false;
            Display();
        }

        private void DemandsGenButt_Click(object sender, EventArgs e)
        {
            input.GenerateDemands((int)NumberOfPoints.Value, (int)MinDemand.Value, (int)MaxDemand.Value);
            AddCycleButt.Enabled = true;
            Display();
        }

        private void Display()
        {
            Graphics g = Graphics.FromImage(map);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(SystemColors.Control);
            cycle_container.Display(g);
            input.Display(g);
            Map.Invalidate();
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(map, 0, 0);
        }

        private void AddCycleButt_Click(object sender, EventArgs e)
        {
            cycle_container.NewCandidate();
            can_add_points = true;
            AddCycleButt.Enabled = false;
        }

        private int Diff(Point a, Point b)
        {
            return (int)(Math.Sqrt(Math.Pow(a.x_ - b.x_, 2) + Math.Pow(a.y_ - b.y_, 2)));
        }

        private bool IsPointClicked(int x, int y)
        {
            Point clicked = new Point(x, y, 0);
            bool found = false;
            foreach(Point p in input.points_)
            {
                if (Diff(p, clicked) < 10)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private Point FindPoint(int x, int y)
        {
            Point clicked = new Point(x, y, 0);
            foreach (Point p in input.points_)
            {
                if (Diff(p, clicked) < 11)
                {
                    clicked = p;
                    break;
                }
            }
            return clicked;
        }

        private void UpdateCandidate()
        {
            LengthField.Text = cycle_container.GetCandidateLength().ToString();
            DemandsSumField.Text = cycle_container.GetCandidateDemandsSum().ToString();
        }

        private void UpdateStats()
        {
            cycle_container.ComputeStats();
            LengthSumAll.Text = cycle_container.length_sum_all.ToString();
            DemandsSumAll.Text = cycle_container.demands_sum_all.ToString();
            AverageFullfilment.Text = String.Format("{0:00.00}", cycle_container.average_fullfilment);
        }

        private void Map_Click(object sender, MouseEventArgs e)
        {
            if (can_add_points)
            {
                if(IsPointClicked(e.X, e.Y))
                {
                    Point p = FindPoint(e.X, e.Y);
                    if (e.Button == System.Windows.Forms.MouseButtons.Left && !p.has_owner_)
                    {
                        cycle_container.AddPoint(p);
                        UpdateCandidate();
                        Display();
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Right && p.has_owner_)
                    {
                        cycle_container.RemovePoint(p);
                        UpdateCandidate();
                        Display();
                    }
                }
                AcceptCycleButt.Enabled = (cycle_container.candidate_.points_.Count() > 0) ? true : false;
            }
        }

        private void AcceptCycleButt_Click(object sender, EventArgs e)
        {
            cycle_container.AcceptCandidate();
            AddCycleButt.Enabled = true;
            AcceptCycleButt.Enabled = false;

            UpdateStats();
            Display();
        }

        private void RemoveCycleButt_Click(object sender, EventArgs e)
        {
            cycle_container.RemoveCycles();
            UpdateStats();
            Display();
        }

        private void saveResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCycles.ShowDialog();
        }

        private void saveCycles_FileOk(object sender, CancelEventArgs e)
        {
            // Get file name.
            string name = SaveCycles.FileName;
            String results = cycle_container.ResultsToFile();
            File.WriteAllText(name, results);
        }

        private void loadResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCycles.ShowDialog();
        }

        private void openCycles_FileOk(object sender, CancelEventArgs e)
        {
            int size = -1;
            DialogResult result = OpenCycles.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = OpenCycles.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size);
            Console.WriteLine(result);
        }

        private void SaveInputButt_Click(object sender, EventArgs e)
        {
            SaveInput.ShowDialog();
        }

        private void saveInput_FileOk(object sender, CancelEventArgs e)
        {
            string name = SaveInput.FileName;
            String points = input.ToFile();
            File.WriteAllText(name, points);
        }

        

    }
}
