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
                                .Select(go => go.transform) // Convertimos los GameObjects en Transform
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

        // Generar personajes en las cajas de abajo activas de forma aleatoria
        for (int i = 0; i < cajasAbajo.Length; i++)
        {
            // Comprobar si la caja abajo está activa
            if (cajasAbajo[i].gameObject.activeSelf)
            {
                // Seleccionar un índice aleatorio para los prefabs
                int indiceAleatorio = Random.Range(0, prefabsPersonajes.Length);

                // Instanciar el personaje correspondiente en la caja activa de abajo
                GameObject personaje = Instantiate(prefabsPersonajes[indiceAleatorio], cajasAbajo[i].position, Quaternion.identity);

                // Asignar el personaje como hijo de la caja de abajo
                personaje.transform.parent = cajasAbajo[i];

                // Añadir el script para arrastrar el personaje
                personaje.AddComponent<Arrastre>(); // Aquí añadimos el componente Arrastre para permitir que el jugador lo mueva
            }
        }
    }
}

