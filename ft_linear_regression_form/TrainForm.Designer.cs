﻿using System.ComponentModel;

namespace ft_linear_regression_form
{
    partial class TrainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.trainResultLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // trainResultLabel
            // 
            this.trainResultLabel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trainResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.trainResultLabel.Location = new System.Drawing.Point(347, 9);
            this.trainResultLabel.Name = "trainResultLabel";
            this.trainResultLabel.Size = new System.Drawing.Size(225, 52);
            this.trainResultLabel.TabIndex = 1;
            this.trainResultLabel.Text = "label1";
            // 
            // TrainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 450);
            this.Controls.Add(this.trainResultLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrainForm";
            this.Text = "TrainForm";
            this.TopMost = true;
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label trainResultLabel;

        #endregion
    }
}