using DashboardTienda.Models;
using DashboardTienda.Properties;
using DashboardTienda.Services;
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
        }

        private void ThemeToggleMain(object sender, RoutedEventArgs e)
        {
            ThemeService.Instance.ToggleTheme();
        }

        
    }
}