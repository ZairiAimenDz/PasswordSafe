using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using PasswordSafe.Services;
using System.Net;
using LiteDB;
using PasswordSafe.Models;

namespace PasswordSafe.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            //SelectedDb.Text = AppSettings.Default["DBLink"].ToString();
        }

        private void FileSelectionClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Database File (*.db)|*.db|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    //SelectedDb.Text = openFileDialog.FileName;
                }
            }
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            //AppSettings.Default["DBLink"] = SelectedDb.Text.Replace("\\", "/");
            AppSettings.Default["DbUsername"] = Username.Text;
            AppSettings.Default["DbPassword"] = AccessPassword.Text;
            AppSettings.Default.Save();
            Close();
        }

        /*        private void TestConnectivity(object sender, RoutedEventArgs e)
                {
                    try
                    {
                        using (new ConnectToSharedFolder(SelectedDb.Text, new NetworkCredential(Username.Text, AccessPassword.Text)))
                        {
                            File.Copy(SelectedDb.Text, "C:/ProgramData/TempPassDb.db");
                            using (var db = new LiteDatabase(@"Filename=C:/ProgramData/TempPassDb.db;Connection=Shared;"))
                            {
                                var col = db.GetCollection<SubGroup>();
                                var count = col.Count();
                                var testvalue = new SubGroup() {Id=Guid.NewGuid()};
                                col.Insert(testvalue);
                                col.Delete(testvalue.Id);
                            }
                            File.Delete("C:/ProgramData/TempPassDb.db");
                        }
                        AccessAvail.Content = "Access Available";
                    }
                    catch
                    {
                        AccessAvail.Content = "Access Unavailable";
                    }
                }*/
    }
}
