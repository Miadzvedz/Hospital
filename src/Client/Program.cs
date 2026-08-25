using ConsoleClient;


namespace Client;



class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Write count patients");
        string input = Console.ReadLine();

        if(int.TryParse(input, out int count))
        {
            var patients = PtientCreator.CreatePatients(count);
            await HospitalApiRequests.PostBatch(patients);
        }
        else
        {
            Console.WriteLine("Incorrect value, enter a number please.");
        }
            Console.ReadLine();       
    }
}