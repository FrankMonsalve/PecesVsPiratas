using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private int _movement = 3;
    [SerializeField] private bool _hasMoved;
    [SerializeField] private int _remainingMovement;
    [SerializeField] private Vector3 _initialPosition;

    public bool HasMoved => _hasMoved;
    public int Movement => _movement;
    public int RemainingMovement => _remainingMovement;

    public void MoveToCell(Vector3Int targetCell, Tilemap tilemap, int cost)
    {

        Debug.Log(tilemap + " MoveToCell");
        // Si ya se movió, no puede mover más
        if (_hasMoved)
        {

            Debug.Log("Este personaje ya no puede moverse este turno.");
            return;
        }

        if (_remainingMovement < cost)
        {
            Debug.Log("No tienes suficientes turnos para este movimiento");
            checkMovement();

            return;
        }


        Vector3 cellCenter = tilemap.GetCellCenterWorld(targetCell);
        transform.position = cellCenter;


        _remainingMovement -= cost;

        checkMovement();
    }

    public void Setup(int maxMovement)
    {
        _movement = maxMovement;
        _remainingMovement = _movement;
        _hasMoved = false;
    }

    public void ResetMovement()
    {
        _remainingMovement = _movement;
        _hasMoved = false;
    }
    public void MoveToInitialPosition(Tilemap tilemap)
    {
        Vector3 cellCenter = tilemap.GetCellCenterWorld(tilemap.WorldToCell(_initialPosition));
        transform.position = cellCenter;
    }

    public void Attack(int cost)
    {
        _remainingMovement = Mathf.Clamp(_remainingMovement - cost, 0, Movement);
        checkMovement();
    }

    public void Attack()
    {
        _remainingMovement = Mathf.Clamp(_remainingMovement - 1, 0, Movement);

        checkMovement();
    }

    public void checkMovement()
    {
        if (_remainingMovement > 0)
        {
            _hasMoved = false;
            return;
        }

        _hasMoved = true;
        Debug.Log($"{gameObject.name} ha terminado suS movimiento.");
    }
}

