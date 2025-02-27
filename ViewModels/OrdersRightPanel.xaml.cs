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


        }

        private async void LoadOrders()
        {
            if (Products == null)
            {

                var result = await api.GetProducts();
                if (result?.status != 200)
                {

                    MessageBox.Show(result?.message ?? "Error al cargar los productos.");
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
