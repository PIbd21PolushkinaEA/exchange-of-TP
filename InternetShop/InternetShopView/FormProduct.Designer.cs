namespace InternetShopView
{
    partial class FormProduct
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRef = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewProducts)).BeginInit();
            this.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название:";
            this.textBoxName.Location = new System.Drawing.Point(103, 7);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(177, 23);
            this.textBoxName.TabIndex = 2;
            this.buttonAdd.Location = new System.Drawing.Point(455, 110);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(88, 27);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Location = new System.Drawing.Point(18, 110);
            this.dataGridViewProducts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(416, 226);
            this.dataGridViewProducts.TabIndex = 4;
            this.textBoxPrice.Location = new System.Drawing.Point(103, 50);
            this.textBoxPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(177, 23);
            this.textBoxPrice.TabIndex = 6;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Цена:";
            this.buttonRef.Location = new System.Drawing.Point(455, 163);
            this.buttonRef.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(88, 27);
            this.buttonRef.TabIndex = 7;
            this.buttonRef.Text = "Изменить";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.ButtonRef_Click);
            this.buttonDel.Location = new System.Drawing.Point(455, 213);
            this.buttonDel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(88, 27);
            this.buttonDel.TabIndex = 8;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.ButtonDel_Click);
            this.buttonUpd.Location = new System.Drawing.Point(455, 262);
            this.buttonUpd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(88, 27);
            this.buttonUpd.TabIndex = 9;
            this.buttonUpd.Text = "Обновить";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.ButtonUpd_Click);
            this.buttonSave.Location = new System.Drawing.Point(282, 358);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(88, 27);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            this.buttonCancel.Location = new System.Drawing.Point(398, 358);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 27);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 423);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonUpd);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewProducts);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormProduct";
            this.Text = "FormProduct";
            this.Load += new System.EventHandler(this.FormProduct_Load);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}