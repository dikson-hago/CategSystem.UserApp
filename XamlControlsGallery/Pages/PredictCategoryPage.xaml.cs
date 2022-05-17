using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using XamlControlsGallery.Contexts.PredictCategories;
using XamlControlsGallery.Contexts.TemplateForAdd;
using XamlControlsGallery.Models;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Pages
{
    public class PredictCategoryPage : TablesComboBoxBase<PredictCategoryViewModel>
    {
        public PredictCategoryPage()
        {
            DataContext = new PredictCategoryViewModel();
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

        public override void ValidateNotNull(PredictCategoryViewModel viewModel)
        {
            CheckNotNull(viewModel.TargetFolder, "Target folder");
            CheckNotNull(viewModel.FilePath, "File path");
            CheckNotNull(viewModel.ServerUrl, "Server url");
        }

        public void PredictCategoryButtonClick(object sender, RoutedEventArgs args)
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
                    var predictCategoryContext =
                        new PredictCategoriesContext(viewModel.ServerUrl,
                            viewModel.AddStatus);

                    predictCategoryContext.PredictAsync(viewModel);
                }
                catch (Exception e)
                {
                    viewModel.AddStatus(e.Message);
                }
            }
        }

        private void ValidateNotNullForDownload(PredictCategoryViewModel viewModel)
        {
            CheckNotNull(viewModel.TableName, "Table");
            CheckNotNull(viewModel.TargetFolder, "Target template folder");
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

                    ValidateNotNullForDownload(viewModel);
                    viewModel.AddStatus("Please, wait...");
                    var template =
                        new TemplateForPredict(viewModel.ServerUrl, viewModel.AddStatus);

                    
                    template.CreateTemplate(viewModel.TableName, viewModel.TargetTemplateFolder);
                    
                    
                    viewModel.AddStatus("Template downloaded");
                }
                catch (Exception e)
                {
                    viewModel.AddStatus(e.Message);
                }
            }
        }

    }
}