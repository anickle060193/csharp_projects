namespace Tanks
{
    partial class TanksForm
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
            this.tanksControl1 = new Tanks.TanksControl();
            this.SuspendLayout();
            // 
            // tanksControl1
            // 
            this.tanksControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tanksControl1.Location = new System.Drawing.Point(0, 0);
            this.tanksControl1.Name = "tanksControl1";
            this.tanksControl1.Size = new System.Drawing.Size(284, 261);
            this.tanksControl1.TabIndex = 0;
            // 
            // TanksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tanksControl1);
            this.Name = "TanksForm";
            this.Text = "Tanks";
            this.ResumeLayout(false);

        }

        #endregion

        private TanksControl tanksControl1;
    }
}

