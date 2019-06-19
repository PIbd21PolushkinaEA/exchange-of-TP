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

        private List<RequestComponentViewModel> _componentsProduct;

        public FormRequest(IRequestService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void buttonRef_Click(object sender, EventArgs e)
        {

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}