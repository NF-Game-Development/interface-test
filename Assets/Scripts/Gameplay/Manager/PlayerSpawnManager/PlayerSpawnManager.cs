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
        PlayerController player = Instantiate(_playerControllerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        player.SetCamera(_mainCamera);
        player.InitializePlayer(_baseClasDictionary.ClassDictionary[classEnum]);
        player.ChangeInputReaderAbilityDictionary(_classAbilityDictionary.AbilityOrderDictionary[classEnum]);
        
        _cameraController.SetCameraTarget(player.transform);
    }
}
