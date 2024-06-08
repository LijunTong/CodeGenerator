using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Lib.Options
{
    public class MenuOptions
    {
        public List<Menu> Menus { get; set; }
    }


    public class Menu
    {
        public string Header { get; set; }

        public string Icon { get; set; }

        public string View { get; set; }

        public bool IsSelected { get; set; }

        public bool IsNotShowClose { get; set; }

        public List<Menu> SubMenu { get; set; }
    }
}
