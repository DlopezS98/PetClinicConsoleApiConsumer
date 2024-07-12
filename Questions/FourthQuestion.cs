using System.Text.Json;
using System.Net.Http.Json;
using PetClinicApiConsumer.Models;
using System.Net.Http.Headers;

namespace PetClinicApiConsumer.Questions;

public class FourthQuestion(Services services) : BaseHttpRequest(services), IQuestion
{
    private async Task<Uri> CreateSpecialityAsync(Speciality speciality)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/specialties", speciality);
        response.EnsureSuccessStatusCode();

        // return URI of the created resource.
        return response.Headers.Location ?? throw new Exception("No location header in response");
    }


    public async Task Run()
    {
        try
        {
            _httpClient.BaseAddress = new Uri("http://localhost:9966/petclinic/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Speciality speciality = new Speciality()
            {
                Id = 100,
                Name = "MySpec100"
            };

            var url = await CreateSpecialityAsync(speciality);
            Console.WriteLine($"Created at {url}");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.ToString());
        }
    }
}