using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string _nombre;
    [SerializeField] private int _id;
    [SerializeField] private int _defaultHealth;
    private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private int _defaultAttack;
    private int _maxAttack;
    [SerializeField] private int _attack;
    [SerializeField] private int _maxMovement;
    [SerializeField] private Animator _animator;

    [SerializeField] private CharacterMovement _characterMovement;

    public string Nombre => _nombre;
    public int Id => _id;
    public int DefaultHealth => _defaultHealth;
    public int DefaulAttack => _defaultAttack;

    public int Health => _health;
    public int Attack => _attack;

    public CharacterMovement Movement => _characterMovement;
    public bool IsAlive { get; set; }

    public Animator Animator => _animator;

    public void Setup(int addHealth, int addAttack)
    {
        _maxHealth = addHealth + _defaultAttack;
        _maxAttack = addAttack + _defaultAttack;

        _health = _maxHealth;
        _attack = _maxAttack;


        IsAlive = true;
    }

    public void Setup()
    {
        _maxHealth = _defaultAttack;
        _maxAttack = _defaultAttack;

        _health = _defaultHealth;
        _attack = _defaultAttack;
        IsAlive = true;
    }

    // Método para aplicar daño
    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        if (_health <= 0)
        {
            IsAlive = false;
        }
    }

    public void IncreaseHealth(int value)
    {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
    }

    // Método de muerte
    /*
    private void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        // Aquí puedes agregar efectos visuales de muerte o eliminar al personaje de la batalla
        Destroy(gameObject); // Esto destruye el objeto del personaje
    }
    */
    // Método de ataque

    /*
    public void Attack(Character target)
    {
        if (target != null)
        {
            target.TakeDamage(attack); // Aplica el daño del atacante al objetivo
            Debug.Log(gameObject.name + " ataca a " + target.gameObject.name + " y le hace " + attack + " de daño.");
        }
    }
    */
}
