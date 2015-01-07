namespace AI4PowerGrid_gui
{
    partial class SellingElectricity
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
            this.PPList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EarnedMoney = new System.Windows.Forms.Label();
            this.SellButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PPList
            // 
            this.PPList.FormattingEnabled = true;
            this.PPList.Location = new System.Drawing.Point(13, 13);
            this.PPList.Name = "PPList";
            this.PPList.Size = new System.Drawing.Size(186, 94);
            this.PPList.TabIndex = 0;
            this.PPList.SelectedIndexChanged += new System.EventHandler(this.PPList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Earned Money:";
            // 
            // EarnedMoney
            // 
            this.EarnedMoney.AutoSize = true;
            this.EarnedMoney.Location = new System.Drawing.Point(99, 114);
            this.EarnedMoney.Name = "EarnedMoney";
            this.EarnedMoney.Size = new System.Drawing.Size(13, 13);
            this.EarnedMoney.TabIndex = 2;
            this.EarnedMoney.Text = "0";
            // 
            // SellButton
            // 
            this.SellButton.Location = new System.Drawing.Point(12, 131);
            this.SellButton.Name = "SellButton";
            this.SellButton.Size = new System.Drawing.Size(187, 27);
            this.SellButton.TabIndex = 3;
            this.SellButton.Text = "Sell Electricity";
            this.SellButton.UseVisualStyleBackColor = true;
            this.SellButton.Click += new System.EventHandler(this.SellButton_Click);
            // 
            // SellingElectricity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 168);
            this.Controls.Add(this.SellButton);
            this.Controls.Add(this.EarnedMoney);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PPList);
            this.Name = "SellingElectricity";
            this.Text = "SellingElectricity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox PPList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label EarnedMoney;
        private System.Windows.Forms.Button SellButton;
    }
}