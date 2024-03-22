using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreTotal;    // referencia al texto del score
    [SerializeField] private TextMeshProUGUI bodysTotal;    // referencia al texto del bodys

    private static GameManager instance;                    // referencia Singleton al GameManager
    private int score = 0;                                  // referencia a la variable Score (puntos acumulados)
    private int bodys = 2;                                  // referencia a la variable bodys (bodys acumulados)

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
        bodysTotal.text = bodys.ToString();

    }

    // Retorna el score acumulado y bodys acumulado
    public int Score()
    {
        return score;
    }

    // Retorna el score acumulado y bodys acumulado
    public void Bodys(int body)
    {
        bodys = body;
    }

    // Adiciona los puntos logrados al Score y los bodys alcanzados
    public void AddScore(int points)
    {
        score += points;
        // AudioManager
    }


    // Restablece a cero el Score
    public void ResetScore()
    {
        score = 0;
        bodys = 2;
    }

}
