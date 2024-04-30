using UnityEngine;
using UnityEngine.Serialization;
using System;
using Unity.Collections;
using UnityEditor;

[HelpURL("URL")]
[AddComponentMenu("Custom/Attribute Example")] [ExecuteAlways] [CanEditMultipleObjects]
public class AttributeExample : MonoBehaviour
{



    [ColorUsage(true, true)]
    public Color colorUsage;

    [TextArea(5, 7)] public string text;
    [ReadOnly] public Color color;
    [Multiline(10)] public string description;
    
    [ContextMenu("Add Component Item")]
    void AddComponentItem()
    {
        gameObject.AddComponent<Rigidbody>();
    }
}
