using UnityEngine;

public class BreakableObject : MonoExt, IBreakable
{
    [SerializeField] private GameObject _breakableObject;
    //[SerializeField] private GameObject _breakEffect; // if there is time
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
    
    public override void Initialize()
    {
        base.Initialize();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
    }

    protected virtual void OnBreak()
    {
        _breakableObject?.SetActive(false);
        //_breakEffect?.SetActive(true);
    }
    
    public void Break()
    {
        OnBreak();
    }
}
