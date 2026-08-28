using Bogus;
using Bogus.DataSets;
using System.Text.Json;
using static Bogus.DataSets.Name;

namespace Client.Extensions;

public static class BogusExtensions
{
    static private List<string> FemalePatronymic { get; }
    static private List<string> MalePatronymic { get; }

    static BogusExtensions()
    {
        string urlpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "patronymics.json");

        using FileStream fs = File.OpenRead(urlpath);
        using JsonDocument doc = JsonDocument.Parse(fs);

        FemalePatronymic = doc.RootElement.GetProperty("femalePatronymic").Deserialize<List<string>>() ?? new();
        MalePatronymic = doc.RootElement.GetProperty("malePatronymic").Deserialize<List<string>>() ?? new();
    }


    public static string Patronymic(this Name name, Gender? gender = null)
    {
        gender ??= new Faker().PickRandom<Gender>();

        return gender == Gender.Female
            ? new Faker().PickRandom(FemalePatronymic)
            : new Faker().PickRandom(MalePatronymic);
    }
}
