using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Unity;

namespace InternetShopView {
    public partial class FormComponentProduct : Form {
        [Dependency] public new IUnityContainer Container { get; set; }

        public ComponentProductViewModel Model {
            set { model = value; }
            get { return model; }
        }

        public int Id {
            set => id = value;
        }

        private readonly IComponentService service;

        private ComponentProductViewModel model;

        private int? id;

        public FormComponentProduct(IComponentService service) {
            InitializeComponent();
            this.service = service;
        }

        private void FormProductComponent_Load(object sender, EventArgs e) {
            try {
                if ( id.HasValue ) {
                    ComponentViewModel view = service.GetElement(id.Value);
                    if ( view != null ) {
                        comboBoxManuf.Enabled = true;
                        comboBoxBrand.Enabled = true;
                        comboBoxDetail.SelectedItem = view.Name;
                        comboBoxManuf.SelectedItem = view.Manufacturer;
                        comboBoxBrand.SelectedItem = view.Brand;
                        textBoxCount.Text = view.CountOfAvailable.ToString();
                    }
                }
                else {
                    List<ComponentViewModel> list = service.GetList();

                    comboBoxDetail.DisplayMember = "Name";
                    comboBoxDetail.ValueMember = "Id";
                    comboBoxDetail.DataSource = list;
                    comboBoxDetail.SelectedItem = null;
                    comboBoxManuf.DisplayMember = "Manufacturer";
                    comboBoxManuf.ValueMember = "Id";
                    comboBoxManuf.Enabled = false;
                    comboBoxBrand.DisplayMember = "Brand";
                    comboBoxBrand.ValueMember = "Id";
                    comboBoxBrand.Enabled = false;
                }
            }
            catch ( Exception ex ) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }

            if ( model != null ) {
                comboBoxDetail.Enabled = false;
                comboBoxDetail.SelectedValue = model.ComponentId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void save_Button_Click(object sender, EventArgs e) {
            if ( string.IsNullOrEmpty(textBoxCount.Text) ) {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ( comboBoxDetail.SelectedValue == null ) {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }

            try {
                if ( model == null ) {
                    model = new ComponentProductViewModel {
                        ComponentId = Convert.ToInt32(comboBoxDetail.SelectedValue),
                        ComponentName = comboBoxDetail.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }

                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch ( Exception ex ) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void cancel_Button_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxDetail_SelectedIndexChanged(object sender, EventArgs e) {
            comboBoxManuf.Enabled = true;
            comboBoxManuf.SelectedItem = null;
            List<ComponentViewModel> listBrand = service.GetList()
                .Where(viewModel => viewModel.Name.Equals(comboBoxDetail.Text)).ToList();
            comboBoxManuf.DataSource = listBrand;
        }

        private void comboBoxManuf_SelectedIndexChanged_1(object sender, EventArgs e) {
            comboBoxBrand.Enabled = true;
            comboBoxBrand.SelectedItem = null;
            List<ComponentViewModel> listManuf = service.GetList()
                .Where(viewModel => viewModel.Manufacturer.Equals(comboBoxManuf.Text)).ToList();
            comboBoxBrand.DataSource = listManuf;
        }
    }
}