using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeGenerator.Lib.Models
{
    public class MenuModel
    {
        public string Header { get; set; }

        public string Icon { get; set; }

        public string View { get; set; }

        public bool IsSelected { get; set; }

        public bool IsNotShowClose { get; set; }

        public Visibility Visibility
        {
            get
            {
                return IsNotShowClose ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public List<MenuModel> SubMenu { get; set; }
    }
}
