namespace LineThingy
{
    partial class LineThingyForm
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
            this.uxLineCanvas = new LineThingy.LineCanvas();
            this.uxSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // uxLineCanvas
            // 
            this.uxLineCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxLineCanvas.Location = new System.Drawing.Point(0, 0);
            this.uxLineCanvas.MinimumDistancePercent = 0.1F;
            this.uxLineCanvas.Name = "uxLineCanvas";
            this.uxLineCanvas.PreventPainting = false;
            this.uxLineCanvas.Size = new System.Drawing.Size(284, 261);
            this.uxLineCanvas.TabIndex = 0;
            // 
            // uxSaveFileDialog
            // 
            this.uxSaveFileDialog.DefaultExt = "png";
            this.uxSaveFileDialog.Filter = "Image Files (*.png)|*.png";
            // 
            // LineThingyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.uxLineCanvas);
            this.Name = "LineThingyForm";
            this.Text = "Line Thingy";
            this.ResumeLayout(false);

        }

        #endregion

        private LineCanvas uxLineCanvas;
        private System.Windows.Forms.SaveFileDialog uxSaveFileDialog;

    }
}

