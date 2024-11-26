using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private int _maxMovement = 3;
    [SerializeField] private bool _hasMoved;
    [SerializeField] private int _remainingMovement;
    [SerializeField] private Vector3 _initialPosition;

    public bool HasMoved => _hasMoved;

    public void MoveToCell(Vector3Int targetCell, Tilemap tilemap)
    {
        Debug.Log(tilemap);
        // Si ya se movió, no puede mover más
        if (_hasMoved)
        {
            
            Debug.Log("Este personaje ya no puede moverse este turno.");
            return;
        }


        Vector3 cellCenter = tilemap.GetCellCenterWorld(targetCell);
        transform.position = cellCenter;


        _remainingMovement--;

        if (_remainingMovement <= 0)
        {
            _hasMoved = true;
            Debug.Log($"{gameObject.name} ha terminado su movimiento.");
        }
    }

    public void Setup(int maxMovement)
    {
        _maxMovement = maxMovement;
        _hasMoved = false;
    }

    public void ResetMovement()
    {
        _remainingMovement = _maxMovement;
        _hasMoved = false;
    }
    public void MoveToInitialPosition(Tilemap tilemap)
    {
        Vector3 cellCenter = tilemap.GetCellCenterWorld(tilemap.WorldToCell(_initialPosition));
        transform.position = cellCenter;
    }
}
