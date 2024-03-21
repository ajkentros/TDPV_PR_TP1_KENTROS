using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreTotal;    // referencia al texto del score

    private static GameManager instance;                    // referencia Singleton al GameManager
    private int score = 0;                                  // referencia a la variable Score (puntos acumulados)

    private void Awake()
    {
        // Garantiza que solo haya una instancia del GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        scoreTotal.text = Score().ToString();
    }

    // Retorna el score acumulado
    public int Score()
    {
        return score;
    }

    // Adiciona los puntos logrados al Score
    public void AddScore(int points)
    {
        score += points;
        // AudioManager

    }


    // Restablece a cero el Score
    public void ResetScore()
    {
        score = 0;
    }

}
