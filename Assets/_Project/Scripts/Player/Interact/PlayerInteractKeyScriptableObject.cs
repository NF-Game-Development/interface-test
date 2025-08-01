using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractKey", menuName = "ScriptableObjects/Player/InteractKey")]
public class PlayerInteractKeyScriptableObject : SerializedScriptableObject
{
    public KeyCode InteractKey;
}
