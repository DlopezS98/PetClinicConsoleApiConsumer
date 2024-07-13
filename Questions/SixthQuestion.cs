using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public class SixthQuestion(Services services) : BaseHttpRequest(services), IQuestion
{
    async Task<Specialty> CreateSpecialtyAsync(Specialty speciality)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("api/specialties", speciality);
        response.EnsureSuccessStatusCode();
        string jsonContent = response.Content.ReadAsStringAsync().Result;
        Specialty dev = JsonSerializer.Deserialize<Specialty>(jsonContent, baseSerializedOptions)!;
        return dev;
    }

    async Task<List<Specialty>> GetSpecialtiesAsync()
    {
        HttpResponseMessage response = await client.GetAsync("api/specialties");
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        List<Specialty> specialities = JsonSerializer.Deserialize<List<Specialty>>(jsonString, baseSerializedOptions) ?? [];
        return specialities;
    }

    async Task<Specialty> GetSpecialtyAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync("api/specialties" + "/" + id);
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        Specialty dev = JsonSerializer.Deserialize<Specialty>(jsonString, baseSerializedOptions)!;
        return dev;
    }

    Specialty? FindByName(List<Specialty> specialityList, string name)
    {
        foreach (Specialty s in specialityList)
        {
            if (s.Name.Equals(name))
                return s;
        }
        return null;
    }

    async Task<Specialty> UpdateSpecialtyAsync(Specialty speciality)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync($"api/specialties/{speciality.Id}", speciality);
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        Specialty dev = JsonSerializer.Deserialize<Specialty>(jsonString, baseSerializedOptions)!;
        return dev;
    }

    public async Task Run()
    {
        try
        {
            client.BaseAddress = new Uri("http://localhost:9966/petclinic/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Specialty specialityToBeCreated = new() { Name = "MySpecialityDemo-Pregunta8" };

            // Añadir
            Specialty specialityCreated = await CreateSpecialtyAsync(specialityToBeCreated);
            Console.WriteLine("specialityCreated -> " + specialityCreated);

            // Buscar todos
            List<Specialty> specialityList = await GetSpecialtiesAsync();
            Console.WriteLine("specialityList size -> " + specialityList.Count);
            Specialty specialtyFounded = FindByName(specialityList, "MySpecialityDemo-Pregunta8") ?? throw new Exception("Speciality not found");
            Console.WriteLine("specialityFounded -> " + specialtyFounded);

            // Buscar por id
            Specialty specialtyFoundedIndividual = await GetSpecialtyAsync(specialtyFounded.Id);
            Console.WriteLine("specialityFoundedIndividual -> " + specialtyFoundedIndividual);

            // Actualizar
            specialtyFoundedIndividual.Name = "MySpecialityDemo-Changed";
            Specialty specialityChanged = await UpdateSpecialtyAsync(specialtyFoundedIndividual);
            Console.WriteLine("specialityChanged -> " + specialityChanged);

            // Buscar por id de nuevo
            specialtyFoundedIndividual = await GetSpecialtyAsync(specialtyFounded.Id);
            Console.WriteLine("specialityFoundedIndividual tras cambio -> " + specialtyFoundedIndividual);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.ToString());
        }
    }
}
