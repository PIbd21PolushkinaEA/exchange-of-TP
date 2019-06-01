using System;
using System.Collections.Generic;
using System.Web.UI;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using InternetShopImplementations.Implementations;
using Unity;
using InternetShopWeb.App_Start;
using System.Drawing;
using InternetShopServiceDAL.BindingModels;

namespace InternetShopWeb
{
    public partial class FormMainClient : System.Web.UI.Page
    {
        private readonly IMainClientServise service = UnityConfig.Container.Resolve<MainClientServiceDB>();

        private readonly IReportService serviceR = UnityConfig.Container.Resolve<ReportServiceDB>();

        List<BasketViewModel> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                list = service.GetListBuy(Convert.ToInt32(Session["ClientId"]));
                dataGridView1.DataSource = list;
                dataGridView1.DataBind();
                dataGridView1.ShowHeaderWhenEmpty = true;
                dataGridView1.SelectedRowStyle.BackColor = Color.Silver;
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
            try
            {
                string index = list[dataGridView1.SelectedIndex].Id.ToString();
                Session["id"] = index;
                Server.Transfer("FormBuy.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonMakeReservation_Click(object sender, EventArgs e)
        {
            try
            {
                service.MakeReservation(list[dataGridView1.SelectedIndex].Id);
                string path = "C:\\Users\\Евгения\\Desktop\\PatientTreatment.xls";
                serviceR.SaveClientBaskets(new ReportBindingModel
                {
                    FileName = path,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now
                }, Convert.ToInt32(Session["ClientId"]));
                service.SendEmail(Session["Email"].ToString(), "Оповещение по резервированию", "Резервирование выполнено", path);
                LoadData();
                Server.Transfer("FormMainClient.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
            Server.Transfer("FormMainClient.aspx");
        }
    }
}