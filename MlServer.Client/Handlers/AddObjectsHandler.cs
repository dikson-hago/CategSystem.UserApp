using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;

using Proto = MlServer.Proto;
using Models = MlServer.Client.Models;

//"http://localhost:5193"
namespace MlServer.Client.Handlers;

internal class AddObjectsHandler : ClientHandlerBase
{
    internal AddObjectsHandler(string url) : base(url)
    {
        
    }
    
    private Proto.ObjectInfo GetObjectInfo(Models.ObjectInfo model)
    {
        var objectInfo = new Proto.ObjectInfo();
        
        objectInfo.Category = model.Category;
        objectInfo.Signs.AddRange(model.Signs);
        
        return objectInfo;
    }

    internal async Task<Models.ErrorsCollection> AddObjects(
        List<Models.ObjectInfo> objects, string tableName)
    {
        using var channel = GrpcChannel.ForAddress(Url);
        Client = new Proto.Greeter.GreeterClient(channel);

        var request = new Proto.AddObjectsRequest();
        request.TableName = tableName;
        request.Objects.Add(
            objects.Select(GetObjectInfo));

        var result = await Client.AddObjectsAsync(request);

        return result.MapToErrorsCollection();
    }
}