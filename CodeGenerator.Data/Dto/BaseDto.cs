using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Data.Dto
{
    public partial class BaseDto : ObservableObject
    {
        public string Id { get; set; }

        [ObservableProperty]
        public bool isSelected;

        public string Creater { get; set; }

        public DateTime CreateTime { get; set; }

        public string Updater { get; set; }

        public DateTime UpTime { get; set; }

        public int IsDel { get; set; }
    }
}
