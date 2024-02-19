using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public Sprite scoreDisplaySprite;
    [SerializeField] private SpriteRenderer scoreRenderer;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float displayTimeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        scoreRenderer.sprite = scoreDisplaySprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayTimeRemaining > 0) 
        {
            displayTimeRemaining -= Time.deltaTime;
            transform.Translate(Vector2.up * Time.deltaTime * movementSpeed);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
