using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    private int currentScore=0;
    static ScoreKeeper instance;
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        //int numberOfAudioManagers = FindObjectsOfType<AudioManager>().Length;
        //if (numberOfAudioManagers > 1)
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public int getScore()
    {
        return currentScore;
    }
    public void setScore(int score)
    {
        currentScore+=score;
        currentScore=Mathf.Clamp(currentScore,0,int.MaxValue);
        print(currentScore);
    }
    public void resetScore()
    {
        currentScore=0;
    }
 
}
