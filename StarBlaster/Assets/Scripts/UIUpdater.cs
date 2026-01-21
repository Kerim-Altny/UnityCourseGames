using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] List<Image> hearts; // Kalp görsellerini buraya sürükleyeceğiz
    [SerializeField] Health playerHealth; // Player'ın Health bileşenine referans

    [Header("Score ")]
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText; // Skor metin bileşeni
    void Start()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    void Update()
    {
        int currentScore = scoreKeeper.getScore();
        scoreText.text =currentScore.ToString("0000000");
        UpdateHealthDisplay(playerHealth.GetCurrentHealth());
    }
    
    public void UpdateHealthDisplay(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

}
