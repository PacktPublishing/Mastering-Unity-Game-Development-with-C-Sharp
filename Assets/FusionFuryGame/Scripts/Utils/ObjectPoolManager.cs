using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FusionFuryGame
{
    public class ObjectPoolManager : Singlton<ObjectPoolManager>
    {
        // Define a dictionary to store object pools
        private Dictionary<string, Queue<GameObject>> objectPools = new Dictionary<string, Queue<GameObject>>();

        // Create or retrieve an object from the pool based on the tag
        public GameObject GetPooledObject(string tag)
        {
            if (objectPools.ContainsKey(tag))
            {
                if (objectPools[tag].Count > 0)
                {
                    GameObject obj = objectPools[tag].Dequeue();
                    obj.SetActive(true);
                    return obj;
                }
            }

            Debug.LogWarning("No available object in the pool with tag: " + tag);
            return null;
        }

        // Return an object to the pool
        public void ReturnToPool(string tag, GameObject obj)
        {
            obj.SetActive(false);
            objectPools[tag].Enqueue(obj);

        }

        // Create an object pool for a specific prefab
        public void CreateObjectPool(GameObject prefab, int poolSize)
        {
            string tag = prefab.tag;

            if (!objectPools.ContainsKey(tag))
            {
                objectPools[tag] = new Queue<GameObject>();

                for (int i = 0; i < poolSize; i++)
                {
                    GameObject obj = Instantiate(prefab);
                    obj.SetActive(false);
                    objectPools[tag].Enqueue(obj);
                }
            }
            else
            {
                Debug.LogWarning("Object pool with tag " + tag + " already exists.");
            }
        }
    }
}