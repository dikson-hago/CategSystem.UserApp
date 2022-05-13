using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using XamlControlsGallery.Contexts.DowloadTable;
using XamlControlsGallery.Models;
using XamlControlsGallery.ViewModels;

using Client = MlServer.Client.Models;

namespace XamlControlsGallery.Pages
{
    public class DownloadTablePage : TablesComboBoxBase<DownloadTableViewModel>
    {
        public DownloadTablePage()
        {
            DataContext = new DownloadTableViewModel();
            this.InitializeComponent();
        }

        public void InitComboBox()
        {
            ComboBox = this.Find<ComboBox>("Select");
            ComboBox.Items = new List<TableServerInfo>();
            ComboBox.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            InitComboBox();
        }
        
        public override void ValidateNotNull(DownloadTableViewModel viewModel)
        {
            CheckNotNull(viewModel.TargetFolderPath, "Target folder path");
        }

        public void DownloadTableButtonClick(object sender, RoutedEventArgs args)
        {
            var viewModel = InitViewModel();

            if (viewModel != null)
            {
                try
                {
                    var table = GetChoosenTable();
                    viewModel.TableName = table.TableName;
                    viewModel.ServerUrl = table.ServerUrl;

                    var tableContext = new DownloadTableContext(
                        viewModel.ServerUrl, viewModel.AddStatus);
                    tableContext.Download(viewModel.TableName, viewModel.TargetFolderPath);
                }
                catch (Exception e)
                {
                    viewModel.AddStatus(e.Message);
                }
            }
        }
    }
}