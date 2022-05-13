using System;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using XamlControlsGallery.Contexts.AddTable;
using XamlControlsGallery.ViewModels;

using Client = MlServer.Client.Models;

namespace XamlControlsGallery.Pages
{
    public class AddTablePage : MlCategSystemPagesBase<AddTableViewModel>
    {
        public AddTablePage()
        {
            DataContext = new AddTableViewModel();
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void ValidateNotNull(AddTableViewModel viewModel)
        {
            CheckNotNull(viewModel.TableInfo.TableName, "ServerUrl");
        }

        public void AddTableButtonClick(object sender, RoutedEventArgs args)
        {
            var viewModel = InitViewModel();

            if (viewModel != null)
            {
                try
                {
                    var tableContext = new AddTableContext(
                        viewModel.ServerUrl, viewModel.AddStatus);
                    tableContext.AddTableAsync(viewModel);
                }
                catch (Exception e)
                {
                    
                }
            }
        }
    }
}