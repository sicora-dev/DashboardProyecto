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
        public string searchText = SearchService.Instance.SearchText;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MainContent.Content = new HomeView();
            RightPanel.Content = new MainRightPanel();
            SearchService.Instance.PropertyChanged += OnSearchTextChanged;


        }

        private void Search(object sender, RoutedEventArgs e)
        {
            SearchService.Instance.SearchText = textBoxSearch.Text;
        }

        private void ThemeToggleMain(object sender, RoutedEventArgs e)
        {
            ThemeService.Instance.ToggleTheme();
        }

        private void ShowHome(object sender, RoutedEventArgs e)
        {
            SearchService.Instance.SearchText = "";
            textBoxSearch.Text = "";
            MainContent.Content = new HomeView();
            RightPanel.Content = new MainRightPanel();
        }

        private void ShowUsers(object sender, RoutedEventArgs e)
        {
            SearchService.Instance.SearchText = "";
            textBoxSearch.Text = "";
            MainContent.Content = new UsersView();
            RightPanel.Content = new UsersRightPanel();
        }

        private void ShowProducts(object sender, RoutedEventArgs e)
        {
            SearchService.Instance.SearchText = "";
            textBoxSearch.Text = "";
            MainContent.Content = new ProductsView();
            RightPanel.Content = new ProductsRightPanel();
        }

        private void ShowOrders(object sender, RoutedEventArgs e)
        {
            SearchService.Instance.SearchText = "";
            textBoxSearch.Text = "";
            MainContent.Content = new OrdersView();
            RightPanel.Content = new OrdersRightPanel();
        }

        private void ShowComments(object sender, RoutedEventArgs e)
        {
            SearchService.Instance.SearchText = "";
            textBoxSearch.Text = "";
            MainContent.Content = new CommentsView();
            RightPanel.Content = new CommentsRightPanel();
        }

        private void OnSearchTextChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchService.SearchText))
            {
                searchText = SearchService.Instance.SearchText;
            }
        }

    }
}