using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using XamlControlsGallery.Contexts.UpdateTables;
using XamlControlsGallery.Models;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Pages;

public class ConnectionInfo
{
    public string Url { get; set; }
}

public abstract class ConnectionsComboBoxBase<TViewModel> : MlCategSystemPagesBase<TViewModel> 
    where TViewModel : ViewModelBase
{
    protected ComboBox ComboBox;
    
    protected ConnectionInfo GetChoosenConnection()
    {
        var id = ComboBox.SelectedIndex;

        if (id < 0)
        {
            throw new Exception("You did't choose server");
        }
        
        var arr = ComboBox.Items as List<ConnectionInfo>;

        if (arr != null)
        {
            return arr[id];
        }
        
        throw new Exception("Connections list is empty");
    }
    
    private void UpdateComboBox(List<ConnectionInfo> connectionInfos)
    {
        ComboBox.Items = new List<ConnectionInfo>(connectionInfos);
    }
        
    private async Task UpdateConnections(TViewModel viewModel)
    {
        try
        {
            var context = new UpdateTablesContext();
            var connections = await context.GetAllConnections();

            if (connections.Count == 0)
            {
                throw new Exception("Connections list is empty");
            }

            UpdateComboBox(connections.Select(item => new ConnectionInfo()
            {
                Url = item
            }).ToList());
            viewModel.AddStatus("Connections updated\n");
        }
        catch (Exception e)
        {
            viewModel.AddStatus(e.Message);
        }
    }
    
    protected void UpdateConnectionsButtonClick(object sender, RoutedEventArgs args)
    {
        var viewModel = InitViewModel();

        if (viewModel != null)
        {
            try
            {
                viewModel.AddStatus("Please wait...");
                UpdateConnections(viewModel);
            }
            catch (Exception e)
            {
                viewModel.AddStatus(e.Message);
            }
        }
    }

}