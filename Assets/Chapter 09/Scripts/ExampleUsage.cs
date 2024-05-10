using UnityEngine;

namespace Chapter9
{
    public class ExampleUsage : MonoBehaviour
    {
        public GameObject prefabToPool;
        public int poolSize = 10;
        public string objectName = "MyTag";

        void Start()
        {
            // Create an object pool with the specified prefab, pool size, and tag 
            ObjectPoolManager.Instance.CreateObjectPool(prefabToPool, poolSize, objectName);

            // Get an object from the pool 
            GameObject obj = ObjectPoolManager.Instance.GetPooledObject(objectName);

            if (obj != null)
            {
                // Use the object 
                obj.transform.position = Vector3.zero;
            }

            // Return the object to the pool 
            ObjectPoolManager.Instance.ReturnToPool(objectName, obj);
        }

    }
}