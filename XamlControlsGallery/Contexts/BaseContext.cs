using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MlServer.Client;
using MlServer.Client.Models;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Contexts;

public abstract class BaseContext
{
    protected readonly MlServerApi Server;
    protected Action<string> Status;

    protected BaseContext(string url, Action<string> newStatus)
    {
        Server = new MlServerApi(url);
        Status = newStatus;
    }
    
    protected void AnalyseServerErrors(ErrorsCollection errors)
    {
        if (errors.Errors.Any())
        {
            throw new Exception(
                "Internal server errors: " + string.Join("\n", errors));
        }
    }
    
    protected async Task<TableInfo> GetCurrentTableInfo(string tableName)
    {
        var tableInfos = await Server.GetAllTablesInfos();

        var currentTable = 
            tableInfos.FirstOrDefault(table => table.TableName == tableName);

        return currentTable ??
               throw new Exception("Current table doesn't exist in current server");
    }

    public void ValidateFilePath(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new Exception("Invalid path: " + filePath);
        }
    }

    public void ValidateDirectoryPath(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            throw new Exception("Invalid path: " + folderPath);
        }
    }

    public async Task TryConnectToServer()
    {
        await Server.TryConnect();
    }
}