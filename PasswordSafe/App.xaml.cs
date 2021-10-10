using LiteDB;
using PasswordSafe.Models;
using PasswordSafe.Services;
using PasswordSafe.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordSafe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LiteDatabase BackUp
        {
            get
            {
                var filePath = @"Filename=C:/ProgramData/BackUpPassDb.db;Connection=Shared;";
                return new LiteDatabase(filePath);
            }
        }

        public static LiteDatabase UserDb
        {
            get
            {
                return new LiteDatabase(@"Filename=C:/ProgramData/TempPassDb.db;Connection=Shared;");
            }
        }
        public static bool isOnline = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                using (new ConnectToSharedFolder(AppSettings.Default["DBLink"].ToString(), new NetworkCredential(AppSettings.Default["DbUsername"].ToString(), AppSettings.Default["DbPassword"].ToString())))
                {
                File.Delete("C:/ProgramData/TempPassDb.db");
                File.Copy(AppSettings.Default["DBLink"].ToString(), "C:/ProgramData/TempPassDb.db");
                using (var db = new LiteDatabase(@"Filename=C:/ProgramData/TempPassDb.db;Connection=Shared;"))
                {
                    var col = db.GetCollection<SubGroup>();
                    var count = col.Count();
                    var testvalue = new SubGroup() { Id = Guid.NewGuid() };
                    col.Insert(testvalue);
                    col.Delete(testvalue.Id);
                }
                isOnline = true;
                }
            }
            catch
            {
                isOnline = false;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            BackUp.Dispose();
            UserDb.Dispose();
            try
            {
                using (new ConnectToSharedFolder(AppSettings.Default["DBLink"].ToString(), new NetworkCredential(AppSettings.Default["DbUsername"].ToString(), AppSettings.Default["DbPassword"].ToString())))
                {
                File.Delete("C:/ProgramData/TempPassDbInter.db");
                File.Copy(AppSettings.Default["DBLink"].ToString(), "C:/ProgramData/TempPassDbInter.db");
                using (var db = UserDb)
                {
                    using var db2 = new LiteDatabase(@"C:/ProgramData/TempPassDbInter.db");
                    var col = db.GetCollection<SubGroup>();
                    var col2 = db2.GetCollection<SubGroup>();
                    foreach (var item in col.FindAll())
                    {
                        if(col2.FindById(item.Id)==null)
                        {
                            col2.Insert(item);
                        }
                    }
                    db.Dispose();
                    db2.Dispose();
                }
                File.Delete(AppSettings.Default["DBLink"].ToString());
                File.Copy("C:/ProgramData/TempPassDbInter.db", AppSettings.Default["DBLink"].ToString());
                File.Delete("C:/ProgramData/TempPassDb.db");
                }
            }
            catch
            {
            }
        }
    }
}
