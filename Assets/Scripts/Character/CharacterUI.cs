using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour
{
    
    [SerializeField] Image _health;

    public void TakeDamage(float damage)
    {
        _health.fillAmount = damage;
    }
}