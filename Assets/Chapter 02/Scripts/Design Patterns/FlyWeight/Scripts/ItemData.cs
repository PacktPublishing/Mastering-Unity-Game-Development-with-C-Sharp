using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyWeightPattern
{
    public enum ItemType
    {
        CLOTH,
        NONCLOTH
    }

    [CreateAssetMenu(fileName ="item" , menuName ="Item Data")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public ItemType itemType;
    }
}