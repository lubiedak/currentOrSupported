using System.Windows.Forms;
namespace AI4PowerGrid_gui
{
    partial class AI4PowerGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Map = new System.Windows.Forms.PictureBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PPAuctionGB = new System.Windows.Forms.GroupBox();
            this.PassedBuyingPPbutton = new System.Windows.Forms.Button();
            this.PPMarket = new System.Windows.Forms.PictureBox();
            this.SimulationPanel = new System.Windows.Forms.Panel();
            this.displayCheckBox = new System.Windows.Forms.CheckBox();
            this.TimeDelay = new System.Windows.Forms.NumericUpDown();
            this.timeToDelayLabel = new System.Windows.Forms.Label();
            this.constIncrease = new System.Windows.Forms.NumericUpDown();
            this.StartingMoney = new System.Windows.Forms.NumericUpDown();
            this.fullAISimulation = new System.Windows.Forms.Button();
            this.moneyIncreaseLabel = new System.Windows.Forms.Label();
            this.startingMoneyLabel = new System.Windows.Forms.Label();
            this.ActivePlayerGB = new System.Windows.Forms.GroupBox();
            this.TurnPhase = new System.Windows.Forms.TextBox();
            this.OrderTrackGB = new System.Windows.Forms.GroupBox();
            this.OrderScoreTrack = new System.Windows.Forms.PictureBox();
            this.ResourceMarketGB = new System.Windows.Forms.GroupBox();
            this.NextPlayerBuyResources = new System.Windows.Forms.Button();
            this.ResourceMarketBoard = new System.Windows.Forms.PictureBox();
            this.PlayersGB = new System.Windows.Forms.GroupBox();
            this.ResourceManageButt = new System.Windows.Forms.Button();
            this.PlayersBoard = new System.Windows.Forms.PictureBox();
            this.NextCityBuyer = new System.Windows.Forms.Button();
            this.costLabel = new System.Windows.Forms.Label();
            this.CostNewCities = new System.Windows.Forms.Label();
            this.ClearSelectedCities = new System.Windows.Forms.Button();
            this.nextRoundbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.menu.SuspendLayout();
            this.PPAuctionGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PPMarket)).BeginInit();
            this.SimulationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.constIncrease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartingMoney)).BeginInit();
            this.ActivePlayerGB.SuspendLayout();
            this.OrderTrackGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderScoreTrack)).BeginInit();
            this.ResourceMarketGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResourceMarketBoard)).BeginInit();
            this.PlayersGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // Map
            // 
            this.Map.Location = new System.Drawing.Point(-1, 26);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(736, 800);
            this.Map.TabIndex = 0;
            this.Map.TabStop = false;
            this.Map.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
            this.Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Map_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1264, 24);
            this.menu.TabIndex = 2;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameMenuItem,
            this.exitMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameMenuItem
            // 
            this.newGameMenuItem.Name = "newGameMenuItem";
            this.newGameMenuItem.Size = new System.Drawing.Size(132, 22);
            this.newGameMenuItem.Text = "New Game";
            this.newGameMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // PPAuctionGB
            // 
            this.PPAuctionGB.Controls.Add(this.PassedBuyingPPbutton);
            this.PPAuctionGB.Controls.Add(this.PPMarket);
            this.PPAuctionGB.Location = new System.Drawing.Point(741, 115);
            this.PPAuctionGB.Name = "PPAuctionGB";
            this.PPAuctionGB.Size = new System.Drawing.Size(515, 196);
            this.PPAuctionGB.TabIndex = 3;
            this.PPAuctionGB.TabStop = false;
            this.PPAuctionGB.Text = "PowerPlant Market";
            // 
            // PassedBuyingPPbutton
            // 
            this.PassedBuyingPPbutton.Enabled = false;
            this.PassedBuyingPPbutton.Location = new System.Drawing.Point(483, 19);
            this.PassedBuyingPPbutton.Name = "PassedBuyingPPbutton";
            this.PassedBuyingPPbutton.Size = new System.Drawing.Size(26, 171);
            this.PassedBuyingPPbutton.TabIndex = 1;
            this.PassedBuyingPPbutton.Text = "PASS";
            this.PassedBuyingPPbutton.UseVisualStyleBackColor = true;
            this.PassedBuyingPPbutton.Click += new System.EventHandler(this.PassedBuyingPPbutton_Click);
            // 
            // PPMarket
            // 
            this.PPMarket.Location = new System.Drawing.Point(6, 19);
            this.PPMarket.Name = "PPMarket";
            this.PPMarket.Size = new System.Drawing.Size(476, 171);
            this.PPMarket.TabIndex = 0;
            this.PPMarket.TabStop = false;
            this.PPMarket.Paint += new System.Windows.Forms.PaintEventHandler(this.PPMarket_Paint);
            this.PPMarket.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PPMarket_Click);
            // 
            // SimulationPanel
            // 
            this.SimulationPanel.Controls.Add(this.displayCheckBox);
            this.SimulationPanel.Controls.Add(this.TimeDelay);
            this.SimulationPanel.Controls.Add(this.timeToDelayLabel);
            this.SimulationPanel.Controls.Add(this.constIncrease);
            this.SimulationPanel.Controls.Add(this.StartingMoney);
            this.SimulationPanel.Controls.Add(this.fullAISimulation);
            this.SimulationPanel.Controls.Add(this.moneyIncreaseLabel);
            this.SimulationPanel.Controls.Add(this.startingMoneyLabel);
            this.SimulationPanel.Location = new System.Drawing.Point(740, 115);
            this.SimulationPanel.Name = "SimulationPanel";
            this.SimulationPanel.Size = new System.Drawing.Size(515, 196);
            this.SimulationPanel.TabIndex = 2;
            this.SimulationPanel.Visible = false;
            // 
            // displayCheckBox
            // 
            this.displayCheckBox.AutoSize = true;
            this.displayCheckBox.Location = new System.Drawing.Point(181, 8);
            this.displayCheckBox.Name = "displayCheckBox";
            this.displayCheckBox.Size = new System.Drawing.Size(111, 17);
            this.displayCheckBox.TabIndex = 20;
            this.displayCheckBox.Text = "Display Simulation";
            this.displayCheckBox.UseVisualStyleBackColor = true;
            // 
            // TimeDelay
            // 
            this.TimeDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.TimeDelay.Location = new System.Drawing.Point(247, 27);
            this.TimeDelay.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.TimeDelay.Name = "TimeDelay";
            this.TimeDelay.Size = new System.Drawing.Size(58, 20);
            this.TimeDelay.TabIndex = 19;
            // 
            // timeToDelayLabel
            // 
            this.timeToDelayLabel.AutoSize = true;
            this.timeToDelayLabel.Location = new System.Drawing.Point(178, 29);
            this.timeToDelayLabel.Name = "timeToDelayLabel";
            this.timeToDelayLabel.Size = new System.Drawing.Size(63, 13);
            this.timeToDelayLabel.TabIndex = 18;
            this.timeToDelayLabel.Text = "Time Delay:";
            // 
            // constIncrease
            // 
            this.constIncrease.Location = new System.Drawing.Point(110, 27);
            this.constIncrease.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.constIncrease.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.constIncrease.Name = "constIncrease";
            this.constIncrease.Size = new System.Drawing.Size(59, 20);
            this.constIncrease.TabIndex = 17;
            this.constIncrease.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // StartingMoney
            // 
            this.StartingMoney.Location = new System.Drawing.Point(110, 5);
            this.StartingMoney.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.StartingMoney.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.StartingMoney.Name = "StartingMoney";
            this.StartingMoney.Size = new System.Drawing.Size(59, 20);
            this.StartingMoney.TabIndex = 15;
            this.StartingMoney.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // fullAISimulation
            // 
            this.fullAISimulation.Enabled = false;
            this.fullAISimulation.Location = new System.Drawing.Point(12, 53);
            this.fullAISimulation.Name = "fullAISimulation";
            this.fullAISimulation.Size = new System.Drawing.Size(157, 23);
            this.fullAISimulation.TabIndex = 14;
            this.fullAISimulation.Text = "Full AI Simulation";
            this.fullAISimulation.UseVisualStyleBackColor = true;
            this.fullAISimulation.Click += new System.EventHandler(this.fullAISimulation_Click);
            // 
            // moneyIncreaseLabel
            // 
            this.moneyIncreaseLabel.AutoSize = true;
            this.moneyIncreaseLabel.Location = new System.Drawing.Point(9, 29);
            this.moneyIncreaseLabel.Name = "moneyIncreaseLabel";
            this.moneyIncreaseLabel.Size = new System.Drawing.Size(86, 13);
            this.moneyIncreaseLabel.TabIndex = 2;
            this.moneyIncreaseLabel.Text = "Money Increase:";
            // 
            // startingMoneyLabel
            // 
            this.startingMoneyLabel.AutoSize = true;
            this.startingMoneyLabel.Location = new System.Drawing.Point(9, 7);
            this.startingMoneyLabel.Name = "startingMoneyLabel";
            this.startingMoneyLabel.Size = new System.Drawing.Size(81, 13);
            this.startingMoneyLabel.TabIndex = 0;
            this.startingMoneyLabel.Text = "Starting Money:";
            // 
            // ActivePlayerGB
            // 
            this.ActivePlayerGB.Controls.Add(this.TurnPhase);
            this.ActivePlayerGB.Location = new System.Drawing.Point(741, 27);
            this.ActivePlayerGB.Name = "ActivePlayerGB";
            this.ActivePlayerGB.Size = new System.Drawing.Size(169, 82);
            this.ActivePlayerGB.TabIndex = 4;
            this.ActivePlayerGB.TabStop = false;
            this.ActivePlayerGB.Text = "Stage.Round.Phase";
            // 
            // TurnPhase
            // 
            this.TurnPhase.Enabled = false;
            this.TurnPhase.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TurnPhase.Location = new System.Drawing.Point(8, 14);
            this.TurnPhase.Name = "TurnPhase";
            this.TurnPhase.ReadOnly = true;
            this.TurnPhase.Size = new System.Drawing.Size(155, 62);
            this.TurnPhase.TabIndex = 0;
            // 
            // OrderTrackGB
            // 
            this.OrderTrackGB.Controls.Add(this.OrderScoreTrack);
            this.OrderTrackGB.Location = new System.Drawing.Point(916, 27);
            this.OrderTrackGB.Name = "OrderTrackGB";
            this.OrderTrackGB.Size = new System.Drawing.Size(340, 81);
            this.OrderTrackGB.TabIndex = 5;
            this.OrderTrackGB.TabStop = false;
            this.OrderTrackGB.Text = "Order and Scoring Track";
            // 
            // OrderScoreTrack
            // 
            this.OrderScoreTrack.Location = new System.Drawing.Point(6, 19);
            this.OrderScoreTrack.Name = "OrderScoreTrack";
            this.OrderScoreTrack.Size = new System.Drawing.Size(326, 51);
            this.OrderScoreTrack.TabIndex = 0;
            this.OrderScoreTrack.TabStop = false;
            this.OrderScoreTrack.Paint += new System.Windows.Forms.PaintEventHandler(this.OrderScoreTrack_Paint);
            // 
            // ResourceMarketGB
            // 
            this.ResourceMarketGB.Controls.Add(this.NextPlayerBuyResources);
            this.ResourceMarketGB.Controls.Add(this.ResourceMarketBoard);
            this.ResourceMarketGB.Location = new System.Drawing.Point(743, 317);
            this.ResourceMarketGB.Name = "ResourceMarketGB";
            this.ResourceMarketGB.Size = new System.Drawing.Size(512, 110);
            this.ResourceMarketGB.TabIndex = 6;
            this.ResourceMarketGB.TabStop = false;
            this.ResourceMarketGB.Text = "Resource Market";
            // 
            // NextPlayerBuyResources
            // 
            this.NextPlayerBuyResources.Location = new System.Drawing.Point(481, 19);
            this.NextPlayerBuyResources.Name = "NextPlayerBuyResources";
            this.NextPlayerBuyResources.Size = new System.Drawing.Size(24, 85);
            this.NextPlayerBuyResources.TabIndex = 1;
            this.NextPlayerBuyResources.Text = "PASS";
            this.NextPlayerBuyResources.UseVisualStyleBackColor = true;
            this.NextPlayerBuyResources.Click += new System.EventHandler(this.NextPlayerBuyResources_Click);
            // 
            // ResourceMarketBoard
            // 
            this.ResourceMarketBoard.Location = new System.Drawing.Point(6, 19);
            this.ResourceMarketBoard.Name = "ResourceMarketBoard";
            this.ResourceMarketBoard.Size = new System.Drawing.Size(474, 85);
            this.ResourceMarketBoard.TabIndex = 0;
            this.ResourceMarketBoard.TabStop = false;
            this.ResourceMarketBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.ResourceMarketBoard_Paint);
            this.ResourceMarketBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ResourceMarketBoard_Click);
            // 
            // PlayersGB
            // 
            this.PlayersGB.Controls.Add(this.ResourceManageButt);
            this.PlayersGB.Controls.Add(this.PlayersBoard);
            this.PlayersGB.Location = new System.Drawing.Point(746, 440);
            this.PlayersGB.Name = "PlayersGB";
            this.PlayersGB.Size = new System.Drawing.Size(508, 374);
            this.PlayersGB.TabIndex = 7;
            this.PlayersGB.TabStop = false;
            this.PlayersGB.Text = "Players";
            // 
            // ResourceManageButt
            // 
            this.ResourceManageButt.Location = new System.Drawing.Point(478, 19);
            this.ResourceManageButt.Name = "ResourceManageButt";
            this.ResourceManageButt.Size = new System.Drawing.Size(24, 348);
            this.ResourceManageButt.TabIndex = 1;
            this.ResourceManageButt.Text = "RESOURCE          MANAGEMENT";
            this.ResourceManageButt.UseVisualStyleBackColor = true;
            this.ResourceManageButt.Click += new System.EventHandler(this.ResourceManageButt_Click);
            // 
            // PlayersBoard
            // 
            this.PlayersBoard.Location = new System.Drawing.Point(7, 20);
            this.PlayersBoard.Name = "PlayersBoard";
            this.PlayersBoard.Size = new System.Drawing.Size(470, 348);
            this.PlayersBoard.TabIndex = 0;
            this.PlayersBoard.TabStop = false;
            this.PlayersBoard.Click += new System.EventHandler(this.PlayersBoard_Click);
            this.PlayersBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayersBoard_Paint);
            // 
            // NextCityBuyer
            // 
            this.NextCityBuyer.Location = new System.Drawing.Point(665, 46);
            this.NextCityBuyer.Name = "NextCityBuyer";
            this.NextCityBuyer.Size = new System.Drawing.Size(70, 23);
            this.NextCityBuyer.TabIndex = 9;
            this.NextCityBuyer.Text = "OK, BUY";
            this.NextCityBuyer.UseVisualStyleBackColor = true;
            this.NextCityBuyer.Click += new System.EventHandler(this.NextCityBuyer_Click);
            // 
            // costLabel
            // 
            this.costLabel.AutoSize = true;
            this.costLabel.Location = new System.Drawing.Point(665, 28);
            this.costLabel.Name = "costLabel";
            this.costLabel.Size = new System.Drawing.Size(31, 13);
            this.costLabel.TabIndex = 10;
            this.costLabel.Text = "Cost:";
            // 
            // CostNewCities
            // 
            this.CostNewCities.AutoSize = true;
            this.CostNewCities.Location = new System.Drawing.Point(703, 28);
            this.CostNewCities.Name = "CostNewCities";
            this.CostNewCities.Size = new System.Drawing.Size(13, 13);
            this.CostNewCities.TabIndex = 11;
            this.CostNewCities.Text = "0";
            // 
            // ClearSelectedCities
            // 
            this.ClearSelectedCities.Location = new System.Drawing.Point(665, 72);
            this.ClearSelectedCities.Name = "ClearSelectedCities";
            this.ClearSelectedCities.Size = new System.Drawing.Size(70, 23);
            this.ClearSelectedCities.TabIndex = 12;
            this.ClearSelectedCities.Text = "CLEAR";
            this.ClearSelectedCities.UseVisualStyleBackColor = true;
            this.ClearSelectedCities.Click += new System.EventHandler(this.ClearSelectedCities_Click);
            // 
            // nextRoundbutton
            // 
            this.nextRoundbutton.Location = new System.Drawing.Point(665, 102);
            this.nextRoundbutton.Name = "nextRoundbutton";
            this.nextRoundbutton.Size = new System.Drawing.Size(69, 23);
            this.nextRoundbutton.TabIndex = 13;
            this.nextRoundbutton.Text = "Nxt Ro.";
            this.nextRoundbutton.UseVisualStyleBackColor = true;
            this.nextRoundbutton.Click += new System.EventHandler(this.nextRoundbutton_Click);
            // 
            // AI4PowerGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 826);
            this.Controls.Add(this.nextRoundbutton);
            this.Controls.Add(this.SimulationPanel);
            this.Controls.Add(this.ClearSelectedCities);
            this.Controls.Add(this.CostNewCities);
            this.Controls.Add(this.costLabel);
            this.Controls.Add(this.NextCityBuyer);
            this.Controls.Add(this.PlayersGB);
            this.Controls.Add(this.ResourceMarketGB);
            this.Controls.Add(this.OrderTrackGB);
            this.Controls.Add(this.ActivePlayerGB);
            this.Controls.Add(this.PPAuctionGB);
            this.Controls.Add(this.Map);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menu;
            this.Name = "AI4PowerGrid";
            this.Text = "AI4PowerGrid";
            this.Load += new System.EventHandler(this.AI4PowerGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.PPAuctionGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PPMarket)).EndInit();
            this.SimulationPanel.ResumeLayout(false);
            this.SimulationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.constIncrease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartingMoney)).EndInit();
            this.ActivePlayerGB.ResumeLayout(false);
            this.ActivePlayerGB.PerformLayout();
            this.OrderTrackGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OrderScoreTrack)).EndInit();
            this.ResourceMarketGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResourceMarketBoard)).EndInit();
            this.PlayersGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PlayersBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Map;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.GroupBox PPAuctionGB;
        private System.Windows.Forms.GroupBox ActivePlayerGB;
        private System.Windows.Forms.GroupBox OrderTrackGB;
        private System.Windows.Forms.GroupBox ResourceMarketGB;
        private System.Windows.Forms.GroupBox PlayersGB;
        private System.Windows.Forms.PictureBox PPMarket;
        private System.Windows.Forms.PictureBox ResourceMarketBoard;
        private System.Windows.Forms.PictureBox OrderScoreTrack;
        private System.Windows.Forms.PictureBox PlayersBoard;
        private TextBox TurnPhase;
        private Button PassedBuyingPPbutton;
        private Button NextPlayerBuyResources;
        private Button NextCityBuyer;
        private Label costLabel;
        private Label CostNewCities;
        private Button ClearSelectedCities;
        private Button ResourceManageButt;
        private Panel SimulationPanel;
        private Label moneyIncreaseLabel;
        private Label startingMoneyLabel;
        private Button fullAISimulation;
        private NumericUpDown constIncrease;
        private NumericUpDown StartingMoney;
        private NumericUpDown TimeDelay;
        private Label timeToDelayLabel;
        private CheckBox displayCheckBox;
        private Button nextRoundbutton;

    }
}

