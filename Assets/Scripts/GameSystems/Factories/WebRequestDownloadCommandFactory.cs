using Zenject;

public class WebRequestDownloadCommandFactory
{
    [Inject]
    private readonly ICommandDispatcher _commandDispatcher;

    public WebRequestDownloadCommand<T> Create<T>(string url, string method = "GET") where T : class
    {
        return new WebRequestDownloadCommand<T>(url, method);
    }
}