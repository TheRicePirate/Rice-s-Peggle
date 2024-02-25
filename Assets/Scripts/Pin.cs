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
    [SerializeField] private AudioSource normalHitSound;
    [SerializeField] private AudioSource superHitSound;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void SpawnScoreDisplay(Sprite scoreSprite)
    {
        GameObject spawnedScoreDisplay = Instantiate(scoreDisplayPrefab, transform.position, Quaternion.identity);
        ScoreDisplay scoreDisplay = spawnedScoreDisplay.GetComponent<ScoreDisplay>();
        scoreDisplay.scoreDisplaySprite = scoreSprite;
    }

    private void PlayHitSound(AudioSource hitSoundEffect)
    {
        if (!audioPlayed)
        {
            hitSoundEffect.PlayOneShot(hitSoundEffect.clip, soundEffectVolume);
            audioPlayed = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        light2D.enabled = true;
        if (collision != null && audioPlayed == false) 
        {
            if (gameManager.inSuperMode && pinType != PinType.Red)
            {
                PlayHitSound(superHitSound);
                SpawnScoreDisplay(superScoreDisplay);
            }
            else
            {
                if (pinType == PinType.Red)
                {
                    gameManager.playerScore += 200;
                }
                else
                {
                    gameManager.playerScore += 50;
                }

                PlayHitSound(normalHitSound);
                SpawnScoreDisplay(normalScoreDisplay);
            }
        }
    }
}
