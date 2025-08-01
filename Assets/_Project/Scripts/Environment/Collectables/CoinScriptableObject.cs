using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "ScriptableObjects/Interactable/Collectable")] [InlineEditor]
public class CoinScriptableObject : SerializedScriptableObject
{
    public GameObject CoindModelPrefab;
    public float CoinValue;
}
