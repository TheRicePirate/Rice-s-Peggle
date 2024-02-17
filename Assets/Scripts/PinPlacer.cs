using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPlacer : MonoBehaviour
{
    [SerializeField] private GameObject sideBorder;
    [SerializeField] private GameObject upperSpawnBorder;
    [SerializeField] private GameObject lowerSpawnBorder;

    [SerializeField] private GameObject[] PinTypes;
    [SerializeField] private int bluePinCount;
    [SerializeField] private int redPinCount;


    private float sideBoundaryPos;
    private float upperHorizontalBoundaryPos;
    private float lowerHorizontalBoundaryPos;
    private bool foundValidPos = true;
    private UnityEngine.Vector2 pinSpawnLocation;
    private List<UnityEngine.Vector2> pinLocations;

    // Start is called before the first frame update
    void Start()
    {
        sideBoundaryPos = sideBorder.transform.position.x;
        upperHorizontalBoundaryPos = upperSpawnBorder.transform.position.y;
        lowerHorizontalBoundaryPos = lowerSpawnBorder.transform.position.y;
        Debug.Log(upperHorizontalBoundaryPos);
        Debug.Log(lowerHorizontalBoundaryPos);
        Debug.Log(sideBoundaryPos);
        SpawnPins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPins()
    {
        for (int i = 0; i < bluePinCount; i++) 
        {
            while (foundValidPos == false)
            {
                //e
                float xPositionPin = Random.Range(-sideBoundaryPos, sideBoundaryPos);
                float yPositionPin = Random.Range(lowerHorizontalBoundaryPos, upperHorizontalBoundaryPos);
                UnityEngine.Vector2 pinSpawnLocation = new Vector2(xPositionPin, yPositionPin);
                List<float> distances = new List<float>();  

                foreach (UnityEngine.Vector2 existingPinLocation in pinLocations)
                {
                    if (Vector2.Distance(pinSpawnLocation, existingPinLocation) < 1)
                    {
                        foundValidPos = false;
                        break;
                    }
                }

                if (foundValidPos)
                {

                    Instantiate(PinTypes[0], pinSpawnLocation, Quaternion.identity);

                }

            }
                UnityEngine.Vector2 pinSpawnLocation = new Vector2(xPositionPin, yPositionPin);
                Instantiate(PinTypes[0], pinSpawnLocation, Quaternion.identity);                
        }

        for (int i = 0; i < redPinCount; i++)
        {
            float xPositionPin = Random.Range(-sideBoundaryPos, sideBoundaryPos);
            float yPositionPin = Random.Range(lowerHorizontalBoundaryPos, upperHorizontalBoundaryPos);
            UnityEngine.Vector2 pinSpawnLocation = new Vector2(xPositionPin, yPositionPin);
            Instantiate(PinTypes[1], pinSpawnLocation, Quaternion.identity);
        }

    }
}
