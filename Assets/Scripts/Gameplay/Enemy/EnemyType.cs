using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/EnemyType")]
public class EnemyType : ScriptableObject
{
    public string EnemyID;
    public ClassType ClassType;
    public Stat MaxHealth;
    
}
