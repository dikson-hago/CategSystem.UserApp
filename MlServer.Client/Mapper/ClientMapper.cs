using System.Collections.Generic;
using ProtoModels = MlServer.Proto;

namespace MlServer.Client.Mapper;

internal static class ClientMapper
{
    internal static List<Models.TableStatusInfo> MapToTableStatusInfoList(this ProtoModels.GetTablesStatusesResponse response)
    {
        return response.TablesStatuses.Select(item => new Models.TableStatusInfo()
        {
            Status = item.Status,
            TableName = item.TableName
        }).ToList();
    }

    internal static Models.ErrorsCollection MapToErrorsCollection(this ProtoModels.ErrorsResultResponse response)
    {
        return new Models.ErrorsCollection()
        {
            Errors = response.Errors.ToList()
        };
    }
    
    internal static List<Models.TableInfo> MapToTableInfo(this ProtoModels.GetAllTablesResponse response)
    {
        return response.TableInfo.Select(item =>
            new Models.TableInfo()
            {
                TableName = item.TableName,
                CategoryColumnName = item.CategoryColumnName,
                ColumnNames = item.ColumnNames.ToList()
            }).ToList();

    }

    internal static List<Models.ObjectInfo> MapToObjectsInfo(this ProtoModels.GetObjectsResponse response)
    {
        var result = new List<Models.ObjectInfo>();

        foreach (var element in response.Objects)
        {
            result.Add(new Models.ObjectInfo()
            {
                Category = element.Category,
                Signs = element.Signs.ToList(),
            });
        }

        return result;
    }
}