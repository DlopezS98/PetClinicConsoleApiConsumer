using System.Text.Json;
using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public class ThirdQuestion(Services services) : BaseHttpRequest(services), IQuestion
{
    public async Task Run()
    {
        try
        {
            HttpResponseMessage reponse = await client.GetAsync(services.PetClinic.Vets);
            // HttpResponseMessage reponse = await _httpClient.GetAsync(services.PetClinic.Visits);
            if (!reponse.IsSuccessStatusCode) throw new Exception("Failed to get vets");

            string content = await reponse.Content.ReadAsStringAsync();
            List<Visit> visits = JsonSerializer.Deserialize<List<Visit>>(content, baseSerializedOptions) ?? [];

            int counter = 0;
            foreach (Visit v in visits)
            {
                Console.WriteLine("Visita " + counter++ + " -> " + v);
            }

        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}