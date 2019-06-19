using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace InternetShopView {
    public partial class FormRequest : Form {

        [Dependency] public new IUnityContainer Container { get; set; }

        public int Id {
            set => id = value;
        }

        private readonly IRequestService service;

        private int? id;

        private List<ComponentsRequestViewModel> _requestComponents;

        public FormRequest(IRequestService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormComponentsRequest>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    _requestComponents.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormComponentsRequest>();
                form.Model = _requestComponents[dataGridViewRequests.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _requestComponents[dataGridViewRequests.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _requestComponents.RemoveAt(dataGridViewRequests.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormRequest_Load(object sender, EventArgs e)
        {
            _requestComponents = new List<ComponentsRequestViewModel>();
        }

        private void LoadData()
        {
            try
            {
                if (_requestComponents != null)
                {
                    dataGridViewRequests.DataSource = null;
                    dataGridViewRequests.DataSource = _requestComponents;
                    dataGridViewRequests.Columns[0].Visible = false;
                    dataGridViewRequests.Columns[1].Visible = false;
                    dataGridViewRequests.Columns[2].Visible = false;
                    dataGridViewRequests.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (_requestComponents == null || _requestComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите формат файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<RequestComponentBindingModel> requestComponentBindings = new List<RequestComponentBindingModel>();
                for (int i = 0; i < _requestComponents.Count; ++i)
                {
                    requestComponentBindings.Add(new RequestComponentBindingModel
                    {
                        Id = _requestComponents[i].Id,
                        RequestID = _requestComponents[i].RequestId,
                        ComponentID = _requestComponents[i].ComponentId,
                        CountComponents = _requestComponents[i].Count,
                        ComponentName = _requestComponents[i].ComponentName
                    });
                }
                service.CreateRequest(new RequestBindingModel
                {
                    Date = DateTime.Now,
                    RequestComponents = requestComponentBindings
                }, comboBox1.SelectedItem.ToString() == "doc");
                MessageBox.Show("Формирование заявок на брони прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}