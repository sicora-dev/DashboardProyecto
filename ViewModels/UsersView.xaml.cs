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
using DashboardTienda.UserControls;

namespace DashboardTienda.Views
{
    public partial class UsersView : UserControl
    {
        private readonly Api api;
        public UsersView()
        {
            InitializeComponent();
            api = new Api();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            List<User> Users = await GetUsersAction();
            foreach (var user in Users)
            {
                UserCard userCard = new UserCard();
                userCard.UserName = user.name;
                userCard.Mail = user.mail;
                userCard.Role = user.role;
                userCard.MouseLeftButtonUp += (s, e) => UserSelectionService.Instance.SelectedUser = user;
                UsersPanel.Children.Add(userCard);
            }

        }

        public void RefreshUsers(object sender, RoutedEventArgs e)
        {
            UsersPanel.Children.Clear();
            LoadUsers();
        }
        private async Task<List<User>> GetUsersAction()
        {
            var result = await api.GetUsers();

            if (result?.status == 200)
            {
                return result.users;
            }
            else
            {
                MessageBox.Show(result?.message ?? "Error al cargar los usuarios.");
                return new List<User>();
            }
        }
    }
}

