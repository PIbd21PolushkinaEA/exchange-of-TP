namespace InternetShopView
{
    partial class FormAuth
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Пароль:";
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(83, 19);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxPass.TabIndex = 1;
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(66, 68);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 2;
            this.buttonEnter.Text = "Войти";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.ButtonEnter_Click);
            // 
            // FormAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 131);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.label1);
            this.Name = "FormAuth";
            this.Text = "FormAuth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.Button buttonEnter;
    }
}