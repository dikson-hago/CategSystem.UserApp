using System;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using XamlControlsGallery.Contexts.AddServer;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Pages
{
    public class AddServerPage : MlCategSystemPagesBase<AddServerViewModel>
    {
        public AddServerPage()
        {
            DataContext = new AddServerViewModel();
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void ValidateNotNull(AddServerViewModel viewModel)
        {
            CheckNotNull(viewModel.Url, "Server Url");
        }

        public void AddServerButtonClick(object sender, RoutedEventArgs args)
        {
            var viewModel = InitViewModel();

            if (viewModel != null)
            {
                try
                {
                    ValidateNotNull(viewModel);
                    
                    var tableContext =
                        new AddServerContext(viewModel.Url, viewModel.AddStatus);
                    
                    tableContext.AddServer();
                }
                catch(Exception e)
                {
                    viewModel.AddStatus(e.Message);
                }
            }
        }
    }
}