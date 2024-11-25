using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Character[] fila1Piratas; // La fila de piratas
    public Character[] fila1Peces;   // La fila de peces

    public void StartBattle()
    {
        StartCoroutine(BattleRoutine());
    }

    private IEnumerator BattleRoutine()
    {
        int pirataIndex = 0; // Índice del pirata actual
        int pezIndex = 0;    // Índice del pez actual

        while (true)
        {
            // Verificar si ambos equipos siguen teniendo personajes vivos
            if (pirataIndex < fila1Piratas.Length && pezIndex < fila1Peces.Length)
            {
                Character pirata = fila1Piratas[pirataIndex];
                Character pez = fila1Peces[pezIndex];

                // Asegurarse de que ambos personajes están vivos antes de atacar
                if (pirata == null || pez == null || pirata.health <= 0 || pez.health <= 0)
                {
                    // Si el pirata está muerto, avanzamos al siguiente personaje
                    if (pirata == null || pirata.health <= 0)
                    {
                        pirataIndex++; 
                        continue;
                    }

                    // Si el pez está muerto, avanzamos al siguiente personaje
                    if (pez == null || pez.health <= 0)
                    {
                        pezIndex++;
                        continue;
                    }
                }

                // Batalla entre pirata y pez mientras ambos estén vivos
                while (pirata != null && pez != null && pirata.health > 0 && pez.health > 0)
                {
                    // Turno del pirata
                    if (pirata != null && pirata.health > 0)
                    {
                        pirata.Attack(pez);
                        yield return new WaitForSeconds(1f); // Espera entre ataques
                    }

                    // Verificar si el pez está muerto
                    if (pez != null && pez.health <= 0)
                    {
                        Debug.Log($"¡{pez.name} ha muerto! El pirata {pirata.name} sigue al siguiente pez.");
                        pezIndex++; // Pasar al siguiente pez
                        break; // Terminar el turno del pez
                    }

                    // Turno del pez
                    if (pez != null && pez.health > 0)
                    {
                        pez.Attack(pirata);
                        yield return new WaitForSeconds(1f); // Espera entre ataques
                    }

                    // Verificar si el pirata está muerto
                    if (pirata != null && pirata.health <= 0)
                    {
                        Debug.Log($"¡{pirata.name} ha muerto! El pez {pez.name} sigue al siguiente pirata.");
                        pirataIndex++; // Pasar al siguiente pirata
                        break; // Terminar el turno del pirata
                    }
                }
            }

            // Verificar si la batalla debe continuar o si alguno de los equipos ha perdido
            if (IsBattleOver())
            {
                Debug.Log("La batalla ha terminado.");
                break; // Termina la batalla si no hay personajes vivos
            }

            yield return new WaitForSeconds(1f); // Espera entre rondas de ataques
        }

        // Determinar al ganador
        if (GetTeamAliveCount(fila1Piratas) > GetTeamAliveCount(fila1Peces))
        {
            Debug.Log("¡Los piratas han ganado!");
        }
        else if (GetTeamAliveCount(fila1Piratas) < GetTeamAliveCount(fila1Peces))
        {
            Debug.Log("¡Los peces han ganado!");
        }
        else
        {
            Debug.Log("¡Es un empate!");
        }
    }

    // Método para verificar si la batalla terminó
    private bool IsBattleOver()
    {
        return GetTeamAliveCount(fila1Piratas) == 0 || GetTeamAliveCount(fila1Peces) == 0;
    }

    // Método para contar los personajes vivos de un equipo
    private int GetTeamAliveCount(Character[] team)
    {
        int aliveCount = 0;
        foreach (var character in team)
        {
            if (character != null && character.health > 0) // Si el personaje está vivo
            {
                aliveCount++;
            }
        }
        return aliveCount;
    }
}
