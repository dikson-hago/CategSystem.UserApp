using System;
using System.Linq;
using System.Threading.Tasks;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Contexts.TablesStatuses;

public class TablesStatusesContext : BaseContext
{
    public TablesStatusesContext(string url, Action<string> status) 
        : base(url, status)
    {
    }

    public async Task GetTablesStatuses(TablesStatusesViewModel viewModel)
    {
        var result = await Server.GetTablesStatusesInfo();
            
        viewModel.UpdateTablesStatuses(result.Select(item => new TableStatus()
        {
            Status = item.Status,
            TableName = item.TableName
        }).ToList());
    }
}