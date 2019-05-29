namespace InternetShopView
{
    partial class FormAdminMain
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
            this.addComponent_Button = new System.Windows.Forms.Button();
            this.report_button = new System.Windows.Forms.Button();
            this.dataGridViewRequests = new System.Windows.Forms.DataGridView();
            this.Add_button = new System.Windows.Forms.Button();
            this.Change_button = new System.Windows.Forms.Button();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // addComponent_Button
            // 
            this.addComponent_Button.Location = new System.Drawing.Point(13, 13);
            this.addComponent_Button.Name = "addComponent_Button";
            this.addComponent_Button.Size = new System.Drawing.Size(141, 23);
            this.addComponent_Button.TabIndex = 0;
            this.addComponent_Button.Text = "Доступные компоненты";
            this.addComponent_Button.UseVisualStyleBackColor = true;
            this.addComponent_Button.Click += new System.EventHandler(this.AddComponent_Button_Click);
            // 
            // report_button
            // 
            this.report_button.Location = new System.Drawing.Point(177, 13);
            this.report_button.Name = "report_button";
            this.report_button.Size = new System.Drawing.Size(75, 23);
            this.report_button.TabIndex = 1;
            this.report_button.Text = "Отчёт";
            this.report_button.UseVisualStyleBackColor = true;
            // 
            // dataGridViewRequests
            // 
            this.dataGridViewRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRequests.Location = new System.Drawing.Point(13, 53);
            this.dataGridViewRequests.Name = "dataGridViewRequests";
            this.dataGridViewRequests.Size = new System.Drawing.Size(451, 247);
            this.dataGridViewRequests.TabIndex = 2;
            // 
            // Add_button
            // 
            this.Add_button.Location = new System.Drawing.Point(482, 85);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(105, 23);
            this.Add_button.TabIndex = 3;
            this.Add_button.Text = "Добавить запрос";
            this.Add_button.UseVisualStyleBackColor = true;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // Change_button
            // 
            this.Change_button.Location = new System.Drawing.Point(482, 127);
            this.Change_button.Name = "Change_button";
            this.Change_button.Size = new System.Drawing.Size(105, 23);
            this.Change_button.TabIndex = 4;
            this.Change_button.Text = "Изменить запрос";
            this.Change_button.UseVisualStyleBackColor = true;
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(482, 174);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(105, 23);
            this.Update_button.TabIndex = 5;
            this.Update_button.Text = "Обновить запрос";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(482, 215);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(105, 23);
            this.Delete_button.TabIndex = 6;
            this.Delete_button.Text = "Удалить запрос";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // FormAdminMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 386);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.Change_button);
            this.Controls.Add(this.Add_button);
            this.Controls.Add(this.dataGridViewRequests);
            this.Controls.Add(this.report_button);
            this.Controls.Add(this.addComponent_Button);
            this.Name = "FormAdminMain";
            this.Text = "FormAdminMain";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addComponent_Button;
        private System.Windows.Forms.Button report_button;
        private System.Windows.Forms.DataGridView dataGridViewRequests;
        private System.Windows.Forms.Button Add_button;
        private System.Windows.Forms.Button Change_button;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
    }
}