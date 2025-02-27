using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DashboardTienda.Models;
using DashboardTienda.Services;

namespace DashboardTienda.Views
{
    /// <summary>
    /// Lógica de interacción para UsersRightPanel.xaml
    /// </summary>
    public partial class UsersRightPanel : UserControl
    {
        public string SelectedName { get; set; }
        private readonly Api api;
        public UsersRightPanel()
        {
            InitializeComponent();
            DataContext = UserSelectionService.Instance;
            api = new Api();
            LoadUser();
        }

        public void LoadUser()
        {
            if (UserSelectionService.Instance.SelectedUser != null)
            {
                txtNombre.Text = UserSelectionService.Instance.SelectedUser.name;
                txtMail.Text = UserSelectionService.Instance.SelectedUser.mail;
                txtCiudad.Text = UserSelectionService.Instance.SelectedUser.city_id.ToString();
                txtPais.Text = UserSelectionService.Instance.SelectedUser.country_id.ToString();
                txtRol.Text = UserSelectionService.Instance.SelectedUser.role;
            }

        }

        public async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var originalMail = UserSelectionService.Instance.SelectedUser.mail;
            var updatedUser = new User
            {
                _id = UserSelectionService.Instance.SelectedUser._id,
                name = txtNombre.Text,
                mail = txtMail.Text,
                city_id = int.Parse(txtCiudad.Text),
                country_id = int.Parse(txtPais.Text),
                blocked = UserSelectionService.Instance.SelectedUser.blocked,
                role = txtRol.Text
            };
            var result = await api.UpdateUser(originalMail, updatedUser);
            MessageBox.Show(result?.message);
            
            
        }
    }
}
