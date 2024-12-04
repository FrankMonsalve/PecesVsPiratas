using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public enum StateGame
{
    InStore,
    InGame,
    GameOver
}
public class GameTurnManager : MonoBehaviour
{
    public static GameTurnManager Instance; // Singleton para acceso global

    [SerializeField] private Tilemap _tilemap; // Referencia al Tilemap
    [SerializeField] private List<List<Character>> _players; // Lista de listas de personajes
    public LayerMask _layerMaskCharacter;
    public LayerMask _layerMaskNode;
    private int _currentPlayerIndex = 0; // Índice del jugador actual
    private Character _selectedCharacter;
    public Tilemap Tilemap => _tilemap;

    public PlayerInTurn IndexPlayerUI;

    public Canvas WinScreen;
    public TMP_Text TextoGanador;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        _layerMaskNode = LayerMask.GetMask("LayerNodo");
        _layerMaskCharacter = LayerMask.GetMask("InteractableLayer");

    }

    public void Initializate()
    {
        if (_tilemap == null)
        {
            Debug.LogError("Tilemap no asignado en GameTurnManager.");
            return;
        }

        StartPlayerTurn();
        _selectedCharacter = null;
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

        _players = playerLists;
        Debug.Log("Lista de Jugadores cargada correctamente.");


    }

    // Inicia el turno del jugador actual
    public void StartPlayerTurn()
    {
        List<Character> currentPlayerCharacters = _players[_currentPlayerIndex];
        UpdatePlayerInturnUI(_currentPlayerIndex + 1);

        foreach (Character character in currentPlayerCharacters)
        {
            character.Movement.ResetMovement();
        }

        Debug.Log($"Es el turno del Jugador {_currentPlayerIndex + 1}. ¡Mueve tus personajes!");
    }

    public void CheckTurnEnd()
    {

        List<Character> currentPlayerCharacters = _players[_currentPlayerIndex];

        
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


        ChangePlayer();
    }

    private void ChangePlayer()
    {
        Debug.Log("Cambiando de Jugador");
        if (_currentPlayerIndex >= _players.Count - 1)
        {
            _currentPlayerIndex = 0;

        }
        else
        {
            _currentPlayerIndex++;
        }
        Debug.Log($"Jugador Seleccionado: {_currentPlayerIndex}");

        StartPlayerTurn();
        CheckGame();

    }

    private void HandlePlayerInput()
    {
        
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit;

            if (_selectedCharacter == null)
            {
                hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 1f, _layerMaskCharacter);
            }
            else
            {
                hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 1f, _layerMaskNode);
            }

            Debug.Log(hit.collider);
            Debug.DrawRay(hit.centroid, Vector2.zero * 1f, Color.red, 2f);

            if (hit.collider != null)
            {
                if (_selectedCharacter == null && IsCharacterInCurrentPlayer(hit.collider.GetComponent<Character>()))
                {
                    _selectedCharacter = hit.collider.GetComponent<Character>();


                    Debug.Log($"Character selected {_selectedCharacter}");

                    if (CharacterHasMoved(_selectedCharacter))
                    {

                        Debug.Log($"Character {_selectedCharacter.name} no tiene movimientos disponibles");

                        _selectedCharacter.OutOfTurn();
                        _selectedCharacter = null;
                        return;
                    }

                    _selectedCharacter.InTurn();

                    return;
                }
                else if (_selectedCharacter != null && IsCharacterInCurrentPlayer(hit.collider.GetComponent<Character>()))
                {
                    _selectedCharacter.OutOfTurn();

                    _selectedCharacter = hit.collider.GetComponent<Character>();
                    _selectedCharacter.InTurn();
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
                    Debug.Log("1");

                    CheckOutOfTurnCharacter();
                    _selectedCharacter = null;
                    return;
                }

                Debug.Log("Entra al que envia la acción de movimiento");
                if (hit.collider.TryGetComponent<Node>(out Node node))
                {
                    Debug.Log("2");
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
        foreach (Character character in _players[_currentPlayerIndex])
        {
            character.OutOfTurn();
        }
    }
    public bool IsCharacterInCurrentPlayer(Character character)
    {
        return _players[_currentPlayerIndex].Contains(character);
    }

    private bool CharacterHasMoved(Character character)
    {
        return character.Movement.HasMoved;
    }

    public void UpdatePlayerInturnUI(int index)
    {
        IndexPlayerUI.UpdatePlayerInturn(index);
    }

    public void CheckGame()
    {
        int Cantidad = _players[_currentPlayerIndex].Count;

        for(int i = 0; i < _players.Count; i++ )
        {
            if (_players[i].Count < 1)
            {
                if (i == 0)
                {
                    TextoGanador.text = $"VICTRY PLAYER 2";
                }
                else
                {
                    TextoGanador.text = $"VICTRY PLAYER 1";
                }

                WinScreen.gameObject.SetActive(true);
                return;
            }
            
        }

       
    }
}
