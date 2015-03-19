namespace SortingVisualizer
{
    partial class SortingVisualizerForm
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
            SortingVisualizer.Sorters.BubbleSorter bubbleSorter1 = new SortingVisualizer.Sorters.BubbleSorter();
            this.uxSortingVisualizerControl = new SortingVisualizer.SortingVisualizerControl();
            this.SuspendLayout();
            // 
            // uxSortingVisualizerControl
            // 
            this.uxSortingVisualizerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxSortingVisualizerControl.Location = new System.Drawing.Point(0, 0);
            this.uxSortingVisualizerControl.Name = "uxSortingVisualizerControl";
            this.uxSortingVisualizerControl.Size = new System.Drawing.Size(484, 311);
            this.uxSortingVisualizerControl.Sorter = bubbleSorter1;
            this.uxSortingVisualizerControl.TabIndex = 0;
            // 
            // SortingVisualizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.Add(this.uxSortingVisualizerControl);
            this.MinimumSize = new System.Drawing.Size(500, 350);
            this.Name = "SortingVisualizerForm";
            this.Text = "Visual Sorting";
            this.ResumeLayout(false);

        }

        #endregion

        private SortingVisualizerControl uxSortingVisualizerControl;
    }
}

