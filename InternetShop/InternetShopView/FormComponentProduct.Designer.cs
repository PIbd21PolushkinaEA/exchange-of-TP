using System.ComponentModel;

namespace InternetShopView {
    partial class FormComponentProduct {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if ( disposing && (components != null) ) {
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
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDetail = new System.Windows.Forms.ComboBox();
            this.cancel_Button = new System.Windows.Forms.Button();
            this.save_Button = new System.Windows.Forms.Button();
            this.comboBoxBrand = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxManuf = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(103, 158);
            this.textBoxCount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(334, 23);
            this.textBoxCount.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 163);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Компонент:";
            // 
            // comboBoxDetail
            // 
            this.comboBoxDetail.FormattingEnabled = true;
            this.comboBoxDetail.Location = new System.Drawing.Point(103, 33);
            this.comboBoxDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxDetail.Name = "comboBoxDetail";
            this.comboBoxDetail.Size = new System.Drawing.Size(334, 23);
            this.comboBoxDetail.TabIndex = 3;
            this.comboBoxDetail.SelectedIndexChanged +=
                new System.EventHandler(this.comboBoxDetail_SelectedIndexChanged);
            // 
            // cancel_Button
            // 
            this.cancel_Button.Location = new System.Drawing.Point(354, 223);
            this.cancel_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cancel_Button.Name = "cancel_Button";
            this.cancel_Button.Size = new System.Drawing.Size(88, 27);
            this.cancel_Button.TabIndex = 4;
            this.cancel_Button.Text = "Отмена";
            this.cancel_Button.UseVisualStyleBackColor = true;
            this.cancel_Button.Click += new System.EventHandler(this.cancel_Button_Click);
            // 
            // save_Button
            // 
            this.save_Button.Location = new System.Drawing.Point(258, 223);
            this.save_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.save_Button.Name = "save_Button";
            this.save_Button.Size = new System.Drawing.Size(88, 27);
            this.save_Button.TabIndex = 5;
            this.save_Button.Text = "Сохранить";
            this.save_Button.UseVisualStyleBackColor = true;
            this.save_Button.Click += new System.EventHandler(this.save_Button_Click);
            // 
            // comboBoxBrand
            // 
            this.comboBoxBrand.Enabled = false;
            this.comboBoxBrand.FormattingEnabled = true;
            this.comboBoxBrand.Location = new System.Drawing.Point(103, 118);
            this.comboBoxBrand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxBrand.Name = "comboBoxBrand";
            this.comboBoxBrand.Size = new System.Drawing.Size(334, 23);
            this.comboBoxBrand.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Брэнд:";
            // 
            // comboBoxManuf
            // 
            this.comboBoxManuf.Enabled = false;
            this.comboBoxManuf.FormattingEnabled = true;
            this.comboBoxManuf.Location = new System.Drawing.Point(103, 74);
            this.comboBoxManuf.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxManuf.Name = "comboBoxManuf";
            this.comboBoxManuf.Size = new System.Drawing.Size(334, 23);
            this.comboBoxManuf.TabIndex = 9;
            this.comboBoxManuf.SelectedIndexChanged +=
                new System.EventHandler(this.comboBoxManuf_SelectedIndexChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 74);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Изготовлено:";
            // 
            // FormComponentProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 262);
            this.Controls.Add(this.comboBoxManuf);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxBrand);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.save_Button);
            this.Controls.Add(this.cancel_Button);
            this.Controls.Add(this.comboBoxDetail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxCount);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormComponentProduct";
            this.Text = "Компонент изделия";
            this.Load += new System.EventHandler(this.FormProductComponent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDetail;
        private System.Windows.Forms.Button cancel_Button;
        private System.Windows.Forms.ComboBox comboBoxBrand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxManuf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button save_Button;
    }
}