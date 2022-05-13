using System;
using System.Threading.Tasks;
using XamlControlsGallery.Connections;
using XamlControlsGallery.Mappers;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Contexts.AddTable;

public class AddTableContext : BaseContext
{
    private ServersStorage _serversStorage;
    public AddTableContext(string url, Action<string> status) : base(url, status)
    {
        _serversStorage = ServersStorage.GetInstance();
    }

    private void CheckNotNull(string value, string name)
    {
        if (value is null)
        {
            throw new Exception(name + " \n couldn't be null");
        }
    }

    public async Task AddTableAsync(AddTableViewModel context)
    {
        try
        {
            Status("Please wait...");
            
            CheckNotNull(context.ServerUrl, "Server Url");
            CheckNotNull(context.TableInfo.TableName, "Table Name");
            CheckNotNull(context.TableInfo.CategoryColumnName, "Column Category");

            
            await _serversStorage.CheckTableExist(context.ServerUrl, context.TableInfo.TableName);
            
            var errors =
                await Server.AddTable(
                    context.TableInfo.MapToClientTableInfo());
            
            AnalyseServerErrors(errors);
                
            Status("New table created\n");
        }
        catch(Exception e)
        {
            Status(e.Message);
        }
    }
}