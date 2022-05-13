using System.Collections.Generic;
using CategorixationSystem.ApplicationServices.Excel.Models;
using ClientModels = MlServer.Client.Models;

namespace XamlControlsGallery.Mappers;

public static class ExcelFilesMapper
{
    public static ExcelFile MapToExcel(this List<ClientModels.ObjectInfo> objects, List<string> headers)
    {
        var excelFile = new ExcelFile();
        
        excelFile.AddRow(headers);

        foreach (var element in objects)
        {
            var row = new List<string>();
            
            row.Add(element.Category);
            row.AddRange(element.Signs);
            
            excelFile.AddRow(row);
        }

        return excelFile;
    }
}