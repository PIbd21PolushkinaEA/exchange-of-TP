using InternetShopImplementations;
using InternetShopServiceDAL;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace InternetShopView
{
    public partial class FormClient : Form
    {
        private readonly IClientService service;

        [Dependency]
        public new IUnityContainer Container { get; set; }
        

        public FormClient(IClientService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void SignInButton_Click(object sender, EventArgs e)
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
                        var form = Container.Resolve<FormMainClient>();
                        form.Show();
                    }
                    else MessageBox.Show("Нет такого пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientRegistration_Button_Click(object sender, EventArgs e)
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
                MessageBox.Show("Регистация прошла успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
