using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Coin : MonoExt, ICollectable
{
    [SerializeField] private int _coinAmountGain;
    [SerializeField] private Rigidbody _rigidbody;
    
    private bool _isAbleToMove = false;

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

    public void Collect(PlayerItemCollectedStats playerItemCollectedStats)
    {
        playerItemCollectedStats.AddCoinsCollected(_coinAmountGain);
        Destroy(this.gameObject);
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
