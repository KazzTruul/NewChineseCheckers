using Zenject;

public class LoadSceneCommandFactory
{
    [Inject]
    private readonly ZenjectSceneLoader _sceneLoader;
    [Inject]
    private readonly SignalBus _signalBus;
    
    public LoadSceneCommand Create(int sceneIndex, bool loadAdditive, int unloadSceneIndex = -1)
    {
        return new LoadSceneCommand(_sceneLoader, _signalBus, sceneIndex, loadAdditive, unloadSceneIndex);
    }
}