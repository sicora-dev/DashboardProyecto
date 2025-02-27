﻿using System;
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

namespace DashboardTienda.Views
{
    /// <summary>
    /// Lógica de interacción para CommentsView.xaml
    /// </summary>
    public partial class CommentsView : UserControl
    {
        private readonly Api api;
        public CommentsView()
        {
            InitializeComponent();
            api = new Api();
            LoadComments();
        }

        private async void LoadComments()
        {
            List<Comment> Comments = await GetCommentsAction();
            foreach (var comment in Comments)
            {
                CommentCard commentCard = new CommentCard();
                commentCard.UserName = comment.name;
                commentCard.Comment = comment.content;
                commentCard.Date = comment.date.ToString("yyyy-MM-dd");
                commentCard.MouseLeftButtonUp += (s, e) => CommentSelectionService.Instance.SelectedComment = comment;
                CommentsPanel.Children.Add(commentCard);
            }

        }

        public void RefreshComments(object sender, RoutedEventArgs e)
        {
            CommentsPanel.Children.Clear();
            LoadComments();
        }
        private async Task<List<Comment>> GetCommentsAction()
        {
            var result = await api.GetComments();

            if (result?.status == 200)
            {
                return result.comments;
            }
            else
            {
                MessageBox.Show(result?.message ?? "Error al cargar los comentarios.");
                return new List<Comment>();
            }
        }
    }
}
