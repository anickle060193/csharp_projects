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
            this.lightBotBoard1 = new MyGamesLibrary.Games.LightBot.LightBotBoard();
            this.SuspendLayout();
            // 
            // lightBotBoard1
            // 
            this.lightBotBoard1.Location = new System.Drawing.Point(12, 12);
            this.lightBotBoard1.Name = "lightBotBoard1";
            this.lightBotBoard1.Size = new System.Drawing.Size(450, 450);
            this.lightBotBoard1.TabIndex = 0;
            // 
            // LightBotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 475);
            this.Controls.Add(this.lightBotBoard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "LightBotForm";
            this.Text = "LightBotForm";
            this.ResumeLayout(false);

        }

        #endregion

        private LightBotBoard lightBotBoard1;
    }
}