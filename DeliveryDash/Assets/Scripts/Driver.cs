using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{
    [SerializeField] float currentSpeed = 5f; 
    [SerializeField] float steerSpeed = 200f;
    [SerializeField] float boostSpeed = 10f;
    [SerializeField] float regularSpeed = 5f;

    [SerializeField] TMP_Text boostText;

   
    void Start()
    {

        boostText.gameObject.SetActive(false);
    }
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boost"))
        {
            currentSpeed = boostSpeed;
            boostText.gameObject.SetActive(true);    
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("World Collision"))
        {
              currentSpeed = regularSpeed;
        boostText.gameObject.SetActive(false);
       
        }
       
        
    }
    void Update()
    {
        float move = 0;
        float steer = 0;
        
        if (Keyboard.current.wKey.isPressed)
        {
            move = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            move=-1;
        }
         if (Keyboard.current.aKey.isPressed)
        {
            steer = 1;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            steer = -1;
        }
        float moveAmount = move * currentSpeed * Time.deltaTime;
        float steerAmount = steer * steerSpeed * Time.deltaTime;
        transform.Rotate(0, 0, steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
}
