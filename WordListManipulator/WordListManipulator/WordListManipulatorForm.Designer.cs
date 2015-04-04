namespace WordListManipulator
{
    partial class WordListManipulatorForm
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
            this.uxBrowse = new System.Windows.Forms.Button();
            this.uxFilename = new System.Windows.Forms.TextBox();
            this.uxFilenameLabel = new System.Windows.Forms.Label();
            this.uxLengthOptions = new System.Windows.Forms.GroupBox();
            this.uxMinimumLengthLabel = new System.Windows.Forms.Label();
            this.uxMinimumLength = new System.Windows.Forms.NumericUpDown();
            this.uxMaximumLength = new System.Windows.Forms.NumericUpDown();
            this.uxMaximumLengthLabel = new System.Windows.Forms.Label();
            this.uxCapitalizationOptions = new System.Windows.Forms.GroupBox();
            this.uxAllLowerCase = new System.Windows.Forms.RadioButton();
            this.uxAllUpperCase = new System.Windows.Forms.RadioButton();
            this.uxProcess = new System.Windows.Forms.Button();
            this.uxOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.uxSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.uxLengthOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxMinimumLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxMaximumLength)).BeginInit();
            this.uxCapitalizationOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxBrowse
            // 
            this.uxBrowse.Location = new System.Drawing.Point(264, 12);
            this.uxBrowse.Name = "uxBrowse";
            this.uxBrowse.Size = new System.Drawing.Size(75, 23);
            this.uxBrowse.TabIndex = 0;
            this.uxBrowse.Text = "Browse...";
            this.uxBrowse.UseVisualStyleBackColor = true;
            this.uxBrowse.Click += new System.EventHandler(this.uxBrowse_Click);
            // 
            // uxFilename
            // 
            this.uxFilename.Location = new System.Drawing.Point(73, 14);
            this.uxFilename.Name = "uxFilename";
            this.uxFilename.Size = new System.Drawing.Size(185, 20);
            this.uxFilename.TabIndex = 1;
            // 
            // uxFilenameLabel
            // 
            this.uxFilenameLabel.AutoSize = true;
            this.uxFilenameLabel.Location = new System.Drawing.Point(12, 17);
            this.uxFilenameLabel.Name = "uxFilenameLabel";
            this.uxFilenameLabel.Size = new System.Drawing.Size(55, 13);
            this.uxFilenameLabel.TabIndex = 2;
            this.uxFilenameLabel.Text = "Word List:";
            // 
            // uxLengthOptions
            // 
            this.uxLengthOptions.Controls.Add(this.uxMaximumLength);
            this.uxLengthOptions.Controls.Add(this.uxMaximumLengthLabel);
            this.uxLengthOptions.Controls.Add(this.uxMinimumLength);
            this.uxLengthOptions.Controls.Add(this.uxMinimumLengthLabel);
            this.uxLengthOptions.Location = new System.Drawing.Point(12, 40);
            this.uxLengthOptions.Name = "uxLengthOptions";
            this.uxLengthOptions.Size = new System.Drawing.Size(196, 78);
            this.uxLengthOptions.TabIndex = 3;
            this.uxLengthOptions.TabStop = false;
            this.uxLengthOptions.Text = "Word Length Options:";
            // 
            // uxMinimumLengthLabel
            // 
            this.uxMinimumLengthLabel.AutoSize = true;
            this.uxMinimumLengthLabel.Location = new System.Drawing.Point(6, 21);
            this.uxMinimumLengthLabel.Name = "uxMinimumLengthLabel";
            this.uxMinimumLengthLabel.Size = new System.Drawing.Size(116, 13);
            this.uxMinimumLengthLabel.TabIndex = 0;
            this.uxMinimumLengthLabel.Text = "Minimum Word Length:";
            // 
            // uxMinimumLength
            // 
            this.uxMinimumLength.Location = new System.Drawing.Point(131, 19);
            this.uxMinimumLength.Name = "uxMinimumLength";
            this.uxMinimumLength.Size = new System.Drawing.Size(51, 20);
            this.uxMinimumLength.TabIndex = 1;
            this.uxMinimumLength.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // uxMaximumLength
            // 
            this.uxMaximumLength.Location = new System.Drawing.Point(131, 45);
            this.uxMaximumLength.Name = "uxMaximumLength";
            this.uxMaximumLength.Size = new System.Drawing.Size(51, 20);
            this.uxMaximumLength.TabIndex = 3;
            this.uxMaximumLength.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // uxMaximumLengthLabel
            // 
            this.uxMaximumLengthLabel.AutoSize = true;
            this.uxMaximumLengthLabel.Location = new System.Drawing.Point(6, 47);
            this.uxMaximumLengthLabel.Name = "uxMaximumLengthLabel";
            this.uxMaximumLengthLabel.Size = new System.Drawing.Size(119, 13);
            this.uxMaximumLengthLabel.TabIndex = 2;
            this.uxMaximumLengthLabel.Text = "Maximum Word Length:";
            // 
            // uxCapitalizationOptions
            // 
            this.uxCapitalizationOptions.Controls.Add(this.uxAllUpperCase);
            this.uxCapitalizationOptions.Controls.Add(this.uxAllLowerCase);
            this.uxCapitalizationOptions.Location = new System.Drawing.Point(214, 40);
            this.uxCapitalizationOptions.Name = "uxCapitalizationOptions";
            this.uxCapitalizationOptions.Size = new System.Drawing.Size(125, 77);
            this.uxCapitalizationOptions.TabIndex = 5;
            this.uxCapitalizationOptions.TabStop = false;
            this.uxCapitalizationOptions.Text = "Capitalization Options:";
            // 
            // uxAllLowerCase
            // 
            this.uxAllLowerCase.AutoSize = true;
            this.uxAllLowerCase.Location = new System.Drawing.Point(6, 19);
            this.uxAllLowerCase.Name = "uxAllLowerCase";
            this.uxAllLowerCase.Size = new System.Drawing.Size(95, 17);
            this.uxAllLowerCase.TabIndex = 0;
            this.uxAllLowerCase.TabStop = true;
            this.uxAllLowerCase.Text = "All Lower Case";
            this.uxAllLowerCase.UseVisualStyleBackColor = true;
            // 
            // uxAllUpperCase
            // 
            this.uxAllUpperCase.AutoSize = true;
            this.uxAllUpperCase.Location = new System.Drawing.Point(6, 44);
            this.uxAllUpperCase.Name = "uxAllUpperCase";
            this.uxAllUpperCase.Size = new System.Drawing.Size(95, 17);
            this.uxAllUpperCase.TabIndex = 2;
            this.uxAllUpperCase.TabStop = true;
            this.uxAllUpperCase.Text = "All Upper Case";
            this.uxAllUpperCase.UseVisualStyleBackColor = true;
            // 
            // uxProcess
            // 
            this.uxProcess.Location = new System.Drawing.Point(114, 124);
            this.uxProcess.Name = "uxProcess";
            this.uxProcess.Size = new System.Drawing.Size(125, 23);
            this.uxProcess.TabIndex = 6;
            this.uxProcess.Text = "Process";
            this.uxProcess.UseVisualStyleBackColor = true;
            this.uxProcess.Click += new System.EventHandler(this.uxProcess_Click);
            // 
            // uxOpenFileDialog
            // 
            this.uxOpenFileDialog.DefaultExt = "txt";
            this.uxOpenFileDialog.Filter = "Text Files (*.txt)|*.txt";
            // 
            // uxSaveFileDialog
            // 
            this.uxSaveFileDialog.DefaultExt = "txt";
            this.uxSaveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            // 
            // WordListManipulatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 158);
            this.Controls.Add(this.uxProcess);
            this.Controls.Add(this.uxCapitalizationOptions);
            this.Controls.Add(this.uxLengthOptions);
            this.Controls.Add(this.uxFilenameLabel);
            this.Controls.Add(this.uxFilename);
            this.Controls.Add(this.uxBrowse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "WordListManipulatorForm";
            this.Text = "Word List Manipulator";
            this.uxLengthOptions.ResumeLayout(false);
            this.uxLengthOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uxMinimumLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxMaximumLength)).EndInit();
            this.uxCapitalizationOptions.ResumeLayout(false);
            this.uxCapitalizationOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uxBrowse;
        private System.Windows.Forms.TextBox uxFilename;
        private System.Windows.Forms.Label uxFilenameLabel;
        private System.Windows.Forms.GroupBox uxLengthOptions;
        private System.Windows.Forms.NumericUpDown uxMaximumLength;
        private System.Windows.Forms.Label uxMaximumLengthLabel;
        private System.Windows.Forms.NumericUpDown uxMinimumLength;
        private System.Windows.Forms.Label uxMinimumLengthLabel;
        private System.Windows.Forms.GroupBox uxCapitalizationOptions;
        private System.Windows.Forms.RadioButton uxAllUpperCase;
        private System.Windows.Forms.RadioButton uxAllLowerCase;
        private System.Windows.Forms.Button uxProcess;
        private System.Windows.Forms.OpenFileDialog uxOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog uxSaveFileDialog;
    }
}

