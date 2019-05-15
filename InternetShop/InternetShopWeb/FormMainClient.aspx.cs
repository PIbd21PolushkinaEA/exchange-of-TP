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
using InternetShopServiceDAL;

namespace InternetShopWeb
{
    public partial class FormMainClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void ButtonMakeReservation_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {

        }
    }
}