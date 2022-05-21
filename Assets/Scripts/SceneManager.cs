using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SceneManager
{
    public static Scene CurrentScene { get; private set; } = Scene.LOADER;

    private static ReadOnlyDictionary<Scene, string> _sceneNames = new ReadOnlyDictionary<Scene, string>(
        new Dictionary<Scene, string>()
        {
            {Scene.LOADER, "Loader"},
            {Scene.DOCK, "Dock"},
            {Scene.CHANNEL, "Channel"},
        });
        
    public static void Load(Scene scene)
    {
        CurrentScene = scene;
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneNames[scene]);
    }

    public static void AddOnSceneLoadedListener(UnityAction<UnityEngine.SceneManagement.Scene, LoadSceneMode> action)
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += action;
    }
    
    public static void RemoveOnSceneLoadedListener(UnityAction<UnityEngine.SceneManagement.Scene, LoadSceneMode> action)
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= action;
    }

}

public enum Scene
{
    NONE = 0,
    LOADER = 1,
    DOCK = 2, 
    CHANNEL = 3,
}