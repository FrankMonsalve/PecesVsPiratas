using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Animator animator;
    public OnTriggerEnter set;
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHealth;
    public int currentHealth;

    public bool TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
            return true;
        else
            return false;
    }

    void Start() 
    {
        animator = GetComponent<Animator>();
        
    }

    void Update() 
    {
        animator.SetTrigger("Move");
        
    }
}
