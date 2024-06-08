namespace CodeGenerator.Data.Entity.DatabaseEntity
{
    public class DbFieldInfo
    {
        public string FieldName { get; set; }
        public string FieldModelName { get; set; }
        public string FieldModelNameCamel { get; set; }
        public string FieldDbType { get; set; }
        public string FieldModelType { get; set; }
        public long FieldLength { get; set; }
        public int NumberLength { get; set; }

        public int DecimalPoint { get; set; }
        public bool IsNotNull { get; set; }
        public bool IsIncrement { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string FieldDes { get; set; }
        public string DefaultValue { get; set; }
    }

    public class DbInfo
    {
        public string DataBase { get; set; }
    }

    public class DbTableInfo
    {
        public string TableName { get; set; }
    }
}
