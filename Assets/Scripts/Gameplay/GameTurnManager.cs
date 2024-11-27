using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GameTurnManager : MonoBehaviour
{
    public static GameTurnManager Instance; // Singleton para acceso global

    [SerializeField] private Tilemap _tilemap; // Referencia al Tilemap
    [SerializeField] private List<List<Character>> players; // Lista de listas de personajes
    private int _currentPlayerIndex = 0; // Índice del jugador actual
    private Character _selectedCharacter;
    public Tilemap Tilemap => _tilemap;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (_tilemap == null)
            Debug.LogError("Tilemap no asignado en GameTurnManager.");
        StartPlayerTurn();
    }

    private void Update()
    {
        HandlePlayerInput();
    }

    public Tilemap GetTilemap()
    {
        return _tilemap;
    }

    public void SetupPlayers(List<List<Character>> playerLists)
    {
        if (playerLists == null || playerLists.Count == 0)
        {
            Debug.LogError("La lista de jugadores está vacía o no es válida.");
            return;
        }

        players = playerLists;
        Debug.Log("Jugadores configurados correctamente.");
    }

    // Inicia el turno del jugador actual
    public void StartPlayerTurn()
    {
        List<Character> currentPlayerCharacters = players[_currentPlayerIndex];

        foreach (Character character in currentPlayerCharacters)
        {
            character.Movement.ResetMovement();
        }

        Debug.Log($"Es el turno del Jugador {_currentPlayerIndex + 1}. ¡Mueve tus personajes!");
    }

    public void CheckTurnEnd()
    {
        List<Character> currentPlayerCharacters = players[_currentPlayerIndex];

        foreach (Character character in currentPlayerCharacters)
        {
            if (!CharacterHasMoved(character))
                return; // Si hay al menos un personaje que no ha terminado, el turno sigue
        }

        EndPlayerTurn();
    }

    private void EndPlayerTurn()
    {
        Debug.Log($"Todos los personajes del Jugador {_currentPlayerIndex + 1} han terminado su turno.");

        _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;

        StartPlayerTurn();
    }

    private void HandlePlayerInput()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null)
            {
                if (_selectedCharacter == null && IsCharacterInCurrentPlayer(hit.collider.GetComponent<Character>()))
                {
                    _selectedCharacter = hit.collider.GetComponent<Character>();

                    _selectedCharacter.InTurn();
                    Debug.Log($"Character selected {_selectedCharacter}");

                    if (CharacterHasMoved(_selectedCharacter))
                    {
                        Debug.Log($"Character {_selectedCharacter.name} no tiene movimientos disponibles");

                        _selectedCharacter.OutOfTurn();
                        _selectedCharacter = null;
                        return;
                    }

                    return;
                }
                else if (_selectedCharacter != null && IsCharacterInCurrentPlayer(hit.collider.GetComponent<Character>()))
                {
                    _selectedCharacter = hit.collider.GetComponent<Character>();
                    Debug.Log($"The Character has bin changed selected {_selectedCharacter}");
                    if (CharacterHasMoved(_selectedCharacter))
                    {
                        Debug.Log($"Character {_selectedCharacter.name} no tiene movimientos disponibles");
                        _selectedCharacter.OutOfTurn();
                        _selectedCharacter = null;
                        return;
                    }
                }

            }

            if (_selectedCharacter != null && !_selectedCharacter.Movement.HasMoved)
            {
                if (hit.collider == null)
                {
                    CheckOutOfTurnCharacter();
                    _selectedCharacter = null;
                    return;
                }
                Debug.Log("Entra al que envia la acción de movimiento");
                if (hit.collider.TryGetComponent<Node>(out Node node))
                {
                    node.Action();
                }


                //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //_selectedCharacter.Movement.MoveToCell(cellPos, _tilemap);
                //Debug.Log($"Character {_selectedCharacter.name} Se movio a la casilla {cellPos}");
                _selectedCharacter.OutOfTurn();
                _selectedCharacter = null;


            }

            CheckTurnEnd(); // Revisa si el turno debe finalizar
        }
    }

    public void CheckOutOfTurnCharacter()
    {
        foreach(Character character in players[_currentPlayerIndex])
        {
            character.OutOfTurn();
        }
    }
    public bool IsCharacterInCurrentPlayer(Character character)
    {
        return players[_currentPlayerIndex].Contains(character);
    }

    private bool CharacterHasMoved(Character character)
    {
        return character.Movement.HasMoved;
    }
}
