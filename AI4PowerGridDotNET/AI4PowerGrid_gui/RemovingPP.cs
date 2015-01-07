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
    public partial class RemovingPP : Form
    {
        List<String> power_plants;
        public int selected_pp;
        public RemovingPP(List<String> power_plants)
        {
            InitializeComponent();
            this.power_plants = power_plants;
            PPRadio = new List<RadioButton>();
            AddPowerPlantsRButtons();
        }

        private void AddPowerPlantsRButtons()
        {
            int i = 0;
            foreach (String pp_text in power_plants)
            {
                RadioButton rb = new RadioButton();
                rb.Text = pp_text;
                rb.Location = new Point(5,20+ 30 * i);
                rb.Width = 200;
                PPRadio.Add(rb);
                groupBox.Controls.Add(rb);
                i++;
            }
        }

        private void RemovePPbutton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i< PPRadio.Count; i++)
            {
                if (PPRadio[i].Checked)
                {
                    selected_pp = i;
                }
            }
            this.Close();
        }
    }
}
