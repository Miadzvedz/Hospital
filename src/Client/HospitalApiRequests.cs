using Client.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Client;

public static class HospitalApiRequests
{
    static private readonly HttpClient Client = new HttpClient();

    static HospitalApiRequests()
    {
        Client.BaseAddress = new Uri(AppConfig.HospitalRoutes.BaseUrl);
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }


    public static async Task PostBatch(List<Patient> patients)
    {
        string path = AppConfig.HospitalRoutes.Endpoints.PatientBatch;

        try
        {
            var response = await Client.PostAsJsonAsync(path, patients);
            response.EnsureSuccessStatusCode();

            //change console text to event
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[SUCCESS] The patient batch was successfully sent and processed by the API.");
            Console.ResetColor();

        }
        catch (HttpRequestException ex) 
        {
            //change console text to event
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] API request error. Status: {ex.StatusCode}. Message: {ex.Message}");
            Console.ResetColor();

            throw;
        }
    }
}
