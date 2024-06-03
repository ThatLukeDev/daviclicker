namespace autohold
{
    partial class Form1
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
            this.tester = new System.Windows.Forms.Button();
            this.enabled = new System.Windows.Forms.CheckBox();
            this.view = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tester
            // 
            this.tester.Dock = System.Windows.Forms.DockStyle.Top;
            this.tester.Location = new System.Drawing.Point(0, 0);
            this.tester.Name = "tester";
            this.tester.Size = new System.Drawing.Size(800, 307);
            this.tester.TabIndex = 0;
            this.tester.Text = "Click here";
            this.tester.UseVisualStyleBackColor = true;
            this.tester.Click += new System.EventHandler(this.tester_Click);
            // 
            // enabled
            // 
            this.enabled.AutoSize = true;
            this.enabled.Location = new System.Drawing.Point(13, 314);
            this.enabled.Name = "enabled";
            this.enabled.Size = new System.Drawing.Size(65, 17);
            this.enabled.TabIndex = 1;
            this.enabled.Text = "Enabled";
            this.enabled.UseVisualStyleBackColor = true;
            // 
            // view
            // 
            this.view.AutoSize = true;
            this.view.Location = new System.Drawing.Point(13, 338);
            this.view.Name = "view";
            this.view.Size = new System.Drawing.Size(45, 13);
            this.view.TabIndex = 2;
            this.view.Text = "000 cps";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.view);
            this.Controls.Add(this.enabled);
            this.Controls.Add(this.tester);
            this.Name = "Form1";
            this.Text = "click";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button tester;
        private System.Windows.Forms.CheckBox enabled;
        private System.Windows.Forms.Label view;
    }
}

