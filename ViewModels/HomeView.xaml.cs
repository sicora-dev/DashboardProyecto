using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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
    public partial class HomeView : UserControl, INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<string> Meses { get; set; }
        
        public SeriesCollection SeriesCollection { get; set; }

        public List<Order> Orders { get; set; }

        public ChartValues<int> Ventas2024 { get; set; }
        public ChartValues<int> Ventas2023 { get; set; }
        
        private readonly Api api;

        public HomeView()
        {
            InitializeComponent();
            api = new Api();
            LoadOrders();

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

        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

