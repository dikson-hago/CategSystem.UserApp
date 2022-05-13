using System;
using CategorixationSystem.ApplicationServices.Excel.Models;
using XamlControlsGallery.Connections;

namespace XamlControlsGallery.Contexts.TemplateForAdd;

public abstract class TemplateContextBase : BaseContext
{
    protected readonly ServersStorage ServersStorage;
    protected readonly CreateExcelContext CreateExcelContext;
    protected readonly string Url;
    
    public TemplateContextBase(string url, Action<string> status) 
        : base(url, status)
    {
        ServersStorage = ServersStorage.GetInstance();
        CreateExcelContext = new CreateExcelContext();
        Url = url;
    }

    public abstract void CreateTemplate(string tableName, string folderPath);
}