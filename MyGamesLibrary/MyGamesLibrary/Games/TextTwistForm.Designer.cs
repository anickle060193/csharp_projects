namespace MyGamesLibrary.Games
{
    partial class TextTwistForm
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
            this.uxLetter1 = new System.Windows.Forms.TextBox();
            this.uxLetter2 = new System.Windows.Forms.TextBox();
            this.uxLetter4 = new System.Windows.Forms.TextBox();
            this.uxLetter3 = new System.Windows.Forms.TextBox();
            this.uxLetter5 = new System.Windows.Forms.TextBox();
            this.uxLetter6 = new System.Windows.Forms.TextBox();
            this.uxLettersGroup = new System.Windows.Forms.GroupBox();
            this.uxWordGroup = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.uxWordsList = new System.Windows.Forms.ListBox();
            this.uxLettersGroup.SuspendLayout();
            this.uxWordGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxLetter1
            // 
            this.uxLetter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxLetter1.Location = new System.Drawing.Point(6, 19);
            this.uxLetter1.MaxLength = 1;
            this.uxLetter1.Name = "uxLetter1";
            this.uxLetter1.ReadOnly = true;
            this.uxLetter1.Size = new System.Drawing.Size(62, 62);
            this.uxLetter1.TabIndex = 0;
            this.uxLetter1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uxLetter2
            // 
            this.uxLetter2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxLetter2.Location = new System.Drawing.Point(74, 19);
            this.uxLetter2.MaxLength = 1;
            this.uxLetter2.Name = "uxLetter2";
            this.uxLetter2.ReadOnly = true;
            this.uxLetter2.Size = new System.Drawing.Size(62, 62);
            this.uxLetter2.TabIndex = 1;
            this.uxLetter2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uxLetter4
            // 
            this.uxLetter4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxLetter4.Location = new System.Drawing.Point(210, 19);
            this.uxLetter4.MaxLength = 1;
            this.uxLetter4.Name = "uxLetter4";
            this.uxLetter4.ReadOnly = true;
            this.uxLetter4.Size = new System.Drawing.Size(62, 62);
            this.uxLetter4.TabIndex = 3;
            this.uxLetter4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uxLetter3
            // 
            this.uxLetter3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxLetter3.Location = new System.Drawing.Point(142, 19);
            this.uxLetter3.MaxLength = 1;
            this.uxLetter3.Name = "uxLetter3";
            this.uxLetter3.ReadOnly = true;
            this.uxLetter3.Size = new System.Drawing.Size(62, 62);
            this.uxLetter3.TabIndex = 2;
            this.uxLetter3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uxLetter5
            // 
            this.uxLetter5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxLetter5.Location = new System.Drawing.Point(278, 19);
            this.uxLetter5.MaxLength = 1;
            this.uxLetter5.Name = "uxLetter5";
            this.uxLetter5.ReadOnly = true;
            this.uxLetter5.Size = new System.Drawing.Size(62, 62);
            this.uxLetter5.TabIndex = 5;
            this.uxLetter5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uxLetter6
            // 
            this.uxLetter6.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxLetter6.Location = new System.Drawing.Point(346, 19);
            this.uxLetter6.MaxLength = 1;
            this.uxLetter6.Name = "uxLetter6";
            this.uxLetter6.ReadOnly = true;
            this.uxLetter6.Size = new System.Drawing.Size(62, 62);
            this.uxLetter6.TabIndex = 4;
            this.uxLetter6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uxLettersGroup
            // 
            this.uxLettersGroup.Controls.Add(this.uxLetter1);
            this.uxLettersGroup.Controls.Add(this.uxLetter2);
            this.uxLettersGroup.Controls.Add(this.uxLetter3);
            this.uxLettersGroup.Controls.Add(this.uxLetter4);
            this.uxLettersGroup.Controls.Add(this.uxLetter6);
            this.uxLettersGroup.Controls.Add(this.uxLetter5);
            this.uxLettersGroup.Location = new System.Drawing.Point(312, 103);
            this.uxLettersGroup.Name = "uxLettersGroup";
            this.uxLettersGroup.Size = new System.Drawing.Size(417, 97);
            this.uxLettersGroup.TabIndex = 12;
            this.uxLettersGroup.TabStop = false;
            this.uxLettersGroup.Text = "Word";
            // 
            // uxWordGroup
            // 
            this.uxWordGroup.Controls.Add(this.textBox1);
            this.uxWordGroup.Controls.Add(this.textBox2);
            this.uxWordGroup.Controls.Add(this.textBox3);
            this.uxWordGroup.Controls.Add(this.textBox4);
            this.uxWordGroup.Controls.Add(this.textBox5);
            this.uxWordGroup.Controls.Add(this.textBox6);
            this.uxWordGroup.Location = new System.Drawing.Point(312, 206);
            this.uxWordGroup.Name = "uxWordGroup";
            this.uxWordGroup.Size = new System.Drawing.Size(417, 97);
            this.uxWordGroup.TabIndex = 13;
            this.uxWordGroup.TabStop = false;
            this.uxWordGroup.Text = "Letters";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.MaxLength = 1;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(62, 62);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(74, 19);
            this.textBox2.MaxLength = 1;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(62, 62);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(142, 19);
            this.textBox3.MaxLength = 1;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(62, 62);
            this.textBox3.TabIndex = 2;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(210, 19);
            this.textBox4.MaxLength = 1;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(62, 62);
            this.textBox4.TabIndex = 3;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(346, 19);
            this.textBox5.MaxLength = 1;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(62, 62);
            this.textBox5.TabIndex = 4;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(278, 19);
            this.textBox6.MaxLength = 1;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(62, 62);
            this.textBox6.TabIndex = 5;
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // uxWordsList
            // 
            this.uxWordsList.FormattingEnabled = true;
            this.uxWordsList.Location = new System.Drawing.Point(12, 12);
            this.uxWordsList.Name = "uxWordsList";
            this.uxWordsList.Size = new System.Drawing.Size(282, 472);
            this.uxWordsList.TabIndex = 14;
            // 
            // TextTwistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 501);
            this.Controls.Add(this.uxWordsList);
            this.Controls.Add(this.uxWordGroup);
            this.Controls.Add(this.uxLettersGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "TextTwistForm";
            this.Text = "Text Twist";
            this.uxLettersGroup.ResumeLayout(false);
            this.uxLettersGroup.PerformLayout();
            this.uxWordGroup.ResumeLayout(false);
            this.uxWordGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox uxLetter1;
        private System.Windows.Forms.TextBox uxLetter2;
        private System.Windows.Forms.TextBox uxLetter4;
        private System.Windows.Forms.TextBox uxLetter3;
        private System.Windows.Forms.TextBox uxLetter5;
        private System.Windows.Forms.TextBox uxLetter6;
        private System.Windows.Forms.GroupBox uxLettersGroup;
        private System.Windows.Forms.GroupBox uxWordGroup;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.ListBox uxWordsList;
    }
}