using DashboardTienda.Properties;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DashboardTienda
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ApplyTheme(bool isDarkTheme)
        {
            Resources.MergedDictionaries.Clear();

            var theme = new ResourceDictionary();
            theme.Source = new Uri(isDarkTheme ?
                "Styles/DarkTheme.xaml" :
                "Styles/LightTheme.xaml", UriKind.Relative);

            Resources.MergedDictionaries.Add(theme);

            // Guardar preferencia
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplyTheme(false);
        }
    }

}
