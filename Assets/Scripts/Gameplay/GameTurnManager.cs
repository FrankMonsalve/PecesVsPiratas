using System.Collections.Generic;
using UnityEngine;

public class GameTurnManager : MonoBehaviour
{
    public static GameTurnManager Instance; // Singleton para acceso global

    [SerializeField] private List<List<Character>> players; // Lista de listas de personajes, una por cada jugador
    private int currentPlayerIndex = 0; // Índice del jugador actual

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartPlayerTurn();
    }

    // Inicia el turno del jugador actual
    public void StartPlayerTurn()
    {
        List<Character> currentPlayerCharacters = players[currentPlayerIndex]; // Obtén los personajes del jugador actual

        // Resetea el movimiento de todos los personajes del jugador actual
        foreach (Character character in currentPlayerCharacters)
        {
            character.Movement.ResetMovement();
        }

        Debug.Log($"Es el turno del Jugador {currentPlayerIndex + 1}. ¡Mueve tus personajes!");
    }

    // Revisa si todos los personajes del jugador actual han terminado su turno
    public void CheckTurnEnd()
    {
        List<Character> currentPlayerCharacters = players[currentPlayerIndex];

        foreach (Character character in currentPlayerCharacters)
        {
            if (!character.Movement.HasMoved)
                return; // Si hay al menos un personaje que no ha terminado, el turno sigue
        }

        EndPlayerTurn();
    }

    // Finaliza el turno del jugador actual y pasa al siguiente jugador
    private void EndPlayerTurn()
    {
        Debug.Log($"Todos los personajes del Jugador {currentPlayerIndex + 1} han terminado su turno.");

        // Pasa al siguiente jugador
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;

        // Inicia el turno del siguiente jugador
        StartPlayerTurn();
    }
}
