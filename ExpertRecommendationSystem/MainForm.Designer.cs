namespace ExpertRecommendationSystem
{
    partial class MainForm
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
            this.messageLabel = new System.Windows.Forms.Label();
            this.choicesPanel = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.nextButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.choicesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.Location = new System.Drawing.Point(12, 35);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(257, 47);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.Text = "label1";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // choicesPanel
            // 
            this.choicesPanel.Controls.Add(this.radioButton1);
            this.choicesPanel.Controls.Add(this.radioButton2);
            this.choicesPanel.Location = new System.Drawing.Point(116, 71);
            this.choicesPanel.Name = "choicesPanel";
            this.choicesPanel.Size = new System.Drawing.Size(416, 145);
            this.choicesPanel.TabIndex = 8;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(163, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(457, 222);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "&Next >";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.OnClickButton);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(116, 222);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(75, 23);
            this.prevButton.TabIndex = 6;
            this.prevButton.Text = "< &Prev";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.OnClickButton);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 316);
            this.Controls.Add(this.choicesPanel);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.messageLabel);
            this.Name = "MainForm";
            this.Text = "Main UI";
            this.choicesPanel.ResumeLayout(false);
            this.choicesPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Panel choicesPanel;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button prevButton;
    }
}

