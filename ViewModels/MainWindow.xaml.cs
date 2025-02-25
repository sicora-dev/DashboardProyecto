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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LogedUser User => TokenService.Instance.User;
        public List<string> Meses { get; set; }
        private List<string> _categories;
        public List<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public SeriesCollection SeriesCollection { get; set; }

        public List<Order> Orders { get; set; }
        
        public ChartValues<int> Ventas2024 { get; set; }
        public ChartValues<int> Ventas2023 { get; set; }
        public ChartValues<int> ProductsPerCategory { get; set; }

        private readonly Api api;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
            api = new Api();
            LoadOrders();
            LoadCategories();
            
            Meses = new List<string> { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
        }


        private async void LoadOrders()
        {
            Orders = await GetOrdersAction();

            int[] ventas2024 = new int[12];
            int[] ventas2023 = new int[12];

            var orders2024 = Orders.Where(order => order.date.Year == 2024)
                                   .GroupBy(order => order.date.Month)
                                   .Select(group => new { Month = group.Key, Count = group.Count() })
                                   .OrderBy(group => group.Month)
                                   .ToList();

            var orders2023 = Orders.Where(order => order.date.Year == 2023)
                                   .GroupBy(order => order.date.Month)
                                   .Select(group => new { Month = group.Key, Count = group.Count() })
                                   .OrderBy(group => group.Month)
                                   .ToList();

            foreach (var order in orders2024)
            {
                ventas2024[order.Month - 1] = order.Count;
            }

            foreach (var order in orders2023)
            {
                ventas2023[order.Month - 1] = order.Count;
            }

            Ventas2024 = new ChartValues<int>(ventas2024);
            Ventas2023 = new ChartValues<int>(ventas2023);

            Ventas2024Graph.Values = Ventas2024;
            Ventas2023Graph.Values = Ventas2023;
        }

        private async void LoadCategories()
        {
            List<Category> CategoriesResult = await GetCategoriesAction();
            List<Product> ProductsResult = await GetProductsAction();
            int[] products_category = new int[CategoriesResult.Count];

            if (Categories == null)
            {
                Categories = new List<string>();
            }
            foreach (var category in CategoriesResult)
            {
                Categories.Add(category.name);
            }
            if (Categories.Count > 0)
            {
                foreach (var cat in CategoriesResult)
                {
                    var prods = ProductsResult.Count(p => p.category_id == cat.id);
                    products_category[CategoriesResult.IndexOf(cat)] = prods;
                }
            }
            ProductsPerCategory = new ChartValues<int>(products_category);
            ProductsPerCategoryGraph.Values = ProductsPerCategory;
        }
        private void ThemeToggleMain(object sender, RoutedEventArgs e)
        {
            ThemeService.Instance.ToggleTheme();
        }

        private async Task<List<Order>> GetOrdersAction()
        {
            var result = await api.GetOrders();

            if (result?.status == 200)
            {
                return result.orders;
            }
            else
            {
                MessageBox.Show(result?.message ?? "Error al cargar los pedidos.");
                return new List<Order>();
            }

        }

        private async Task<List<Category>> GetCategoriesAction()
        {
            var result = await api.GetCategories();
            if (result?.status == 200)
            {
                return result.categories;
            }
            else
            {
                MessageBox.Show(result?.message ?? "Error al cargar las categorías.");
                return new List<Category>();
            }
        }

        private async Task<List<Product>> GetProductsByCategoryAction(int category_id)
        {
            var result = await api.GetProductsByCategory(category_id);
            if (result?.status == 200)
            {
                return result.products;
            }
            else
            {
                MessageBox.Show(result?.message ?? "Error al cargar las categorías.");
                return new List<Product>();
            }
        }

        private async Task<List<Product>> GetProductsAction()
        {
            var result = await api.GetProducts();
            if (result?.status == 200)
            {
                return result.products;
            }
            else
            {
                MessageBox.Show(result?.message ?? "Error al cargar las categorías.");
                return new List<Product>();
            }
        }

        

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}