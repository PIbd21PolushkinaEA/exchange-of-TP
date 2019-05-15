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
    public partial class FormClient : System.Web.UI.Page
    {
        private readonly IClientService service = UnityConfig.Container.Resolve<ClientServiceDB>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegistrationButton_Click(object sender, EventArgs e)
        {
            try
            {
                String name = textBoxName.Text;
                String email = textBoxEmail.Text;
                String password = textBoxPassword.Text;

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    service.AddElement(new ClientBindingModel
                    {
                        Name = name,
                        Email = email,
                        Password = password
                    });
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Регистрация прошла успешно');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните все поля');</script>");
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Есть клиент с такой почтой');</script>");
            }
        }

        protected void SignInButton_Click(object sender, EventArgs e)
        {
            String email = textBoxEmail.Text;
            String password = textBoxPassword.Text;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                List<ClientViewModel> clients = service.GetList();
                foreach (ClientViewModel client in clients)
                {
                    if (client.Email.Equals(email) && client.Password.Equals(password))
                    {
                        Server.Transfer("FormClient.aspx");//MainClient
                    }
                    else Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Нет такого пользователя');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Заполните все поля');</script>");
            }
        }
    }
}