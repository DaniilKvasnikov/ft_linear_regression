using System.ComponentModel;

namespace ft_linear_regression_form.Forms
{
    partial class GetResultForm
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
            this.regressorInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelFunction = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelPrediction = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // regressorInput
            // 
            this.regressorInput.Location = new System.Drawing.Point(76, 37);
            this.regressorInput.Name = "regressorInput";
            this.regressorInput.Size = new System.Drawing.Size(138, 20);
            this.regressorInput.TabIndex = 0;
            this.regressorInput.TextChanged += new System.EventHandler(this.regressorInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Regressor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Function:";
            // 
            // labelFunction
            // 
            this.labelFunction.AutoSize = true;
            this.labelFunction.Location = new System.Drawing.Point(73, 9);
            this.labelFunction.Name = "labelFunction";
            this.labelFunction.Size = new System.Drawing.Size(63, 13);
            this.labelFunction.TabIndex = 3;
            this.labelFunction.Text = "y = 0 + 0 * x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Prediction:";
            // 
            // labelPrediction
            // 
            this.labelPrediction.AutoSize = true;
            this.labelPrediction.Location = new System.Drawing.Point(73, 78);
            this.labelPrediction.Name = "labelPrediction";
            this.labelPrediction.Size = new System.Drawing.Size(13, 13);
            this.labelPrediction.TabIndex = 5;
            this.labelPrediction.Text = "0";
            // 
            // GetResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 101);
            this.Controls.Add(this.labelPrediction);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelFunction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.regressorInput);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetResultForm";
            this.Text = "GetResultForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox regressorInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelFunction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelPrediction;
    }
}