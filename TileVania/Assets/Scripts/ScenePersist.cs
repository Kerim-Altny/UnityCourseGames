using UnityEngine;

public class ScenePersist : MonoBehaviour
{
     void Awake()
    {
        int scenePersistCount = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length;
        if (scenePersistCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
