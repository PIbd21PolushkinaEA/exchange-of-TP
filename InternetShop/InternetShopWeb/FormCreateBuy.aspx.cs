using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using InternetShopImplementations.Implementations;
using Unity;
using InternetShopWeb.App_Start;
using System.Drawing;

namespace InternetShopWeb
{
    public partial class FormCreateBuy : System.Web.UI.Page
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
                        ClientId = Int32.Parse(Session["ClientId"].ToString()),
                        NameBuy = "Введите название",
                        SumOfChoosedProducts = 0,
                        ProductsBasket = setPartBM
                    });
                    Session["id"] = service.GetList().Last().Id.ToString();
                    Session["Change"] = "0";
                }
            }

            int Sum = 0;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {

                Sum += Int32.Parse(dataGridView.Rows[i].Cells[1].Text);

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
                    //dataGridView.Columns[1].Visible = false;
                    //dataGridView.Columns[2].Visible = false;
                    //dataGridView.Columns[3].Visible = false;
                }
                textBoxPrice.Text = "0";
                foreach (var product in ProductsBasket)
                {
                    ProductViewModel productView = productService.GetElement(product.ProductId);
                    textBoxPrice.Text = (Convert.ToInt32(textBoxPrice.Text)+(productView.Price * product.Count)).ToString();
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
            if (dataGridView.SelectedIndex >= 0)
            {
                model = service.GetElement(id).ProductsBasket[dataGridView.SelectedIndex];
                Session["SEId"] = model.Id;
                Session["SEBasketId"] = model.BasketId;
                Session["SEProductId"] = model.ProductId;
                Session["SEProductName"] = model.ProductName;
                Session["SECount"] = model.Count;
                Session["SEIs"] = dataGridView.SelectedIndex;
                Session["Change"] = "0";
                Server.Transfer("FormProduct.aspx");
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedIndex >= 0)
            {
                try
                {
                    ProductsBasket.RemoveAt(dataGridView.SelectedIndex);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
                }
                LoadData();
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните название');</script>");
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните цену');</script>");
                return;
            }
            if (ProductsBasket == null || ProductsBasket.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните товары');</script>");
                return;
            }
            try
            {
                List<ProductBasketBindingModel> productBasketBM = new List<ProductBasketBindingModel>();
                for (int i = 0; i < ProductsBasket.Count; ++i)
                {
                    productBasketBM.Add(new ProductBasketBindingModel
                    {
                        Id = ProductsBasket[i].Id,
                        ProductId = ProductsBasket[i].ProductId,
                        BasketId = ProductsBasket[i].BasketId,
                        Count = ProductsBasket[i].Count
                    });
                }
                if (Int32.TryParse((string)Session["id"], out id))
                {
                    service.UpdElement(new BasketBindingModel
                    {
                        Id = id,
                        ClientId = Int32.Parse(Session["ClientId"].ToString()),
                        NameBuy = textBoxName.Text,
                        SumOfChoosedProducts = Convert.ToInt32(textBoxPrice.Text),
                        ProductsBasket = productBasketBM
                    });
                }
                else
                {
                    service.AddElement(new BasketBindingModel
                    {
                        ClientId = Int32.Parse(Session["ClientId"].ToString()),
                        NameBuy = textBoxName.Text,
                        SumOfChoosedProducts = Convert.ToInt32(textBoxPrice.Text),
                        ProductsBasket = productBasketBM
                    });
                }
                Session["id"] = null;
                Session["Change"] = null;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormMainClient.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (service.GetList().Count != 0 && service.GetList().Last().NameBuy == null)
            {
                service.DelElement(service.GetList().Last().Id);
            }
            if (!String.Equals(Session["Change"], null))
            {
                service.DelElement(id);
            }
            Session["id"] = null;
            Session["Change"] = null;
            Server.Transfer("FormMainClient.aspx");
        }
        protected void dataGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
    }
}