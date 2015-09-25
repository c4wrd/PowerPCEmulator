namespace PowerPCEmulator
{
    partial class EmulatorForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.parseTimeLabel = new System.Windows.Forms.Label();
            this.execTimeLabel = new System.Windows.Forms.Label();
            this.locLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.MaxLength = 2147483647;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(513, 508);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 578);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(334, 99);
            this.button1.TabIndex = 1;
            this.button1.Text = "Parse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // parseTimeLabel
            // 
            this.parseTimeLabel.AutoSize = true;
            this.parseTimeLabel.Location = new System.Drawing.Point(352, 615);
            this.parseTimeLabel.Name = "parseTimeLabel";
            this.parseTimeLabel.Size = new System.Drawing.Size(173, 25);
            this.parseTimeLabel.TabIndex = 2;
            this.parseTimeLabel.Text = "Parse Time: 0ms";
            // 
            // execTimeLabel
            // 
            this.execTimeLabel.AutoSize = true;
            this.execTimeLabel.Location = new System.Drawing.Point(352, 652);
            this.execTimeLabel.Name = "execTimeLabel";
            this.execTimeLabel.Size = new System.Drawing.Size(212, 25);
            this.execTimeLabel.TabIndex = 3;
            this.execTimeLabel.Text = "Execution Time: 0ms";
            // 
            // locLabel
            // 
            this.locLabel.AutoSize = true;
            this.locLabel.Location = new System.Drawing.Point(12, 523);
            this.locLabel.Name = "locLabel";
            this.locLabel.Size = new System.Drawing.Size(88, 25);
            this.locLabel.TabIndex = 4;
            this.locLabel.Text = "Lines: 0";
            this.locLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(540, 12);
            this.textBox2.MaxLength = 2147483647;
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(604, 508);
            this.textBox2.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1056, 536);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Output";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 689);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.locLabel);
            this.Controls.Add(this.execTimeLabel);
            this.Controls.Add(this.parseTimeLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "PPCGen";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label parseTimeLabel;
        private System.Windows.Forms.Label execTimeLabel;
        private System.Windows.Forms.Label locLabel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
    }
}

