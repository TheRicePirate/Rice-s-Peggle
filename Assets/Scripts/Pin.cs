using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum PinType
{
    Blue,
    Red
}

public class Pin : MonoBehaviour
{
    public PinType pinType;
    private GameManager gameManager;
    [SerializeField] private GameObject scoreDisplayPrefab;

    public bool notHit = true;
    public Sprite normalScoreDisplay;
    public Sprite superScoreDisplay;

    private bool audioPlayed = false;
    [SerializeField] private float soundEffectVolume = 1.0f;
    [SerializeField] private Light2D light2D;
    [SerializeField] private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && audioPlayed == false && !gameManager.inSuperMode) 
        {
            audioSource.PlayOneShot(audioSource.clip, soundEffectVolume);
            light2D.enabled = true;
            audioPlayed = true;
            GameObject spawnedScoreDisplay = Instantiate(scoreDisplayPrefab, transform.position, Quaternion.identity);
            Debug.Log("Found spawned score display");
            ScoreDisplay scoreDisplay = spawnedScoreDisplay.GetComponent<ScoreDisplay>();
            scoreDisplay.scoreDisplaySprite = normalScoreDisplay;
        }
    }
}
