using System.Collections.Generic;

public sealed class CommandHandler : ICommandHandler
{
    private List<CommandBase> _commandList = new List<CommandBase>();

    public void ExecuteCommand(CommandBase command)
    {
        _commandList.Add(command);
        command.Execute();
    }
}