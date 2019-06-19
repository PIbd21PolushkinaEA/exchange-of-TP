using System;
using System.Collections.Generic;
using System.Web.UI;
using InternetShopImplementations.Implementations;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using InternetShopWeb.App_Start;
using Unity;

namespace InternetShopWeb {
    public partial class FormProducts : Page {
        private readonly IProductService service = UnityConfig.Container.Resolve<ProductServiceDB>();

        List<ProductViewModel> list;

        protected void Page_Load(object sender, EventArgs e) {
            LoadData();
        }

        private void LoadData() {
            try {
                list = service.GetList();
                dataGridView.Columns[0].Visible = false;
            }
            catch ( Exception ex ) {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e) {
            LoadData();
            Server.Transfer("FormProducts.aspx");
        }

        protected void ButtonBack_Click(object sender, EventArgs e) {
            Server.Transfer("FormMainClient.aspx");
        }
    }
}