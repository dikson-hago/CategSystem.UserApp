namespace XamlControlsGallery.Models;

public class TableServerInfo
{
    public string TableName { get; set; }
    public string ServerUrl { get; set; }
    
    public string TableNameWithConnection => 
        TableName + '(' + ServerUrl + ')';
}