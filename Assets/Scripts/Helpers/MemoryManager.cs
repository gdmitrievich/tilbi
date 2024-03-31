using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

[InitializeOnLoad]
public static class MemoryManager
{
    static MemoryManager()
    {
        EditorSceneManager.sceneOpened += OnSceneOpened;
    }

    static void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        GarbageCollect();
    }

    [MenuItem("Tools/Garbage Collect")]
    static void GarbageCollect()
    {
        EditorUtility.UnloadUnusedAssetsImmediate();
        GC.Collect();
    }
}