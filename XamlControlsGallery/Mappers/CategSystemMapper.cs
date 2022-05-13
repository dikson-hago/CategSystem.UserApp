using System.Collections.Generic;
using System.Linq;
using CategorixationSystem.ApplicationServices.Excel.Models;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Mappers;

using ClientModels = MlServer.Client.Models;

public static class CategSystemMapper
{
    private static List<string> ConvertColumnNamesToList(this NewTableInfo context)
    {
        var result = new List<string>();
            
        foreach (var prop in context.GetType().GetProperties())
        {
            if (prop.Name.Contains("Sign"))
            {
                var value = prop.GetValue(context) ?? "";
                result.Add(value.ToString());
            }
        }
            
        return result;
    }
    
    public static ClientModels.TableInfo MapToClientTableInfo(this NewTableInfo tableInfo)
    {
        return new ClientModels.TableInfo()
        {
            TableName = tableInfo.TableName,
            CategoryColumnName = tableInfo.CategoryColumnName,
            ColumnNames = tableInfo.ConvertColumnNamesToList()
        };
    }

    public static List<ClientModels.ObjectInfo> MapToObjectsInfosForAdd(this ExcelFile file, string tableName)
    {
        var result = new List<ClientModels.ObjectInfo>();

        for (int rowId = 1; rowId < file.Rows.Count(); rowId++)
        {
            int columnsAmount = file.Rows[rowId].Columns.Count();
            result.Add(new ClientModels.ObjectInfo()
            {
                Category = file.Rows[rowId].Columns.First(),
                Signs = file.Rows[rowId].Columns.GetRange(1, columnsAmount - 1)
            });
        }

        return result;
    }

    public static List<ClientModels.ObjectInfo> MapToObjectsInfosForPredict(this List<List<string>> rowInfos)
    {
        var result = new List<ClientModels.ObjectInfo>();

        foreach (var rowInfo in rowInfos)
        {
            var model = new ClientModels.ObjectInfo();
            model.Signs = rowInfo;

            result.Add(model);
        }

        return result;
    }
    
    public static List<ClientModels.ObjectInfo> MapToObjectInfoForPredict(this ExcelFile file)
    {
        var result = new List<ClientModels.ObjectInfo>();
        
        foreach (var rowInfo in file.RowsWithoutHeader())
        {
            var model = new ClientModels.ObjectInfo();
            model.Signs = rowInfo.GetColumns();

            result.Add(model);
        }

        return result;
    }
}