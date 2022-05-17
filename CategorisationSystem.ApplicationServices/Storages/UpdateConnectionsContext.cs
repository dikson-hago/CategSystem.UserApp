using MlServer.Client;
using MlServer.Client.Models;

namespace CategorisationSystem.ApplicationServices.Storages;

public class UpdateConnectionsContext
{
    private async Task<List<TableInfo>> UpdateTablesInfos(MlServerApi api)
    {
        var tablesInfos =  await api.GetAllTablesInfos();

        return tablesInfos;
    }

    public async Task TryConnect(string connectionUrl)
    {
        MlServerApi api = new MlServerApi(connectionUrl);

        if (!(await api.TryConnect()))
        {
            throw new Exception("Current server is anavaliable");
        }
    }
    public async Task<List<StoragesTableInfo>> UpdateConnectionInfo(string connectionUrl)
    {
        MlServerApi api = new MlServerApi(connectionUrl);

        if (!(await api.TryConnect()))
        {
            return null;
        }

        var tablesInfos = await UpdateTablesInfos(api);

        return tablesInfos.Select(info => new StoragesTableInfo()
        {
            ServerUrl = connectionUrl,
            TableName = info.TableName,
            CategoryColumnName = info.CategoryColumnName,
            ColumnNames = info.ColumnNames
        }).ToList();
    }
    public async Task<ConnectionsInfo> UpdateAllConnections(ConnectionsInfo oldConnectionInfo)
    {
        var connectionInfo = new ConnectionsInfo();
        
        foreach (var connection in oldConnectionInfo.GetAllConnections())
        {
            var updatedTables = 
                await UpdateConnectionInfo(connection);

            if (updatedTables is not null)
            {
                connectionInfo.TryAddConnection(connection);
                connectionInfo.AddTableInfos(updatedTables);
            }
        }

        return connectionInfo;
    }
}