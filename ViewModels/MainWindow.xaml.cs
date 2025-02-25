using DashboardTienda.Models;
using DashboardTienda.Properties;
using DashboardTienda.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using DashboardTienda.Views;

namespace DashboardTienda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public LogedUser User => TokenService.Instance.User;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MainContent.Content = new HomeView();
            RightPanel.Content = new MainRightPanel();


        }

        private void ThemeToggleMain(object sender, RoutedEventArgs e)
        {
            ThemeService.Instance.ToggleTheme();
        }

        private void ShowHome(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new HomeView();
        }

        private void ShowUsers(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UsersView();
        }

        private void ShowProducts(object sender, RoutedEventArgs e)
        {
            // Implementar la vista de Productos si es necesario
        }

        private void ShowOrders(object sender, RoutedEventArgs e)
        {
            // Implementar la vista de Pedidos si es necesario
        }

    }
}