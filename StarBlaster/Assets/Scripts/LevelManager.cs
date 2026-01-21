using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class LevelManager : MonoBehaviour
{

    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
        scoreKeeper.resetScore();
    }
    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad("GameOver",2f));
    }
    public void QuitGame()
    {      
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string scenename,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(scenename);
    }
}
