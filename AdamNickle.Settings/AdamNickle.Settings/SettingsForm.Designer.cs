namespace AdamNickle.Settings
{
    partial class SettingsForm
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
            this.uxSettingsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.uxCancel = new System.Windows.Forms.Button();
            this.uxOK = new System.Windows.Forms.Button();
            this.uxButtonsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.uxButtonsLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxSettingsLayout
            // 
            this.uxSettingsLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxSettingsLayout.AutoSize = true;
            this.uxSettingsLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uxSettingsLayout.ColumnCount = 2;
            this.uxSettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uxSettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uxSettingsLayout.Location = new System.Drawing.Point(12, 12);
            this.uxSettingsLayout.MinimumSize = new System.Drawing.Size(160, 30);
            this.uxSettingsLayout.Name = "uxSettingsLayout";
            this.uxSettingsLayout.RowCount = 1;
            this.uxSettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.uxSettingsLayout.Size = new System.Drawing.Size(160, 30);
            this.uxSettingsLayout.TabIndex = 0;
            // 
            // uxCancel
            // 
            this.uxCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uxCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxCancel.Location = new System.Drawing.Point(92, 3);
            this.uxCancel.Name = "uxCancel";
            this.uxCancel.Size = new System.Drawing.Size(76, 27);
            this.uxCancel.TabIndex = 1;
            this.uxCancel.Text = "Cancel";
            this.uxCancel.UseVisualStyleBackColor = true;
            // 
            // uxOK
            // 
            this.uxOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxOK.Location = new System.Drawing.Point(8, 3);
            this.uxOK.Name = "uxOK";
            this.uxOK.Size = new System.Drawing.Size(78, 27);
            this.uxOK.TabIndex = 2;
            this.uxOK.Text = "OK";
            this.uxOK.UseVisualStyleBackColor = true;
            this.uxOK.Click += new System.EventHandler(this.uxOK_Click);
            // 
            // uxButtonsLayout
            // 
            this.uxButtonsLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxButtonsLayout.ColumnCount = 2;
            this.uxButtonsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uxButtonsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uxButtonsLayout.Controls.Add(this.uxCancel, 1, 0);
            this.uxButtonsLayout.Controls.Add(this.uxOK, 0, 0);
            this.uxButtonsLayout.Location = new System.Drawing.Point(3, 44);
            this.uxButtonsLayout.Name = "uxButtonsLayout";
            this.uxButtonsLayout.RowCount = 1;
            this.uxButtonsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.uxButtonsLayout.Size = new System.Drawing.Size(179, 33);
            this.uxButtonsLayout.TabIndex = 1;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(184, 81);
            this.Controls.Add(this.uxButtonsLayout);
            this.Controls.Add(this.uxSettingsLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 120);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.uxButtonsLayout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel uxSettingsLayout;
        private System.Windows.Forms.Button uxCancel;
        private System.Windows.Forms.Button uxOK;
        private System.Windows.Forms.TableLayoutPanel uxButtonsLayout;
    }
}