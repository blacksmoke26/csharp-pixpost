// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

namespace PixPost;

sealed class Program {
  // Initialization code. Don't use any Avalonia, third-party APIs or any
  // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
  // yet and stuff might break.
  [STAThread]
  public static void Main(string[] args) => BuildAvaloniaApp()
    .StartWithClassicDesktopLifetime(args);

  // Avalonia configuration, don't remove; also used by visual designer.
  public static AppBuilder BuildAvaloniaApp()
    => AppBuilder.Configure<App>()
      .UsePlatformDetect()
      .WithInterFont()
      .LogToTrace();
}