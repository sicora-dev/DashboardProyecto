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
using System.Xml.Linq;
using DashboardTienda.Models;
using DashboardTienda.Services;
using MahApps.Metro.IconPacks;
using Sprache;

namespace DashboardTienda.Views
{
    /// <summary>
    /// Lógica de interacción para OrdersRightPanel.xaml
    /// </summary>
    public partial class OrdersRightPanel : UserControl
    {
        private List<Product> Products;
        private readonly Api api;
        private List<OrderItem> newOrderItems = new List<OrderItem>();
        public OrdersRightPanel()
        {
            InitializeComponent();
            DataContext = OrderSelectionService.Instance;
            OrderSelectionService.Instance.PropertyChanged += OnOrderSelectionChanged;
            api = new Api();
            LoadOrders();

        }

        public async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OrderSelectionService.Instance.SelectedOrder;
            if (selectedOrder == null)
            {
                MessageBox.Show("No hay una orden seleccionada.");
                return;
            }
            selectedOrder.items.AddRange(newOrderItems);

            var updatedOrder = new Order
            {
                _id = OrderSelectionService.Instance.SelectedOrder._id,
                user_id = OrderSelectionService.Instance.SelectedOrder.user_id,
                payment_method = OrderSelectionService.Instance.SelectedOrder.payment_method,
                date = OrderSelectionService.Instance.SelectedOrder.date,
                items = OrderSelectionService.Instance.SelectedOrder.items,
                total_amount = OrderSelectionService.Instance.SelectedOrder.total_amount,
            };
            var result = await api.UpdateOrder(updatedOrder);
            MessageBox.Show(result?.message);
            newOrderItems.Clear();

            foreach (var item in FormOrders.Children.OfType<UIElement>().ToList())
            {
                if (item is FrameworkElement frameworkElement && frameworkElement.Name != "OriginalFields")
                {
                    FormOrders.Children.Remove(item);         
                }
            }
            


        }

        public async void OnAddProduct(object sender, RoutedEventArgs e)
        {
            var selectedOrder = OrderSelectionService.Instance.SelectedOrder;
            if (selectedOrder == null)
            {
                MessageBox.Show("No hay una orden seleccionada.");
                return;
            }

            var newItem = new OrderItem
            {
                product_id = "",
                quantity = 0,
                price = 0,
                size = ""
            };

            newOrderItems.Add(newItem);

            var productPanel = new StackPanel { Orientation = Orientation.Vertical, VerticalAlignment = VerticalAlignment.Center };

            var newProductTextBlock = new TextBlock
            {
                Text = "Nuevo Producto",
                Style = (Style)FindResource("menuTitle")
            };
            productPanel.Children.Add(newProductTextBlock);

            var productIdTextBlock = new TextBlock
            {
                Text = "Id del producto",
                Style = (Style)FindResource("menuTitle")
            };
            productPanel.Children.Add(productIdTextBlock);

            var productIdTextBox = new TextBox
            {
                Text = newItem.product_id,
                Width = 200,
                Style = (Style)FindResource("searchTextBox")
            };
            productIdTextBox.TextChanged += (s, args) => newItem.product_id = productIdTextBox.Text;
            productPanel.Children.Add(productIdTextBox);

            var quantityTextBlock = new TextBlock
            {
                Text = "Cantidad",
                Style = (Style)FindResource("menuTitle")
            };
            productPanel.Children.Add(quantityTextBlock);

            var quantityTextBox = new TextBox
            {
                Text = newItem.quantity.ToString(),
                Width = 200,
                Style = (Style)FindResource("searchTextBox")
            };
            quantityTextBox.TextChanged += (s, args) =>
            {
                if (int.TryParse(quantityTextBox.Text, out int quantity))
                {
                    newItem.quantity = quantity;
                }
                else
                {
                    newItem.quantity = 0;
                }
            };
            productPanel.Children.Add(quantityTextBox);

            var priceTextBlock = new TextBlock
            {
                Text = "Precio",
                Style = (Style)FindResource("menuTitle")
            };
            productPanel.Children.Add(priceTextBlock);

            var priceTextBox = new TextBox
            {
                Text = newItem.price.ToString(),
                Width = 200,
                Style = (Style)FindResource("searchTextBox")
            };
            priceTextBox.TextChanged += (s, args) =>
            {
                if (int.TryParse(priceTextBox.Text, out int price))
                {
                    newItem.price = price;
                }
                else
                {
                    newItem.price = 0;
                }
            };
            productPanel.Children.Add(priceTextBox);

            var sizeTextBlock = new TextBlock
            {
                Text = "Talla",
                Style = (Style)FindResource("menuTitle")
            };
            productPanel.Children.Add(sizeTextBlock);

            var sizeTextBox = new TextBox
            {
                Text = newItem.size,
                Width = 200,
                Style = (Style)FindResource("searchTextBox")
            };
            sizeTextBox.TextChanged += (s, args) => newItem.size = sizeTextBox.Text;
            productPanel.Children.Add(sizeTextBox);

            FormOrders.Children.Add(productPanel);
           


        }

        private async void LoadOrders()
        {
            if (Products == null)
            {

                var result = await api.GetProducts();
                if (result?.status != 200)
                {

                    MessageBox.Show(result?.message ?? "Error al cargar los pedidos.");
                }
                else
                {
                    Products = result.products;
                }

            }
            else
            {

                var selectedOrder = OrderSelectionService.Instance.SelectedOrder;
                if (selectedOrder?.items != null)
                {
                    foreach (var item in selectedOrder.items)
                    {
                        var product = Products.FirstOrDefault(p => p._id == item.product_id);
                        if (product != null)
                        {
                            item.product_name = product.name;
                        }
                    }
                }
            }
        }

        private void OnOrderSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderSelectionService.SelectedOrder))
            {
                LoadOrders();
            }
        }
    }
}
