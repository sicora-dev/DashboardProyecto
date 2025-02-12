using DashboardTienda.Properties;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ThemeToggle_Checked(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).ApplyTheme(true);
        }

        private void ThemeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).ApplyTheme(false);
            
        }
    }
}