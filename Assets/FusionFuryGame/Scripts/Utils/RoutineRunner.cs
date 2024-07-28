using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutineRunner : MonoBehaviour
{
    private static RoutineRunner _instance;

    public static RoutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                // Create a new GameObject if one doesn't already exist
                GameObject routineRunnerObject = new GameObject("RoutineRunner");
                _instance = routineRunnerObject.AddComponent<RoutineRunner>();
                DontDestroyOnLoad(routineRunnerObject);
            }
            return _instance;
        }
    }

    /// <summary>
    /// Starts a coroutine using the singleton RoutineRunner instance.
    /// </summary>
    /// <param name="routine">The IEnumerator routine to start.</param>
    public static void StartRoutine(IEnumerator routine)
    {
        Instance.StartCoroutine(routine);
    }

    /// <summary>
    /// Stops a coroutine using the singleton RoutineRunner instance.
    /// </summary>
    /// <param name="routine">The IEnumerator routine to stop.</param>
    public static void StopRoutine(IEnumerator routine)
    {
        Instance.StopCoroutine(routine);
    }
}
