namespace Questions;

public class FirstQuestion : IQuestion
{
    public Task Run()
    {
        return Task.CompletedTask;
    }
}