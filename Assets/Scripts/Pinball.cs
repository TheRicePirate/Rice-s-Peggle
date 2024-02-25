using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private GameManager gameManager;
    private bool ballStuck;
    [SerializeField] private float stuckTime;
    private float workingStuckTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rb2d.velocity == Vector2.zero && ballStuck == false)
        {
            ballStuck = true;
            BallStuck();
            ballStuck = false;
        }
        else
        {
            workingStuckTime = stuckTime;
        }

    }

    private void BallStuck()
    {
        if (workingStuckTime > 0)
        {
            workingStuckTime -= Time.deltaTime;
        }
        else
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
            if (gameManager.numPinballs == 0)
            {
                gameManager.RestartGame();
            }
            else
            {
                gameManager.canFirePinball = true;
            }
        }
    }

    // Method to detect if pin is about to hit last red pin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.redPinCount == 1)
        {
            Time.timeScale = gameManager.slowMotionTimeScale;
            gameManager.isNearLastPin = true;
            Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameManager.isNearLastPin = false;
    }

}
