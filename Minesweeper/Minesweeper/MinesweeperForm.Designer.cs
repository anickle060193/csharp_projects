namespace Minesweeper
{
    partial class MinesweeperForm
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
            this.minesweeperBoardControl1 = new Minesweeper.MinesweeperBoardControl();
            this.SuspendLayout();
            // 
            // minesweeperBoardControl1
            // 
            this.minesweeperBoardControl1.Columns = 20;
            this.minesweeperBoardControl1.Location = new System.Drawing.Point(-1, -1);
            this.minesweeperBoardControl1.Margin = new System.Windows.Forms.Padding(0);
            this.minesweeperBoardControl1.Name = "minesweeperBoardControl1";
            this.minesweeperBoardControl1.Rows = 20;
            this.minesweeperBoardControl1.Size = new System.Drawing.Size(600, 600);
            this.minesweeperBoardControl1.TabIndex = 0;
            // 
            // MinesweeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.minesweeperBoardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MinesweeperForm";
            this.Text = "Minesweeper";
            this.ResumeLayout(false);

        }

        #endregion

        private MinesweeperBoardControl minesweeperBoardControl1;

    }
}

