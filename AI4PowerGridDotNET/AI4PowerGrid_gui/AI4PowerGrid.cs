using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameModel;

namespace AI4PowerGrid_gui
{
    public delegate void InvalidateCallbackType();
    public partial class AI4PowerGrid : Form
    {
        bool board_initialized;
        bool simulation;
        GameUI ui;
        SimulationMode sm;

        Bitmap MapBoard;
        Bitmap PPMarketBoard;
        Bitmap ResMarketBoard;
        Bitmap OrderScoringTrack;
        Bitmap PlayersPP;
        InvalidateCallbackType invalidateAllDelegate;

        public AI4PowerGrid()
        {
            board_initialized = false;
            simulation = false;
            InitializeComponent();

            MapBoard = new Bitmap(Map.ClientSize.Width, Map.ClientSize.Height);
            PPMarketBoard = new Bitmap(PPMarket.ClientSize.Width, PPMarket.ClientSize.Height);
            ResMarketBoard = new Bitmap(ResourceMarketBoard.ClientSize.Width, ResourceMarketBoard.ClientSize.Height);
            OrderScoringTrack = new Bitmap(OrderTrackGB.ClientSize.Width, OrderTrackGB.ClientSize.Height);
            PlayersPP = new Bitmap(PlayersBoard.ClientSize.Width, PlayersBoard.ClientSize.Height);

            invalidateAllDelegate = new InvalidateCallbackType(this.InvalidateAll);
        }

        public void FooCallbackType()
        {
        }

        private void AI4PowerGrid_Load(object sender, EventArgs e)
        {
            
        }

        public void InitializeGame(object sender, EventArgs e, List<String> player_list)
        {

            ui = new GameUI(player_list,
                            MapBoard,
                            PPMarketBoard,
                            ResMarketBoard,
                            OrderScoringTrack,
                            PlayersPP,
                            invalidateAllDelegate);
            board_initialized = true;
            GameFlow.active_phase = phase.AUCTION;
            GameFlow.round = 0;
            
            InvalidateAll();
            if (player_list[0] == "AI")
            {
                ui.AuctionPhase(sender, null);
            }
        }

        public void InitializeSimulation(object sender, EventArgs e, List<String> player_list)
        {
            MapBoard = new Bitmap(Map.ClientSize.Width, Map.ClientSize.Height);
            OrderScoringTrack = new Bitmap(OrderTrackGB.ClientSize.Width, OrderTrackGB.ClientSize.Height);
            sm = new SimulationMode(player_list, MapBoard, OrderScoringTrack, PlayersPP);

            SimulationPanel.Visible = true;

            if (sm.AllPlayersAreAI())
            {
                fullAISimulation.Enabled = true;
            }

            Map.Invalidate();
            OrderScoreTrack.Invalidate();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGameWindow new_game = new NewGameWindow(this);
            new_game.ShowDialog();
        }

        private void InvalidateAll()
        {
            Map.Invalidate();
            PPMarket.Invalidate();
            OrderScoreTrack.Invalidate();
            ResourceMarketBoard.Invalidate();
            PlayersBoard.Invalidate();

            int activ = (int)GameFlow.active_phase;
            if (activ > (int)phase.AUCTION && GameFlow.round < 1)
            {
                PassedBuyingPPbutton.Enabled = true;
            }
            TurnPhase.Text = (GameFlow.step+1).ToString()+"."+ (GameFlow.round+1).ToString() + "." + activ.ToString();
            CostNewCities.Text = ui.cost_of_buying_selected_cities.ToString();
            ColorizeActivePhase();
        }

        private void ColorizeToDefault()
        {
            PPAuctionGB.BackColor = SystemColors.Control;
            ResourceMarketGB.BackColor = SystemColors.Control;
            PlayersGB.BackColor = SystemColors.Control;
        }

