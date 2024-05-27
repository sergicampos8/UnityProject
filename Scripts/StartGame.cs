using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public TMP_Text scoreText; // Referencia al Text del puntaje final
    public void StartingGame()
    {
        // Carga la siguiente escena del juego
        // Asegúrate de que el nombre de la escena coincida con el nombre de tu escena de juego
        SceneManager.LoadScene("MainGame");
    }

    public void SetScore()
    {
        // Actualizar el texto del puntaje final
        scoreText.text = "Last Score: " + GameManager.LastScore;
    }
}
