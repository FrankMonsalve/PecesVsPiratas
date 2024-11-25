using UnityEngine;
using System.Collections.Generic;  // Asegúrate de agregar esta línea

public class BoxControl : MonoBehaviour
{
    // Variables para gestionar la detección
    [Header("Detección de objetos")]
    public LayerMask capaObjetosDetectables; // Capa de objetos que la caja puede detectar
    public Transform puntoDeDeteccion;      // Punto desde el cual se detectan objetos
    public float rangoDeDeteccion = 1f;     // Rango para detectar objetos

    // Variables para restricciones
    [Header("Restricciones")]
    public string tagPermitido = "Permitido"; // Tag de los objetos que pueden interactuar
    private bool objetoEncima = false;        // Indica si hay un objeto sobre la caja

    // Registros de objetos detectados
    private HashSet<GameObject> objetosDetectados = new HashSet<GameObject>();

    // Eventos o acciones al detectar objetos
    public delegate void CajaEventHandler(GameObject objeto);
    public event CajaEventHandler OnObjetoEncima;
    public event CajaEventHandler OnObjetoDentro;

    private void Update()
    {
        // Detectar objetos encima de la caja
        DetectarObjetoEncima();

        // Detectar objetos dentro de la caja (o cerca)
        DetectarObjetoDentro();
    }

    private void DetectarObjetoEncima()
    {
        // Detectar objetos encima utilizando un Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, rangoDeDeteccion, capaObjetosDetectables))
        {
            if (hit.collider.CompareTag(tagPermitido))
            {
                // Solo activar el evento si el objeto no ha sido detectado previamente
                if (!objetosDetectados.Contains(hit.collider.gameObject))
                {
                    objetosDetectados.Add(hit.collider.gameObject);
                    OnObjetoEncima?.Invoke(hit.collider.gameObject);
                    Debug.Log($"Objeto permitido detectado encima: {hit.collider.gameObject.name}");
                }
            }
        }
        else
        {
            // Si ya no hay objetos encima, se reinicia el flag
            objetoEncima = false;
        }
    }

    private void DetectarObjetoDentro()
    {
        // Detectar objetos dentro utilizando una esfera
        Collider[] objetosEnRango = Physics.OverlapSphere(puntoDeDeteccion.position, rangoDeDeteccion, capaObjetosDetectables);

        foreach (var objeto in objetosEnRango)
        {
            if (objeto.CompareTag(tagPermitido))
            {
                // Solo activar el evento si el objeto no ha sido detectado previamente
                if (!objetosDetectados.Contains(objeto.gameObject))
                {
                    objetosDetectados.Add(objeto.gameObject);
                    OnObjetoDentro?.Invoke(objeto.gameObject);
                    Debug.Log($"Objeto permitido detectado dentro: {objeto.gameObject.name}");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar un gizmo para visualizar el rango de detección
        if (puntoDeDeteccion != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(puntoDeDeteccion.position, rangoDeDeteccion);
        }
    }
}
