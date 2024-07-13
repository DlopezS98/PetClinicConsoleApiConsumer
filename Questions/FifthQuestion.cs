using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public class FifthQuestion(Services services) : BaseHttpRequest(services), IQuestion
{
    async Task<Specialty> CreateSpecialityAsync(Specialty speciality)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "api/specialties", speciality);
        response.EnsureSuccessStatusCode();
        string jsonContent = response.Content.ReadAsStringAsync().Result;
        Specialty dev = JsonSerializer.Deserialize<Specialty>(jsonContent, baseSerializedOptions) ?? throw new Exception("Error deserializing the response");
        return dev;
    }

    async Task<List<Specialty>> GetSpecialitiesAsync()
    {
        HttpResponseMessage response = await client.GetAsync(
            "api/specialties");
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        List<Specialty> specialities = JsonSerializer.Deserialize<List<Specialty>>(jsonString, baseSerializedOptions) ?? [];
        return specialities;
    }

    async Task<Specialty> GetSpecialityAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync(
            "api/specialties" + "/" + id);
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        Specialty dev = JsonSerializer.Deserialize<Specialty>(jsonString, baseSerializedOptions) ?? throw new Exception("Error deserializing the response");
        return dev;
    }

    static Specialty? FindByName(List<Specialty> specialityList, string name)
    {
        foreach (Specialty s in specialityList)
        {
            if (s.Name.Equals(name))
                return s;
        }
        return null;
    }


    public async Task Run()
    {
        try
        {
            client.BaseAddress = new Uri("http://localhost:9966/petclinic/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Specialty specialityToBeCreated = new Specialty()
            {
                Id = 123,
                Name = "MySpecialityDemo"
            };

            // Añadir
            Specialty specialityCreated = await CreateSpecialityAsync(specialityToBeCreated);
            Console.WriteLine("specialityCreated -> " + specialityCreated);

            // Buscar todos
            List<Specialty> specialityList = await GetSpecialitiesAsync();
            Console.WriteLine("specialityList size -> " + specialityList.Count);
            Specialty specialityFounded = FindByName(specialityList, "MySpecialityDemo") ?? throw new Exception("Speciality not found");
            Console.WriteLine("specialityFounded -> " + specialityFounded);

            // Buscar por id
            Specialty specialityFoundedIndividual = await GetSpecialityAsync(specialityFounded.Id);
            Console.WriteLine("specialityFoundedIndividual -> " + specialityFoundedIndividual);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.ToString());
        }
        Console.ReadLine();
    }
}
