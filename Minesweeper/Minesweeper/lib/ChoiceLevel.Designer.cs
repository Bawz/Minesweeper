namespace Minesweeper
{
    partial class ChoiceLevel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.small_field = new System.Windows.Forms.Button();
            this.middle_size = new System.Windows.Forms.Button();
            this.large_size = new System.Windows.Forms.Button();
            this.highscoreSmallButton = new System.Windows.Forms.Button();
            this.highscoreMiddleButton = new System.Windows.Forms.Button();
            this.highscoreBigButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // small_field
            // 
            this.small_field.Location = new System.Drawing.Point(64, 32);
            this.small_field.Name = "small_field";
            this.small_field.Size = new System.Drawing.Size(153, 23);
            this.small_field.TabIndex = 0;
            this.small_field.Text = "Klein (8x8)";
            this.small_field.UseVisualStyleBackColor = true;
            this.small_field.Click += new System.EventHandler(this.small_field_Click);
            // 
            // middle_size
            // 
            this.middle_size.Location = new System.Drawing.Point(64, 61);
            this.middle_size.Name = "middle_size";
            this.middle_size.Size = new System.Drawing.Size(153, 23);
            this.middle_size.TabIndex = 1;
            this.middle_size.Text = "Mittel (16x16)";
            this.middle_size.UseVisualStyleBackColor = true;
            this.middle_size.Click += new System.EventHandler(this.middle_size_Click);
            // 
            // large_size
            // 
            this.large_size.Location = new System.Drawing.Point(64, 90);
            this.large_size.Name = "large_size";
            this.large_size.Size = new System.Drawing.Size(153, 23);
            this.large_size.TabIndex = 2;
            this.large_size.Text = "Groß (20x20)";
            this.large_size.UseVisualStyleBackColor = true;
            this.large_size.Click += new System.EventHandler(this.large_size_Click);
            // 
            // highscoreSmallButton
            // 
            this.highscoreSmallButton.Location = new System.Drawing.Point(223, 32);
            this.highscoreSmallButton.Name = "highscoreSmallButton";
            this.highscoreSmallButton.Size = new System.Drawing.Size(75, 23);
            this.highscoreSmallButton.TabIndex = 3;
            this.highscoreSmallButton.Text = "Bestenliste";
            this.highscoreSmallButton.UseVisualStyleBackColor = true;
            this.highscoreSmallButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // highscoreMiddleButton
            // 
            this.highscoreMiddleButton.Location = new System.Drawing.Point(223, 61);
            this.highscoreMiddleButton.Name = "highscoreMiddleButton";
            this.highscoreMiddleButton.Size = new System.Drawing.Size(75, 23);
            this.highscoreMiddleButton.TabIndex = 4;
            this.highscoreMiddleButton.Text = "Bestenliste";
            this.highscoreMiddleButton.UseVisualStyleBackColor = true;
            this.highscoreMiddleButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // highscoreBigButton
            // 
            this.highscoreBigButton.Location = new System.Drawing.Point(223, 90);
            this.highscoreBigButton.Name = "highscoreBigButton";
            this.highscoreBigButton.Size = new System.Drawing.Size(75, 23);
            this.highscoreBigButton.TabIndex = 5;
            this.highscoreBigButton.Text = "Bestenliste";
            this.highscoreBigButton.UseVisualStyleBackColor = true;
            this.highscoreBigButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // ChoiceLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 147);
            this.Controls.Add(this.highscoreBigButton);
            this.Controls.Add(this.highscoreMiddleButton);
            this.Controls.Add(this.highscoreSmallButton);
            this.Controls.Add(this.large_size);
            this.Controls.Add(this.middle_size);
            this.Controls.Add(this.small_field);
            this.Name = "ChoiceLevel";
            this.Text = "Level Auswahl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button small_field;
        private System.Windows.Forms.Button middle_size;
        private System.Windows.Forms.Button large_size;
        private System.Windows.Forms.Button highscoreSmallButton;
        private System.Windows.Forms.Button highscoreMiddleButton;
        private System.Windows.Forms.Button highscoreBigButton;
    }
}