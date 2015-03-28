namespace GameOfLife
{
    partial class GameOfLifeBoardControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.uxOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // uxSaveFileDialog
            // 
            this.uxSaveFileDialog.DefaultExt = "csv";
            this.uxSaveFileDialog.Filter = "Comma Seperated Values (*.csv)|*.csv";
            // 
            // uxOpenFileDialog
            // 
            this.uxOpenFileDialog.DefaultExt = "csv";
            this.uxOpenFileDialog.FileName = "uxOpenFileDialog";
            this.uxOpenFileDialog.Filter = "Comma Seperated Values (*.csv)|*.csv";
            // 
            // GameOfLifeBoardControl
            // 
            this.DoubleBuffered = true;
            this.Name = "GameOfLifeBoardControl";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameOfLifeBoard_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameOfLifeBoard_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameOfLifeBoard_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameOfLifeBoard_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameOfLifeBoard_MouseUp);
            this.Resize += new System.EventHandler(this.GameOfLifeBoard_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog uxSaveFileDialog;
        private System.Windows.Forms.OpenFileDialog uxOpenFileDialog;
    }
}
