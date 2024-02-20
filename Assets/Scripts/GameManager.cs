using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numPinballsText;
    [SerializeField] private TextMeshProUGUI scoreText;
    public List<GameObject> hitPins = new List<GameObject>();
    public int numPinballs;
    public int bluePinCount;
    public int redPinCount;
    public int playerScore;
    public bool canFirePinball = true;
    public bool inSuperMode = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (redPinCount == 0)
        {
            RestartGame();
        }
        numPinballsText.text = $"Pinballs: {numPinballs.ToString()}";
        scoreText.text = $"Score: {playerScore.ToString()}";
    }

    public void AddToHitPins(GameObject hitPin)
    {
        hitPins.Add(hitPin);
        Pin pin = hitPin.GetComponent<Pin>();
        if (pin.notHit)
        {
            switch (pin.pinType)
            {
                case PinType.Blue:
                    bluePinCount--;
                    break;
                case PinType.Red:
                    redPinCount--;
                    break;
            }
            pin.notHit = false;
        }
    }

    public void CleanUpHitPins()
    {
        foreach(GameObject hitPin in hitPins) 
        {
            Destroy(hitPin);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
