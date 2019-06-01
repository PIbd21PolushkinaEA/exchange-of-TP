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
    public partial class FormProducts : System.Web.UI.Page
    {
        private readonly IProductService service = UnityConfig.Container.Resolve<ProductServiceDB>();

        private readonly IComponentService serviceC = UnityConfig.Container.Resolve<ComponentServiceDB>();

        List<ProductViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    List<ComponentViewModel> list = serviceC.GetList();
                    if (list != null)
                    {
                        DropDownListBrand.DataSource = list;
                        DropDownListBrand.DataValueField = "Id";
                        DropDownListBrand.DataTextField = "Brand";
                        DropDownListBrand.SelectedIndex = -1;
                        Page.DataBind();
                    }

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                list = service.GetList();
                dataGridView.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }
        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("FormProducts.aspx");
        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormMainClient.aspx");
        }

        protected void Buttonfilter_Click(object sender, EventArgs e)
        {
            String brand = DropDownListBrand.SelectedValue;

        }
    }
}