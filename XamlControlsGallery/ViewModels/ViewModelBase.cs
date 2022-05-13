using ReactiveUI;
using XamlControlsGallery.Models;

namespace XamlControlsGallery.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public AddStatusInfoModel Status { get; set; }

        protected ViewModelBase()
        {
            Status = new AddStatusInfoModel();
        }
        
        public void AddStatus(string status)
        {
            Status.AddStatusInfo(status);
        }
    }
}
