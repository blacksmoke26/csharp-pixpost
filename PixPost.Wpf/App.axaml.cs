// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using PixPost.Wpf.Core.Base;
using PixPost.Wpf.ViewModels;
using PixPost.Wpf.Views;

namespace PixPost.Wpf;

public partial class App : Application {
  public override void Initialize() {
    AvaloniaXamlLoader.Load(this);
  }

  public override void OnFrameworkInitializationCompleted() {
    // configure services
    ServicesManager servicesManager = new();
    servicesManager.Build();

    DataTemplates.Add(Ioc.Default.GetRequiredService<ViewLocator>());

    // Remove vanilla validation plugins

    #region ValidationPlugins

    // https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins

    // Get an array of plugins to remove
    var dataValidationPluginsToRemove =
      BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

    // remove each entry found
    foreach (var plugin in dataValidationPluginsToRemove) {
      BindingPlugins.DataValidators.Remove(plugin);
    }

    #endregion

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
      // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
      // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
      DisableAvaloniaDataAnnotationValidation();
      desktop.MainWindow = new MainWindow {
        DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>(),
      };
    }

    base.OnFrameworkInitializationCompleted();
  }

  private void DisableAvaloniaDataAnnotationValidation() {
    // Get an array of plugins to remove
    var dataValidationPluginsToRemove =
      BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

    // remove each entry found
    foreach (var plugin in dataValidationPluginsToRemove) {
      BindingPlugins.DataValidators.Remove(plugin);
    }
  }

  /// <summary>
  /// Register DI services<br/>
  /// Reference: https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection
  /// </summary>
  private void ConfigureServices() {
    ServiceCollection services = new();

    #region View Models

    services.AddSingleton<ViewLocator>();
    services.AddSingleton<MainWindowViewModel>();

    #endregion

    #region Miscellaneous

    services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

    #endregion

    Ioc.Default.ConfigureServices(services.BuildServiceProvider());
  }
}