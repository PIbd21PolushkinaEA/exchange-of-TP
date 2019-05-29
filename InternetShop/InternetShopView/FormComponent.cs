using InternetShopServiceDAL.BindingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InternetShopImplementations.Implementations;
using Unity;
using InternetShopServiceDAL.Interfaces;

namespace InternetShopView
{
    public partial class FormComponent : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IComponentService service;

        private int? id;


        public FormComponent(IComponentService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            ComponentBindingModel model = new ComponentBindingModel()
            {
                Name = NameBox.Text,
                Brand = BrandBox.Text,
                Manufacturer = ManufBox.Text,
                Rating = float.Parse(RatingBox.Text),
                Price = Convert.ToInt32(PriceBox.Text)
            };
            service.AddElement(model);
        }
    }
}
