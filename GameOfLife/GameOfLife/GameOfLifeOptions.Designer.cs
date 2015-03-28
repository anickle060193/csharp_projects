namespace GameOfLife
{
    partial class GameOfLifeOptions
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
            this.uxRowsPrompt = new System.Windows.Forms.Label();
            this.uxRows = new System.Windows.Forms.NumericUpDown();
            this.uxColumns = new System.Windows.Forms.NumericUpDown();
            this.uxColumnsPrompt = new System.Windows.Forms.Label();
            this.uxBlockHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.uxBlockWidth = new System.Windows.Forms.NumericUpDown();
            this.uxBlockWidthPrompt = new System.Windows.Forms.Label();
            this.uxIntervalPrompt = new System.Windows.Forms.Label();
            this.uxInterval = new System.Windows.Forms.NumericUpDown();
            this.uxSeedOnStart = new System.Windows.Forms.CheckBox();
            this.uxShowTransitions = new System.Windows.Forms.CheckBox();
            this.uxSeedOnStartPrompt = new System.Windows.Forms.Label();
            this.uxShowTransitionPrompt = new System.Windows.Forms.Label();
            this.uxOK = new System.Windows.Forms.Button();
            this.uxCancel = new System.Windows.Forms.Button();
            this.uxDynamicBoardSizePrompt = new System.Windows.Forms.Label();
            this.uxAutosizeBoard = new System.Windows.Forms.CheckBox();
            this.uxRuleSet = new System.Windows.Forms.ComboBox();
            this.uxRuleSetPrompt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uxRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxBlockHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxBlockWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // uxRowsPrompt
            // 
            this.uxRowsPrompt.AutoSize = true;
            this.uxRowsPrompt.Location = new System.Drawing.Point(12, 34);
            this.uxRowsPrompt.Name = "uxRowsPrompt";
            this.uxRowsPrompt.Size = new System.Drawing.Size(37, 13);
            this.uxRowsPrompt.TabIndex = 1;
            this.uxRowsPrompt.Text = "Rows:";
            // 
            // uxRows
            // 
            this.uxRows.Enabled = false;
            this.uxRows.Location = new System.Drawing.Point(109, 32);
            this.uxRows.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.uxRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxRows.Name = "uxRows";
            this.uxRows.Size = new System.Drawing.Size(81, 20);
            this.uxRows.TabIndex = 2;
            this.uxRows.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // uxColumns
            // 
            this.uxColumns.Enabled = false;
            this.uxColumns.Location = new System.Drawing.Point(109, 58);
            this.uxColumns.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.uxColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxColumns.Name = "uxColumns";
            this.uxColumns.Size = new System.Drawing.Size(81, 20);
            this.uxColumns.TabIndex = 4;
            this.uxColumns.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // uxColumnsPrompt
            // 
            this.uxColumnsPrompt.AutoSize = true;
            this.uxColumnsPrompt.Location = new System.Drawing.Point(12, 60);
            this.uxColumnsPrompt.Name = "uxColumnsPrompt";
            this.uxColumnsPrompt.Size = new System.Drawing.Size(50, 13);
            this.uxColumnsPrompt.TabIndex = 3;
            this.uxColumnsPrompt.Text = "Columns:";
            // 
            // uxBlockHeight
            // 
            this.uxBlockHeight.Location = new System.Drawing.Point(109, 110);
            this.uxBlockHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxBlockHeight.Name = "uxBlockHeight";
            this.uxBlockHeight.Size = new System.Drawing.Size(81, 20);
            this.uxBlockHeight.TabIndex = 8;
            this.uxBlockHeight.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Block Height:";
            // 
            // uxBlockWidth
            // 
            this.uxBlockWidth.Location = new System.Drawing.Point(109, 84);
            this.uxBlockWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.uxBlockWidth.Name = "uxBlockWidth";
            this.uxBlockWidth.Size = new System.Drawing.Size(81, 20);
            this.uxBlockWidth.TabIndex = 6;
            this.uxBlockWidth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // uxBlockWidthPrompt
            // 
            this.uxBlockWidthPrompt.AutoSize = true;
            this.uxBlockWidthPrompt.Location = new System.Drawing.Point(12, 86);
            this.uxBlockWidthPrompt.Name = "uxBlockWidthPrompt";
            this.uxBlockWidthPrompt.Size = new System.Drawing.Size(68, 13);
            this.uxBlockWidthPrompt.TabIndex = 5;
            this.uxBlockWidthPrompt.Text = "Block Width:";
            // 
            // uxIntervalPrompt
            // 
            this.uxIntervalPrompt.AutoSize = true;
            this.uxIntervalPrompt.Location = new System.Drawing.Point(12, 138);
            this.uxIntervalPrompt.Name = "uxIntervalPrompt";
            this.uxIntervalPrompt.Size = new System.Drawing.Size(45, 13);
            this.uxIntervalPrompt.TabIndex = 10;
            this.uxIntervalPrompt.Text = "Interval:";
            // 
            // uxInterval
            // 
            this.uxInterval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.uxInterval.Location = new System.Drawing.Point(109, 136);
            this.uxInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.uxInterval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.uxInterval.Name = "uxInterval";
            this.uxInterval.Size = new System.Drawing.Size(81, 20);
            this.uxInterval.TabIndex = 11;
            this.uxInterval.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // uxSeedOnStart
            // 
            this.uxSeedOnStart.AutoSize = true;
            this.uxSeedOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uxSeedOnStart.Location = new System.Drawing.Point(109, 189);
            this.uxSeedOnStart.Name = "uxSeedOnStart";
            this.uxSeedOnStart.Size = new System.Drawing.Size(15, 14);
            this.uxSeedOnStart.TabIndex = 12;
            this.uxSeedOnStart.UseVisualStyleBackColor = true;
            // 
            // uxShowTransitions
            // 
            this.uxShowTransitions.AutoSize = true;
            this.uxShowTransitions.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uxShowTransitions.Location = new System.Drawing.Point(109, 209);
            this.uxShowTransitions.Name = "uxShowTransitions";
            this.uxShowTransitions.Size = new System.Drawing.Size(15, 14);
            this.uxShowTransitions.TabIndex = 13;
            this.uxShowTransitions.UseVisualStyleBackColor = true;
            // 
            // uxSeedOnStartPrompt
            // 
            this.uxSeedOnStartPrompt.AutoSize = true;
            this.uxSeedOnStartPrompt.Location = new System.Drawing.Point(12, 189);
            this.uxSeedOnStartPrompt.Name = "uxSeedOnStartPrompt";
            this.uxSeedOnStartPrompt.Size = new System.Drawing.Size(75, 13);
            this.uxSeedOnStartPrompt.TabIndex = 14;
            this.uxSeedOnStartPrompt.Text = "Seed on Start:";
            // 
            // uxShowTransitionPrompt
            // 
            this.uxShowTransitionPrompt.AutoSize = true;
            this.uxShowTransitionPrompt.Location = new System.Drawing.Point(12, 209);
            this.uxShowTransitionPrompt.Name = "uxShowTransitionPrompt";
            this.uxShowTransitionPrompt.Size = new System.Drawing.Size(91, 13);
            this.uxShowTransitionPrompt.TabIndex = 15;
            this.uxShowTransitionPrompt.Text = "Show Transitions:";
            // 
            // uxOK
            // 
            this.uxOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uxOK.Location = new System.Drawing.Point(23, 236);
            this.uxOK.Name = "uxOK";
            this.uxOK.Size = new System.Drawing.Size(75, 23);
            this.uxOK.TabIndex = 16;
            this.uxOK.Text = "OK";
            this.uxOK.UseVisualStyleBackColor = true;
            // 
            // uxCancel
            // 
            this.uxCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxCancel.Location = new System.Drawing.Point(104, 236);
            this.uxCancel.Name = "uxCancel";
            this.uxCancel.Size = new System.Drawing.Size(75, 23);
            this.uxCancel.TabIndex = 17;
            this.uxCancel.Text = "Cancel";
            this.uxCancel.UseVisualStyleBackColor = true;
            // 
            // uxDynamicBoardSizePrompt
            // 
            this.uxDynamicBoardSizePrompt.AutoSize = true;
            this.uxDynamicBoardSizePrompt.Location = new System.Drawing.Point(12, 12);
            this.uxDynamicBoardSizePrompt.Name = "uxDynamicBoardSizePrompt";
            this.uxDynamicBoardSizePrompt.Size = new System.Drawing.Size(81, 13);
            this.uxDynamicBoardSizePrompt.TabIndex = 19;
            this.uxDynamicBoardSizePrompt.Text = "Autosize Board:";
            // 
            // uxAutosizeBoard
            // 
            this.uxAutosizeBoard.AutoSize = true;
            this.uxAutosizeBoard.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uxAutosizeBoard.Checked = true;
            this.uxAutosizeBoard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uxAutosizeBoard.Location = new System.Drawing.Point(109, 12);
            this.uxAutosizeBoard.Name = "uxAutosizeBoard";
            this.uxAutosizeBoard.Size = new System.Drawing.Size(15, 14);
            this.uxAutosizeBoard.TabIndex = 18;
            this.uxAutosizeBoard.UseVisualStyleBackColor = true;
            this.uxAutosizeBoard.CheckedChanged += new System.EventHandler(this.uxAutosizeBoard_CheckedChanged);
            // 
            // uxRuleSet
            // 
            this.uxRuleSet.FormattingEnabled = true;
            this.uxRuleSet.Location = new System.Drawing.Point(109, 162);
            this.uxRuleSet.Name = "uxRuleSet";
            this.uxRuleSet.Size = new System.Drawing.Size(81, 21);
            this.uxRuleSet.TabIndex = 20;
            // 
            // uxRuleSetPrompt
            // 
            this.uxRuleSetPrompt.AutoSize = true;
            this.uxRuleSetPrompt.Location = new System.Drawing.Point(12, 165);
            this.uxRuleSetPrompt.Name = "uxRuleSetPrompt";
            this.uxRuleSetPrompt.Size = new System.Drawing.Size(51, 13);
            this.uxRuleSetPrompt.TabIndex = 21;
            this.uxRuleSetPrompt.Text = "Rule Set:";
            // 
            // GameOfLifeOptions
            // 
            this.AcceptButton = this.uxOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uxCancel;
            this.ClientSize = new System.Drawing.Size(202, 269);
            this.Controls.Add(this.uxRuleSetPrompt);
            this.Controls.Add(this.uxRuleSet);
            this.Controls.Add(this.uxDynamicBoardSizePrompt);
            this.Controls.Add(this.uxAutosizeBoard);
            this.Controls.Add(this.uxCancel);
            this.Controls.Add(this.uxOK);
            this.Controls.Add(this.uxShowTransitionPrompt);
            this.Controls.Add(this.uxSeedOnStartPrompt);
            this.Controls.Add(this.uxShowTransitions);
            this.Controls.Add(this.uxSeedOnStart);
            this.Controls.Add(this.uxIntervalPrompt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uxBlockHeight);
            this.Controls.Add(this.uxInterval);
            this.Controls.Add(this.uxRowsPrompt);
            this.Controls.Add(this.uxRows);
            this.Controls.Add(this.uxBlockWidth);
            this.Controls.Add(this.uxColumnsPrompt);
            this.Controls.Add(this.uxBlockWidthPrompt);
            this.Controls.Add(this.uxColumns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "GameOfLifeOptions";
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.uxRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxBlockHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxBlockWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label uxRowsPrompt;
        private System.Windows.Forms.NumericUpDown uxRows;
        private System.Windows.Forms.NumericUpDown uxColumns;
        private System.Windows.Forms.Label uxColumnsPrompt;
        private System.Windows.Forms.NumericUpDown uxBlockHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown uxBlockWidth;
        private System.Windows.Forms.Label uxBlockWidthPrompt;
        private System.Windows.Forms.Label uxIntervalPrompt;
        private System.Windows.Forms.NumericUpDown uxInterval;
        private System.Windows.Forms.CheckBox uxSeedOnStart;
        private System.Windows.Forms.CheckBox uxShowTransitions;
        private System.Windows.Forms.Label uxSeedOnStartPrompt;
        private System.Windows.Forms.Label uxShowTransitionPrompt;
        private System.Windows.Forms.Button uxOK;
        private System.Windows.Forms.Button uxCancel;
        private System.Windows.Forms.Label uxDynamicBoardSizePrompt;
        private System.Windows.Forms.CheckBox uxAutosizeBoard;
        private System.Windows.Forms.ComboBox uxRuleSet;
        private System.Windows.Forms.Label uxRuleSetPrompt;

    }
}