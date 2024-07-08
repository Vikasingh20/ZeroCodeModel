// See https://aka.ms/new-console-template for more information
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class FileUtils
{
    //Console.WriteLine("Hello, World!");

    string url = "https://www.iiiexams.org/NotificationDocs/TBX-Schedule.xlsx"; // Replace with the actual HTTP path
    string path = @"I:\ExcelDmp\TBX-Schedule.xlsx";
    //await DownloadFileAsync(url,path);

    //Console.WriteLine("File downloaded successfully!");

    ////*[@id="ResponseFile"]/label[1]/a
    static async Task DownloadFileAsync(string url, string mPath)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode(); // Ensure successful response status code

                    using (HttpContent content = response.Content)
                    {
                        byte[] fileBytes = await content.ReadAsByteArrayAsync();

                        // Specify the local path where you want to save the downloaded file
                        //string localPath = "C:\\path\\to\\save\\file.txt"; // Replace with your desired local path

                        // Write the file bytes to the local file
                        System.IO.File.WriteAllBytes(mPath, fileBytes);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
            }
        }
    }
}

