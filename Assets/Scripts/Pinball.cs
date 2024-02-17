using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.magnitude == 0)
        {
            gameManager.CleanUpHitPins();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pin") 
        {
            gameManager.AddToHitPins(collision.gameObject);  
        }
        if (collision.gameObject.tag == "PinballDestroyer")
        {
            Destroy(gameObject);
            gameManager.CleanUpHitPins();
        }
    }

}
