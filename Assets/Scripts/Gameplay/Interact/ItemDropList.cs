using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemDropList", menuName = "ScriptableObjects/Interactable/ItemDropList")]
public class ItemDropList : ScriptableObject
{ 
    public List<ItemDrop> ItemDrops;
}

[System.Serializable]
public class ItemDrop
{
    public float ItemDropRate;
    public GameObject ItemPrefab;
}
