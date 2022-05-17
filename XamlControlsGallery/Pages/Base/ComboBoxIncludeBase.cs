using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using XamlControlsGallery.Contexts.UpdateTables;
using XamlControlsGallery.Models;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Pages;

public abstract class TablesComboBoxBase<TViewModel> : MlCategSystemPagesBase<TViewModel>
where TViewModel : ViewModelBase
{
    protected ComboBox ComboBox;

    public TablesComboBoxBase()
    { }

    protected void UpdateComboBox(List<TableServerInfo> tableNames)
    {
        ComboBox.Items = new List<TableServerInfo>(tableNames);
    }
    
    protected void UpdateTablesButtonClick(object sender, RoutedEventArgs args)
    {
        var viewModel = InitViewModel();

        if (viewModel != null)
        {
            viewModel.AddStatus("Please wait...");
            UpdateTables(viewModel);
        }
    }

    protected async Task UpdateTables(TViewModel viewModel)
    {
        var context = new UpdateTablesContext();
        var tableNames = await context.GetAllTables();
        viewModel.AddStatus("Tables updated\n");
        UpdateComboBox(tableNames);
    }

    protected TableServerInfo GetChoosenTable()
    {
        var id = ComboBox.SelectedIndex;

        if (id < 0)
        {
            throw new Exception("You did't choose table");
        }
        
        var arr = ComboBox.Items as List<TableServerInfo>;

        if (arr != null)
        {
            return arr[id];
        }
        
        throw new Exception("Error: tables list is empty");
    }

    public abstract override void ValidateNotNull(TViewModel viewModel);
}