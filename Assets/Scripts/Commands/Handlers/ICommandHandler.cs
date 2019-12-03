public interface ICommandDispatcher
{
    void ExecuteCommand(SynchronousCommand command);
    void ExecuteCommand(CoroutineCommand command);
}