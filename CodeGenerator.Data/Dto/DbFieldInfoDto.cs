using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Data.Dto
{
    public partial class DbFieldInfoDto : ObservableObject
    {
        [ObservableProperty]
        public bool _isSelected;

        [ObservableProperty]
        public string _fieldName;

        [ObservableProperty]
        public string _fieldModelName;

        [ObservableProperty]
        public string _fieldModelNameCamel;

        [ObservableProperty]
        public string _fieldDbType;

        [ObservableProperty]
        public string _fieldModelType;

        [ObservableProperty]
        public long _fieldLength;

        [ObservableProperty]
        public int _numberLength;

        [ObservableProperty]
        public int _decimalPoint;

        [ObservableProperty]
        public bool _isNotNull;

        [ObservableProperty]
        public bool _isIncrement;

        [ObservableProperty]
        public bool _isPrimaryKey;

        [ObservableProperty]
        public string _fieldDes;

        [ObservableProperty]
        public string _defaultValue;
    }
}
