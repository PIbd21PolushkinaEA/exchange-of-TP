using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Unity;

namespace InternetShopView {
    public partial class FormProducts : Form {
        [Dependency] public new IUnityContainer Container { get; set; }

        private IProductService service;

        public FormProducts(IProductService service) {
            InitializeComponent();
            this.service = service;
        }

        private void Button_Add_Click(object sender, EventArgs e) {
            var form = Container.Resolve<FormProduct>();
            if (form.ShowDialog() == DialogResult.OK) {
                LoadData();
            }
        }

        private void Button_Change_Click(object sender, EventArgs e) {
            if ( dataGridView.SelectedRows.Count == 1 ) {
                var form = Container.Resolve<FormProduct>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if ( form.ShowDialog() == DialogResult.OK ) {
                    LoadData();
                }
            }
        }

        private void Button_Update_Click(object sender, EventArgs e) {
            LoadData();
        }

        private void LoadData() {
            try {
                List<ProductViewModel> list = service.GetList();
                if ( list != null ) {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[0].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch ( Exception ex ) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e) {
        }

        private void FormProducts_Load(object sender, EventArgs e) {
            LoadData();
        }
    }
}