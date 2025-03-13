using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private List<User> Users;
        public UsersView()
        {
            InitializeComponent();
            api = new Api();
            LoadUsers();
            SearchService.Instance.PropertyChanged += OnSearchTextChanged;
        }

        private async void LoadUsers()
        {
            if (Users == null)
            {
                Users = await GetUsersAction();
            }
            UsersPanel.Children.Clear();
            foreach (var user in Users)
            {
              
                if(SearchService.Instance.SearchText != null)
                {
                    if (user.name.ToLower().Contains(SearchService.Instance.SearchText.ToLower()))
                    {
                        UserCard userCard = new UserCard();
                        userCard.UserName = user.name;
                        userCard.Mail = user.mail;
                        userCard.Role = user.role;
                        userCard.MouseLeftButtonUp += (s, e) => UserSelectionService.Instance.SelectedUser = user;
                        UsersPanel.Children.Add(userCard);
                    }
                }
                else
                {
                    UserCard userCard = new UserCard();
                    userCard.UserName = user.name;
                    userCard.Mail = user.mail;
                    userCard.Role = user.role;
                    userCard.MouseLeftButtonUp += (s, e) => UserSelectionService.Instance.SelectedUser = user;
                    UsersPanel.Children.Add(userCard);
                }
            }

        }

        public async void RefreshUsers(object sender, RoutedEventArgs e)
        {
            UsersPanel.Children.Clear();
            Users = await GetUsersAction();
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

        private void OnSearchTextChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchService.SearchText))
            {
                LoadUsers();
            }
        }
    }
}

