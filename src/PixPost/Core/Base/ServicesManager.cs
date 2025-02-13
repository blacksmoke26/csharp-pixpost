// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using CommunityToolkit.Mvvm.Messaging;
using PixPost.ViewModels;

namespace PixPost.Core.Base;

public sealed class ServicesManager {
  /// <summary>
  /// Initializes a new instance of the ServicesInitializer
  /// </summary>
  private readonly ServiceCollection _services = new();

  /// <summary>
  /// Get services collection instance
  /// </summary>
  public ServiceCollection GetInstance() => _services;

  /// <summary>
  /// Register miscellaneous services
  /// </summary>
  private void ConfigureMiscellaneous() {
    _services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
  }

  /// <summary>
  /// Build services and register in a dependency injection mechanism
  /// </summary>
  public void Build() {
    // view models
    ConfigureViewModels();

    // miscellaneous
    ConfigureMiscellaneous();

    Ioc.Default.ConfigureServices(_services.BuildServiceProvider());
  }

  /// <summary>
  /// Register view-model services
  /// </summary>
  private void ConfigureViewModels() {
    _services.AddSingleton<ViewLocator>();
    _services.AddSingleton<MainWindowViewModel>();
  }
}