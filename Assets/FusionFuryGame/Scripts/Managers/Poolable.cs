using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    private string poolKey;

    public void Initialize(string key)
    {
        poolKey = key;
    }

    // Call this method to return the object to its pool
    public void ReturnToPool()
    {
        PoolManager.Instance.ReturnToPool(gameObject, poolKey);
    }

    // Optional: Automatically return to pool on certain conditions, e.g., after a delay
    private void OnDisable()
    {
        // Reset any necessary states here
        PoolManager.Instance.ReturnToPool(gameObject, poolKey);
    }
}
