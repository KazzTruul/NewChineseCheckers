using Zenject;

public class LoadSceneCommandFactory
{
    private readonly ZenjectSceneLoader _sceneLoader;
    private readonly SignalBus _signalBus;

    public LoadSceneCommandFactory(ZenjectSceneLoader sceneLoader, SignalBus signalBus)
    {
        _sceneLoader = sceneLoader;
        _signalBus = signalBus;
    }

    public LoadSceneCommand Create(int sceneIndex, bool loadAdditive, int unloadSceneIndex = -1)
    {
        return new LoadSceneCommand(_sceneLoader, _signalBus, sceneIndex, loadAdditive, unloadSceneIndex);
    }
}