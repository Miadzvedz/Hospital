namespace Client;

class Program
{
    static async Task Main(string[] args)
    {
        await HealthCheck.TryConnectToApiAsync(AppConfig.HospitalRoutes.BaseUrl);




        var patients = PatientCreator.CreatePatients(1);

        await HospitalApiRequests.PostBatch(patients);   
    }
}