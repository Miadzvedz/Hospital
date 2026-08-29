using Bogus;
using Bogus.DataSets;
using static Bogus.DataSets.Name;

namespace Client.Extensions;

public static class BogusExtensions
{
    static private List<string> FemalePatronymic => new() {
        "Сергеевна",
        "Владимировна",
        "Семёновна",
        "Дмитриевна", 
        "Александровна", 
        "Ивановна", 
        "Генадьевна", 
        "Анатольевна",
        "Петровна",
        "Кузьминична" };

    static private List<string> MalePatronymic => new() {
        "Сергеевич", 
        "Владимирович", 
        "Семёновнович", 
        "Дмитриевич",
        "Александрович",
        "Иванович", 
        "Генадьевич", 
        "Анатольевич", 
        "Петрович", 
        "Валерьевич" };

    public static string Patronymic(this Name name, Gender? gender = null)
    {
        gender ??= new Faker().PickRandom<Gender>();

        return gender == Gender.Female
            ? new Faker().PickRandom(FemalePatronymic)
            : new Faker().PickRandom(MalePatronymic);
    }
}
