using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashboardTienda.Models;

namespace DashboardTienda.Services
{
    public class CommentSelectionService : INotifyPropertyChanged
    {
        private static CommentSelectionService _instance;
        public static CommentSelectionService Instance => _instance ??= new CommentSelectionService();

        private Comment _selectedComment;
        public Comment SelectedComment
        {
            get => _selectedComment;
            set
            {
                _selectedComment = value;
                OnPropertyChanged(nameof(SelectedComment));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
