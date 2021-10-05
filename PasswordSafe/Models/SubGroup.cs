using System;
using System.Collections.Generic;

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
        public List<Account> Accounts { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}
