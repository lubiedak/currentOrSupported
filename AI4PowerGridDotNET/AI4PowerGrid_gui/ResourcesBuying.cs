using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI4PowerGrid_gui
{
    public partial class ResourcesBuying : Form
    {
        Dictionary<String, Dictionary<R.type, int>> pp_with_res_;
        public Dictionary<String, Dictionary<R.type, int>> pp_with_res_requests;
        Dictionary<Label, Dictionary<Label, NumericUpDown>> pp_with_res_ui;
        Dictionary<R.type, List<int>> prices_;

        public ResourcesBuying(Dictionary<String, Dictionary<R.type, int>> pp_with_res, Dictionary<R.type, List<int>> prices)
        {
            pp_with_res_ = pp_with_res;
            prices_ = prices;
            pp_with_res_requests = new Dictionary<string, Dictionary<R.type, int>>();
            InitializeComponent();
            CreateInterface();
        }

        private void CreateInterface()
        {
            pp_with_res_ui = new Dictionary<Label, Dictionary<Label,NumericUpDown>>();

            int table_size = table.Width;
            foreach (String pp in pp_with_res_.Keys)
            {
                Label name = new Label();
                name.Text = pp;
                name.Dock = DockStyle.Fill;
                name.TextAlign = ContentAlignment.MiddleLeft;
                Dictionary<Label, NumericUpDown> dict = new Dictionary<Label, NumericUpDown>();
                Dictionary<R.type, int> request = new Dictionary<R.type, int>();
                foreach(R.type res_t in pp_with_res_[pp].Keys)
                {

                    request.Add(res_t, 0);
                    NumericUpDown num = new NumericUpDown();
                    num.Maximum = pp_with_res_[pp][res_t];
                    num.Dock = DockStyle.Fill;
                    
                    num.ValueChanged += new System.EventHandler(ComputeCost);

                    Label res_n = new Label();
                    res_n.Text = res_t.ToString();
                    res_n.Dock = DockStyle.Fill;
                    dict.Add(res_n, num);
                }
                pp_with_res_ui.Add(name, dict);
                pp_with_res_requests.Add(pp, request);
            }

            int col = 0, row = 0;
            foreach(Label pp_name in pp_with_res_ui.Keys)
            {
                table.Controls.Add(pp_name);
                table.SetCellPosition(pp_name, new TableLayoutPanelCellPosition(col, row));
                table.SetRowSpan(pp_name, 2);
                col++;
                foreach (Label res_name in pp_with_res_ui[pp_name].Keys)
                {
                    table.Controls.Add(res_name);
                    table.SetCellPosition(res_name, new TableLayoutPanelCellPosition( col, row));
                    
                    table.Controls.Add(pp_with_res_ui[pp_name][res_name]);
                    table.SetCellPosition(pp_with_res_ui[pp_name][res_name], new TableLayoutPanelCellPosition(col, row + 1));
                    col++;
                }
                row += 2;
                col = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int check = 0;
            int x = check;
        }

        private void AddToRequest()
        {
            
        }

        private void ComputeCost(object sender, EventArgs e)
        {
            
        }

        private void AcceptSelctedRequests()
        {

        }
    }

    
}
