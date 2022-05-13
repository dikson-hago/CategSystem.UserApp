using System.Collections.ObjectModel;

namespace XamlControlsGallery.Models;

public class AddStatusInfoModel
{
    public ObservableCollection<StatusInfo> StatusInfos { get; set; }

    public AddStatusInfoModel()
    {
        StatusInfos = new ObservableCollection<StatusInfo>();
    }

    public void AddStatusInfo(string statusInfo)
    {
        StatusInfos.Clear();
        
        StatusInfos.Add(new StatusInfo()
        {
            Message = statusInfo
        });
    }
}