namespace MyGamesLibrary.Games.LightBot
{
    partial class LightBotForm
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
            this.uxLightBotBoard = new MyGamesLibrary.Games.LightBot.LightBotBoard();
            this.uxLightBotMoveQueue = new MyGamesLibrary.Games.LightBot.MoveQueueControl();
            this.uxPossibleMoves = new MyGamesLibrary.Games.LightBot.PossibleMoves();
            this.SuspendLayout();
            // 
            // uxLightBotBoard
            // 
            this.uxLightBotBoard.BackColor = System.Drawing.Color.Silver;
            this.uxLightBotBoard.Game = null;
            this.uxLightBotBoard.Location = new System.Drawing.Point(12, 12);
            this.uxLightBotBoard.Name = "uxLightBotBoard";
            this.uxLightBotBoard.Size = new System.Drawing.Size(450, 450);
            this.uxLightBotBoard.TabIndex = 0;
            // 
            // uxLightBotMoveQueue
            // 
            this.uxLightBotMoveQueue.BackColor = System.Drawing.Color.Gray;
            this.uxLightBotMoveQueue.Game = null;
            this.uxLightBotMoveQueue.Location = new System.Drawing.Point(468, 12);
            this.uxLightBotMoveQueue.Name = "uxLightBotMoveQueue";
            this.uxLightBotMoveQueue.Size = new System.Drawing.Size(320, 538);
            this.uxLightBotMoveQueue.TabIndex = 1;
            // 
            // uxPossibleMoves
            // 
            this.uxPossibleMoves.AutoSize = true;
            this.uxPossibleMoves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uxPossibleMoves.Location = new System.Drawing.Point(12, 468);
            this.uxPossibleMoves.Name = "uxPossibleMoves";
            this.uxPossibleMoves.Size = new System.Drawing.Size(450, 82);
            this.uxPossibleMoves.TabIndex = 2;
            // 
            // LightBotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.uxPossibleMoves);
            this.Controls.Add(this.uxLightBotMoveQueue);
            this.Controls.Add(this.uxLightBotBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "LightBotForm";
            this.Text = "LightBotForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LightBotBoard uxLightBotBoard;
        private MoveQueueControl uxLightBotMoveQueue;
        private PossibleMoves uxPossibleMoves;
    }
}