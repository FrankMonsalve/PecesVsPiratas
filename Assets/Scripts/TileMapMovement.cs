using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapMovement : MonoBehaviour
{
    public Tilemap tilemap; // Referencia al Tilemap
    public Transform player; // Referencia al objeto del jugador (tu prefab de Character 1)
    public int maxMovement = 3; // Máximo de casillas que puede moverse por turno

    private Vector3Int startCell; // Celda inicial del jugador
    public bool isPlayerTurn = true; // Indica si es el turno del jugador

    void Start()
    {
        // Calcula la celda inicial del jugador según su posición
        startCell = tilemap.WorldToCell(player.position);
        player.position = tilemap.GetCellCenterWorld(startCell); // Centra al jugador
    }

    void Update()
    {
        if (isPlayerTurn && Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int targetCell = tilemap.WorldToCell(mouseWorldPos);

            // Calcula la distancia en casillas
            int distance = Mathf.Abs(targetCell.x - startCell.x) + Mathf.Abs(targetCell.y - startCell.y);

            if (distance <= maxMovement) // Si está dentro del rango permitido
            {
                MovePlayerToCell(targetCell);
                isPlayerTurn = false; // Termina el turno del jugador
                // Aquí puedes añadir el turno de la IA después
            }
            else
            {
                Debug.Log("El objetivo está fuera de rango.");
            }
        }
    }

    void MovePlayerToCell(Vector3Int targetCell)
    {
        // Mueve al jugador al centro de la celda seleccionada
        Vector3 cellCenter = tilemap.GetCellCenterWorld(targetCell);
        player.position = cellCenter;
        startCell = targetCell; // Actualiza la celda actual del jugador
        Debug.Log($"Jugador movido a la celda {targetCell}");
        TurnComplete(); // Notifica al TurnManager que el turno ha terminado
    }

    public void TurnComplete()
{
    TurnManager.Instance.NextTurn(); // Llama al siguiente turno en el TurnManager
}

}
