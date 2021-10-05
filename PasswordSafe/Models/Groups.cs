using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasswordSafe.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public ObservableCollection<SubGroup> ChildNodes { get; set; }

    }
}
