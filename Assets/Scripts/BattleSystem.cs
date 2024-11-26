using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, FIGHT, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public BattleState start;
    public GameObject playerPrefab1; 
    public GameObject playerPrefab2; 
    public GameObject playerPrefab3;
    public GameObject playerPrefab4;
    public GameObject playerPrefab5;
    public GameObject playerPrefab6; 
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;  
    public GameObject enemyPrefab3;
    public GameObject enemyPrefab4;
    public GameObject enemyPrefab5;
    public GameObject enemyPrefab6;

    public Transform[] playerBattleStations; 
    public Transform[] enemyBattleStations;


    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        Instantiate(playerPrefab1, playerBattleStations[0].position, Quaternion.Euler(0, 0, 0));
        Instantiate(playerPrefab2, playerBattleStations[1].position, Quaternion.Euler(0, 0, 0));
        Instantiate(playerPrefab3, playerBattleStations[2].position, Quaternion.Euler(0, 0, 0));
        Instantiate(playerPrefab4, playerBattleStations[3].position, Quaternion.Euler(0, 0, 0));
        Instantiate(playerPrefab5, playerBattleStations[4].position, Quaternion.Euler(0, 0, 0));
        Instantiate(playerPrefab6, playerBattleStations[5].position, Quaternion.Euler(0, 0, 0));

        
        Instantiate(enemyPrefab1, enemyBattleStations[0].position, Quaternion.Euler(0, 180, 0));
        Instantiate(enemyPrefab2, enemyBattleStations[1].position, Quaternion.Euler(0, 180, 0));
        Instantiate(enemyPrefab3, enemyBattleStations[2].position, Quaternion.Euler(0, 180, 0));
        Instantiate(enemyPrefab4, enemyBattleStations[3].position, Quaternion.Euler(0, 180, 0));
        Instantiate(enemyPrefab5, enemyBattleStations[4].position, Quaternion.Euler(0, 180, 0));
        Instantiate(enemyPrefab6, enemyBattleStations[5].position, Quaternion.Euler(0, 180, 0));

        state = BattleState.FIGHT;

    }
    
    
}
