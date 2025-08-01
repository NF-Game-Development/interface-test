using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InteractRange", menuName = "ScriptableObjects/Player/InteractRange")]
public class PlayerInteractRangeScriptableObject : SerializedScriptableObject
{
    public float InteractRadiusRange;
    
    /*public event Action<float> OnRangeChanged;

    public void SetRange(float newRange)
    {
        InteractRadiusRange = newRange;
        OnRangeChanged?.Invoke(newRange);
    }*/
}
