using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoExt
{
    [TabGroup("UI")] [SerializeField][OdinSerialize] public Dictionary<AbilityExtendableEnum, Image> CooldownImageDictionary = new Dictionary<AbilityExtendableEnum, Image>();
    [TabGroup("UI")] [SerializeField] private GameObject _errorText;
    [TabGroup("References")] [SerializeField] private AbilityList _abilityList;
    [TabGroup("References")] [SerializeField] private AbilityParameterHandler _abilityParameterHandler;
    [TabGroup("References")] [SerializeField] private Spawner _spawner;

    
    [TabGroup("References")] [SerializeField] private List<ClassType> _classTypes;
    [TabGroup("References")] [SerializeField] private List<EnemyType> _enemyTypes;


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
        _abilityParameterHandler.AbilityStarted = new Subject<AbilityExtendableEnum>();
        _abilityParameterHandler.AbilityStillExecuting = new Subject<bool>();
    }
    
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        AddEvent(_abilityParameterHandler.AbilityStarted, OnAbilityStarted);
        AddEvent(_abilityParameterHandler.AbilityStillExecuting, OnAbilityStillExecuting);
    }

    private void OnAbilityStillExecuting(bool isAbilityStillExecuting)
    {
        _errorText.gameObject.SetActive(isAbilityStillExecuting);
    }

    private void OnAbilityStarted(AbilityExtendableEnum abilityEnum)
    {
        StartCooldownUI(abilityEnum).Forget();
    }

    private async UniTask StartCooldownUI(AbilityExtendableEnum abilityEnum)
    {
        Image image = CooldownImageDictionary[abilityEnum];
        image.gameObject.SetActive(true);

        Ability ability = _abilityList.AbilityDictionary[abilityEnum];

        image.fillAmount = 1f;
        while (ability.GetNormalizedRemainingTime() >= 0)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, ability.GetNormalizedRemainingTime(), 0.1f);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        image.gameObject.SetActive(false);
    }

    public void SpawnPlayerArcher()
    {
        _spawner.SpawnPlayer(_classTypes[0]);
    }
    
    public void SpawnPlayerHealer()
    {
        _spawner.SpawnPlayer(_classTypes[1]);
    }
    
    public void SpawnPlayerTank()
    {
        _spawner.SpawnPlayer(_classTypes[2]);
    }
    
    public void SpawnEnemyArcher()
    {
        _spawner.SpawnEnemy(_enemyTypes[0]);
    }
    
    public void SpawnEnemyHealer()
    {
        _spawner.SpawnEnemy(_enemyTypes[1]);
    }
    
    public void SpawnEnemyTank()
    {
        _spawner.SpawnEnemy(_enemyTypes[2]);
    }
}