        private void ColorizeActivePhase()
        {
            ColorizeToDefault();
            if (GameFlow.active_phase == phase.AUCTION)
            {
                PPAuctionGB.BackColor = Color.LightBlue;
            }
            else if (GameFlow.active_phase == phase.RESOURCE_BUYING)
            {
                ResourceMarketGB.BackColor = Color.LightBlue;
            }
            else if (GameFlow.active_phase == phase.SELLING_ELECTRICITY)
            {
                PlayersGB.BackColor = Color.LightBlue;
            }
            
            PPAuctionGB.Invalidate();
            ResourceMarketGB.Invalidate();
            PlayersGB.Invalidate();
        }

        

        private void PPMarket_Click(object sender, MouseEventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.AUCTION)
            {
                ui.AuctionPhase(sender, e);
                InvalidateAll();
            }
        }

        private void PassedBuyingPPbutton_Click(object sender, EventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.AUCTION)
            {
                ui.AuctionPhasePassed();
                InvalidateAll();
            }
        }



        //
        // RESOURCE_BUYING   RESOURCE_BUYING   RESOURCE_BUYING
        //

        private void ResourceMarketBoard_Click(object sender, MouseEventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.RESOURCE_BUYING)
            {
                ui.ResourceBuying();
                InvalidateAll();
            }
        }

        private void NextPlayerBuyResources_Click(object sender, EventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.RESOURCE_BUYING)
            {
                ui.ResourceBuyingPassed();
                InvalidateAll();
            }
        }


        //
        // BUYING_NEW_HOUSES   BUYING_NEW_HOUSES   BUYING_NEW_HOUSES
        //

        private void Map_Click(object sender, MouseEventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.BUYING_NEW_HOUSES)
            {
                ui.BuyingNewHouses(e);
                InvalidateAll();
            }
            if (simulation)
            {
                sm.SelectFirstCity(e);
                Map.Invalidate();
            }
        }

        private void NextCityBuyer_Click(object sender, EventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.BUYING_NEW_HOUSES)
            {
                ui.AcceptSelectedCities();
                InvalidateAll();
            }
            if (simulation)
            {
                sm.PreviousPlayer();
                OrderScoreTrack.Invalidate();
                PlayersBoard.Invalidate();
            }
        }

        private void ClearSelectedCities_Click(object sender, EventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.BUYING_NEW_HOUSES)
            {
                ui.ClearAllSelectedCities();
                InvalidateAll();
            }
        }


        private void PlayersBoard_Click(object sender, EventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.SELLING_ELECTRICITY)
            {
                ui.SellingElectricity();
                InvalidateAll();
            }
        }

        private void ResourceManageButt_Click(object sender, EventArgs e)
        {
            if (board_initialized)
            {
                ui.ManageResources();
                InvalidateAll();
            }
        }


        private void Map_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(MapBoard, 0, 0, MapBoard.Width, MapBoard.Height);
        }

        private void PPMarket_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(PPMarketBoard, 0, 0, PPMarketBoard.Width, PPMarketBoard.Height);
        }

        private void ResourceMarketBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(ResMarketBoard, 0, 0, ResMarketBoard.Width, ResMarketBoard.Height);
        }

        private void OrderScoreTrack_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(OrderScoringTrack, 0, 0, OrderScoringTrack.Width, OrderScoringTrack.Height);
        }

        private void PlayersBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(PlayersPP, 0, 0, PlayersPP.Width, PlayersPP.Height);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void startSimulation_Click(object sender, EventArgs e)
        {
            simulation = true;
        }

        private void fullAISimulation_Click(object sender, EventArgs e)
        {
            sm.SetSimulationParameters((int)StartingMoney.Value, (int)constIncrease.Value, (int)TimeDelay.Value);
            sm.RunFullAI(Map, OrderScoreTrack, PlayersBoard);
            Map.Invalidate();
        }

        private void nextRoundbutton_Click(object sender, EventArgs e)
        {
            if (board_initialized && GameFlow.active_phase == phase.AUCTION)
            {
                ui.AuctionPhase(sender, null);
                InvalidateAll();
            }
        }
    }
}
