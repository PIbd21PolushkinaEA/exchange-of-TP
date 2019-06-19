namespace InternetShopView
{
    partial class FormComponents
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
            this.add_Button = new System.Windows.Forms.Button();
            this.change_button = new System.Windows.Forms.Button();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.dataGridViewComponents = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewComponents)).BeginInit();
            this.SuspendLayout();
            // 
            // add_Button
            // 
            this.add_Button.Location = new System.Drawing.Point(629, 57);
            this.add_Button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.add_Button.Name = "add_Button";
            this.add_Button.Size = new System.Drawing.Size(88, 27);
            this.add_Button.TabIndex = 0;
            this.add_Button.Text = "Добавить";
            this.add_Button.UseVisualStyleBackColor = true;
            this.add_Button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // change_button
            // 
            this.change_button.Location = new System.Drawing.Point(629, 107);
            this.change_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.change_button.Name = "change_button";
            this.change_button.Size = new System.Drawing.Size(88, 27);
            this.change_button.TabIndex = 1;
            this.change_button.Text = "Изменить";
            this.change_button.UseVisualStyleBackColor = true;
            this.change_button.Click += new System.EventHandler(this.Change_button_Click);
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(629, 159);
            this.Update_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(88, 27);
            this.Update_button.TabIndex = 2;
            this.Update_button.Text = "Обновить";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(629, 213);
            this.Delete_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(88, 27);
            this.Delete_button.TabIndex = 3;
            this.Delete_button.Text = "Удалить";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // dataGridViewComponents
            // 
            this.dataGridViewComponents.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComponents.Location = new System.Drawing.Point(15, 15);
            this.dataGridViewComponents.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewComponents.Name = "dataGridViewComponents";
            this.dataGridViewComponents.Size = new System.Drawing.Size(574, 346);
            this.dataGridViewComponents.TabIndex = 4;
            // 
            // FormComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 381);
            this.Controls.Add(this.dataGridViewComponents);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.change_button);
            this.Controls.Add(this.add_Button);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormComponents";
            this.Text = "Компоненты";
            this.Load += new System.EventHandler(this.FormComponents_Load);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewComponents)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button add_Button;
        private System.Windows.Forms.Button change_button;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.DataGridView dataGridViewComponents;
    }
}