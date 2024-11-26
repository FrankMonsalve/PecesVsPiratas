using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName = "Character";
    public int health = 100;
    public int attackDamage = 10;
    public float attackRange = 1.5f; // Rango de ataque (en unidades de distancia)
    public bool isEnemy; // Para diferenciar entre personajes del jugador y enemigos
    public bool isAlive = true; // Para verificar si el personaje está vivo

    private void Update()
    {
        if (isAlive)
        {
            // Lógica para manejar ataques, movimientos u otras acciones
        }
    }

    // Esta función puede ser llamada cuando el personaje esté en su turno y quiera atacar
    public void TryAttack(Character target)
    {
        if (target == null || !target.isAlive) return;

        // Verifica si el objetivo está dentro del rango de ataque
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= attackRange)
        {
            Attack(target);
        }
    }

    // Lógica de ataque que reduce la vida del objetivo
    private void Attack(Character target)
    {
        Debug.Log($"{characterName} ataca a {target.characterName}!");
        target.TakeDamage(attackDamage);
    }

    // Función para recibir daño
    public void TakeDamage(int damage)
    {
        if (!isAlive) return;

        health -= damage;
        Debug.Log($"{characterName} ha recibido {damage} de daño.");

        if (health <= 0)
        {
            Die();
        }
    }

    // Función para manejar la muerte del personaje
    private void Die()
    {
        isAlive = false;
        Debug.Log($"{characterName} ha muerto.");
        // Destruye el objeto o desactiva el personaje en el juego
        // Destroy(gameObject); // Destruir el objeto de personaje
        gameObject.SetActive(false); // Desactiva el personaje en lugar de destruirlo
    }

    // Función que puede ser llamada para sanar al personaje
    public void Heal(int amount)
    {
        if (!isAlive) return;

        health += amount;
        Debug.Log($"{characterName} ha sido curado por {amount}. Salud actual: {health}");
    }

    // Lógica para hacer que el personaje se mueva (si es necesario)
    public void MoveTo(Vector3 targetPosition)
    {
        if (isAlive)
        {
            transform.position = targetPosition;
        }
    }
}