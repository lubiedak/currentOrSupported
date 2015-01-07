namespace AI4PowerGrid_gui
{
    partial class AuctionWindow
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
            this.bidPrice = new System.Windows.Forms.ComboBox();
            this.buttonBid = new System.Windows.Forms.Button();
            this.buttonPass = new System.Windows.Forms.Button();
            this.BiddersTrack = new System.Windows.Forms.PictureBox();
            this.PPlant = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BiddersTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PPlant)).BeginInit();
            this.SuspendLayout();
            // 
            // bidPrice
            // 
            this.bidPrice.FormattingEnabled = true;
            this.bidPrice.Location = new System.Drawing.Point(98, 57);
            this.bidPrice.Name = "bidPrice";
            this.bidPrice.Size = new System.Drawing.Size(69, 21);
            this.bidPrice.TabIndex = 0;
            // 
            // buttonBid
            // 
            this.buttonBid.Location = new System.Drawing.Point(98, 84);
            this.buttonBid.Name = "buttonBid";
            this.buttonBid.Size = new System.Drawing.Size(69, 23);
            this.buttonBid.TabIndex = 1;
            this.buttonBid.Text = "Bid";
            this.buttonBid.UseVisualStyleBackColor = true;
            this.buttonBid.Click += new System.EventHandler(this.buttonBid_Click);
            // 
            // buttonPass
            // 
            this.buttonPass.Location = new System.Drawing.Point(98, 114);
            this.buttonPass.Name = "buttonPass";
            this.buttonPass.Size = new System.Drawing.Size(69, 23);
            this.buttonPass.TabIndex = 2;
            this.buttonPass.Text = "Pass";
            this.buttonPass.UseVisualStyleBackColor = true;
            this.buttonPass.Click += new System.EventHandler(this.buttonPass_Click);
            // 
            // BiddersTrack
            // 
            this.BiddersTrack.Location = new System.Drawing.Point(12, 12);
            this.BiddersTrack.Name = "BiddersTrack";
            this.BiddersTrack.Size = new System.Drawing.Size(155, 30);
            this.BiddersTrack.TabIndex = 3;
            this.BiddersTrack.TabStop = false;
            this.BiddersTrack.Paint += new System.Windows.Forms.PaintEventHandler(this.BiddersTrack_Paint);
            // 
            // PPlant
            // 
            this.PPlant.Location = new System.Drawing.Point(12, 57);
            this.PPlant.Name = "PPlant";
            this.PPlant.Size = new System.Drawing.Size(80, 80);
            this.PPlant.TabIndex = 4;
            this.PPlant.TabStop = false;
            this.PPlant.Paint += new System.Windows.Forms.PaintEventHandler(this.PPlant_Paint);
            // 
            // AuctionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 147);
            this.Controls.Add(this.PPlant);
            this.Controls.Add(this.BiddersTrack);
            this.Controls.Add(this.buttonPass);
            this.Controls.Add(this.buttonBid);
            this.Controls.Add(this.bidPrice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AuctionWindow";
            this.Text = "Auction";
            ((System.ComponentModel.ISupportInitialize)(this.BiddersTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PPlant)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox bidPrice;
        private System.Windows.Forms.Button buttonBid;
        private System.Windows.Forms.Button buttonPass;
        private System.Windows.Forms.PictureBox BiddersTrack;
        private System.Windows.Forms.PictureBox PPlant;
    }
}