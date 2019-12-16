using Zenject;

public class WebRequestCommandFactory
{
    [Inject]
    private readonly ICommandDispatcher _commandDispatcher;

    public WebRequestCommand Create(string url, string method = "GET")
    {
        return new WebRequestCommand(url, method);
    }
}