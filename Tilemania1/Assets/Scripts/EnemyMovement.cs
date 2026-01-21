using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed=1f;
    Rigidbody2D enemyBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyBody.linearVelocity=new Vector2(moveSpeed,0f);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
         moveSpeed=-moveSpeed;
         FlipFace();
    }
    void FlipFace()
    {
       transform.localScale=new Vector2(-(Mathf.Sign(enemyBody.linearVelocity.x)),1f); 
    }
}
