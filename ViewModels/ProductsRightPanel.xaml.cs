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

namespace DashboardTienda.Views
{
    /// <summary>
    /// Lógica de interacción para ProductsRightPanel.xaml
    /// </summary>
    public partial class ProductsRightPanel : UserControl
    {
        public string SelectedName { get; set; }
        private readonly Api api;
        public ProductsRightPanel()
        {
            InitializeComponent();
            DataContext = ProductSelectionService.Instance;
            api = new Api();
            LoadProduct();
        }

        public void LoadProduct()
        {
            if (ProductSelectionService.Instance.SelectedProduct != null)
            {
                txtNombre.Text = ProductSelectionService.Instance.SelectedProduct.name;
                txtDesc.Text = ProductSelectionService.Instance.SelectedProduct.description;
                txtImage.Text = ProductSelectionService.Instance.SelectedProduct.img;
                txtPrice.Text = ProductSelectionService.Instance.SelectedProduct.price.ToString();
                txtCategory.Text = ProductSelectionService.Instance.SelectedProduct.category_id.ToString();
            }

        }


        public async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var updatedProduct = new Product
            {
                _id = ProductSelectionService.Instance.SelectedProduct._id,
                name = txtNombre.Text,
                description = txtDesc.Text,
                img = txtImage.Text,
                price = int.Parse(txtPrice.Text),
                category_id = int.Parse(txtCategory.Text),
                sizes = ProductSelectionService.Instance.SelectedProduct.sizes


             
            };
            var result = await api.UpdateProduct(updatedProduct);
            MessageBox.Show(result?.message);


        }
    }
}
