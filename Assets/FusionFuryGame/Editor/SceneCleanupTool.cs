using UnityEditor;
using UnityEngine;

public class SceneCleanupTool : EditorWindow
{
    private static string resultMessage = "";

    [MenuItem("Tools/Scene Cleanup Tool")]
    public static void ShowWindow()
    {
        GetWindow<SceneCleanupTool>("Scene Cleanup Tool");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Clean Up Scene"))
        {
            CleanUpScene();
        }

        if (!string.IsNullOrEmpty(resultMessage))
        {
            EditorGUILayout.HelpBox(resultMessage, MessageType.Info);
        }
    }

    private void CleanUpScene()
    {
        resultMessage = "";
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int deletedCount = 0;

        foreach (var obj in allObjects)
        {
            if (obj.transform.childCount == 0 && obj.GetComponents<Component>().Length == 1) // Empty GameObject
            {
                resultMessage += $"Deleted empty GameObject: {obj.name}\n";
                DestroyImmediate(obj);
                deletedCount++;
            }
            else
            {
                var components = obj.GetComponents<Component>();
                foreach (var component in components)
                {
                    if (component == null) // Missing script
                    {
                        Debug.LogWarning($"Object {obj.name} has missing script components.", obj);
                        resultMessage += $"Missing script component on GameObject: {obj.name}\n";
                    }
                }
            }
        }

        if (deletedCount == 0 && string.IsNullOrEmpty(resultMessage))
        {
            resultMessage = "No objects were cleaned up.";
        }

        AssetDatabase.SaveAssets();
    }
}
