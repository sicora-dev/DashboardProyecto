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
using LiveCharts;

namespace DashboardTienda.Views
{
    /// <summary>
    /// Lógica de interacción para ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private readonly Api api;
        private List<Category> Categories;
        public ProductsView()
        {
            InitializeComponent();
            api = new Api();
            LoadCategories();
            LoadProducts();
        }

        private async void LoadCategories()
        {
            List<Category> CategoriesResult = await GetCategoriesAction();


            if (Categories == null)
            {
                Categories = new List<Category>();
            }
            foreach (var category in CategoriesResult)
            {
                Categories.Add(category);
            }
           
        }

        private async void LoadProducts()
        {
            if (Categories == null)
            {
                // Handle the null case, e.g., initialize or load categories
                LoadCategories();
            }
            List<Product> Products = await GetProductsAction();
            foreach (var product in Products)
            {
                var categoryName = Categories.FirstOrDefault(cat => cat.id == product.category_id)?.name;
                ProductCard productCard = new ProductCard();
                productCard.ProductName = product.name;
                productCard.Category = categoryName;
                productCard.Price = product.price;
                productCard.Image = product.img;
                productCard.MouseLeftButtonUp += (s, e) => ProductSelectionService.Instance.SelectedProduct = product;
                ProductsPanel.Children.Add(productCard);
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

        public void RefreshUsers(object sender, RoutedEventArgs e)
        {
            ProductsPanel.Children.Clear();
            LoadProducts();
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
                MessageBox.Show(result?.message ?? "Error al cargar los usuarios.");
                return new List<Product>();
            }
        }
    }
}
