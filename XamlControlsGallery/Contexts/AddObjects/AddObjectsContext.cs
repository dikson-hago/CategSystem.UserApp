using System;
using System.Threading.Tasks;
using XamlControlsGallery.Mappers;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Contexts.AddObjects;

class AddObjectsContext : BaseContext
{
    public AddObjectsContext(string url, Action<string> status) 
        : base(url, status)
    {
    }

    public async Task AddFileWithNewObjectsAsync(AddObjectsViewModel viewModel)
    {
        try
        {
            var fileParserContext = 
                new FileParserNewObjectsContext(viewModel.TableName, viewModel.ServerUrl);

            ValidateFilePath(viewModel.FilePath);
            var parsedData = fileParserContext.ParseFile(viewModel.FilePath);

            await TryConnectToServer();
            var errors = await
                Server.AddObjects(
                    parsedData.MapToObjectsInfosForAdd(viewModel.TableName), viewModel.TableName);

            AnalyseServerErrors(errors);
                
            viewModel.AddStatus("New objects added successfully");
        }
        catch (Exception e)
        {
            viewModel.AddStatus(e.Message);
        }
    }
}