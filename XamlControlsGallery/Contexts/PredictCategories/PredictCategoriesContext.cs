using System;
using System.Threading.Tasks;
using CategorisationSystem.ApplicationServices.Storages;
using XamlControlsGallery.Mappers;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Contexts.PredictCategories;

public class PredictCategoriesContext : BaseContext
{
    private CreateExcelContext _createExcelContext;
    private ServersStorage _serversStorage;
    
    public PredictCategoriesContext(string url, Action<string> status) : 
        base(url, status)
    {
        _createExcelContext = new CreateExcelContext();
        _serversStorage = ServersStorage.GetInstance();
    }

    public async Task PredictAsync(PredictCategoryViewModel viewModel)
    {
        try
        {
            Status("Please wait...");
            var fileParserContext =
                new FileParserForPredictionsContext(viewModel.TableName, viewModel.ServerUrl);
            var file = fileParserContext.Parse(viewModel.FilePath);

            var table =
                await Server.PredictCategory(file.MapToObjectInfoForPredict(), viewModel.TableName);
            var headers = _serversStorage.GetTableColumns(viewModel.TableName, viewModel.ServerUrl);

            var newFile = table.MapToExcel(headers);
            _createExcelContext.Create(newFile, viewModel.TargetFolder);

            Status("Prediction finish");
        }
        catch(Exception e)
        {
            Status(e.Message);
        }
    }
}