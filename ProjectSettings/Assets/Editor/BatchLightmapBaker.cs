#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class BatchLightmapBaker
{
    [MenuItem("Tools/Lightmapping/Batch Bake All Scenes")]
    public static void BakeAllScenes()
    {
        // Get all scenes from Build Settings
        var scenes = EditorBuildSettings.scenes;

        foreach (var scene in scenes)
        {
            if (!scene.enabled || !File.Exists(scene.path))
                continue;

            // Open the scene
            var openedScene = EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Single);
            Debug.Log("🔄 Baking lighting for: " + openedScene.name);

            // Bake lighting
            Lightmapping.Bake();

            // Save the scene
            EditorSceneManager.SaveScene(openedScene);
        }

        Debug.Log("✅ Finished baking all scenes.");
    }
}
#endif
