using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using InternetShopImplementations.Implementations;
using Unity;
using InternetShopWeb.App_Start;
using System.Drawing;
using InternetShopServiceDAL.BindingModels;

namespace InternetShopWeb
{
    public partial class FormClientBaskets : System.Web.UI.Page
    {
        private readonly IMainClientServise service = UnityConfig.Container.Resolve<MainClientServiceDB>();

        private readonly IReportService serviceR = UnityConfig.Container.Resolve<ReportServiceDB>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonMake_Click(object sender, EventArgs e)
        {
            if (Calendar1.SelectedDate >= Calendar2.SelectedDate)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllertDate", "<script>alert('Дата начала должна быть меньше даты окончания');</script>");
                return;
            }
            try
            {
                string path = "C:\\Users\\Евгения\\Desktop\\ClientAllBasket.pdf";
                serviceR.SaveClientAllBaskets(new ReportBindingModel
                {
                    FileName = path,
                    DateFrom = Calendar1.SelectedDate,
                    DateTo = Calendar2.SelectedDate
                }, Convert.ToInt32(Session["ClientId"]));
                service.SendEmail(Session["Email"].ToString(), "Покупки клиента", "", path);
                Server.Transfer("FormMainClient.aspx");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScriptAllert", "<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormMainClient.aspx");
        }
    }
}