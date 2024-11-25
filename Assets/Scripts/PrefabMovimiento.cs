using UnityEngine;

public class PrefabMovimiento : MonoBehaviour
{
    private Transform cajaObjetivo; // Referencia a la caja donde se almacenar� el prefab
    private bool dentroDeCaja = false; // Verifica si el prefab est� dentro de la caja

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el prefab entra en contacto con una caja v�lida
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
        // Verifica si el prefab sali� de la caja
        if (collision.CompareTag("boxUp"))
        {
            dentroDeCaja = false; // El prefab ya no est� dentro de la caja
            cajaObjetivo = null;
        }
    }

    // Mueve el prefab a la posici�n de la caja detectada
    private void MoverPrefabACaja()
    {
        if (cajaObjetivo != null)
        {
            // Fija la posici�n del prefab a la posici�n de la caja
            transform.position = cajaObjetivo.position;

            // Marcar que el prefab est� dentro de la caja
            dentroDeCaja = true;
            Debug.Log("Prefab almacenado en la caja.");
        }
    }
}
