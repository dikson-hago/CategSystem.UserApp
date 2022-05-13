using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamlControlsGallery.Connections;
using XamlControlsGallery.Models;

namespace XamlControlsGallery.Contexts.UpdateTables;

public class UpdateTablesContext
{
    private ServersStorage _serversStorage;

    public UpdateTablesContext()
    {
        _serversStorage = ServersStorage.GetInstance();
    }

    public async Task<List<TableServerInfo>> GetAllTables()
    {
        var result = await _serversStorage.GetAllTablesNamesWithConnections();
        return result.Select(table => new TableServerInfo()
        {
            ServerUrl = table.ServerUrl,
            TableName = table.TableName
        }).ToList();
    }
}