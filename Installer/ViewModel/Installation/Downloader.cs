using System.IO;
using System.Net.Http;
// ReSharper disable ConvertToUsingDeclaration

namespace Installer.ViewModel.Installation;

public class Downloader
{
    private readonly HttpClient _client = new();

    public async Task DownloadPackages(string url, string targetDirectory, IProgress<float> progress)
    {
        var response = await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        var contentLength = response.Content.Headers.ContentLength ?? -1L;

        // ReSharper disable once UseAwaitUsing
        using (var stream = await response.Content.ReadAsStreamAsync())
        {
            const int readChunkSize = 8192;
            var totalBytesRead = 0L;

            await using (var fs = new FileStream(targetDirectory, FileMode.Create, FileAccess.Write, FileShare.None,
                             readChunkSize, true))
            {
                var buffer = new byte[readChunkSize];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer.AsMemory(0, readChunkSize))) > 0)
                {
                    await fs.WriteAsync(buffer.AsMemory(0, bytesRead));
                    totalBytesRead += bytesRead;
                    if (contentLength != -1)
                    {
                        progress.Report((float)totalBytesRead / contentLength);
                    }
                }
            }
        }
    }
}