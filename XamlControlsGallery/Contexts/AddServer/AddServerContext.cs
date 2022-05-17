using System;
using System.Threading.Tasks;
using CategorisationSystem.ApplicationServices.Storages;

namespace XamlControlsGallery.Contexts.AddServer;

public class AddServerContext : BaseContext
{
    private readonly string _connectionUrl;
    
    public AddServerContext(string url, Action<string> status) 
        : base(url, status)
    {
        _connectionUrl = url;
    }

    private async Task TryAddConnection()
    {
        var serversStorage = ServersStorage.GetInstance();
        await serversStorage.AddConnection(_connectionUrl);
    }

    public async Task AddServer()
    {
        try
        {
            Status("Please wait...");
            
            await TryAddConnection();
            
            Status("Connected");
        }
        catch (Exception e)
        {
            Status(e.Message);
        }
    }
}