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
            this.uxGameList = new System.Windows.Forms.ComboBox();
            this.uxPlayGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uxGameList
            // 
            this.uxGameList.FormattingEnabled = true;
            this.uxGameList.Location = new System.Drawing.Point(12, 12);
            this.uxGameList.Name = "uxGameList";
            this.uxGameList.Size = new System.Drawing.Size(215, 21);
            this.uxGameList.TabIndex = 1;
            // 
            // uxPlayGame
            // 
            this.uxPlayGame.Location = new System.Drawing.Point(12, 39);
            this.uxPlayGame.Name = "uxPlayGame";
            this.uxPlayGame.Size = new System.Drawing.Size(215, 23);
            this.uxPlayGame.TabIndex = 2;
            this.uxPlayGame.Text = "Play Game!";
            this.uxPlayGame.UseVisualStyleBackColor = true;
            this.uxPlayGame.Click += new System.EventHandler(this.uxPlayGame_Click);
            // 
            // GameSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 74);
            this.Controls.Add(this.uxPlayGame);
            this.Controls.Add(this.uxGameList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "GameSelectorForm";
            this.Text = "Game Selector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox uxGameList;
        private System.Windows.Forms.Button uxPlayGame;
    }
}

