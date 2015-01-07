using System.Collections.Generic;
namespace AI4PowerGrid_gui
{
    partial class RemovingPP
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.RemovePPbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Location = new System.Drawing.Point(13, 13);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(222, 133);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Which Power Plant want you to remove?";
            // 
            // RemovePPbutton
            // 
            this.RemovePPbutton.Location = new System.Drawing.Point(13, 153);
            this.RemovePPbutton.Name = "RemovePPbutton";
            this.RemovePPbutton.Size = new System.Drawing.Size(222, 23);
            this.RemovePPbutton.TabIndex = 1;
            this.RemovePPbutton.Text = "Remove";
            this.RemovePPbutton.UseVisualStyleBackColor = true;
            this.RemovePPbutton.Click += new System.EventHandler(this.RemovePPbutton_Click);
            // 
            // RemovingPP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 189);
            this.Controls.Add(this.RemovePPbutton);
            this.Controls.Add(this.groupBox);
            this.Name = "RemovingPP";
            this.Text = "RemovingPP";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button RemovePPbutton;
        private List<System.Windows.Forms.RadioButton> PPRadio;
    }
}