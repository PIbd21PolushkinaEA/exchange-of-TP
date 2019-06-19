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
                    List<ComponentViewModel> list = service.GetList();
                    if ( view != null ) {
                        initComboBox(list);
                        comboBoxManuf.Enabled = true;
                        comboBoxBrand.Enabled = true;
                        comboBoxDetail.SelectedValue = view.Id;
                        comboBoxManuf.SelectedValue = view.Id;
                        comboBoxBrand.SelectedValue = view.Id;
                        textBoxCount.Text = "0";
                    }
                }
                else {
                    List<ComponentViewModel> list = service.GetList();

                    initComboBox(list);
                }
            }
            catch ( Exception ex ) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }

            if ( model != null ) {
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
            ComponentViewModel element = service.GetElement(Convert.ToInt32(comboBoxBrand.SelectedValue));
            try {
                if ( model == null ) {
                    model = new ComponentProductViewModel {
                        ComponentId = element.Id,
                        ComponentName = element.Name,
                        Manuf = element.Manufacturer,
                        Brand = element.Brand,
                        ComponentRating = element.Rating,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else {
                    model.ComponentName = comboBoxDetail.Text;
                    model.Manuf = comboBoxManuf.Text;
                    model.Brand = comboBoxBrand.Text;
                    model.ComponentRating = element.Rating;
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

        private void initComboBox(List<ComponentViewModel> list)
        {
            comboBoxDetail.DisplayMember = "Name";
            comboBoxDetail.ValueMember = "Id";
            comboBoxDetail.DataSource = list;
            comboBoxDetail.SelectedItem = null;
        }

        private void cancel_Button_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}