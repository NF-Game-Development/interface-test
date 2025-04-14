using TMPro;
using UnityEngine;

public class HealthTextUI : MonoExt
{
    [SerializeField] private TextMeshPro _healthText;
    [SerializeField] private Health _health;
    private void LateUpdate()
    {
        _healthText.text = _health.HP.ToString();
    }
}
