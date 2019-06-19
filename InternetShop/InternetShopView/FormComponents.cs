using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Unity;

namespace InternetShopView {
    public partial class FormComponents : Form {
        [Dependency] public new IUnityContainer Container { get; set; }

        private IComponentService service;

        public FormComponents(IComponentService service) {
            InitializeComponent();
            this.service = service;
        }

        private void Add_button_Click(object sender, EventArgs e) {
            var form = Container.Resolve<FormComponent>();
            if ( form.ShowDialog() == DialogResult.OK ) {
                LoadData();
            }
        }

        private void LoadData() {
            try {
                List<ComponentViewModel> list = service.GetList();
                if ( list != null ) {
                    dataGridViewComponents.DataSource = list;
                    dataGridViewComponents.Columns[0].Visible = false;
                    dataGridViewComponents.Columns[6].Visible = false;
                    dataGridViewComponents.Columns[0].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch ( Exception ex ) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void Change_button_Click(object sender, EventArgs e) {
            if ( dataGridViewComponents.SelectedRows.Count == 1 ) {
                var form = Container.Resolve<FormComponent>();
                form.Id = Convert.ToInt32(dataGridViewComponents.SelectedRows[0].Cells[0].Value);
                if ( form.ShowDialog() == DialogResult.OK ) {
                    LoadData();
                }
            }
        }

        private void Update_button_Click(object sender, EventArgs e) {
            LoadData();
        }

        private void Delete_button_Click(object sender, EventArgs e) {
            if ( dataGridViewComponents.SelectedRows.Count == 1 ) {
                if ( MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question) == DialogResult.Yes ) {
                    int id =
                        Convert.ToInt32(dataGridViewComponents.SelectedRows[0].Cells[0].Value);
                    try {
                        service.DelElement(id);
                        LoadData();
                    }
                    catch ( Exception ex ) {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FormComponents_Load(object sender, EventArgs e) {
            LoadData();
        }
    }
}