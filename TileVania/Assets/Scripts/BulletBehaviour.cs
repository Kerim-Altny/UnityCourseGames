using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] float bulletSpeed=20f;
    Rigidbody2D bulletbody;
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
        bulletbody=GetComponent<Rigidbody2D>();
        player=FindFirstObjectByType<PlayerMovement>();
        xSpeed=player.transform.localScale.x*bulletSpeed;
    }

    void Update()
    {
        bulletbody.linearVelocity=new Vector2(xSpeed,0f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
