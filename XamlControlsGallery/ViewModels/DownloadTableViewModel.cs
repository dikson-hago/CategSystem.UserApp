namespace XamlControlsGallery.ViewModels;

public class DownloadTableViewModel : ViewModelBase
{
    public string TableName { get; set; }
    
    public string ServerUrl { get; set; }
    
    public string TargetFolderPath { get; set; }
}