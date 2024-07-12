using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public class FirstQuestion(Services services) : BaseHttpRequest(services), IQuestion
{
    public async Task Run()
    {
        try
        {
            HttpResponseMessage reponse = await _httpClient.GetAsync(services.PetClinic.Vets);
            if (!reponse.IsSuccessStatusCode) throw new Exception("Failed to get vets");

            string content = await reponse.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}