using ConsoleClient;


namespace Client;



class Program
{
    static async Task Main(string[] args)
    {
        var patients = PtientCreator.CreatePatients(1);

        await HospitalApiRequests.PostBatch(patients);

        Console.ReadLine();       
    }
}