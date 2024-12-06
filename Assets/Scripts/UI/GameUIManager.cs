using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Canvas _winScreen;
    [SerializeField] private Button _inGame;
    [SerializeField] private TMP_Text _winnerText;
    [SerializeField] private TMP_Text _currentPlayerText;

    public void ShowWinner(int winnerPlayerIndex)
    {
        _winnerText.text = $"VICTORY PLAYER {winnerPlayerIndex}";
        _winScreen.gameObject.SetActive(true);
    }

    public void UpdateCurrentPlayer(int playerIndex)
    {
        _currentPlayerText.text = $"Player {playerIndex}'s Turn";
    }

    public void ShowUIInGame()
    {
        _inGame.gameObject.SetActive(true);
    }


}