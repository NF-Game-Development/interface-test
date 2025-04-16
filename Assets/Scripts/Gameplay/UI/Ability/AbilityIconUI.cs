using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AbilityIconUI : MonoExt
{ 
    [SerializeField] private Image _abilityIcon; 
    [SerializeField] private TextMeshProUGUI _abilityName; 
    [SerializeField] private Image _cooldownOverlay;

    public void SetAbility(Sprite sprite, string abilityName)
    {
        _abilityIcon.sprite = sprite;
        _abilityName.text = abilityName;
        _cooldownOverlay.fillAmount = 0f; // Start with no cooldown
        _cooldownOverlay.gameObject.SetActive(false);
    }

    public Image GetCooldownOverlay() => _cooldownOverlay;
}
