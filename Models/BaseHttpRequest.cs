namespace PetClinicApiConsumer.Models;
public class BaseHttpRequest
{
    protected readonly HttpClient _httpClient;
    protected readonly Services services;

    public BaseHttpRequest(Services services)
    {
        this.services = services;
        _httpClient = new HttpClient { BaseAddress = new Uri(services.PetClinic.BaseUrl) };
    }
}