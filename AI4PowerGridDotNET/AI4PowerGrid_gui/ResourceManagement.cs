using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameModel;

namespace AI4PowerGrid_gui
{
    public partial class ResourceManagement : Form
    {
        public List<ManagerPP> pp_managers;
        public List<ManagerPP_UI> pp_managers_ui;
        Dictionary<R.ResType, int> available_resources;
        Dictionary<R.ResType, Label> used_resources;
        List<PowerPlant_gui> players_power_plants_;

        public bool should_be_managed;

        public ResourceManagement(List<PowerPlant_gui> power_plants_source)
        {
            InitializeComponent();
            players_power_plants_ = power_plants_source;
            InitPP(power_plants_source);
            CountAvailableResources();
            InitTable();
            ShouldBeManaged();
        }

        private void InitPP(List<PowerPlant_gui> power_plants_source)
        {
            pp_managers = new List<ManagerPP>();
            pp_managers_ui = new List<ManagerPP_UI>();
            foreach (PowerPlant_gui pp in power_plants_source)
            {
                if (pp.GetPPType() != PP.PPtype.FREE)
                {
                    pp_managers.Add(new ManagerPP(pp));
                    pp_managers_ui.Add(new ManagerPP_UI(pp));
                }
            }
        }

        private void CountAvailableResources()
        {
            available_resources = new Dictionary<R.ResType, int>();
            foreach (ManagerPP pp in pp_managers)
            {
                foreach (R.ResType res_t in pp.resources_.Keys)
                {
                    if (available_resources.ContainsKey(res_t))
                    {
                        available_resources[res_t] += pp.resources_[res_t];
                    }
                    else
                    {
                        available_resources.Add(res_t, pp.resources_[res_t]);
                    }
                }
            }
            
        }

        private void InitTable()
        {
            used_resources = new Dictionary<R.ResType, Label>();
            foreach (R.ResType res_t in available_resources.Keys)
            {
                Label all_res_label = new Label();
                all_res_label.Text = available_resources[res_t].ToString();
                all_res_label.Dock = DockStyle.Fill;
                all_res_label.TextAlign = ContentAlignment.MiddleCenter;
                table.Controls.Add(all_res_label, (int)res_t + 1, 1);

                Label used = new Label();
                used.Text = available_resources[res_t].ToString();
                used.Dock = DockStyle.Fill;
                used.TextAlign = ContentAlignment.MiddleCenter;
                used_resources.Add(res_t, used);
            }

            foreach (R.ResType res_t in used_resources.Keys)
            {
                table.Controls.Add(used_resources[res_t], (int)res_t + 1, 6);
            }

            int row = 2;
            foreach(ManagerPP_UI pp_ui in pp_managers_ui)
            {
                table.Controls.Add(pp_ui.name_, 0, row);
                foreach (R.ResType res_t in pp_ui.res_numerics_.Keys)
                {
                    table.Controls.Add(pp_ui.res_numerics_[res_t], (int)res_t + 1, row);
                    pp_ui.res_numerics_[res_t].ValueChanged += ResourceManagement_ValueChanged;
                }
                row++;
            }
        }

        private void ShouldBeManaged()
        {
            List<R.ResType> res_list = new List<R.ResType>();
            should_be_managed = false;
            foreach (PowerPlant_gui pp in players_power_plants_)
            {
                List<R.ResType> resources = pp.GetRTypes();
                foreach (R.ResType res_t in resources)
                {
                    if (res_list.Contains(res_t))
                    {
                        should_be_managed = true;
                        return;
                    }
                    else
                    {
                        res_list.Add(res_t);
                    }
                }
            }
        }

        void ResourceManagement_ValueChanged(object sender, EventArgs e)
        {
            Dictionary<R.ResType, int> used = new Dictionary<R.ResType,int>(available_resources);
            foreach (R.ResType res_t in available_resources.Keys)
            {
                used[res_t] = 0;
            }
            foreach (ManagerPP_UI pp_ui in pp_managers_ui)
            {
                foreach (R.ResType res_t in pp_ui.res_numerics_.Keys)
                {
                    used[res_t] += (int)pp_ui.res_numerics_[res_t].Value;
                }
            }
            foreach (R.ResType res_t in used.Keys)
            {
                used_resources[res_t].Text = used[res_t].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckDataCorectness())
            {
                int i = 0;
                foreach(PowerPlant_gui pp in  players_power_plants_)
                {
                    if (pp.GetPPType() != PP.PPtype.FREE)
                    {
                        pp.ApplyManagerChanges(pp_managers_ui[i]);
                        i++;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Available resources doesn't match used one.",
                    "Important Note",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private bool CheckDataCorectness()
        {
            foreach (R.ResType res_t in used_resources.Keys)
            {
                if (used_resources[res_t].Text != available_resources[res_t].ToString())
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class Manager
    {
        public int id_;
        public int capacity_;

        public Manager() { }
    }

    public class ManagerPP : Manager
    {
        public Dictionary<R.ResType, int> resources_;
        
        public ManagerPP(PowerPlant_gui pp)
        {
            id_ = pp.getIdPrice();
            capacity_ = 2*pp.getCapacity();
            resources_ = pp.GetResourcesAsDict();
        }
    }

    public class ManagerPP_UI : Manager
    {
        public Label name_;
        public Dictionary<R.ResType, NumericUpDown> res_numerics_;

        public ManagerPP_UI(PowerPlant_gui pp)
        {
            name_ = new Label();
            name_.Text = pp.ToString();
            name_.Dock = DockStyle.Fill;
            name_.TextAlign = ContentAlignment.MiddleLeft;
            
            id_ = pp.getIdPrice();
            capacity_ = 2*pp.getCapacity();

            res_numerics_ = new Dictionary<R.ResType, NumericUpDown>();

            Dictionary<R.ResType, int> res_dict = pp.GetResourcesAsDict();
            foreach (R.ResType res_t in res_dict.Keys)
            {
                NumericUpDown numeric = new NumericUpDown();
                numeric.Dock = DockStyle.Fill;
                numeric.Maximum = capacity_;
                numeric.Value = res_dict[res_t];
                res_numerics_.Add(res_t, numeric);
            }
        }
    }
}
