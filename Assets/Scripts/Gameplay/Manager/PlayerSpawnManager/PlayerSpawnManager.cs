using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpawnManager : MonoExt
{
    //[SerializeField] private PlayerController _playerControllerPrefab;
    [SerializeField] private ClassPrefabDictionary classPrefabDictionary;
    [SerializeField] Transform _playerSpawnPoint;
    
    [SerializeField] private ClassAbilityDictionary _classAbilityDictionary;
    [FormerlySerializedAs("_baseClasDictionary")] [SerializeField] private BaseClassDictionary baseClassDictionary;
    
    [SerializeField] private Camera _mainCamera;
    
    [SerializeField] private CameraController _cameraController;
    
    [SerializeField] private HUDManager _hudManager;

    private PlayerController _playercontrollerRef;
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

    [Button]
    public void SpawnPlayer(ClassEnum classEnum)
    {
        if (_playercontrollerRef != null)
        {
            Destroy(_playercontrollerRef.gameObject);
        }

        _playercontrollerRef = Instantiate(classPrefabDictionary.PrefabDictionary[classEnum], 
            _playerSpawnPoint.position, Quaternion.identity);
        
        _playercontrollerRef.SetCamera(_mainCamera);
        _playercontrollerRef.InitializePlayer(baseClassDictionary.ClassDictionary[classEnum]);
        _playercontrollerRef.ChangeInputReaderAbilityDictionary(_classAbilityDictionary.AbilityOrderDictionary[classEnum]);
        
        _hudManager.SetAbilityList(_playercontrollerRef.GetAbilityList());
        _cameraController.SetCameraTarget(_playercontrollerRef.transform);
    }
}
