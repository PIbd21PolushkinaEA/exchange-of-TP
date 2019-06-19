using System;
using System.Collections.Generic;
using System.Web.UI;
using InternetShopImplementations.Implementations;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using InternetShopWeb.App_Start;
using Unity;

namespace InternetShopWeb {
    public partial class FormProduct : Page {
        private readonly IProductService serviceS = UnityConfig.Container.Resolve<ProductServiceDB>();

        private ProductBasketViewModel model;

        protected void Page_Load(object sender, EventArgs e) {
            if ( !Page.IsPostBack ) {
                try {
                    List<ProductViewModel> list = serviceS.GetList();
                    if ( list != null ) {
                        DropDownListProduct.DataSource = list;
                        DropDownListProduct.DataValueField = "Id";
                        DropDownListProduct.DataTextField = "ProductName";
                        DropDownListProduct.SelectedIndex = -1;
                        Page.DataBind();
                    }
                }
                catch ( Exception ex ) {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                    "<script>alert('" + ex.Message + "');</script>");
                }
            }

            if ( Session["SEId"] != null ) {
                model = new ProductBasketViewModel {
                    ProductId = Convert.ToInt32(Session["SEProductId"]),
                    ProductName = Session["SEProductName"].ToString(),
                    Count = Convert.ToInt32(Session["SECount"].ToString())
                };
                DropDownListProduct.Enabled = false;
                DropDownListProduct.SelectedValue = Session["SEProductId"].ToString();
            }

            if ( (Session["SEId"] != null) && (!Page.IsPostBack) ) {
                TextBoxCount.Text = Session["SECount"].ToString();
            }
        }

        private void CalcSum() {
            if ( DropDownListProduct.SelectedValue != null && !string.IsNullOrEmpty(TextBoxCount.Text) ) {
                try {
                    int id = Convert.ToInt32(DropDownListProduct.SelectedValue);
                    ProductViewModel product = serviceS.GetElement(id);
                    int count = Convert.ToInt32(TextBoxCount.Text);
                    TextBoxSum.Text = (count * product.Price).ToString();

                }
                catch ( Exception ex ) {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                    "<script>alert('" + ex.Message + "');</script>");
                }
            }
        }

        protected void TextBoxCount_TextChanged(object sender, EventArgs e) {
            CalcSum();
        }

        protected void ButtonSave_Click(object sender, EventArgs e) {
            if ( string.IsNullOrEmpty(TextBoxCount.Text) ) {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                "<script>alert('Заполните поле Количество');</script>");
                return;
            }

            if ( DropDownListProduct.SelectedValue == null ) {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                "<script>alert('Выберите товар');</script>");
                return;
            }

            try {
                if ( Session["SEId"] == null ) {
                    model = new ProductBasketViewModel {
                        ProductId = Convert.ToInt32(DropDownListProduct.SelectedValue),
                        ProductName = DropDownListProduct.SelectedItem.Text,
                        Count = Convert.ToInt32(TextBoxCount.Text)
                    };
                    Session["SEId"] = model.Id;
                    Session["SEBasketId"] = model.BasketId;
                    Session["SEProductId"] = model.ProductId;
                    Session["SEProductName"] = model.ProductName;
                    Session["SECount"] = model.Count;
                }
                else {
                    model.Count = Convert.ToInt32(TextBoxCount.Text);
                    Session["SEId"] = model.Id;
                    Session["SEBasketId"] = model.BasketId;
                    Session["SEProductId"] = model.ProductId;
                    Session["SEProductName"] = model.ProductName;
                    Session["SECount"] = model.Count;
                    Session["Change"] = "1";
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                "<script>alert('Сохранение прошло успешно');</script>");
                Server.Transfer("FormCreateBuy.aspx");
            }
            catch ( Exception ex ) {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts",
                "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e) {
            Server.Transfer("FormCreateBuy.aspx");
        }
    }
}