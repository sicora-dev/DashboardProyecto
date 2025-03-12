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
using DashboardTienda.UserControls;

namespace DashboardTienda.Views
{
    /// <summary>
    /// Lógica de interacción para OrdersView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {
        private readonly Api api;
        private List<Order> Orders;
        public OrdersView()
        {
            InitializeComponent();
            api = new Api();
            LoadOrders();
            SearchService.Instance.PropertyChanged += OnSearchTextChanged;
        }

        private async void LoadOrders()
        {
            if (Orders == null)
            {
                Orders = await GetOrdersAction();
            }
            OrdersPanel.Children.Clear();
            foreach (var order in Orders)
            {
                if (SearchService.Instance.SearchText != null)
                {
                    if(order._id.Contains(SearchService.Instance.SearchText))
                    {
                        OrderCard orderCard = new OrderCard();
                        orderCard.Id = order._id;
                        orderCard.Date = order.date.ToString("yyyy-MM-dd");
                        orderCard.TotalAmount = order.total_amount.ToString();
                        orderCard.MouseLeftButtonUp += (s, e) => OrderSelectionService.Instance.SelectedOrder = order;
                        OrdersPanel.Children.Add(orderCard);
                    }
                }
                else
                {
                    OrderCard orderCard = new OrderCard();
                    orderCard.Id = order._id;
                    orderCard.Date = order.date.ToString("yyyy-MM-dd");
                    orderCard.TotalAmount = order.total_amount.ToString();
                    orderCard.MouseLeftButtonUp += (s, e) => OrderSelectionService.Instance.SelectedOrder = order;
                    OrdersPanel.Children.Add(orderCard);
                }
            }

        }

        public async void RefreshOrders(object sender, RoutedEventArgs e)
        {
            OrdersPanel.Children.Clear();
            Orders = await GetOrdersAction();
            LoadOrders();
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
                MessageBox.Show(result?.message ?? "Error al cargar las orders.");
                return new List<Order>();
            }
        }

        private void OnSearchTextChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchService.SearchText))
            {
                LoadOrders();
            }
        }
    }
}
