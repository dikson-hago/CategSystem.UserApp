using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;

namespace XamlControlsGallery.ViewModels;

public class TableStatus
{
    public string TableName { get; set; }
    
    public string Status { get; set; }

    public string Result => TableName + ": " + Status + "\n\n";
}

public class TablesStatusesViewModel : ViewModelBase
{
    public ObservableCollection<TableStatus> TablesStatuses { get; set; }

    public TablesStatusesViewModel()
    {
        TablesStatuses = new ObservableCollection<TableStatus>();
    }

    public void UpdateTablesStatuses(List<TableStatus> newTablesStatuses)
    {
        TablesStatuses.Clear();
        TablesStatuses.AddRange(newTablesStatuses);
    }
}