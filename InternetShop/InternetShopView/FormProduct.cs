using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Unity;

namespace InternetShopView {
    public partial class FormProduct : Form {
        [Dependency] public new IUnityContainer Container { get; set; }

        public int Id {
            set => id = value;
        }

        private readonly IProductService service;

        private int? id;

        private List<ComponentProductViewModel> _componentsProduct;

        public FormProduct(IProductService service) {
            InitializeComponent();
            this.service = service;
        }

        private void ButtonAdd_Click(object sender, EventArgs e) {
            var form = Container.Resolve<FormComponentProduct>(); 
            if ( form.ShowDialog() == DialogResult.OK ) {
                if ( form.Model != null ) {
                    if ( id.HasValue ) {
                        form.Model.ProductId = id.Value;
                    }
                    _componentsProduct.Add(form.Model);
                }

                LoadData();
            }
        }

        private void ButtonRef_Click(object sender, EventArgs e) {
            if ( dataGridViewProducts.SelectedRows.Count == 1 ) {
                var form = Container.Resolve<FormComponentProduct>();
                form.Model =
               _componentsProduct[dataGridViewProducts.SelectedRows[0].Cells[0].RowIndex];
                if ( form.ShowDialog() == DialogResult.OK ) {
                    _componentsProduct[dataGridViewProducts.SelectedRows[0].Cells[0].RowIndex] =
                   form.Model;
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e) {
            if ( dataGridViewProducts.SelectedRows.Count == 1 ) {
                if ( MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question) == DialogResult.Yes ) {
                    int id =
                        Convert.ToInt32(dataGridViewProducts.SelectedRows[0].Cells[0].Value);
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

        private void ButtonUpd_Click(object sender, EventArgs e) {
            LoadData();
        }

        private void FormProduct_Load(object sender, EventArgs e) {
            if ( id.HasValue ) {
                try {
                    ProductViewModel view = service.GetElement(id.Value);
                    if ( view != null ) {
                        textBoxName.Text = view.ProductName;
                        textBoxPrice.Text = view.Price.ToString();
                        _componentsProduct = view.ComponentsProduct;
                        LoadData();
                    }
                }
                catch ( Exception ex ) {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else {
                _componentsProduct = new List<ComponentProductViewModel>();
            }
        }

        private void LoadData() {
            try {
                if ( _componentsProduct != null ) {
                    dataGridViewProducts.DataSource = null;
                    dataGridViewProducts.DataSource = _componentsProduct;
                    dataGridViewProducts.Columns[0].Visible = false;
                    dataGridViewProducts.Columns[1].Visible = false;
                    dataGridViewProducts.Columns[2].Visible = false;
                    dataGridViewProducts.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewProducts.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewProducts.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewProducts.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch ( Exception ex ) {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(textBoxName.Text)) {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text)) {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (_componentsProduct == null || _componentsProduct.Count == 0) {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try {
                List<ComponentProductBindingModel> productComponentBM = new
               List<ComponentProductBindingModel>();
                for (int i = 0; i < _componentsProduct.Count; ++i) {
                    productComponentBM.Add(new ComponentProductBindingModel{
                        Id = _componentsProduct[i].Id,
                        ProductId = _componentsProduct[i].ProductId,
                        ComponentId = _componentsProduct[i].ComponentId,
                        ComponentName = _componentsProduct[i].ComponentName,
                        Manuf = _componentsProduct[i].Manuf,
                        Brand = _componentsProduct[i].Brand,
                        ComponentRating = _componentsProduct[i].ComponentRating,
                        Count = _componentsProduct[i].Count
                    });
                }
                if (id.HasValue) {
                    service.UpdElement(new ProductBindingModel {
                        Id = id.Value,
                        ProductName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        ComponentsProduct = productComponentBM
                    });
                } else {
                    service.AddElement(new ProductBindingModel {
                        ProductName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        ComponentsProduct = productComponentBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            } catch (Exception ex){
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}