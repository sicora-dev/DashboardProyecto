using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardTienda.Services
{
    public class SearchService : INotifyPropertyChanged
    {
        private static SearchService _instance;
        public static SearchService Instance => _instance ??= new SearchService(); //arrow fun (lamda fun) ??= = null coalescing assignment operator = if null then create new instance else return instance

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
