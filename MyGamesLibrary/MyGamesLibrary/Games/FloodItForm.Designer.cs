namespace MyGamesLibrary.Games
{
    partial class FloodItForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxPanel = new System.Windows.Forms.Panel();
            this.ux0 = new System.Windows.Forms.Button();
            this.ux1 = new System.Windows.Forms.Button();
            this.ux2 = new System.Windows.Forms.Button();
            this.ux3 = new System.Windows.Forms.Button();
            this.ux4 = new System.Windows.Forms.Button();
            this.ux5 = new System.Windows.Forms.Button();
            this.uxProgressBar = new System.Windows.Forms.Panel();
            this.uxFloodsRemaining = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uxPanel
            // 
            this.uxPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.uxPanel.Location = new System.Drawing.Point(65, 52);
            this.uxPanel.Name = "uxPanel";
            this.uxPanel.Size = new System.Drawing.Size(510, 510);
            this.uxPanel.TabIndex = 0;
            this.uxPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.uxPanel_Paint);
            // 
            // ux0
            // 
            this.ux0.Location = new System.Drawing.Point(65, 568);
            this.ux0.Name = "ux0";
            this.ux0.Size = new System.Drawing.Size(80, 80);
            this.ux0.TabIndex = 1;
            this.ux0.UseVisualStyleBackColor = true;
            // 
            // ux1
            // 
            this.ux1.Location = new System.Drawing.Point(151, 568);
            this.ux1.Name = "ux1";
            this.ux1.Size = new System.Drawing.Size(80, 80);
            this.ux1.TabIndex = 2;
            this.ux1.UseVisualStyleBackColor = true;
            // 
            // ux2
            // 
            this.ux2.Location = new System.Drawing.Point(237, 568);
            this.ux2.Name = "ux2";
            this.ux2.Size = new System.Drawing.Size(80, 80);
            this.ux2.TabIndex = 3;
            this.ux2.UseVisualStyleBackColor = true;
            // 
            // ux3
            // 
            this.ux3.Location = new System.Drawing.Point(323, 568);
            this.ux3.Name = "ux3";
            this.ux3.Size = new System.Drawing.Size(80, 80);
            this.ux3.TabIndex = 4;
            this.ux3.UseVisualStyleBackColor = true;
            // 
            // ux4
            // 
            this.ux4.Location = new System.Drawing.Point(409, 568);
            this.ux4.Name = "ux4";
            this.ux4.Size = new System.Drawing.Size(80, 80);
            this.ux4.TabIndex = 5;
            this.ux4.UseVisualStyleBackColor = true;
            // 
            // ux5
            // 
            this.ux5.Location = new System.Drawing.Point(495, 568);
            this.ux5.Name = "ux5";
            this.ux5.Size = new System.Drawing.Size(80, 80);
            this.ux5.TabIndex = 6;
            this.ux5.UseVisualStyleBackColor = true;
            // 
            // uxProgressBar
            // 
            this.uxProgressBar.Location = new System.Drawing.Point(12, 52);
            this.uxProgressBar.Name = "uxProgressBar";
            this.uxProgressBar.Size = new System.Drawing.Size(47, 510);
            this.uxProgressBar.TabIndex = 7;
            this.uxProgressBar.Paint += new System.Windows.Forms.PaintEventHandler(this.uxProgressBar_Paint);
            // 
            // uxFloodsRemaining
            // 
            this.uxFloodsRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxFloodsRemaining.Location = new System.Drawing.Point(65, 9);
            this.uxFloodsRemaining.Name = "uxFloodsRemaining";
            this.uxFloodsRemaining.Size = new System.Drawing.Size(510, 40);
            this.uxFloodsRemaining.TabIndex = 8;
            this.uxFloodsRemaining.Text = "Floods Remaining: 0";
            this.uxFloodsRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FloodItForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 658);
            this.Controls.Add(this.uxFloodsRemaining);
            this.Controls.Add(this.uxProgressBar);
            this.Controls.Add(this.ux5);
            this.Controls.Add(this.ux4);
            this.Controls.Add(this.ux3);
            this.Controls.Add(this.ux2);
            this.Controls.Add(this.ux1);
            this.Controls.Add(this.ux0);
            this.Controls.Add(this.uxPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FloodItForm";
            this.Text = "Flood It";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel uxPanel;
        private System.Windows.Forms.Button ux0;
        private System.Windows.Forms.Button ux1;
        private System.Windows.Forms.Button ux2;
        private System.Windows.Forms.Button ux3;
        private System.Windows.Forms.Button ux4;
        private System.Windows.Forms.Button ux5;
        private System.Windows.Forms.Panel uxProgressBar;
        private System.Windows.Forms.Label uxFloodsRemaining;
    }
}