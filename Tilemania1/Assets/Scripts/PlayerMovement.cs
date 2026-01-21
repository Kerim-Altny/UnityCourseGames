using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    [SerializeField] float moveSpeed=10f;
    [SerializeField] float jumpSpeed=5f;
    [SerializeField] float climbSpeed=4f;
    Animator myAnimator;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;
    float gravityScaleStart;
    bool isAlive=true;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Vector2 deathKick=new Vector2(10f,20f);

    void Start()
    {
        myRigidbody=GetComponent<Rigidbody2D>();
        myAnimator=GetComponent<Animator>();
        capsuleCollider=GetComponent<CapsuleCollider2D>();
        gravityScaleStart=myRigidbody.gravityScale;
        boxCollider=GetComponent<BoxCollider2D>();
    }

    [System.Obsolete]
    void Update()
    {
        if (!isAlive){return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive){return;}
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myRigidbody.linearVelocity+=new Vector2(0f,jumpSpeed);
        }
    }
    void Run(){
        Vector2 playerVelocity=new Vector2(moveInput.x*moveSpeed,myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity=playerVelocity;
        bool hasFlip=Mathf.Abs(myRigidbody.linearVelocity.x)>Mathf.Epsilon;
        myAnimator.SetBool("isRunning",hasFlip);
    }
    void FlipSprite()
    {
        bool hasFlip=Mathf.Abs(myRigidbody.linearVelocity.x)>Mathf.Epsilon;
        if (hasFlip)
        {
           transform.localScale=new Vector2(Mathf.Sign(myRigidbody.linearVelocity.x),1f); 
        }
        
    }
    void ClimbLadder()
    { if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
             myAnimator.SetBool("isClimbing",false);
            myRigidbody.gravityScale=gravityScaleStart;
            return;
        }
        myRigidbody.gravityScale=0f;
         Vector2 climbVelocity=new Vector2(myRigidbody.linearVelocity.x,moveInput.y*climbSpeed);
           myRigidbody.linearVelocity=climbVelocity;
              bool hasClimbing=Mathf.Abs(myRigidbody.linearVelocity.y)>Mathf.Epsilon;
        myAnimator.SetBool("isClimbing",hasClimbing);
    }
    void OnAttack(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        Instantiate(bullet,gun.position,transform.rotation);
    }

    [System.Obsolete]

    void Die()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))
        {
            isAlive=false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.linearVelocity=deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
