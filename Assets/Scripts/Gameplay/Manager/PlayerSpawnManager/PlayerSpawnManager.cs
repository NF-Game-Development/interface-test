using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerSpawnManager : MonoExt
{
    [SerializeField] private PlayerController _playerControllerPrefab;
    [SerializeField] Transform _playerSpawnPoint;
    
    [SerializeField] private ClassAbilityDictionary _classAbilityDictionary;
    [SerializeField] private BaseClasDictionary _baseClasDictionary;
    
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
        if (_playercontrollerRef == null)
        {
            _playercontrollerRef = Instantiate(_playerControllerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        }
    
        _playercontrollerRef.transform.position = _playerSpawnPoint.position;
        _playercontrollerRef.SetCamera(_mainCamera);
        _playercontrollerRef.InitializePlayer(_baseClasDictionary.ClassDictionary[classEnum]);
        _playercontrollerRef.ChangePlayerModel();
        _playercontrollerRef.ChangeInputReaderAbilityDictionary(_classAbilityDictionary.AbilityOrderDictionary[classEnum]);
        
        _hudManager.SetAbilityList(_playercontrollerRef.GetAbilityList());
        
        _cameraController.SetCameraTarget(_playercontrollerRef.transform);
    }
}
