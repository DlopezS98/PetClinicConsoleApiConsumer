using System.Net.Http.Headers;
using System.Text.Json;
using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public class EighthQuestion(Services services) : BaseHttpRequest(services), IQuestion
{
    async Task<List<Specialty>> GetSpecialtiesAsync()
    {
        HttpResponseMessage response = await client.GetAsync("api/specialties");
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        List<Specialty> specialities = JsonSerializer.Deserialize<List<Specialty>>(jsonString, baseSerializedOptions) ?? [];
        return specialities;
    }

    public async Task Run()
    {
        try
        {
            client.BaseAddress = new Uri("http://localhost:9966/petclinic/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var clientId = "admin";
            var clientSecret = "admin";

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authenticationString));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);

            // Buscar todos
            List<Specialty> specialityList = await GetSpecialtiesAsync();
            Console.WriteLine("specialityList size -> " + specialityList.Count);

            foreach (Specialty s in specialityList) Console.WriteLine("Speciality -> " + s);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.ToString());
        }
    }
}