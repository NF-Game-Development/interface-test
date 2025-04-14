using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Scriptable Objects/EnemyType")]
public class EnemyType : ScriptableObject
{
    public string EnemyID;
    public ClassType ClassType;
    public Stat MaxHealth;
    
}
