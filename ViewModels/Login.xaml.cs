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
using System.Windows.Shapes;
using DashboardTienda.Services;
using DashboardTienda.Models;

namespace DashboardTienda
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly Api api;
        public Login()
        {
            InitializeComponent();
            api = new Api();
        }

        private async void LoginButton(object sender, RoutedEventArgs e)
        {
            string mail = MailBox.Text;
            string pass = PassBox.Password;

            if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Por favor, ingrese su correo electrónico y contraseña.");
                return;
            }

            var user = new UserLogin { mail = mail, password = pass };
            var result = await api.Login(user);

            if (result?.status == 200)
            {
                TokenService.Instance.Token = result.token;
                TokenService.Instance.DecodeToken();
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
                
            }
            else
            {
                MessageBox.Show(result?.message ?? "Error en el inicio de sesión.");
            }

        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassBox.Password.Length > 0)
            {
                PasswordTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void ThemeToggleLogin(object sender, RoutedEventArgs e)
        {
            ThemeService.Instance.ToggleTheme();
        }

    }
}
