// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using System.Net.Http;

namespace PixPost.Wpf.Core.Helpers;

public static class FileHelper {
  /// <summary>
  /// Download content from the given uri and save it in a file
  /// </summary>
  /// <param name="url">The uri</param>
  /// <param name="path">Optional file path to save downloaded file</param>
  /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
  /// <returns>The absolute file path to downloaded content</returns>
  public static async Task<string?> DownloadFileAsync(string url, string? path = null,
    CancellationToken cancellationToken = default) {
    var filename = path ?? Path.GetTempFileName();

    try {
      using HttpClient client = new();
      var downloadStream = await client.GetStreamAsync(url, cancellationToken);

      await using var stream = File.OpenWrite(filename);
      await downloadStream.CopyToAsync(stream, cancellationToken);

      return filename;
    }
    catch {
      return null;
    }
  }
}