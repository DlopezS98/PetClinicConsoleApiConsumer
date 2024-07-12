using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public enum QuestionType
{
    FirstQuestion,
    SecondQuestion,
    ThirdQuestion,
}

public class Container(Services services)
{
    private readonly Services _services = services;

    public static Container GetInstace(Services services)
    {
        return new Container(services);
    }

    public IQuestion GetQuestion(QuestionType questionType)
    {
        return questionType switch
        {
            QuestionType.FirstQuestion => new FirstQuestion(_services),
            QuestionType.SecondQuestion => new SecondQuestion(_services),
            QuestionType.ThirdQuestion => new ThirdQuestion(_services),
            _ => throw new ArgumentException($"Question {questionType} not found")
        };
    }
}
