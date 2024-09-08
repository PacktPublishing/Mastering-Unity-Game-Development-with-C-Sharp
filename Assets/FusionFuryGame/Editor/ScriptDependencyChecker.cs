using UnityEditor;
using UnityEngine;

public class ScriptDependencyChecker : EditorWindow
{
    private static Vector2 scrollPosition;
    private static string[] missingScripts = new string[0];

    [MenuItem("Tools/Script Dependency Checker")]
    public static void ShowWindow()
    {
        GetWindow<ScriptDependencyChecker>("Script Dependency Checker");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Check Script Dependencies"))
        {
            CheckDependencies();
        }

        if (missingScripts.Length > 0)
        {
            EditorGUILayout.LabelField("Objects with Missing Scripts:", EditorStyles.boldLabel);
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));
            foreach (var entry in missingScripts)
            {
                EditorGUILayout.LabelField(entry);
            }
            EditorGUILayout.EndScrollView();
        }
        else
        {
            EditorGUILayout.LabelField("No missing scripts found.");
        }
    }

    private void CheckDependencies()
    {
        var missingList = new System.Collections.Generic.List<string>();
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {
            var components = obj.GetComponents<Component>();
            foreach (var component in components)
            {
                if (component == null)
                {
                    missingList.Add($"Missing script on GameObject: {obj.name}");
                }
            }
        }

        missingScripts = missingList.ToArray();
    }
}
