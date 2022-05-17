namespace CategorisationSystem.ApplicationServices.Storages;

public class StoragesTableInfo
{
    public string TableName { get; set; }
    public string ServerUrl { get; set; }
    
    public List<string> ColumnNames { get; set; }
    
    public string CategoryColumnName { get; set; }
}