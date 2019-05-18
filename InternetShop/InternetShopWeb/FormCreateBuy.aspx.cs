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
    public partial class FormProduct : System.Web.UI.Page
    {
        private readonly IProductService serviceS = UnityConfig.Container.Resolve<ProductServiceDB>();

        private readonly IMainClientServise serviceM = UnityConfig.Container.Resolve<MainClientServiceDB>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    List<ProductViewModel> listP = serviceS.GetList();
                    if (listP != null)
                    {
                        DropDownListProduct.DataSource = listP;
                        DropDownListProduct.DataBind();
                        DropDownListProduct.DataTextField = "ProductName";
                        DropDownListProduct.DataValueField = "Id";
                    }
                    Page.DataBind();

                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }
        private void CalcSum()
        {
            if (DropDownListProduct.SelectedValue != null && !string.IsNullOrEmpty(TextBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(DropDownListProduct.SelectedValue);
                    ProductViewModel product = serviceS.GetElement(id);
                    int count = Convert.ToInt32(TextBoxCount.Text);
                    TextBoxSum.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCount.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните поле Количество');</script>");
                return;
            }
            
            if (DropDownListProduct.SelectedValue == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Выберите товар');</script>");
                return;
            }
            try
            {
                serviceM.AddToBasket(new BasketBindingModel
                {
                    ClientId = Convert.ToInt32(DropDownListProduct.SelectedValue),
                    CountOfChoosedProducts = Convert.ToInt32(TextBoxCount.Text),
                    SumOfChoosedProducts = Convert.ToInt32(TextBoxSum.Text)
                });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormMain.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormMainClient.aspx");
        }
    }
}