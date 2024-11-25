using UnityEngine;

public class Character : MonoBehaviour
{
    public int health; // Salud inicial del personaje
    public int attack;  // Ataque inicial del personaje

    // Método para aplicar daño
    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce la salud
        if (health <= 0)
        {
            Die(); // Si la salud es 0 o menor, el personaje muere
        }
    }

    // Método de muerte
    private void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        // Aquí puedes agregar efectos visuales de muerte o eliminar al personaje de la batalla
        Destroy(gameObject); // Esto destruye el objeto del personaje
    }

    // Método de ataque
    public void Attack(Character target)
    {
        if (target != null)
        {
            target.TakeDamage(attack); // Aplica el daño del atacante al objetivo
            Debug.Log(gameObject.name + " ataca a " + target.gameObject.name + " y le hace " + attack + " de daño.");
        }
    }
}
