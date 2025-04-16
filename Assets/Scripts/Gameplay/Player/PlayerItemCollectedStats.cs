using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerItemCollectedStats", menuName = "ScriptableObjects/Player/PlayerItemCollectedStats")]
public class PlayerItemCollectedStats : SerializedScriptableObject
{
    [SerializeField] private int _coinsCollected;

    public void AddCoinsCollected(int amountToAdd)
    {
        _coinsCollected += amountToAdd;
    }
}
