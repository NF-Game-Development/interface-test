using UnityEngine;

[CreateAssetMenu(fileName = "LootDropAmountRange", menuName = "ScriptableObjects/Loot/LootDropAmountRange")]
public class LootDropAmountRange : ScriptableObject
{
    public int MinAmountLootDrop;
    public int MaxAmountLootDrop;
}
