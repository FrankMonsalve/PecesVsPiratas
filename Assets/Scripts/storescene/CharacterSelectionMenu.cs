using UnityEngine;
using TMPro;  // Necesario para trabajar con TextMeshPro

public class CharacterSelectionMenu : MonoBehaviour
{
    public GameObject[] playerObjects;  // Los prefabs de los personajes
    public int selectedCharacter = 0;

    // Variables para el texto de UI utilizando TextMeshPro
    public TMP_Text textForma;   // Referencia al TextMeshPro de Forma
    public TMP_Text textColor;   // Referencia al TextMeshPro de Color

    private string selectedCharacterDataName = "SelectedCharacter";

    void Start()
    {
        HideAllCharacters();

        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        playerObjects[selectedCharacter].SetActive(true);

        // Mostrar la información del primer personaje seleccionado
        UpdateCharacterInfo(selectedCharacter);
    }

    private void HideAllCharacters()
    {
        foreach (GameObject g in playerObjects)
        {
            g.SetActive(false);
        }
    }

    public void NextCharacter()
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter >= playerObjects.Length)
        {
            selectedCharacter = 0;
        }
        playerObjects[selectedCharacter].SetActive(true);

        // Actualizar la información
        UpdateCharacterInfo(selectedCharacter);
    }

    public void PreviousCharacter()
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter = playerObjects.Length - 1;
        }
        playerObjects[selectedCharacter].SetActive(true);

        // Actualizar la información
        UpdateCharacterInfo(selectedCharacter);
    }

    // Método para actualizar los textos con la información de forma y color
    private void UpdateCharacterInfo(int characterIndex)
    {
        // Obtener el script Personaje del prefab actual
        Personaje personaje = playerObjects[characterIndex].GetComponent<Personaje>();

        // Actualizar los textos en la UI con TextMeshPro
        textForma.text = "Forma: " + personaje.Forma;
        textColor.text = "Color: " + personaje.Color;
    }
}
