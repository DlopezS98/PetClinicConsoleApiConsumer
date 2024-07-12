// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using PetClinicApiConsumer.Models;

Console.WriteLine("Hello, World!");
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// var appsettings = configuration.Get<AppSettings>();
IConfigurationSection section = configuration.GetSection(Services.SectionName);
Services services = section.Get<Services>()!;
