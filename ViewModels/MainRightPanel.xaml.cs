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
using LiveCharts;

namespace DashboardTienda.Views
{
    public partial class MainRightPanel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
        public ChartValues<int> ProductsPerCategory { get; set; }
        private readonly Api api;
        public MainRightPanel()
        {
            InitializeComponent();
            DataContext = this;
            api = new Api();
            LoadCategories();
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
