
using UnityEngine;
using System.Linq; // Necesario para usar LINQ

public class GenerarPersonajes : MonoBehaviour
{
    public GameObject[] prefabsPersonajes; // Array de prefabs de personajes
    private Transform[] cajasAbajo; // Array de cajas abajo para posicionar los personajes

    public void GenerarPersonajesEnCajas()
    {
        // Encontrar todas las cajas activas de abajo
        cajasAbajo = GameObject.FindGameObjectsWithTag("boxDown")
                                .Where(go => go.activeSelf) // Solo obtener las cajas activas
                                .Select(go => go.transform) // Convertir GameObjects a Transform
                                .ToArray();

        // Eliminar personajes actuales en las cajas de abajo
        foreach (Transform caja in cajasAbajo)
        {
            // Limpiar todos los hijos dentro de la caja, pero solo si está vacía
            if (caja.childCount == 0)
            {
                int indiceAleatorio = Random.Range(0, prefabsPersonajes.Length); // Seleccionar un prefab aleatorio
                GameObject personaje = Instantiate(prefabsPersonajes[indiceAleatorio], caja.position, Quaternion.identity);
                personaje.transform.SetParent(caja); // Asignar al transform de la caja
                Debug.Log($"Generado {personaje.name} en la caja {caja.name}");
            }
        }
    }
}
