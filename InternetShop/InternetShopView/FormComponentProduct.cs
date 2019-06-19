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

        public FormComponentProduct(IComponentService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormProductComponent_Load(object sender, EventArgs e)
        {
            try
            {
                List<ComponentViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewComponents.DataSource = list;
                    dataGridViewComponents.Columns[0].Visible = false;
                    dataGridViewComponents.Columns[6].Visible = false;
                    dataGridViewComponents.Columns[0].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                }
                if (model != null)
                {
                    textBoxCount.Text = model.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void save_Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (model == null)
                {
                    if (dataGridViewComponents.SelectedRows.Count == 1)
                    {
                        ComponentViewModel element = service.GetElement(Convert.ToInt32(dataGridViewComponents.SelectedRows[0].Cells[0].Value));
                        model = new ComponentProductViewModel
                        {
                            ComponentId = element.Id,
                            ComponentName = element.Name,
                            Manuf = element.Manufacturer,
                            Brand = element.Brand,
                            ComponentRating = element.Rating,
                            Count = int.Parse(textBoxCount.Text)
                        };
                    }
                }
                else
                {
                    if (dataGridViewComponents.SelectedRows.Count == 1)
                    {

                        ComponentViewModel element = service.GetElement(Convert.ToInt32(dataGridViewComponents.SelectedRows[0].Cells[0].Value));
                        model = new ComponentProductViewModel
                        {
                            ComponentId = element.Id,
                            ComponentName = element.Name,
                            Manuf = element.Manufacturer,
                            Brand = element.Brand,
                            ComponentRating = element.Rating,
                            Count = int.Parse(textBoxCount.Text)
                        };
                    }
                    else MessageBox.Show("Выбрано больше 1 элемента", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }


        private void cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}