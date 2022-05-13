using XamlControlsGallery.Models;

namespace XamlControlsGallery.ViewModels
{
    public class NewTableInfo
    {
        public string TableName { get; set; }
        
        public string ColumnNameSign1 { get; set; }
        
        public string ColumnNameSign2 { get; set; }
        
        public string ColumnNameSign3 { get; set; }
        
        public string ColumnNameSign4 { get; set; }
        
        public string ColumnNameSign5 { get; set; }
        
        public string CategoryColumnName { get; set; }
    }
    
    public class AddTableViewModel : ViewModelBase
    {
        public NewTableInfo TableInfo { get; set; }
        
        public string ServerUrl { get; set; }

        public AddTableViewModel()
        {
            TableInfo = new NewTableInfo();
            Status = new AddStatusInfoModel();
        }
    }
}