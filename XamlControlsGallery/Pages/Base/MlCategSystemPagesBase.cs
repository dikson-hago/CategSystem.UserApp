using System;
using Avalonia.Controls;
using JetBrains.Annotations;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery.Pages;

public abstract class MlCategSystemPagesBase<TViewModel> : UserControl
where TViewModel : ViewModelBase
{
    public MlCategSystemPagesBase()
    {
    }

    [CanBeNull]
    public TViewModel InitViewModel()
    {
        try
        {
            var result = DataContext as TViewModel;
            return result;
        }
        catch
        {
            return null;
        }
    }

    public void CheckNotNull(string value, string valueName)
    {
        if (value is null)
        {
            throw new Exception(valueName + " couldn't be null");
        }
    }

    public abstract void ValidateNotNull(TViewModel viewModel);
}