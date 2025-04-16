using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectable : MonoExt, ICollectable
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private bool _isAbleToMove = false;
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

    protected virtual void OnCollect(PlayerItemCollectedStats playerItemCollectedStats)
    {
        
    }

    public void Collect(PlayerItemCollectedStats playerItemCollectedStats)
    {
        OnCollect(playerItemCollectedStats);
    }

    public void MoveTowardsPlayer(Transform target)
    {
        if(_isAbleToMove == false)
            return;
        
        transform.position = Vector3.MoveTowards(this.transform.position, target.position, 1f * Time.deltaTime);
    }

    public void OnSpawnItemJumpUp()
    {
        _rigidbody.AddForce(Vector3.up * 4, ForceMode.Impulse);
        StartCoroutine(CO_DelayBeforeObjectMove());
    }
    
    private IEnumerator CO_DelayBeforeObjectMove()
    {
        yield return new WaitForSeconds(2f);
        _isAbleToMove = true;
    }
}
