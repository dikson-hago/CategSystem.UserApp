using System;
using System.Threading.Tasks;
using CategorisationSystem.ApplicationServices.Storages;
using XamlControlsGallery.Mappers;

namespace XamlControlsGallery.Contexts.DowloadTable;

public class DownloadTableContext : BaseContext
{
    private ServersStorage _serversStorage;
    private readonly CreateExcelContext _createExcelContext;
    private string _url;

    public DownloadTableContext(string url, Action<string> status) : base(url, status)
    {
        _serversStorage = ServersStorage.GetInstance();
        _createExcelContext = new CreateExcelContext();
        _url = url;
    }

    public async Task Download(string tableName, string targetFolder)
    {
        try
        {
            var table = await Server.DownloadTable(tableName);
            var headers = _serversStorage.GetTableColumns(tableName, _url);

            var file = table.MapToExcel(headers);
            
            ValidateDirectoryPath(targetFolder);
            _createExcelContext.Create(file, targetFolder);
            
            Status("Downloaded");
        }
        catch (Exception e)
        {
            Status(e.Message);
        }
    }
    
}