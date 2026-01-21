using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Start()
    {
        livesText.text = "Lives:" + playerLives.ToString();
        scoreText.text = "Score:" + playerScore.ToString();
    }

    void Awake()
    {
        int gameSessionCount = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (gameSessionCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    [System.Obsolete]
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
        livesText.text = "Lives:" + playerLives.ToString();
    }

    [System.Obsolete]
    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        scoreText.text = "Score:" + playerScore.ToString();
    }
}