using System.Collections;

public abstract class CoroutineCommand : CommandBase
{
    public abstract IEnumerator Execute();
}