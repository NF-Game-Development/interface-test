using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drops", menuName = "ScriptableObjects/ItemDrops")]
public class Drops : ScriptableObject
{
    public List<GameObject> ItemDrops;
}
