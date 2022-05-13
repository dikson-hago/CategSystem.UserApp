using System;
using CategorixationSystem.ApplicationServices.Excel.Models;

namespace XamlControlsGallery.Contexts.TemplateForAdd;

public class TemplateForAddContext : TemplateContextBase
{
    public TemplateForAddContext(string url, Action<string> status) : base(url,
        status)
    {
    }

    public override void CreateTemplate(string tableName, string folderPath)
    {
        try
        {
            var columnNames = ServersStorage.GetTableColumns(tableName, Url);

            ExcelFile file = new ExcelFile();
            file.AddRow(columnNames);

            ValidateDirectoryPath(folderPath);
            CreateExcelContext.Create(file, folderPath);

            Status("Template downloaded");
        }
        catch (Exception e)
        {
            Status(e.Message);
        }
    }
    
}