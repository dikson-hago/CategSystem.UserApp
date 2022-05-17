namespace CategorisationSystem.ApplicationServices.Storages;

public class ConnectionsInfo
{
    public ConnectionsInfo()
    {
        TableInfos = new List<StoragesTableInfo>();
        ConnectionsList = new List<string>();
    }
    public List<StoragesTableInfo> TableInfos { get; set; }
    public List<string> ConnectionsList { get; set; }

    public void TryAddConnection(string serverUrl)
    {
        if(IsConnected(serverUrl))
        {
            throw new Exception("Current connection already exist");
        }
        
        ConnectionsList.Add(serverUrl);
    }

    private bool EqualUrls(string url1, string url2)
    {
        return url1.Contains(url2) || url2.Contains(url1);
    }
    
    private bool EqualValues(string value1, string value2)
    {
        return value1.Contains(value2) || value2.Contains(value1);
    }

    public void RemoveConnection(string serverUrl)
    {
        ConnectionsList = ConnectionsList.Where(url => 
            !(EqualUrls(url, serverUrl))).ToList();
        
        TableInfos = TableInfos.Where(item => 
                !(EqualUrls(serverUrl, item.ServerUrl)))
            .ToList();
    }

    public void AddConnections(List<string> connections)
    {
        ConnectionsList.AddRange(connections);
    }

    public void AddTableInfo(StoragesTableInfo tableInfo)
    {
        TableInfos.Add(tableInfo);
    }

    public void AddTableInfos(List<StoragesTableInfo> tableInfos)
    {
        TableInfos.AddRange(tableInfos);
    }

    public bool CheckTableExist(string serverUrl, string tableName)
    {
        return TableInfos.Exists(item =>
            EqualUrls(item.ServerUrl, serverUrl) && 
            EqualValues(item.TableName, tableName));
    }

    public List<string> GetAllConnections()
    {
        return new List<string>(ConnectionsList);
    }

    public bool IsConnected(string serverUrl) => ConnectionsList.Exists( 
        url => EqualUrls(url, serverUrl));
}