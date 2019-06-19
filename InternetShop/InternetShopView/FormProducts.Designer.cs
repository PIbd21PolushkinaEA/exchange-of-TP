namespace InternetShopView
{
    partial class FormProducts
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
            this.button_Add = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.button_Change = new System.Windows.Forms.Button();
            this.button_Update = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView)).BeginInit();
            this.SuspendLayout();
            this.button_Add.Location = new System.Drawing.Point(654, 50);
            this.button_Add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(88, 27);
            this.button_Add.TabIndex = 0;
            this.button_Add.Text = "Добавить";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            this.dataGridView.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(15, 15);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(612, 320);
            this.dataGridView.TabIndex = 1;
            this.button_Change.Location = new System.Drawing.Point(654, 104);
            this.button_Change.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Change.Name = "button_Change";
            this.button_Change.Size = new System.Drawing.Size(88, 27);
            this.button_Change.TabIndex = 2;
            this.button_Change.Text = "Изменить";
            this.button_Change.UseVisualStyleBackColor = true;
            this.button_Change.Click += new System.EventHandler(this.Button_Change_Click);
            this.button_Update.Location = new System.Drawing.Point(654, 155);
            this.button_Update.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(88, 27);
            this.button_Update.TabIndex = 3;
            this.button_Update.Text = "Обновить";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.Button_Update_Click);
            this.button_Delete.Location = new System.Drawing.Point(654, 209);
            this.button_Delete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(88, 27);
            this.button_Delete.TabIndex = 4;
            this.button_Delete.Text = "Удалить";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.Button_Delete_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 366);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.button_Change);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.button_Add);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormProducts";
            this.Text = "FormProducts";
            this.Load += new System.EventHandler(this.FormProducts_Load);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button button_Change;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Button button_Delete;
    }
}