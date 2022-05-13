using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using XamlControlsGallery.ViewModels;

namespace XamlControlsGallery
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }
        
        public static Window ResolveViewFromViewModel<T>(T vm) where T : AddObjectsViewModel
        {
            var name = vm.GetType().AssemblyQualifiedName;
            var type = Type.GetType(name);
            return type != null ? (Window)Activator.CreateInstance(type)! : null;
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}