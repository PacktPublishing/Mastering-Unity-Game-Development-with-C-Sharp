using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singlton<PoolManager>
{
    public List<ItemToPool> allObjects;     //make a list of item to pool
    public bool isExpand;                   //check if its expandable


    private Dictionary<string, List<GameObject>> pooledObjects;
    private Dictionary<string, Transform> parentTransforms;
    protected override void Awake()
    {
        base.Awake();
        pooledObjects = new Dictionary<string, List<GameObject>>();
        parentTransforms = new Dictionary<string, Transform>();
    }


    private void Start()
    {
        foreach (ItemToPool item in allObjects)
        {
            CreatePool(item.prefab, item.size);
        }
    }

    //this to get an object fram a pool
    public GameObject GetPooledObject(string key)
    {
        if (pooledObjects.ContainsKey(key))
        {

            foreach (GameObject obj in pooledObjects[key])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            if (isExpand)
            {
                return ExpandPool(key);
            }
        }

        return null;
    }

    private GameObject ExpandPool(string key)
    {
        ItemToPool item = allObjects.Find(x => x.prefab.name == key);
        if (item.prefab != null)
        {
            GameObject go = Instantiate(item.prefab, parentTransforms[key]);
            go.SetActive(false);
            pooledObjects[key].Add(go);
            return go;
        }

        return null;
    }

    public void ReturnToPool(GameObject obj, string key)
    {
        if (!obj.activeInHierarchy) return;
        obj.SetActive(false);
        obj.transform.SetParent(parentTransforms[key]);
    }


    public void AddNewPoolItem(GameObject prefab, int size)
    {
        CreatePool(prefab, size);
    }

    // Method to create a pool at runtime
    public void CreatePool(GameObject prefab, int size)
    {
        string key = prefab.name;

        if (!pooledObjects.ContainsKey(key))
        {
            pooledObjects[key] = new List<GameObject>();

            // Create a parent for the group of objects
            Transform parent = new GameObject($"{key}_Pool").transform;
            parent.SetParent(transform);
            parentTransforms[key] = parent;
        }

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.SetParent(parentTransforms[key]);
            pooledObjects[key].Add(obj);
        }
    }

}

[System.Serializable]
public struct ItemToPool
{
    public GameObject prefab;
    public int size;
}
