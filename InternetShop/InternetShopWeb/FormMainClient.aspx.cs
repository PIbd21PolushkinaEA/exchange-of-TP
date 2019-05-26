using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using InternetShopImplementations.Implementations;
using Unity;
using InternetShopWeb.App_Start;

namespace InternetShopWeb
{
    public partial class FormMainClient : System.Web.UI.Page
    {
        private readonly IMainClientServise service = UnityConfig.Container.Resolve<MainClientServiceDB>();

        List<BasketViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                list = service.GetList();
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonProduct_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormProducts.aspx");
        }

        protected void ButtonReport_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonCreateBuy_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormCreateBuy.aspx");
        }

        protected void ButtonUpdBuy_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedIndex >= 0)
            {
                string index = list[dataGridView1.SelectedIndex].Id.ToString();
                Session["id"] = index;
                Server.Transfer("FormCreateBuy.aspx");
            }
        }

        protected void ButtonMakeReservation_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("FormMainClient.aspx");
        }
    }
}