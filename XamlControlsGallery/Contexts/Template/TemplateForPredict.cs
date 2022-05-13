using System;
using CategorixationSystem.ApplicationServices.Excel.Models;

namespace XamlControlsGallery.Contexts.TemplateForAdd;

public class TemplateForPredict : TemplateContextBase
{
    public TemplateForPredict(string url,
        Action<string> status) : base(url, status)
    {
    }

    public override void CreateTemplate(string tableName, string folderPath)
    {
        var columnNames = ServersStorage.GetTableColumns(tableName, Url);
        
        ExcelFile file = new ExcelFile();

        columnNames = columnNames.GetRange(1, columnNames.Count - 1);
        file.AddRow(columnNames);
        
        CreateExcelContext.Create(file, folderPath);
    }
}