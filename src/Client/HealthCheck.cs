using System.Net.Sockets;

namespace Client;

public static class HealthCheck
{

    public static async Task TryConnectToApiAsync(string baseUrl)
    {
        Uri uri = new Uri(baseUrl);

        Console.WriteLine($"Waiting for the API to be fully ready ({uri.OriginalString})...");

        while (true)
        {
            try
            {
                using var client = new TcpClient();

                await client.ConnectAsync(uri.Host, uri.Port);

                Console.WriteLine("API is ready to work!");
                break;
            }
            catch (SocketException)
            {
                Console.WriteLine("API not available. Wait 1 second...");
                await Task.Delay(1000);
            }
        }
    }

}
