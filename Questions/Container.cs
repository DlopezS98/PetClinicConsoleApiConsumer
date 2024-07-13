using PetClinicApiConsumer.Models;

namespace PetClinicApiConsumer.Questions;

public enum QuestionType
{
    FirstQuestion,
    SecondQuestion,
    ThirdQuestion,
    FourthQuestion,
    FifthQuestion,
    SixthQuestion,
    SeventhQuestion
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
            QuestionType.FourthQuestion => new FourthQuestion(_services),
            QuestionType.FifthQuestion => new FifthQuestion(_services),
            QuestionType.SixthQuestion => new SixthQuestion(_services),
            // QuestionType.SeventhQuestion => new SeventhQuestion(_services),
            _ => throw new ArgumentException($"Question {questionType} not found")
        };
    }
}
