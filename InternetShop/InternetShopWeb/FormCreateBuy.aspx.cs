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
using System.Drawing;

namespace InternetShopWeb
{
    public partial class FormCreateBuy : System.Web.UI.Page
    {
        private readonly IMainClientServise service = UnityConfig.Container.Resolve<MainClientServiceDB>();

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
                        ProductName = (string)Session["SEProductName"],
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
                    Count = this.ProductsBasket[i].Count
                });
            }
            if (setPartBM.Count != 0)
            {
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new BasketBindingModel
                    {
                        Id = id,
                        NameBuy = "Введите название",
                        SumOfChoosedProducts = 0,
                        ProductsBasket = setPartBM
                    });
                }
                else
                {
                    service.AddElement(new BasketBindingModel
                    {
                        NameBuy = "Введите название",
                        SumOfChoosedProducts = 0,
                        ProductsBasket = setPartBM
                    });
                    Session["id"] = service.GetList().Last().Id.ToString();
                    Session["Change"] = "0";
                }
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
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormProduct.aspx");
        }

        protected void ButtonChange_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {

        }
        protected void dataGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[3].Visible = false;
        }
    }
}