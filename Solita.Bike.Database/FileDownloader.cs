namespace Solita.Bike.Database;

using System.Net.Http;
using System.Threading.Tasks;

public class FileDownloader
{
    private readonly HttpClient m_httpClient;

    public FileDownloader()
    {
        m_httpClient = new HttpClient();
    }

    public async Task DownloadFile(string fileUrl, string savePath)
    {
        if (File.Exists(savePath))
        {
            Console.WriteLine($"File exists in {savePath}, not proceeding download.");
            return;
        }
        
        var directoryPath = Path.GetDirectoryName(savePath);
        if (directoryPath != null)
        {
            Directory.CreateDirectory(directoryPath);
        }

        using var response = await m_httpClient.GetAsync(fileUrl);
        response.EnsureSuccessStatusCode();

        await using var fileStream = File.Create(savePath);
        await response.Content.CopyToAsync(fileStream);
        Console.WriteLine($"File downloaded in {savePath}");
    }
}