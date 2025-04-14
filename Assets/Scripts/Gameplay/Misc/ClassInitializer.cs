using UnityEngine;

public class ClassInitializer : MonoExt
{
    [SerializeField] private BaseClassDictionary _baseClassDictionary;
    private void Awake()
    {
        //Initialize mono extension
        Initialize();
    }
    private void Start()
    {
        //Events
        OnSubscriptionSet();
    }

    public void InitializeUnitClass(ClassEnum classEnum, BaseUnit baseUnit)
    {
        baseUnit.InitializeUnitClass(_baseClassDictionary.ClassDictionary[classEnum]);
    }
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }    
}
