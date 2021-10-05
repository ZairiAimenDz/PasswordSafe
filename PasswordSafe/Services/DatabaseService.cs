using LiteDB;
using PasswordSafe.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PasswordSafe.Services
{
    public static class DatabaseService
    {
        public static ILiteCollection<SubGroup> Collection = App.OfflineDatabase.GetCollection<SubGroup>();

        public static IEnumerable<SubGroup> GetData()
        {
            return Collection.FindAll() ?? new List<SubGroup>();
        }

        public static void AddGroup(SubGroup group)
        {
            Collection.Insert(group);
        }
        
        public static void DeleteGroup(SubGroup group)
        {
            Collection.Delete(group.Id);
        }
        public static void UpdateGroup(SubGroup group)
        {
            Collection.Update(group);
        }

        public static IEnumerable<string> GetIcons()
        {
            return new List<string>() { 
                "\uE703", "\uE704", "\uE716", "\uE730", "\uE736",
                "\uE749", "\uE756", "\uE774", "\uE775", "\uE786",
                "\uE787", "\uE7BE", "\uE7C0", "\uE7E3", "\uE7EC",
                "\uE806", "\uE816", "\uE81E", "\uE825", "\uE9D2",
                "\uE9F9", "\uE9D9"
            };
        }
    }
}
