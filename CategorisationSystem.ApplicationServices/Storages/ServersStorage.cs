namespace XamlControlsGallery.Connections;

public class ServersStorage
{
    private static ServersStorage _serversStorage;

    private readonly UpdateConnectionsContext _updateConnectionsContext;
    private ConnectionsInfo _connectionsInfo;
    private readonly ServersBackUpStorage _serversBackUpStorage;

    private void InitConnectionInfo()
    {
        _connectionsInfo = new ConnectionsInfo();
        _connectionsInfo.AddConnections(_serversBackUpStorage.GetAllConnections());
    }

    private ServersStorage()
    {
        _updateConnectionsContext = new UpdateConnectionsContext();
        _serversBackUpStorage = new ServersBackUpStorage();
        
        InitConnectionInfo();
    }
    
    public static ServersStorage GetInstance()
    {
        if (_serversStorage == null)
        {
            _serversStorage = new ServersStorage();
        }

        return _serversStorage;
    }
    
    private async Task UpdateConnectionInfo(string serverUrl)
    {
        _connectionsInfo.RemoveConnection(serverUrl);
        
        await TryConnect(serverUrl);

        var result = 
            await _updateConnectionsContext.UpdateConnectionInfo(serverUrl);
        
        _connectionsInfo.TryAddConnection(serverUrl);
        _connectionsInfo.AddTableInfos(result);
    }

    private async Task TryConnect(string serverUrl)
    {
        await _updateConnectionsContext.TryConnect(serverUrl);
    }

    public async Task AddConnection(string connectionUrl)
    {
        await TryConnect(connectionUrl);
        
        _connectionsInfo.TryAddConnection(connectionUrl);
        
        await UpdateConnectionInfo(connectionUrl);

        if (!_connectionsInfo.IsConnected(connectionUrl))
        {
            throw new Exception("Server is anavaliable");
        }
        
        _serversBackUpStorage.AddConnection(connectionUrl);
    }

    private async Task UpdateAllConnections()
    {
        _connectionsInfo =
            await _updateConnectionsContext.UpdateAllConnections(_connectionsInfo);
    }

    public async Task CheckTableExist(string serverUrl, string tableName)
    {
        await UpdateConnectionInfo(serverUrl);
        
        if(_connectionsInfo.CheckTableExist(serverUrl, tableName))
        {
            throw new Exception("Current table name already \n exists");
        }
    }

    public async Task<List<StoragesTableInfo>> GetAllTablesNamesWithConnections()
    {
        await UpdateAllConnections();

        await _serversBackUpStorage.Rewrite(_connectionsInfo.GetAllConnections());
        
        return _connectionsInfo.TableInfos;
    }

    public List<string> GetTableColumns(string tableName, string serverUrl)
    {
        var tableInfo = _connectionsInfo.TableInfos
            .FirstOrDefault(item => 
                item.TableName.Equals(tableName) && item.ServerUrl.Equals(serverUrl));
        
        var result = new List<string>();
        result.Add(tableInfo.CategoryColumnName);
        result.AddRange(tableInfo.ColumnNames);
        return result;
    } 
}