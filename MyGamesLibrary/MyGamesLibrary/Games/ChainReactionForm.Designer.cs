﻿namespace MyGamesLibrary.Games
{
    partial class ChainReactionForm
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
            this.components = new System.ComponentModel.Container();
            this.uxUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // uxUpdateTimer
            // 
            this.uxUpdateTimer.Interval = 50;
            this.uxUpdateTimer.Tick += new System.EventHandler(this.uxUpdateTimer_Tick);
            // 
            // ChainReactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(516, 479);
            this.DoubleBuffered = true;
            this.Name = "ChainReactionForm";
            this.Text = "Chain Reaction";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChainReactionForm_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChainReactionForm_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChainReactionForm_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChainReactionForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChainReactionForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChainReactionForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer uxUpdateTimer;
    }
}