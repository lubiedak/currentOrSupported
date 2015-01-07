namespace AI4PowerGrid_gui
{
    partial class ResourceBuying
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
            this.BuyButt = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CoalPrice = new System.Windows.Forms.Label();
            this.OilPrice = new System.Windows.Forms.Label();
            this.GarbagePrice = new System.Windows.Forms.Label();
            this.UraniumPrice = new System.Windows.Forms.Label();
            this.SumPrice = new System.Windows.Forms.Label();
            this.numericCoal = new System.Windows.Forms.NumericUpDown();
            this.numericOil = new System.Windows.Forms.NumericUpDown();
            this.numericGarbage = new System.Windows.Forms.NumericUpDown();
            this.numericUranium = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCoal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericGarbage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUranium)).BeginInit();
            this.SuspendLayout();
            // 
            // BuyButt
            // 
            this.BuyButt.Location = new System.Drawing.Point(12, 121);
            this.BuyButt.Name = "BuyButt";
            this.BuyButt.Size = new System.Drawing.Size(246, 23);
            this.BuyButt.TabIndex = 0;
            this.BuyButt.Text = "Buy";
            this.BuyButt.UseVisualStyleBackColor = true;
            this.BuyButt.Click += new System.EventHandler(this.BuyButt_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.CoalPrice, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.OilPrice, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.GarbagePrice, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.UraniumPrice, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.SumPrice, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.numericCoal, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericOil, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericGarbage, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUranium, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(245, 103);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Coal";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Oil";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Garbage";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Uranium";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(149, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sum:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CoalPrice
            // 
            this.CoalPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CoalPrice.AutoSize = true;
            this.CoalPrice.Location = new System.Drawing.Point(24, 56);
            this.CoalPrice.Name = "CoalPrice";
            this.CoalPrice.Size = new System.Drawing.Size(13, 13);
            this.CoalPrice.TabIndex = 11;
            this.CoalPrice.Text = "0";
            // 
            // OilPrice
            // 
            this.OilPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OilPrice.AutoSize = true;
            this.OilPrice.Location = new System.Drawing.Point(85, 56);
            this.OilPrice.Name = "OilPrice";
            this.OilPrice.Size = new System.Drawing.Size(13, 13);
            this.OilPrice.TabIndex = 12;
            this.OilPrice.Text = "0";
            // 
            // GarbagePrice
            // 
            this.GarbagePrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GarbagePrice.AutoSize = true;
            this.GarbagePrice.Location = new System.Drawing.Point(146, 56);
            this.GarbagePrice.Name = "GarbagePrice";
            this.GarbagePrice.Size = new System.Drawing.Size(13, 13);
            this.GarbagePrice.TabIndex = 13;
            this.GarbagePrice.Text = "0";
            // 
            // UraniumPrice
            // 
            this.UraniumPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UraniumPrice.AutoSize = true;
            this.UraniumPrice.Location = new System.Drawing.Point(207, 56);
            this.UraniumPrice.Name = "UraniumPrice";
            this.UraniumPrice.Size = new System.Drawing.Size(13, 13);
            this.UraniumPrice.TabIndex = 14;
            this.UraniumPrice.Text = "0";
            // 
            // SumPrice
            // 
            this.SumPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SumPrice.AutoSize = true;
            this.SumPrice.Location = new System.Drawing.Point(207, 82);
            this.SumPrice.Name = "SumPrice";
            this.SumPrice.Size = new System.Drawing.Size(13, 13);
            this.SumPrice.TabIndex = 15;
            this.SumPrice.Text = "0";
            // 
            // numericCoal
            // 
            this.numericCoal.Enabled = false;
            this.numericCoal.Location = new System.Drawing.Point(3, 28);
            this.numericCoal.Name = "numericCoal";
            this.numericCoal.Size = new System.Drawing.Size(55, 20);
            this.numericCoal.TabIndex = 16;
            this.numericCoal.ValueChanged += new System.EventHandler(this.numericCoal_ValueChanged);
            // 
            // numericOil
            // 
            this.numericOil.Enabled = false;
            this.numericOil.Location = new System.Drawing.Point(64, 28);
            this.numericOil.Name = "numericOil";
            this.numericOil.Size = new System.Drawing.Size(55, 20);
            this.numericOil.TabIndex = 17;
            this.numericOil.ValueChanged += new System.EventHandler(this.numericOil_ValueChanged);
            // 
            // numericGarbage
            // 
            this.numericGarbage.Enabled = false;
            this.numericGarbage.Location = new System.Drawing.Point(125, 28);
            this.numericGarbage.Name = "numericGarbage";
            this.numericGarbage.Size = new System.Drawing.Size(55, 20);
            this.numericGarbage.TabIndex = 18;
            this.numericGarbage.ValueChanged += new System.EventHandler(this.numericGarbage_ValueChanged);
            // 
            // numericUranium
            // 
            this.numericUranium.Enabled = false;
            this.numericUranium.Location = new System.Drawing.Point(186, 28);
            this.numericUranium.Name = "numericUranium";
            this.numericUranium.Size = new System.Drawing.Size(56, 20);
            this.numericUranium.TabIndex = 19;
            this.numericUranium.ValueChanged += new System.EventHandler(this.numericUranium_ValueChanged);
            // 
            // ResourceBuying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 157);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BuyButt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ResourceBuying";
            this.Text = "ResourceBuying";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCoal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericGarbage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUranium)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BuyButt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label CoalPrice;
        private System.Windows.Forms.Label OilPrice;
        private System.Windows.Forms.Label GarbagePrice;
        private System.Windows.Forms.Label UraniumPrice;
        private System.Windows.Forms.Label SumPrice;
        private System.Windows.Forms.NumericUpDown numericCoal;
        private System.Windows.Forms.NumericUpDown numericOil;
        private System.Windows.Forms.NumericUpDown numericGarbage;
        private System.Windows.Forms.NumericUpDown numericUranium;
    }
}