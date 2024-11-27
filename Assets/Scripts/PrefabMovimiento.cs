using UnityEngine;

public class PrefabMovimiento : MonoBehaviour
{
    private Transform cajaObjetivo; // Referencia a la caja donde se almacenará el prefab
    private bool dentroDeCaja = false; // Verifica si el prefab está dentro de la caja

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el prefab entra en contacto con una caja válida
        if (collision.CompareTag("boxUp"))
        {
            // Guardamos la referencia a la caja
            cajaObjetivo = collision.transform;

            // Mueve el prefab directamente a la caja
            MoverPrefabACaja();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica si el prefab salió de la caja
        if (collision.CompareTag("boxUp"))
        {
            dentroDeCaja = false; // El prefab ya no está dentro de la caja
            cajaObjetivo = null;
        }
    }

    // Mueve el prefab a la posición de la caja detectada
    private void MoverPrefabACaja()
    {
        if (cajaObjetivo != null)
        {
            // Fija la posición del prefab a la posición de la caja
            transform.position = cajaObjetivo.position;

            // Marcar que el prefab está dentro de la caja
            dentroDeCaja = true;
            Debug.Log("Prefab almacenado en la caja.");
        }
    }
}
