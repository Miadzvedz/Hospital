using Client.Models;

namespace Client;

class Program
{
    static async Task Main(string[] args)
    {
        await HealthCheck.TryConnectToApiAsync(AppConfig.HospitalRoutes.BaseUrl);

        List<Patient> patients = PatientCreator.CreatePatients(100);

        await HospitalApiRequests.PostBatch(patients);
    }
}