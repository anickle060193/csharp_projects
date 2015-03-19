namespace MinesweeperSolver
{
    partial class MinesweeperSolverForm
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
            this.uxImage = new System.Windows.Forms.PictureBox();
            this.uxLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // uxImage
            // 
            this.uxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uxImage.Location = new System.Drawing.Point(12, 12);
            this.uxImage.Name = "uxImage";
            this.uxImage.Size = new System.Drawing.Size(733, 502);
            this.uxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.uxImage.TabIndex = 0;
            this.uxImage.TabStop = false;
            this.uxImage.Paint += new System.Windows.Forms.PaintEventHandler(this.uxImage_Paint);
            // 
            // uxLabel
            // 
            this.uxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxLabel.AutoSize = true;
            this.uxLabel.Location = new System.Drawing.Point(12, 504);
            this.uxLabel.Name = "uxLabel";
            this.uxLabel.Size = new System.Drawing.Size(42, 13);
            this.uxLabel.TabIndex = 1;
            this.uxLabel.Text = "Testing";
            this.uxLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // MinesweeperSolverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 526);
            this.Controls.Add(this.uxLabel);
            this.Controls.Add(this.uxImage);
            this.Name = "MinesweeperSolverForm";
            this.Text = "Minesweeper Solver";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MinesweeperSolverForm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MinesweeperSolverForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.uxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox uxImage;
        private System.Windows.Forms.Label uxLabel;
    }
}

