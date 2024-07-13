using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public class SeventhQuestion(Services services) : BaseHttpRequest(services), IQuestion
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

    async Task<string> UpdateSpecialtyAsync(Specialty speciality)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync($"api/specialties/{speciality.Id}", speciality);
        response.EnsureSuccessStatusCode();
        string jsonString = await response.Content.ReadAsStringAsync();
        // Specialty dev = JsonSerializer.Deserialize<Specialty>(jsonString, baseSerializedOptions)!;
        return jsonString;
    }

    async Task<HttpStatusCode> DeleteSpecialityAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync("api/specialties/" + id);
        return response.StatusCode;
    }

    public async Task Run()
    {
        try
        {
            client.BaseAddress = new Uri("http://localhost:9966/petclinic/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Specialty specialityToBeCreated = new() { Name = "MySpecialityDemo-Pregunta9" };

            // Añadir
            Specialty specialityCreated = await CreateSpecialtyAsync(specialityToBeCreated);
            Console.WriteLine("specialityCreated -> " + specialityCreated);

            // Buscar todos
            List<Specialty> specialityList = await GetSpecialtiesAsync();
            Console.WriteLine("specialityList size -> " + specialityList.Count);
            Specialty specialtyFounded = FindByName(specialityList, "MySpecialityDemo-Pregunta9") ?? throw new Exception("Speciality not found");
            Console.WriteLine("specialityFounded -> " + specialtyFounded);

            // Buscar por id
            Specialty specialtyFoundedIndividual = await GetSpecialtyAsync(specialtyFounded.Id);
            Console.WriteLine("specialityFoundedIndividual -> " + specialtyFoundedIndividual);

            // Actualizar
            specialtyFoundedIndividual.Name = "MySpecialityDemo-Changed";
            string json = await UpdateSpecialtyAsync(specialtyFoundedIndividual);
            Console.WriteLine("specialityChanged -> " + json);

            // Buscar por id de nuevo
            specialtyFoundedIndividual = await GetSpecialtyAsync(specialtyFounded.Id);
            Console.WriteLine("specialityFoundedIndividual tras cambio -> " + specialtyFoundedIndividual);

            // Borrar
            var statusCode = await DeleteSpecialityAsync(specialtyFounded.Id);
            Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.ToString());
        }
    }
}
