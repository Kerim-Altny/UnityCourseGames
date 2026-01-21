using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Shooting SFX")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0,1)] float shootingVolume=1f;
    [Header("Damage SFX")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0,1)] float damageVolume=1f;
    static AudioManager instance;
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
    public void PlayShootingSFX()
    {
        if (shootingClip)
        {
            AudioSource.PlayClipAtPoint(shootingClip,Camera.main.transform.position,shootingVolume);
        }
    }

    public void PlayDamageSFX()
    {
        if (damageClip)
        {
            AudioSource.PlayClipAtPoint(damageClip,Camera.main.transform.position,damageVolume);
        }
    }
    

}
