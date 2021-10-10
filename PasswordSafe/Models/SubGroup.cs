using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PasswordSafe.Models
{
    public class SubGroup
    {
        public SubGroup()
        {
            Id = Guid.NewGuid();
            Name = "New Group";
            Icon = "\uE716";
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public ObservableCollection<Account> Accounts { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}
