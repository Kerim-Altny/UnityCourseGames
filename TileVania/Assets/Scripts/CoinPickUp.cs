using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private AudioClip coinSound;
    bool isCollected = false;

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
            FindObjectOfType<GameSession>().AddToScore(1);
            
        }
    }
}
