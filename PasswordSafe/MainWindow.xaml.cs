using PasswordSafe.Models;
using PasswordSafe.Services;
using PasswordSafe.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PasswordSafe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VaultUsername.Text == AppSettings.Default["DbUsername"].ToString()
                && AppSettings.Default["DbPassword"].ToString() == VaultPassword.Password)
            {
                new MainPage().Show();
                Close();
            }
        }
    }
}
