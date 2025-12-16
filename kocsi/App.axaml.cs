using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using kocsi.Models;
using kocsi.Persistence;
using kocsi.ViewModels;
using kocsi.Views;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Chrome;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using kocsi.Models;
using kocsi.Persistence;
using kocsi.ViewModels;
using kocsi.Views;

namespace kocsi;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        BindingPlugins.DataValidators.RemoveAt(0);

        TextDataAccess dataAccess = new TextDataAccess();
        MainModel mainModel = new MainModel(dataAccess);
        MainViewModel viewModel = new MainViewModel(mainModel);


        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MainView view = new MainView();
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel,
                Content = view
            };

            // Navigáció: Részletek oldal
            viewModel.ChangeView += (s, e) =>
            {
                // A DescriptionPage DataContext-je most már az adott Car lesz
                desktop.MainWindow.DataContext = e.Car;
                desktop.MainWindow.Content = new DescriptionPage();
            };

            // Navigáció: Vissza a főoldalra
            viewModel.BackToMainView += (s, e) =>
            {
                desktop.MainWindow.DataContext = viewModel;
                desktop.MainWindow.Content = new MainView();
            };

            // Mentés
            viewModel.SaveEvent += async (s, e) =>
            {
                TopLevel topLevel = TopLevel.GetTopLevel(view);
                var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
                {
                    Title = "Save Cars",
                    DefaultExtension = "txt",
                });
                if (file is not null)
                {
                    mainModel.Save(file.Path.AbsolutePath);
                }
            };

            // Betöltés
            viewModel.LoadEvent += async (s, e) =>
            {
                TopLevel topLevel = TopLevel.GetTopLevel(view);
                var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Load Cars",
                    AllowMultiple = true,
                });
                if (file is not null && file.Count > 0)
                {
                    mainModel.Load(file[0].Path.AbsolutePath);
                }
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = viewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}