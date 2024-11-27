using UnityEngine;
using System.Linq;  // Necesario para usar LINQ

public class GenerarPersonajes : MonoBehaviour
{
    public GameObject[] prefabsPersonajes;  // Array de prefabs de personajes
    private Transform[] cajasAbajo; // Array de cajas abajo para posicionar los personajes

    private void Start()
    {
        // Llamamos a la función para generar los personajes al inicio de la escena
        GenerarPersonajesEnCajas();
    }
    public void GenerarPersonajesEnCajas()
    {
        // Encontrar todas las cajas activas de abajo
        cajasAbajo = GameObject.FindGameObjectsWithTag("boxDown")
                                .Where(go => go.activeSelf)  // Solo obtener las cajas activas
                                .Select(go => go.transform)  // Convertir GameObjects a Transform
                                .ToArray();

        // Eliminar personajes actuales en las cajas de abajo
        foreach (Transform caja in cajasAbajo)
        {
            // Eliminar cualquier hijo (personaje) de las cajas de abajo
            if (caja.childCount > 0)
            {
                Destroy(caja.GetChild(0).gameObject);
            }
        }

        // Generar personajes en las cajas de abajo activas
        for (int i = 0; i < cajasAbajo.Length; i++)
        {
            // Verificar si la caja está activa
            if (cajasAbajo[i].gameObject.activeSelf)
            {
                int indiceAleatorio = Random.Range(0, prefabsPersonajes.Length); // Seleccionar un prefab aleatorio
                GameObject personaje = Instantiate(prefabsPersonajes[indiceAleatorio], cajasAbajo[i].position, Quaternion.identity);
                personaje.transform.SetParent(cajasAbajo[i]);  // Asignar al transform de la caja
            }
        }




    }



}

