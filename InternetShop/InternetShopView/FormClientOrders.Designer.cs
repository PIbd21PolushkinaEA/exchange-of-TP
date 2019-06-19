namespace InternetShopView {
    partial class FormClientOrders {
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
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.toForm_button = new System.Windows.Forms.Button();
            this.to_pdf_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(43, 12);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFrom.TabIndex = 0;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(300, 12);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerTo.TabIndex = 1;
            // 
            // toForm_button
            // 
            this.toForm_button.Location = new System.Drawing.Point(530, 8);
            this.toForm_button.Name = "toForm_button";
            this.toForm_button.Size = new System.Drawing.Size(75, 23);
            this.toForm_button.TabIndex = 2;
            this.toForm_button.Text = "На форме";
            this.toForm_button.UseVisualStyleBackColor = true;
            this.toForm_button.Click += new System.EventHandler(this.toForm_button_Click);
            // 
            // to_pdf_button
            // 
            this.to_pdf_button.Location = new System.Drawing.Point(648, 8);
            this.to_pdf_button.Name = "to_pdf_button";
            this.to_pdf_button.Size = new System.Drawing.Size(75, 23);
            this.to_pdf_button.TabIndex = 3;
            this.to_pdf_button.Text = "Pdf";
            this.to_pdf_button.UseVisualStyleBackColor = true;
            this.to_pdf_button.Click += new System.EventHandler(this.to_pdf_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "С";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "По";
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "InternetShopView.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(5, 38);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(793, 413);
            this.reportViewer1.TabIndex = 6;
            // 
            // FormClientOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.to_pdf_button);
            this.Controls.Add(this.toForm_button);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Name = "FormClientOrders";
            this.Text = "FormClientOrders";
            this.Load += new System.EventHandler(this.FormClientOrders_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Button toForm_button;
        private System.Windows.Forms.Button to_pdf_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}