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

namespace DashboardTienda.UserControls
{
    /// <summary>
    /// Lógica de interacción para OrderCard.xaml
    /// </summary>
    public partial class OrderCard : UserControl
    {



        public OrderCard()
        {
            InitializeComponent();
        }

        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(string), typeof(OrderCard));

        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(string), typeof(OrderCard));

        public string TotalAmount
        {
            get { return (string)GetValue(TotalAmountProperty); }
            set { SetValue(TotalAmountProperty, value); }
        }
        public static readonly DependencyProperty TotalAmountProperty =
            DependencyProperty.Register("TotalAmount", typeof(string), typeof(OrderCard));


    }
}
