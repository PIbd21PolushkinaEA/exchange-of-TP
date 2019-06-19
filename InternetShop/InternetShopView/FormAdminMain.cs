using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Unity;

namespace InternetShopView {
    public partial class FormAdminMain : Form {
        [Dependency] public new IUnityContainer Container { get; set; }

        private IRequestService service;

        public FormAdminMain(IRequestService service) {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData() {
            try {
                List<RequestViewModel> list = service.GetList();
                if ( list != null ) {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch ( Exception ex ) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void Add_button_Click(object sender, EventArgs e) {
            var form = Container.Resolve<FormRequest>();
            form.ShowDialog();
        }

        private void Update_button_Click(object sender, EventArgs e) {
            LoadData();
        }

        private void КомплектующиеToolStripMenuItem_Click(object sender, EventArgs e) {
            var form = Container.Resolve<FormComponents>();
            form.ShowDialog();
        }

        private void ТоварыToolStripMenuItem_Click(object sender, EventArgs e) {
            var form = Container.Resolve<FormProducts>();
            form.ShowDialog();
        }

        private void отчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClientOrders>();
            form.ShowDialog();
        }
    }
}