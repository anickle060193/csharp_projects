namespace SlidingPuzzle
{
    partial class SlidingPuzzleForm
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
            this.uxFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // uxFlowLayout
            // 
            this.uxFlowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxFlowLayout.Location = new System.Drawing.Point(0, 0);
            this.uxFlowLayout.Name = "uxFlowLayout";
            this.uxFlowLayout.Size = new System.Drawing.Size(406, 378);
            this.uxFlowLayout.TabIndex = 0;
            // 
            // SlidingPuzzleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 378);
            this.Controls.Add(this.uxFlowLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "SlidingPuzzleForm";
            this.Text = "Sliding Puzzle";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel uxFlowLayout;
    }
}

