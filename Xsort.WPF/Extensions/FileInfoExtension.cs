using System.IO;

namespace Xsort.WPF.Extensions;

public static class FileInfoExtension
{
    public static async Task MoveWithRetry(this FileInfo self, string destinationFileName, int retryCount = 5, int delay = 500)
    {
        for (var i = 0; i <= retryCount; i++)
        {
            try
            {
                self.MoveTo(destinationFileName);
            }
            catch (IOException)
            {
                await Task.Delay(delay);
            }
        }
    }
}