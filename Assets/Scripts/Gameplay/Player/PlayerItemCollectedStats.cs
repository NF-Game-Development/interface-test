using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerItemCollectedStats", menuName = "ScriptableObjects/Player/PlayerItemCollectedStats")]
public class PlayerItemCollectedStats : SerializedScriptableObject
{
    [SerializeField] private int _coinsCollected;
    [SerializeField] private List<ArmorItemEnum> _armorItems;

    public void AddCoinsCollected(int amountToAdd)
    {
        _coinsCollected += amountToAdd;
    }

    public void AddArmorItem(ArmorItemEnum armorItemEnum)
    {
        _armorItems.Add(armorItemEnum);
    }
}
