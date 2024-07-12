using System.Text.Json;

namespace PetClinicApiConsumer.Models;
public class BaseHttpRequest
{
    protected readonly HttpClient client;
    protected readonly Services services;

    protected JsonSerializerOptions baseSerializedOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public BaseHttpRequest(Services services)
    {
        this.services = services;
        client = new HttpClient { BaseAddress = new Uri(services.PetClinic.BaseUrl) };
    }
}