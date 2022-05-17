using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using XamlControlsGallery.Contexts.AddTable;
using XamlControlsGallery.ViewModels;

using Client = MlServer.Client.Models;

namespace XamlControlsGallery.Pages
{

    public class AddTablePage2 : ConnectionsComboBoxBase<AddTableViewModel>
    {
        public AddTablePage2()
        {
            DataContext = new AddTableViewModel();
            this.InitializeComponent();
        }

        public void InitComboBox()
        {
            ComboBox = this.Find<ComboBox>("Select");
            ComboBox.Items = new List<ConnectionInfo>();
            ComboBox.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            InitComboBox();
        }

        public override void ValidateNotNull(AddTableViewModel viewModel)
        {
            CheckNotNull(viewModel.TableInfo.TableName, "Table name");
            CheckNotNull(viewModel.TableInfo.CategoryColumnName, "Category Column");
            CheckNotNull(viewModel.ServerUrl, "Server url");
        }

        public void AddTableButtonClick(object sender, RoutedEventArgs args)
        {
            var viewModel = InitViewModel();

            if (viewModel != null)
            {
                try
                {
                    var connection = GetChoosenConnection();
                    viewModel.ServerUrl = connection.Url;
                    
                    ValidateNotNull(viewModel);
                    var tableContext = new AddTableContext(
                        viewModel.ServerUrl, viewModel.AddStatus);
                    
                    tableContext.AddTableAsync(viewModel);
                }
                catch (Exception e)
                {
                    viewModel.AddStatus(e.Message);
                }
            }
        }
    }
}