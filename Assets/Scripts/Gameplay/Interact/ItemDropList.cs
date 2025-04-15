using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemDropList", menuName = "ScriptableObjects/Interactable/ItemDropList")]
public class ItemDropList : ScriptableObject
{ 
    public List<GameObject> ItemDrops;
}
