using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractKey", menuName = "ScriptableObjects/Player/InteractKey")] [InlineEditor]
public class PlayerInteractKeyScriptableObject : SerializedScriptableObject
{
    public KeyCode InteractKey;
}
