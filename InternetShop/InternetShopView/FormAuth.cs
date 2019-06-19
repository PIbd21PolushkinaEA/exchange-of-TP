using System;
using System.Windows.Forms;
using Unity;

namespace InternetShopView {
    public partial class FormAuth : Form {
        [Dependency] public new IUnityContainer Container { get; set; }

        public FormAuth() {
            InitializeComponent();
        }

        private void ButtonEnter_Click(object sender, EventArgs e) {
            if ( textBoxPass.Text == "123" ) {
                var form = Container.Resolve<FormAdminMain>();
                Hide();
                form.Show();
            }
            else {
                MessageBox.Show("Неправильный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}