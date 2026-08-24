using ConsoleApp.Models;
using ConsoleClient;
using System.Net.Http.Headers;
using System.Net.Http.Json;



namespace Client;

class Program
{
    static readonly HttpClient Client = new HttpClient();


    static async Task Main(string[] args)
    {

        string baseUrl = AppConfig.HospitalRoutes.BaseUrl;
        string patientBatch = AppConfig.HospitalRoutes.Endpoints.PatientBatch;

        Client.BaseAddress = new Uri(baseUrl);
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        var patientList = PtientCreator.CreatePatients(100);

        await PostBatch(patientList, patientBatch);

        Console.ReadLine();       
    }



    private static async Task PostBatch(List<Patient> patients, string endpoint)
    {
        
        if (!patients.Any())
            Console.WriteLine("The patient list is empty");// переписать на эксепшен

        if (string.IsNullOrEmpty(endpoint))
            Console.WriteLine("Request path not provided");// переписать на эксепшен


        try
        {
            var response = await Client.PostAsJsonAsync(endpoint, patients);
            response.EnsureSuccessStatusCode();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Adding patients successfully! Status code {response.StatusCode}");

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Adding patients error: {ex.Message}");
        }

        Console.ResetColor();
    }
}
