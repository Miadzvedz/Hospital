using Bogus;
using Bogus.DataSets;
using Client.Models;
using System.Text.Json;

namespace Client;

public class PatientCreator
{
    static private  List<string> FemalePatronymic { get; }
    static private List<string> MalePatronymic { get; }


    static PatientCreator()
    {
        string urlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "patronymics.json");

        using FileStream fs = File.OpenRead(urlPath);
        using JsonDocument doc = JsonDocument.Parse(fs);

        FemalePatronymic = doc.RootElement.GetProperty("femalePatronymic").Deserialize<List<string>>() ?? new();
        MalePatronymic = doc.RootElement.GetProperty("malePatronymic").Deserialize<List<string>>() ?? new();
    }


    static public List<Patient> CreatePatients(int count)
    {
        string locale = "ru";
        var patients = new List<Patient>(count);

        var random = new Random();

        var patientGenerator = new Faker<Patient>(locale)
            .StrictMode(true)
            .RuleFor(i => i.Gender, f => f.PickRandom<Genders>())
            .RuleFor(i => i.BirthDate, f => f.Person.DateOfBirth)
            .RuleFor(i => i.Active, f => f.Random.Bool())
            .Ignore(i => i.Name);

        var nameFemaleGenerator = new Faker<PatientName>(locale)
            .StrictMode(true)
            .RuleFor(i => i.Family, f => f.Name.LastName(Name.Gender.Female))
            .RuleFor(i => i.Use, set => "official")
            .RuleFor(i => i.Given, f => f.Make(1, () => f.Name.FirstName(Name.Gender.Female)));

        var nameMaleGenerator = new Faker<PatientName>(locale)
            .StrictMode(true)
            .RuleFor(i => i.Family, f => f.Name.LastName(Name.Gender.Male))
            .RuleFor(i => i.Use, set => "official")
            .RuleFor(i => i.Given, f => f.Make(1, () => f.Name.FirstName(Name.Gender.Male)));


        for (int i = 0; count > i; i++)
        {
            var facePatient = patientGenerator.Generate();

            if(facePatient.Gender == Genders.Male)
            {
                facePatient.Name = nameMaleGenerator.Generate();

                int index = random.Next(MalePatronymic.Count);
                facePatient.Name.Given.Add(MalePatronymic[index]);
            }
            else if (facePatient.Gender == Genders.Female)
            {
                facePatient.Name = nameFemaleGenerator.Generate();
                int index = random.Next(FemalePatronymic.Count);
                facePatient.Name.Given.Add(FemalePatronymic[index]);
            }
            else
            {
                facePatient.Name = nameMaleGenerator.Generate();
                int index = random.Next(FemalePatronymic.Count);
                facePatient.Name.Given.Add(FemalePatronymic[index]);
            }

            patients.Add(facePatient);
        }

        return patients;
    }
}
