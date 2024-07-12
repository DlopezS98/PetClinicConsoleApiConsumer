using System.Text.Json;
using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public class SecondQuestion(Services services) : BaseHttpRequest(services), IQuestion
{
    public async Task Run()
    {
        try
        {
            HttpResponseMessage reponse = await _httpClient.GetAsync(services.PetClinic.Vets);
            if (!reponse.IsSuccessStatusCode) throw new Exception("Failed to get vets");

            string content = await reponse.Content.ReadAsStringAsync();
            List<Vet> vets = JsonSerializer.Deserialize<List<Vet>>(content, baseSerializedOptions) ?? [];

            Console.WriteLine(vets.Count);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}