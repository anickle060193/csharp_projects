namespace MyGamesLibrary
{
    partial class GameSelectorForm
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
            this.uxPlayGame = new System.Windows.Forms.Button();
            this.uxGamesList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // uxPlayGame
            // 
            this.uxPlayGame.Location = new System.Drawing.Point(12, 191);
            this.uxPlayGame.Name = "uxPlayGame";
            this.uxPlayGame.Size = new System.Drawing.Size(212, 23);
            this.uxPlayGame.TabIndex = 2;
            this.uxPlayGame.Text = "Play Game!";
            this.uxPlayGame.UseVisualStyleBackColor = true;
            this.uxPlayGame.Click += new System.EventHandler(this.uxPlayGame_Click);
            // 
            // uxGamesList
            // 
            this.uxGamesList.FormattingEnabled = true;
            this.uxGamesList.Location = new System.Drawing.Point(12, 12);
            this.uxGamesList.Name = "uxGamesList";
            this.uxGamesList.Size = new System.Drawing.Size(212, 173);
            this.uxGamesList.TabIndex = 3;
            this.uxGamesList.DoubleClick += new System.EventHandler(this.uxGamesList_DoubleClick);
            // 
            // GameSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 222);
            this.Controls.Add(this.uxGamesList);
            this.Controls.Add(this.uxPlayGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "GameSelectorForm";
            this.Text = "Game Selector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uxPlayGame;
        private System.Windows.Forms.ListBox uxGamesList;
    }
}

