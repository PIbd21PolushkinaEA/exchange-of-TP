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
        private void InitializeComponent() {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Add_button = new System.Windows.Forms.Button();
            this.Change_button = new System.Windows.Forms.Button();
            this.Update_button = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.комплектующиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.товарыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 47);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(451, 247);
            this.dataGridView.TabIndex = 2;
            // 
            // Add_button
            // 
            this.Add_button.Location = new System.Drawing.Point(482, 85);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(105, 23);
            this.Add_button.TabIndex = 3;
            this.Add_button.Text = "Добавить заявку";
            this.Add_button.UseVisualStyleBackColor = true;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // Change_button
            // 
            this.Change_button.Location = new System.Drawing.Point(482, 127);
            this.Change_button.Name = "Change_button";
            this.Change_button.Size = new System.Drawing.Size(105, 23);
            this.Change_button.TabIndex = 4;
            this.Change_button.Text = "Изменить заявку";
            this.Change_button.UseVisualStyleBackColor = true;
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(482, 174);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(105, 23);
            this.Update_button.TabIndex = 5;
            this.Update_button.Text = "Обновить заявку";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.отчётToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(610, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.комплектующиеToolStripMenuItem,
            this.товарыToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            this.справочникToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникToolStripMenuItem.Text = "Справочники";
            // 
            // комплектующиеToolStripMenuItem
            // 
            this.комплектующиеToolStripMenuItem.Name = "комплектующиеToolStripMenuItem";
            this.комплектующиеToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.комплектующиеToolStripMenuItem.Text = "Комплектующие";
            this.комплектующиеToolStripMenuItem.Click += new System.EventHandler(this.КомплектующиеToolStripMenuItem_Click);
            // 
            // товарыToolStripMenuItem
            // 
            this.товарыToolStripMenuItem.Name = "товарыToolStripMenuItem";
            this.товарыToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.товарыToolStripMenuItem.Text = "Товары";
            this.товарыToolStripMenuItem.Click += new System.EventHandler(this.ТоварыToolStripMenuItem_Click);
            // 
            // отчётToolStripMenuItem
            // 
            this.отчётToolStripMenuItem.Name = "отчётToolStripMenuItem";
            this.отчётToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчётToolStripMenuItem.Text = "Отчёты";
            this.отчётToolStripMenuItem.Click += new System.EventHandler(this.отчётToolStripMenuItem_Click);
            // 
            // FormAdminMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 386);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.Change_button);
            this.Controls.Add(this.Add_button);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormAdminMain";
            this.Text = "FormAdminMain";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button Add_button;
        private System.Windows.Forms.Button Change_button;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem комплектующиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem товарыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётToolStripMenuItem;
    }
}