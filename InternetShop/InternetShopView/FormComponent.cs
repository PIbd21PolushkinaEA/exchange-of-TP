using System;
using System.Windows.Forms;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Unity;

namespace InternetShopView {
    public partial class FormComponent : Form {
        [Dependency] public new IUnityContainer Container { get; set; }

        public int Id {
            set => id = value;
        }

        private readonly IComponentService service;

        private int? id;


        public FormComponent(IComponentService service) {
            InitializeComponent();
            this.service = service;
        }

        private void Save_button_Click(object sender, EventArgs e) {
            if ( string.IsNullOrEmpty(NameBox.Text) && string.IsNullOrEmpty(BrandBox.Text)
                                                    && string.IsNullOrEmpty(ManufBox.Text) &&
                                                    string.IsNullOrEmpty(RatingBox.Text)
                                                    && string.IsNullOrEmpty(PriceBox.Text) ) {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }

            try {
                if ( id.HasValue ) {
                    service.UpdElement(new ComponentBindingModel {
                        Id = id.Value,
                        Name = NameBox.Text,
                        Brand = BrandBox.Text,
                        Manufacturer = ManufBox.Text,
                        Rating = float.Parse(RatingBox.Text),
                        Price = Convert.ToInt32(PriceBox.Text)
                    });
                }
                else {
                    service.AddElement(new ComponentBindingModel {
                        Name = NameBox.Text,
                        Brand = BrandBox.Text,
                        Manufacturer = ManufBox.Text,
                        Rating = float.Parse(RatingBox.Text),
                        Price = Convert.ToInt32(PriceBox.Text)
                    });
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

        private void FormComponent_Load(object sender, EventArgs e) {
            if ( id.HasValue ) {
                try {
                    ComponentViewModel view = service.GetElement(id.Value);
                    if ( view != null ) {
                        NameBox.Text = view.Name;
                        BrandBox.Text = view.Brand;
                        ManufBox.Text = view.Manufacturer;
                        RatingBox.Text = view.Rating.ToString();
                        PriceBox.Text = view.Price.ToString();
                    }
                }
                catch ( Exception ex ) {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
    }
}