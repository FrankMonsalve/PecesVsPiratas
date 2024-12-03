using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour
{
    
    [SerializeField] Image _head;

    public void TakeDamage(float damage)
    {
        _head.fillAmount = damage;
    }
}