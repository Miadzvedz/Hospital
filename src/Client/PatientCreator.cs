using Bogus;
using Bogus.DataSets;
using Client.Extensions;
using Client.Models;


namespace Client;

public static class PatientCreator
{
    public static List<Patient> CreatePatients(int count)
    {
        string locale = "ru";

        var nameFemaleGenerator = new Faker<PatientName>(locale)
            .StrictMode(true)
            .RuleFor(i => i.Use, set => "official")
            .RuleFor(i => i.Family, f => f.Name.LastName(Name.Gender.Female))
            .RuleFor(i => i.Given, f => new List<string>
            {
                f.Name.FirstName(Name.Gender.Female), 
                f.Name.Patronymic(Name.Gender.Female)
            });

        var nameMaleGenerator = new Faker<PatientName>(locale)
            .StrictMode(true)
            .RuleFor(i => i.Use, set => "official")
            .RuleFor(i => i.Family, f => f.Name.LastName(Name.Gender.Male))
            .RuleFor(i => i.Given, f => new List<string>
            {
                f.Name.FirstName(Name.Gender.Male),
                f.Name.Patronymic(Name.Gender.Male)
            });

        var nameGenerators = new List<Func<PatientName>>()
            {
                () => nameFemaleGenerator.Generate(),
                () => nameMaleGenerator.Generate()
            };
      
        var patientGenerator = new Faker<Patient>(locale)
            .StrictMode(true)
            .RuleFor(i => i.BirthDate, f => f.Person.DateOfBirth)
            .RuleFor(i => i.Active, f => f.Random.Bool())
            .RuleFor(i => i.Gender, f => f.PickRandom<Genders>())
            .RuleFor(i => i.Name, (f, i) => i.Gender switch
            {
                Genders.Male => nameMaleGenerator.Generate(),
                Genders.Female => nameFemaleGenerator.Generate(),
                _ => nameGenerators[Random.Shared.Next(nameGenerators.Count)].Invoke()
            });
        
        return patientGenerator.Generate(count);
    }
}
