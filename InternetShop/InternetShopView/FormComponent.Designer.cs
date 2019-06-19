namespace InternetShopView
{
    partial class FormComponent
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
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.BrandBox = new System.Windows.Forms.TextBox();
            this.ManufBox = new System.Windows.Forms.TextBox();
            this.RatingBox = new System.Windows.Forms.TextBox();
            this.PriceBox = new System.Windows.Forms.TextBox();
            this.save_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Брэнд:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Изготовлено:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Рейтинг";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 188);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Цена:";
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(107, 12);
            this.NameBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(228, 23);
            this.NameBox.TabIndex = 5;
            // 
            // BrandBox
            // 
            this.BrandBox.Location = new System.Drawing.Point(107, 52);
            this.BrandBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BrandBox.Name = "BrandBox";
            this.BrandBox.Size = new System.Drawing.Size(228, 23);
            this.BrandBox.TabIndex = 6;
            // 
            // ManufBox
            // 
            this.ManufBox.Location = new System.Drawing.Point(107, 98);
            this.ManufBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ManufBox.Name = "ManufBox";
            this.ManufBox.Size = new System.Drawing.Size(228, 23);
            this.ManufBox.TabIndex = 7;
            // 
            // RatingBox
            // 
            this.RatingBox.Location = new System.Drawing.Point(107, 144);
            this.RatingBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RatingBox.Name = "RatingBox";
            this.RatingBox.Size = new System.Drawing.Size(228, 23);
            this.RatingBox.TabIndex = 8;
            // 
            // PriceBox
            // 
            this.PriceBox.Location = new System.Drawing.Point(107, 185);
            this.PriceBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PriceBox.Name = "PriceBox";
            this.PriceBox.Size = new System.Drawing.Size(228, 23);
            this.PriceBox.TabIndex = 9;
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(226, 226);
            this.save_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(88, 27);
            this.save_button.TabIndex = 10;
            this.save_button.Text = "Сохранить";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // FormComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 285);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.PriceBox);
            this.Controls.Add(this.RatingBox);
            this.Controls.Add(this.ManufBox);
            this.Controls.Add(this.BrandBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormComponent";
            this.Text = "Добавить компонент";
            this.Load += new System.EventHandler(this.FormComponent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox BrandBox;
        private System.Windows.Forms.TextBox RatingBox;
        private System.Windows.Forms.TextBox PriceBox;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.TextBox ManufBox;
    }
}