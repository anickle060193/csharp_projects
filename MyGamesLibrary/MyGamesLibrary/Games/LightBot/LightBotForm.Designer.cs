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
            this.uxExecute = new System.Windows.Forms.Button();
            this.uxReset = new System.Windows.Forms.Button();
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
            this.uxLightBotMoveQueue.AutoScroll = true;
            this.uxLightBotMoveQueue.BackColor = System.Drawing.Color.Gray;
            this.uxLightBotMoveQueue.Game = null;
            this.uxLightBotMoveQueue.Location = new System.Drawing.Point(468, 12);
            this.uxLightBotMoveQueue.Name = "uxLightBotMoveQueue";
            this.uxLightBotMoveQueue.Size = new System.Drawing.Size(320, 450);
            this.uxLightBotMoveQueue.TabIndex = 1;
            // 
            // uxPossibleMoves
            // 
            this.uxPossibleMoves.AutoScroll = true;
            this.uxPossibleMoves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uxPossibleMoves.Game = null;
            this.uxPossibleMoves.Location = new System.Drawing.Point(12, 468);
            this.uxPossibleMoves.Name = "uxPossibleMoves";
            this.uxPossibleMoves.Size = new System.Drawing.Size(450, 104);
            this.uxPossibleMoves.TabIndex = 2;
            this.uxPossibleMoves.WrapContents = false;
            // 
            // uxExecute
            // 
            this.uxExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxExecute.Location = new System.Drawing.Point(468, 468);
            this.uxExecute.Name = "uxExecute";
            this.uxExecute.Size = new System.Drawing.Size(320, 52);
            this.uxExecute.TabIndex = 3;
            this.uxExecute.Text = "EXECUTE";
            this.uxExecute.UseVisualStyleBackColor = true;
            this.uxExecute.Click += new System.EventHandler(this.uxExecute_Click);
            // 
            // uxReset
            // 
            this.uxReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxReset.Location = new System.Drawing.Point(468, 520);
            this.uxReset.Name = "uxReset";
            this.uxReset.Size = new System.Drawing.Size(320, 52);
            this.uxReset.TabIndex = 4;
            this.uxReset.Text = "RESET";
            this.uxReset.UseVisualStyleBackColor = true;
            this.uxReset.Click += new System.EventHandler(this.uxReset_Click);
            // 
            // LightBotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 584);
            this.Controls.Add(this.uxReset);
            this.Controls.Add(this.uxExecute);
            this.Controls.Add(this.uxPossibleMoves);
            this.Controls.Add(this.uxLightBotMoveQueue);
            this.Controls.Add(this.uxLightBotBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "LightBotForm";
            this.Text = "Light Bot";
            this.ResumeLayout(false);

        }

        #endregion

        private LightBotBoard uxLightBotBoard;
        private MoveQueueControl uxLightBotMoveQueue;
        private PossibleMoves uxPossibleMoves;
        private System.Windows.Forms.Button uxExecute;
        private System.Windows.Forms.Button uxReset;
    }
}