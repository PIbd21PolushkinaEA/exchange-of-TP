using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using InternetShopImplementations.Implementations;
using Unity;
using InternetShopWeb.App_Start;
using System.Drawing;
using System.Web.UI.WebControls;

namespace InternetShopWeb
{
    public partial class FormBuy : System.Web.UI.Page
    {
        private readonly IMainClientServise service = UnityConfig.Container.Resolve<MainClientServiceDB>();

        private readonly IProductService productService = UnityConfig.Container.Resolve<ProductServiceDB>();

        private int id;

        private List<ProductBasketViewModel> ProductsBasket;

        private ProductBasketViewModel model;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Int32.TryParse((string)Session["id"], out id))
            {
                try
                {
                    BasketViewModel view = service.GetElement(id);
                    if (view != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            textBoxName.Text = view.NameBuy;
                            textBoxPrice.Text = view.SumOfChoosedProducts.ToString();
                        }
                        this.ProductsBasket = view.ProductsBasket;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                this.ProductsBasket = new List<ProductBasketViewModel>();
            }
            if (Session["SEId"] != null)
            {
                if ((Session["SEIs"] != null) && (Session["Change"].ToString() != "0"))
                {
                    model = new ProductBasketViewModel
                    {
                        Id = (int)Session["SEId"],
                        BasketId = (int)Session["SEBasketId"],
                        ProductId = (int)Session["SEProductId"],
                        ProductName = (string)Session["SEProductName"],
                        Count = (int)Session["SECount"]
                    };

                    this.ProductsBasket[(int)Session["SEIs"]] = model;
                    Session["Change"] = "0";
                }
                else
                {
                    model = new ProductBasketViewModel
                    {
                        BasketId = (int)Session["SEBasketId"],
                        ProductId = (int)Session["SEProductId"],
                        ProductName = Session["SEProductName"].ToString(),
                        Count = (int)Session["SECount"]
                    };
                    this.ProductsBasket.Add(model);
                }
                Session["SEId"] = null;
                Session["SEBasketId"] = null;
                Session["SEProductId"] = null;
                Session["SEProductName"] = null;
                Session["SECount"] = null;
                Session["SEIs"] = null;
            }
            List<ProductBasketBindingModel> setPartBM = new List<ProductBasketBindingModel>();
            for (int i = 0; i < this.ProductsBasket.Count; ++i)
            {
                setPartBM.Add(new ProductBasketBindingModel
                {
                    Id = this.ProductsBasket[i].Id,
                    BasketId = this.ProductsBasket[i].BasketId,
                    ProductId = this.ProductsBasket[i].ProductId,
                    ProductName = this.ProductsBasket[i].ProductName,
                    Count = this.ProductsBasket[i].Count
                });
            }
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                if (ProductsBasket != null)
                {
                    dataGridView.DataBind();
                    dataGridView.DataSource = ProductsBasket;
                    dataGridView.DataBind();
                    dataGridView.ShowHeaderWhenEmpty = true;
                    dataGridView.SelectedRowStyle.BackColor = Color.Silver;
                }
                textBoxPrice.Text = "0";
                foreach (var product in ProductsBasket)
                {
                    ProductViewModel productView = productService.GetElement(product.ProductId);
                    textBoxPrice.Text = (Convert.ToInt32(textBoxPrice.Text) + (productView.Price * product.Count)).ToString();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }
        protected void dataGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormMainClient.aspx");
        }
    }
}