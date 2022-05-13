using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamlControlsGallery.Models;

public class PredictedCategory
{
    public string Category { get; set; }

    public PredictedCategory(string category)
    {
        Category = category + "\n";
    }
}

public class CategoriesPredictions
{
    public ObservableCollection<PredictedCategory> Predictions { get; set; }

    public CategoriesPredictions()
    {
        Predictions = new ObservableCollection<PredictedCategory>();
    }

    public void AddPredictions(List<string> categories)
    {
        foreach (var category in categories)
        {
            Predictions.Add(new PredictedCategory(category));
        }
    }

    public void ClearPredictions()
    {
        Predictions.Clear();
    }
}