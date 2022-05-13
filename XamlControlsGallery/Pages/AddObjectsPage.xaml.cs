using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using XamlControlsGallery.Contexts.AddObjects;
using XamlControlsGallery.Contexts.TemplateForAdd;
using XamlControlsGallery.Models;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Pages
{
    public class AddObjectsPage : TablesComboBoxBase<AddObjectsViewModel>
    {
        public AddObjectsPage()
        {
            DataContext = new AddObjectsViewModel();
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

        public override void ValidateNotNull(AddObjectsViewModel viewModel)
        {
            CheckNotNull(viewModel.FilePath, "File path");
            CheckNotNull(viewModel.TableName, "File path");
            CheckNotNull(viewModel.ServerUrl, "Server url");
        }

        public void AddFileWithObjectsClick(object sender, RoutedEventArgs args)
        {
            var viewModel = InitViewModel();

            if (viewModel != null)
            {
                try
                {
                    var table = GetChoosenTable();
                    viewModel.TableName = table.TableName;
                    viewModel.ServerUrl = table.ServerUrl;

                    ValidateNotNull(viewModel);
                    
                    var newObjectsContext = 
                        new AddObjectsContext(table.ServerUrl, viewModel.AddStatus);
                    
                    newObjectsContext.AddFileWithNewObjectsAsync(viewModel);
                }
                catch (Exception e)
                {
                    viewModel.AddStatus(e.Message);
                }
            }
        }
        
        public void ValidateNotNullDownload(AddObjectsViewModel viewModel)
        {
            CheckNotNull(viewModel.TableName, "TableName");
            CheckNotNull(viewModel.ServerUrl, "Server url");
            CheckNotNull(viewModel.TemplateFolderPath, "Folder path");
        }
        
        public void DownloadTemplateButtonClick(object sender, RoutedEventArgs args)
        {
            var viewModel = InitViewModel();

            if (viewModel != null)
            {
                try
                {
                    var table = GetChoosenTable();
                    viewModel.TableName = table.TableName;
                    viewModel.ServerUrl = table.ServerUrl;

                    ValidateNotNullDownload(viewModel);
                    var downloadTableContext = 
                        new TemplateForAddContext(viewModel.ServerUrl,
                            viewModel.AddStatus);

                    downloadTableContext.CreateTemplate(viewModel.TableName, viewModel.TemplateFolderPath);
                }
                catch (Exception ex)
                {
                    viewModel.AddStatus(ex.Message);
                }
            }
        }
    }
}