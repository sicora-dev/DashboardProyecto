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
    /// Lógica de interacción para CommentsRightPanel.xaml
    /// </summary>
    public partial class CommentsRightPanel : UserControl
    {
        public string SelectedComment { get; set; }
        private readonly Api api;
        public CommentsRightPanel()
        {
            InitializeComponent();
            DataContext = CommentSelectionService.Instance;
            api = new Api();
            LoadComment();
        }

        public void LoadComment()
        {
            if (CommentSelectionService.Instance.SelectedComment != null)
            {
                txtNombre.Text = CommentSelectionService.Instance.SelectedComment.name;
                txtContent.Text = CommentSelectionService.Instance.SelectedComment.content;
                txtFecha.Text = CommentSelectionService.Instance.SelectedComment.date.ToString("yyyy-MM-dd");
                txtUserId.Text = CommentSelectionService.Instance.SelectedComment.user_id.ToString();
                txtProductId.Text = CommentSelectionService.Instance.SelectedComment.product_id.ToString();
            }

        }


        public async void OnDelete(object sender, RoutedEventArgs e)
        {
            
            var result = await api.DeleteComment(CommentSelectionService.Instance.SelectedComment._id);
            MessageBox.Show(result?.message);

        }

        public async void OnUserBan(object sender, RoutedEventArgs e)
        {
            var result = await api.BanUser(CommentSelectionService.Instance.SelectedComment.user_id);
            MessageBox.Show(result?.message);
        }
    }
}
