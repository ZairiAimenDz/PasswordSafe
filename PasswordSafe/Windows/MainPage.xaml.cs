using PasswordSafe.Models;
using PasswordSafe.Services;
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
using System.Windows.Shapes;

namespace PasswordSafe.Windows
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        private List<SubGroup> Data;
        public ObservableCollection<Group> Database;
        public SubGroup SelectedGroup;
        public Account SelectedAccount;
        public MainPage()
        {
            InitializeComponent();
            InitializeComponent();
            SelectedGroupIcon.ItemsSource = DatabaseService.GetIcons();
            GetData();
            DataContext = this;
            /*if (!App.isOnline)
            {
                MessageBox.Show("We Couldn't Find The Database, You Will Be Using A BackUp Database, You Can Also Try And Setup The Database in The Settings Again","Error Database Not Found",MessageBoxButton.OK);
            }*/
        }

        public void GetData()
        {
            Data = new(DatabaseService.GetData());
            Database = new()
            {
                new Group()
                {
                    Name = "Database",
                    ChildNodes = Data != null ? new(Data) : null
                }
            };
            GroupsTree.ItemsSource = Database;
        }

        private void NewGroupClicked(object sender, RoutedEventArgs e)
        {
            var grp = new SubGroup();
            Database.FirstOrDefault().ChildNodes.Add(grp);
            SelectedGroup = grp;
            SelectedGroupName.Text = SelectedGroup.Name;
            DatabaseService.AddGroup(grp);
            GroupDetails.IsExpanded = true;
        }

        private void Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sen = (ListView)sender;
            SelectedAccount = sen.SelectedItem as Account;
        }

        private void GroupsTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue.GetType() == typeof(SubGroup))
            {
                SelectedGroup = e.NewValue as SubGroup;
                if (SelectedGroup != null)
                {
                    Users.ItemsSource = SelectedGroup.Accounts;
                }
            }
            else
            {
                SelectedGroup = null;
            }
        }

        private void SearchSelectionChanged(object sender, TextChangedEventArgs e)
        {
            if (Users.ItemsSource != null)
            {
                var sen = (TextBox)sender;
                Users.ItemsSource = SelectedGroup.Accounts.Where(a => a.ComputerName.ToLower().Contains(sen.Text.ToLower())
                    || a.OwnerName.ToLower().Contains(sen.Text.ToLower())
                    || a.ComputerName.ToLower().Contains(sen.Text.ToLower())
                    );
            }
        }

        private void IPMacSearch(object sender, TextChangedEventArgs e)
        {
            if (Users.ItemsSource != null)
            {
                var sen = (TextBox)sender;
                Users.ItemsSource = SelectedGroup.Accounts.Where(a => a.IPAddress.Contains(sen.Text)
                    || a.MacAddress.ToLower().Contains(sen.Text.ToLower())
                    || a.ComputerModel.ToLower().Contains(sen.Text.ToLower())
                    );
            }
        }

        private void ReloadClicked(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void SettingsClicked(object sender, RoutedEventArgs e)
        {
            var set = new Settings();
            set.ShowDialog();
            GetData();
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This Software Was Made by Zairi Aimen," +
                " If There Are Any Problems Or Issues, You Are very Welcome To " +
                "add an issue in the Github repository @ZairiAimenDz in Github", "Help",
                MessageBoxButton.OK);
        }

        private void DeleteGroupClicked(object sender, RoutedEventArgs e)
        {
            if (SelectedGroup != null)
                if (MessageBox.Show("Are You Sure you want to delete this groupe ?", "Deletion Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    DatabaseService.DeleteGroup(SelectedGroup);
                    GetData();
                    SelectedGroup = null;
                    Users.ItemsSource = null;
                    (SelectedGroupName.Text, SelectedGroupIcon.Text) = ("", "");
                    SelectedAccount = null;
                }
                else
                    MessageBox.Show("Select The Element You Want To Delete");
        }

        private void SaveDetailedClicked(object sender, RoutedEventArgs e)
        {
            if (SelectedGroup != null)
            {
                (SelectedGroup.Name, SelectedGroup.Icon) = (SelectedGroupName.Text, SelectedGroupIcon.Text);
                DatabaseService.UpdateGroup(SelectedGroup);
            }
        }

        private void CreateAccountClicked(object sender, RoutedEventArgs e)
        {
            if (SelectedGroup != null)
            {
                var acc = new Account();
                if (SelectedGroup.Accounts is null)
                    SelectedGroup.Accounts = new();
                SelectedGroup.Accounts.Add(acc);
                Users.ItemsSource = SelectedGroup.Accounts;
                Users.SelectedItem = acc;
                AccountDetails.IsExpanded = true;
                DatabaseService.UpdateGroup(SelectedGroup);
            }
            else
            {
                MessageBox.Show("Select A Group");
            }
        }

        private void DeleteAccountClicked(object sender, RoutedEventArgs e)
        {
            if (SelectedAccount != null)
                if (MessageBox.Show("Are You Sure you want to delete this account ?", "Deletion Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                {
                    SelectedGroup.Accounts.Remove(SelectedAccount);
                    DatabaseService.UpdateGroup(SelectedGroup);
                    Users.ItemsSource = SelectedGroup.Accounts;
                }
                else
                    MessageBox.Show("Select The Element You Want To Delete");
        }
    }
}
