using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DashboardTienda.Services
{
    public class ThemeService
    {
        private static ThemeService _instance;
        public static ThemeService Instance => _instance ??= new ThemeService(); //arrow fun (lamda fun) ??= = null coalescing assignment operator = if null then create new instance else return instance

        private ThemeService() { }

        public void ToggleTheme()
        {
            ((App)Application.Current).ApplyTheme();
        }
    }
}
