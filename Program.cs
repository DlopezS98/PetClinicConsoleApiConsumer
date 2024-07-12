// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using PetClinicApiConsumer.Models;
using PetClinicApiConsumer.Questions;

Console.WriteLine("Hello, World!");
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// var appsettings = configuration.Get<AppSettings>();
IConfigurationSection section = configuration.GetSection(Services.SectionName);
Services services = section.Get<Services>()!;

Console.WriteLine("Select a question: ");
Console.WriteLine("1. First Question");
Console.WriteLine("2. Second Question");
Console.WriteLine("3. Third Question");
Console.WriteLine("4. Fourth Question");

int questionNumber = int.Parse(Console.ReadLine()!) - 1;
QuestionType questionType = Enum.Parse<QuestionType>(questionNumber.ToString());

var container = Container.GetInstace(services);
var question = container.GetQuestion(questionType);

Console.WriteLine("Running question...");
await question.Run();
Console.WriteLine("Question completed.");
Console.ReadLine();