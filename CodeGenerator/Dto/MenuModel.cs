using System.Collections.Generic;
using System.Windows;

namespace CodeGenerator.Dto
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
