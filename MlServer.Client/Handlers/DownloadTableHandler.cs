using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using Models = MlServer.Client.Models;
using Proto = MlServer.Proto;

namespace MlServer.Client.Handlers;

internal class DownloadTableHandler : ClientHandlerBase
{
    public DownloadTableHandler(string url) : base(url)
    {
    }
    
    public async Task<List<Models.ObjectInfo>> DownloadTable(string tableName)
    {
        using var channel = GrpcChannel.ForAddress(Url);
        Client = new Proto.Greeter.GreeterClient(channel);

        var table = await Client.DownloadTableAsync(new Proto.DownloadTableRequest()
        {
            TableName = tableName
        });

        return table.MapToObjectsInfo();
    }
}