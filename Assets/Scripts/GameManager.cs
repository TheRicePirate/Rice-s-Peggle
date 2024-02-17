using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> hitPins = new List<GameObject>();
    public int bluePinCount;
    public int redPinCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bluePinCount == 0 && redPinCount == 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
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

}
