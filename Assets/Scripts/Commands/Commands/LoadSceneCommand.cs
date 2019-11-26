﻿using Zenject;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadSceneCommand : CoroutineCommand
{
    private readonly ZenjectSceneLoader _sceneLoader;
    private readonly int _loadSceneIndex;
    private readonly int _unloadSceneIndex;
    private readonly bool _loadAdditive;
    private readonly SignalBus _signalBus;

    public LoadSceneCommand(ZenjectSceneLoader sceneLoader, SignalBus signalBus, int sceneIndex, bool loadAdditive, int unloadSceneIndex)
    {
        _sceneLoader = sceneLoader;
        _signalBus = signalBus;
        _loadSceneIndex = sceneIndex;
        _loadAdditive = loadAdditive;
        _unloadSceneIndex = unloadSceneIndex;
    }

    public override IEnumerator Execute()
    {
        var op = _sceneLoader.LoadSceneAsync(_loadSceneIndex, LoadSceneMode.Additive, null, _loadAdditive ? LoadSceneRelationship.Child : LoadSceneRelationship.Sibling);

        op.allowSceneActivation = true;
        yield return op;

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_loadSceneIndex));

        if (_unloadSceneIndex > 0)
        {
            yield return SceneManager.UnloadSceneAsync(_unloadSceneIndex);
        }

        _signalBus.Fire(new ActiveSceneChangedSignal { OldSceneIndex = _unloadSceneIndex, NewSceneIndex = _loadSceneIndex });
    }
}