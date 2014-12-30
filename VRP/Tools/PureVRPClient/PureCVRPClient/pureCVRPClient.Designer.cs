namespace PureCVRPClient
{
    partial class pureCVRPClient
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
            this.components = new System.ComponentModel.Container();
            this.Map = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NumberOfPoints = new System.Windows.Forms.NumericUpDown();
            this.MaxDemand = new System.Windows.Forms.NumericUpDown();
            this.MinDemand = new System.Windows.Forms.NumericUpDown();
            this.SaveInputButt = new System.Windows.Forms.Button();
            this.DemandsGenButt = new System.Windows.Forms.Button();
            this.CoordinatesGenButt = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RemoveCycleButt = new System.Windows.Forms.Button();
            this.AverageFullfilment = new System.Windows.Forms.TextBox();
            this.DemandsSumAll = new System.Windows.Forms.TextBox();
            this.LengthSumAll = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ResultList = new System.Windows.Forms.CheckedListBox();
            this.AcceptCycleButt = new System.Windows.Forms.Button();
            this.AddCycleButt = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.LengthField = new System.Windows.Forms.TextBox();
            this.DemandsSumField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveCycles = new System.Windows.Forms.SaveFileDialog();
            this.OpenCycles = new System.Windows.Forms.OpenFileDialog();
            this.SaveInput = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Map)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxDemand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinDemand)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Map
            // 
            this.Map.Location = new System.Drawing.Point(0, 27);
            this.Map.Name = "Map";
            this.Map.Size = new System.Drawing.Size(800, 800);
            this.Map.TabIndex = 0;
            this.Map.TabStop = false;
            this.Map.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
            this.Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NumberOfPoints);
            this.groupBox1.Controls.Add(this.MaxDemand);
            this.groupBox1.Controls.Add(this.MinDemand);
            this.groupBox1.Controls.Add(this.SaveInputButt);
            this.groupBox1.Controls.Add(this.DemandsGenButt);
            this.groupBox1.Controls.Add(this.CoordinatesGenButt);
            this.groupBox1.Location = new System.Drawing.Point(806, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 129);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Data";
            // 
            // NumberOfPoints
            // 
            this.NumberOfPoints.Location = new System.Drawing.Point(7, 20);
            this.NumberOfPoints.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.NumberOfPoints.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NumberOfPoints.Name = "NumberOfPoints";
            this.NumberOfPoints.Size = new System.Drawing.Size(61, 20);
            this.NumberOfPoints.TabIndex = 6;
            this.NumberOfPoints.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // MaxDemand
            // 
            this.MaxDemand.Location = new System.Drawing.Point(7, 71);
            this.MaxDemand.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.MaxDemand.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.MaxDemand.Name = "MaxDemand";
            this.MaxDemand.Size = new System.Drawing.Size(61, 20);
            this.MaxDemand.TabIndex = 5;
            this.MaxDemand.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // MinDemand
            // 
            this.MinDemand.Location = new System.Drawing.Point(7, 45);
            this.MinDemand.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.MinDemand.Minimum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.MinDemand.Name = "MinDemand";
            this.MinDemand.Size = new System.Drawing.Size(61, 20);
            this.MinDemand.TabIndex = 4;
            this.MinDemand.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // SaveInputButt
            // 
            this.SaveInputButt.Location = new System.Drawing.Point(7, 97);
            this.SaveInputButt.Name = "SaveInputButt";
            this.SaveInputButt.Size = new System.Drawing.Size(185, 23);
            this.SaveInputButt.TabIndex = 3;
            this.SaveInputButt.Text = "Save Input";
            this.SaveInputButt.UseVisualStyleBackColor = true;
            this.SaveInputButt.Click += new System.EventHandler(this.SaveInputButt_Click);
            // 
            // DemandsGenButt
            // 
            this.DemandsGenButt.Location = new System.Drawing.Point(74, 48);
            this.DemandsGenButt.Name = "DemandsGenButt";
            this.DemandsGenButt.Size = new System.Drawing.Size(118, 43);
            this.DemandsGenButt.TabIndex = 2;
            this.DemandsGenButt.Text = "GenerateDemands";
            this.DemandsGenButt.UseVisualStyleBackColor = true;
            this.DemandsGenButt.Click += new System.EventHandler(this.DemandsGenButt_Click);
            // 
            // CoordinatesGenButt
            // 
            this.CoordinatesGenButt.Location = new System.Drawing.Point(74, 19);
            this.CoordinatesGenButt.Name = "CoordinatesGenButt";
            this.CoordinatesGenButt.Size = new System.Drawing.Size(118, 23);
            this.CoordinatesGenButt.TabIndex = 0;
            this.CoordinatesGenButt.Text = "Generate Coordinates";
            this.CoordinatesGenButt.UseVisualStyleBackColor = true;
            this.CoordinatesGenButt.Click += new System.EventHandler(this.CoordinatesGenButt_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RemoveCycleButt);
            this.groupBox2.Controls.Add(this.AverageFullfilment);
            this.groupBox2.Controls.Add(this.DemandsSumAll);
            this.groupBox2.Controls.Add(this.LengthSumAll);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.ResultList);
            this.groupBox2.Controls.Add(this.AcceptCycleButt);
            this.groupBox2.Controls.Add(this.AddCycleButt);
            this.groupBox2.Location = new System.Drawing.Point(806, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 662);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output Data";
            // 
            // RemoveCycleButt
            // 
            this.RemoveCycleButt.Location = new System.Drawing.Point(7, 563);
            this.RemoveCycleButt.Name = "RemoveCycleButt";
            this.RemoveCycleButt.Size = new System.Drawing.Size(185, 23);
            this.RemoveCycleButt.TabIndex = 10;
            this.RemoveCycleButt.Text = "Remove Selected";
            this.RemoveCycleButt.UseVisualStyleBackColor = true;
            this.RemoveCycleButt.Click += new System.EventHandler(this.RemoveCycleButt_Click);
            // 
            // AverageFullfilment
            // 
            this.AverageFullfilment.Location = new System.Drawing.Point(105, 125);
            this.AverageFullfilment.Name = "AverageFullfilment";
            this.AverageFullfilment.ReadOnly = true;
            this.AverageFullfilment.Size = new System.Drawing.Size(87, 20);
            this.AverageFullfilment.TabIndex = 9;
            // 
            // DemandsSumAll
            // 
            this.DemandsSumAll.Location = new System.Drawing.Point(105, 103);
            this.DemandsSumAll.Name = "DemandsSumAll";
            this.DemandsSumAll.ReadOnly = true;
            this.DemandsSumAll.Size = new System.Drawing.Size(87, 20);
            this.DemandsSumAll.TabIndex = 8;
            // 
            // LengthSumAll
            // 
            this.LengthSumAll.Location = new System.Drawing.Point(105, 81);
            this.LengthSumAll.Name = "LengthSumAll";
            this.LengthSumAll.ReadOnly = true;
            this.LengthSumAll.Size = new System.Drawing.Size(87, 20);
            this.LengthSumAll.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "% Fullfilment:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Demands Sum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Length Sum:";
            // 
            // ResultList
            // 
            this.ResultList.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ResultList.FormattingEnabled = true;
            this.ResultList.Location = new System.Drawing.Point(7, 148);
            this.ResultList.Name = "ResultList";
            this.ResultList.Size = new System.Drawing.Size(185, 409);
            this.ResultList.TabIndex = 3;
            // 
            // AcceptCycleButt
            // 
            this.AcceptCycleButt.Enabled = false;
            this.AcceptCycleButt.Location = new System.Drawing.Point(7, 48);
            this.AcceptCycleButt.Name = "AcceptCycleButt";
            this.AcceptCycleButt.Size = new System.Drawing.Size(185, 23);
            this.AcceptCycleButt.TabIndex = 1;
            this.AcceptCycleButt.Text = "Accept Cycle";
            this.AcceptCycleButt.UseVisualStyleBackColor = true;
            this.AcceptCycleButt.Click += new System.EventHandler(this.AcceptCycleButt_Click);
            // 
            // AddCycleButt
            // 
            this.AddCycleButt.Enabled = false;
            this.AddCycleButt.Location = new System.Drawing.Point(7, 19);
            this.AddCycleButt.Name = "AddCycleButt";
            this.AddCycleButt.Size = new System.Drawing.Size(185, 23);
            this.AddCycleButt.TabIndex = 0;
            this.AddCycleButt.Text = "Add Cycle";
            this.AddCycleButt.UseVisualStyleBackColor = true;
            this.AddCycleButt.Click += new System.EventHandler(this.AddCycleButt_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LengthField
            // 
            this.LengthField.Location = new System.Drawing.Point(741, 27);
            this.LengthField.Name = "LengthField";
            this.LengthField.ReadOnly = true;
            this.LengthField.Size = new System.Drawing.Size(59, 20);
            this.LengthField.TabIndex = 3;
            // 
            // DemandsSumField
            // 
            this.DemandsSumField.Location = new System.Drawing.Point(741, 48);
            this.DemandsSumField.Name = "DemandsSumField";
            this.DemandsSumField.ReadOnly = true;
            this.DemandsSumField.Size = new System.Drawing.Size(59, 20);
            this.DemandsSumField.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(667, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Length";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(666, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Demands Sum";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1010, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveResultsToolStripMenuItem,
            this.loadResultsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveResultsToolStripMenuItem
            // 
            this.saveResultsToolStripMenuItem.Name = "saveResultsToolStripMenuItem";
            this.saveResultsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveResultsToolStripMenuItem.Text = "Save Results";
            this.saveResultsToolStripMenuItem.Click += new System.EventHandler(this.saveResultsToolStripMenuItem_Click);
            // 
            // loadResultsToolStripMenuItem
            // 
            this.loadResultsToolStripMenuItem.Name = "loadResultsToolStripMenuItem";
            this.loadResultsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loadResultsToolStripMenuItem.Text = "Load Results";
            this.loadResultsToolStripMenuItem.Click += new System.EventHandler(this.loadResultsToolStripMenuItem_Click);
            // 
            // SaveCycles
            // 
            this.SaveCycles.FileOk += new System.ComponentModel.CancelEventHandler(this.saveCycles_FileOk);
            // 
            // OpenCycles
            // 
            this.OpenCycles.FileOk += new System.ComponentModel.CancelEventHandler(this.openCycles_FileOk);
            // 
            // SaveInput
            // 
            this.SaveInput.FileOk += new System.ComponentModel.CancelEventHandler(this.saveInput_FileOk);
            // 
            // pureCVRPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 828);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DemandsSumField);
            this.Controls.Add(this.LengthField);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Map);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "pureCVRPClient";
            this.Text = "pureCVRPClient";
            ((System.ComponentModel.ISupportInitialize)(this.Map)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumberOfPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxDemand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinDemand)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Map;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CoordinatesGenButt;
        private System.Windows.Forms.Button DemandsGenButt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button AcceptCycleButt;
        private System.Windows.Forms.Button AddCycleButt;
        private System.Windows.Forms.Button SaveInputButt;
        private System.Windows.Forms.NumericUpDown MaxDemand;
        private System.Windows.Forms.NumericUpDown MinDemand;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.NumericUpDown NumberOfPoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DemandsSumField;
        private System.Windows.Forms.TextBox LengthField;
        private System.Windows.Forms.CheckedListBox ResultList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AverageFullfilment;
        private System.Windows.Forms.TextBox DemandsSumAll;
        private System.Windows.Forms.TextBox LengthSumAll;
        private System.Windows.Forms.Button RemoveCycleButt;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadResultsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SaveCycles;
        private System.Windows.Forms.OpenFileDialog OpenCycles;
        private System.Windows.Forms.SaveFileDialog SaveInput;
    }
}

