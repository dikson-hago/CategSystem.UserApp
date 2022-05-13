using System.Collections.Generic;
using XamlControlsGallery.Models;

namespace XamlControlsGallery.ViewModels;

public class ObjectCategory
{
    public string TableName { get; set; }
    
    public string Sign1 { get; set; }
    
    public string Sign2 { get; set; }
    
    public string Sign3 { get; set; }
    
    public string Sign4 { get; set; }

    public string Sign5 { get; set; }
}

public class PredictCategoryViewModel : ViewModelBase
{
    public string TableName { get; set; }
    
    public string ServerUrl { get; set; }
    
    public string FilePath { get; set; }
    
    public string TargetFolder { get; set; }
    
    public string TargetTemplateFolder { get; set; }
    
    public CategoriesPredictions Categories { get; set; }

    public void AddPredictedCategories(List<string> categories)
    {
        Categories.ClearPredictions();
        Categories.AddPredictions(categories);
    }
    
    public PredictCategoryViewModel()
    {
        Categories = new CategoriesPredictions();
    }
}