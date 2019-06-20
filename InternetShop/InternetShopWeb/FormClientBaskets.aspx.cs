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
using Microsoft.Reporting.WebForms;

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
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                               "c " + Calendar1.SelectedDate.ToShortDateString() +
                                               " по " + Calendar2.SelectedDate.ToShortDateString());
                ReportViewer.LocalReport.SetParameters(parameter);

                var dataSource = serviceR.GetBaskets(new ReportBindingModel
                {
                    DateFrom = Calendar1.SelectedDate,
                    DateTo = Calendar2.SelectedDate
                }, Convert.ToInt32(Session["ClientId"]));

                ReportDataSource source = new ReportDataSource("DataSet", dataSource);
                ReportViewer.LocalReport.DataSources.Add(source);
                ReportViewer.DataBind();

                string path = "C:\\Users\\Евгения\\Desktop\\Baskets.pdf";
                serviceR.SaveBaskets(new ReportBindingModel
                {
                    FileName = path,
                    DateFrom = Calendar1.SelectedDate,
                    DateTo = Calendar2.SelectedDate
                }, Convert.ToInt32(Session["ClientId"]));
                service.SendEmail(Session["Email"].ToString(), "Покупка клиента", "", path);
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