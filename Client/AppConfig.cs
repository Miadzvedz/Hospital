using Client.Options;
using System.Text.Json;

namespace Client;

public static class AppConfig
{
    public static HospitalApiOptions HospitalRoutes { get; private set; }

    static AppConfig()
    {
        HospitalRoutes = GetHospitalApiOptions();

    }

    private static HospitalApiOptions GetHospitalApiOptions()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string urlPath = Path.Combine(basePath, "appsettings.json");

        using FileStream fs = File.OpenRead(urlPath);
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<HospitalApiOptions>(fs, options)
            ?? throw new FileNotFoundException();
    }
}
