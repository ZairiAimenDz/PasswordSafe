using LiteDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordSafe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LiteDatabase OfflineDatabase
        {
            get
            {
                var filePath = @"Filename=Pass.db;Connection=Shared;";
                return new LiteDatabase(filePath);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            OfflineDatabase.Dispose();
        }
    }
}
