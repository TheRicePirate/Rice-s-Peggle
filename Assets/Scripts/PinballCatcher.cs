using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballCatcher : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private float speed;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        float maxLeft = leftBorder.transform.position.x;
        float maxRight = rightBorder.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            if (transform.position.x < rightBorder .transform.position.x) 
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            if (transform.position.x > leftBorder.transform.position.x)
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
            else
            {
                movingRight = true;
            }
        }
    }

    // Pinball is caught
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        gameManager.canFirePinball = true;
        gameManager.CleanUpHitPins();
        gameManager.numPinballs++;
    }

    // Moving left and right
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RightBorder")
        {
            Debug.Log("Hit right border");
            movingRight = false;
        }
        if (collision.gameObject.tag == "LeftBorder")
        {
            movingRight = true;
        }
    }

}
