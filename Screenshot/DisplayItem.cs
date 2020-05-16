using MakCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot
{
    public class DisplayItem : ViewModelBase
    {
        public bool IsChecked { get; set; }

        public string DisplayName { get; set; }

        public string FormatDisplayName { get; set; }
    }
}
