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
    public partial class NewGameWindow : Form
    {
        AI4PowerGrid sender_;
        public NewGameWindow(AI4PowerGrid sender)
        {
            sender_ = sender;
            InitializeComponent();
        }

        private void NewGameOKB_Click(object sender, EventArgs e)
        {
            //new game creation
            this.Close();
            List<String> players_list =
                new List<String> { comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text };

            sender_.InitializeGame(sender, e, players_list);
            
        }

        private void NewGameCancelB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simulationButton_Click(object sender, EventArgs e)
        {
            List<String> players_list =
                new List<String> { comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text };
            
            sender_.InitializeSimulation(sender, e, players_list);
            this.Close();
        }

    }
}
