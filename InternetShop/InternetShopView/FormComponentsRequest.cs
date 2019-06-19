using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace InternetShopView {
    public partial class FormComponentsRequest : Form {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IComponentService service;

        private ComponentsRequestViewModel model;

        public ComponentsRequestViewModel Model {
            set { model = value; }
            get { return model; }
        }
        public FormComponentsRequest(IComponentService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) && dataGridViewComponents.SelectedRows.Count == 1)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    ComponentViewModel element = service.GetElement(Convert.ToInt32(dataGridViewComponents.SelectedRows[0].Cells[0].Value));
                    model = new ComponentsRequestViewModel
                    {
                        ComponentId = element.Id,
                        ComponentName = element.Name,
                        Manufacterer = element.Manufacturer,
                        Brand = element.Brand,
                        Count = Convert.ToInt32(textBox1.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBox1.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormRequest_Load(object sender, System.EventArgs e)
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
                    textBox1.Text = model.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                textBox1.Text = model.Count.ToString();
            }
        }
    }
}
