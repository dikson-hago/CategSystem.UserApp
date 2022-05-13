using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using XamlControlsGallery.Contexts.TablesStatuses;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Pages;

public class TablesStatusesPage : MlCategSystemPagesBase<TablesStatusesViewModel>
{
    public TablesStatusesPage()
    {
        DataContext = new TablesStatusesViewModel();
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void ValidateNotNull(TablesStatusesViewModel viewModel)
    {
        throw new System.NotImplementedException();
    }

    public void UpdadeTablesStatusesButtonClick(object sender, RoutedEventArgs args)
    {
        var viewModel = InitViewModel();

        if (viewModel != null)
        {
            var tablesStatusesContext = new TablesStatusesContext("", viewModel.AddStatus);

            tablesStatusesContext.GetTablesStatuses(viewModel);
        }
    }
}