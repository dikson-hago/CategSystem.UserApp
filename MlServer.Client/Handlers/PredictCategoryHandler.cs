using Google.Protobuf.Collections;
using Grpc.Net.Client;
using MlServer.Client.Handlers.Base;
using MlServer.Client.Mapper;
using MlServer.Client.Models;
using MlServer.Proto;

namespace MlServer.Client.Handlers;

internal class PredictCategoryHandler : ClientHandlerBase
{
    internal PredictCategoryHandler(string url) : base(url)
    {
    }
    
    private Proto.ObjectInfo GetPredictableObject(Models.ObjectInfo mlObject)
    {
        var result = new Proto.ObjectInfo();
        result.Signs.AddRange(mlObject.Signs);
        return result;
    }
    
    internal async Task<List<Models.ObjectInfo>> PredictCategory(
        List<Models.ObjectInfo> objects, string tableName)
    {
        using var channel = GrpcChannel.ForAddress(Url);
        Client = new Greeter.GreeterClient(channel);

        var request = new PredictRequest();
        request.TableName = tableName;
        request.Objects.Add(
            objects.Select(GetPredictableObject));
        
        var result = await Client.PredictCategoryAsync(request);

        if (result.Errors.Count() != 0)
        {
            throw new Exception(result.Errors.FirstOrDefault());
        }
        
        return result.MapToObjectsInfo();
    }
}