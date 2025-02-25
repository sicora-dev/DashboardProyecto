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
using DashboardTienda.UserControls;

namespace DashboardTienda.UserControls
{
    
    public partial class UserCard : UserControl
    {
        


        public UserCard()
        {
            InitializeComponent();
        }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(UserCard));

        public string Mail
        {
            get { return (string)GetValue(MailProperty); }
            set { SetValue(MailProperty, value); }
        }
        public static readonly DependencyProperty MailProperty =
            DependencyProperty.Register("Mail", typeof(string), typeof(UserCard));

        public string Role
        {
            get { return (string)GetValue(RoleProperty); }
            set { SetValue(RoleProperty, value); }
        }
        public static readonly DependencyProperty RoleProperty =
            DependencyProperty.Register("Role", typeof(string), typeof(UserCard));
        
        
    }
}
