using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public class FifthQuestion(Services services) : BaseHttpRequest(services), IQuestion
{
    async Task<Speciality> CreateSpecialityAsync(Speciality speciality)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync(
            "api/specialties", speciality);
        response.EnsureSuccessStatusCode();
        string jsonContent = response.Content.ReadAsStringAsync().Result;
        Speciality dev = JsonSerializer.Deserialize<Speciality>(jsonContent, baseSerializedOptions) ?? throw new Exception("Error deserializing the response");
        return dev;
    }

    async Task<List<Speciality>> GetSpecialitiesAsync()
    {
        HttpResponseMessage response = await client.GetAsync(
            "api/specialties");
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        List<Speciality> specialities = JsonSerializer.Deserialize<List<Speciality>>(jsonString, baseSerializedOptions) ?? [];
        return specialities;
    }

    async Task<Speciality> GetSpecialityAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync(
            "api/specialties" + "/" + id);
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        Speciality dev = JsonSerializer.Deserialize<Speciality>(jsonString, baseSerializedOptions) ?? throw new Exception("Error deserializing the response");
        return dev;
    }

    static Speciality? FindByName(List<Speciality> specialityList, string name)
    {
        foreach (Speciality s in specialityList)
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

            Speciality specialityToBeCreated = new Speciality()
            {
                Id = 123,
                Name = "MySpecialityDemo"
            };

            // Añadir
            Speciality specialityCreated = await CreateSpecialityAsync(specialityToBeCreated);
            Console.WriteLine("specialityCreated -> " + specialityCreated);

            // Buscar todos
            List<Speciality> specialityList = await GetSpecialitiesAsync();
            Console.WriteLine("specialityList size -> " + specialityList.Count);
            Speciality specialityFounded = FindByName(specialityList, "MySpecialityDemo") ?? throw new Exception("Speciality not found");
            Console.WriteLine("specialityFounded -> " + specialityFounded);

            // Buscar por id
            Speciality specialityFoundedIndividual = await GetSpecialityAsync(specialityFounded.Id);
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
