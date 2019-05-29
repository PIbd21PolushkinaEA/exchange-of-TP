using InternetShopServiceDAL.Interfaces;
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

namespace InternetShopView
{
    public partial class FormAdminMain : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private IRequestService service;
        public FormAdminMain(IRequestService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void AddComponent_Button_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormComponents>();
            form.ShowDialog();
        }

        private void Add_button_Click(object sender, EventArgs e)
        {

        }

        private void Update_button_Click(object sender, EventArgs e)
        {

        }

        private void Delete_button_Click(object sender, EventArgs e)
        {

        }
    }
}
