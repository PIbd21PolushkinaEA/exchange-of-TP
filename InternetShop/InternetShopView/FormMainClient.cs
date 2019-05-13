using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Unity;

namespace InternetShopView
{
    public partial class FormMainClient : Form
    {
        private readonly IMainClientServise service;

        private ClientViewModel client;

        [Dependency]
        public new IUnityContainer Container { get; set; }

        public FormMainClient(ClientViewModel client, IMainClientServise service)
        {
            InitializeComponent();
            this.client = client;
            this.service = service;
        }
        private void FormMainClient_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            try
            {
                List<ProductViewModel> list = service.GetList();
                list.Add(new ProductViewModel
                {
                    Id = 1,
                    ProductName = "Some computer",
                    Price = 10000
                });
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                service.AddToBasket(new ProductBindingModel
                {
                    Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value),
                    ProductName = dataGridView.SelectedRows[0].Cells[1].Value.ToString(),
                    Price = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[2].Value)
                });
                MessageBox.Show("Товар успешно добавлен в корзину", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonMyBasket_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormBasket>();
            form.Show();
        }
    }
}
