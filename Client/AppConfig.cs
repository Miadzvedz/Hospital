using Client.Route;
using System;
using System.Text.Json;

namespace Client;

public static class AppConfig
{
    public static Routes HospitalRoutes { get; private set; }

    static AppConfig()
    {
        HospitalRoutes = GetHospitalApiRoutes() 
            ?? throw new NullReferenceException("Critical startup error: routes were not provided");
    }


    private static Routes? GetHospitalApiRoutes() =>
        GetHospitalApiRoutesFromContainer() 
        ?? GetHospitalApiRoutesFromLocal();


    private static Routes? GetHospitalApiRoutesFromLocal()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string urlPath = Path.Combine(basePath, "appsettings.json");

        using FileStream fs = File.OpenRead(urlPath);
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<Routes>(fs, options);
    }


    private static Routes? GetHospitalApiRoutesFromContainer()
    {
        string? jsonConfig = Environment.GetEnvironmentVariable("HOSPITAL_API_CONFIG");

        if (string.IsNullOrWhiteSpace(jsonConfig)) return null;
            
        var jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<Routes>(jsonConfig, jsonOptions);
    }
}
