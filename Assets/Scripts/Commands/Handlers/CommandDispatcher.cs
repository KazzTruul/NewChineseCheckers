using System.Collections.Generic;
using Zenject;

public sealed class CommandDispatcher : ICommandDispatcher
{
    [Inject]
    private readonly CoroutineRunner _coroutineRunner;

    private List<CommandBase> _commandList = new List<CommandBase>();

    public void ExecuteCommand(SynchronousCommand command)
    {
        _commandList.Add(command);
        command.Execute();
    }

    public void ExecuteCommand(CoroutineCommand command)
    {
        _commandList.Add(command);
        _coroutineRunner.StartExternalCoroutine(command.Execute());
    }
}