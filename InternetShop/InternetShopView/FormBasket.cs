using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InternetShopImplementations;
using InternetShopServiceDAL;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using Unity;

namespace InternetShopView
{
    public partial class FormBasket : Form
    {
        private readonly IBasketService service;

        //private ClientViewModel client;
        private BasketViewModel basket;

        [Dependency]
        public new IUnityContainer Container { get; set; }

        public FormBasket(BasketViewModel basket, IBasketService service)
        {
            InitializeComponent();
            this.service = service;
            this.basket = basket;
        }
        private void FormBasket_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            try
            {
                List<ProductBasketViewModel> list = service.GetList(basket.Id);

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
        private void buttonDel_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {

        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonBuy_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReport>();
            form.Show();
        }
        private void buttonReserve_Click(object sender, EventArgs e)
        {

        }

        private void buttonReport_Click(object sender, EventArgs e)
        {

        }
    }
}
