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
    [SerializeField] private float minPinDistance;

    private float sideBoundaryPos;
    private float upperHorizontalBoundaryPos;
    private float lowerHorizontalBoundaryPos;
    private List<Vector2> pinLocations = new List<Vector2>();
    private Vector2 workingSpawnLocation;

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

    Vector2 GenerateSpawnLocation()
    {
        float xPositionPin = Random.Range(-sideBoundaryPos, sideBoundaryPos);
        float yPositionPin = Random.Range(lowerHorizontalBoundaryPos, upperHorizontalBoundaryPos);
        return new Vector2(xPositionPin, yPositionPin);
    }

    void SpawnPins()
    {
        for (int i = 0; i < bluePinCount; i++) 
        {
            Debug.Log(i);
            // Initializes pinLocations list to permit the for loop
            if (i == 0)
            {
                Vector2 spawnLocation = GenerateSpawnLocation();
                Instantiate(PinTypes[0], spawnLocation, Quaternion.identity);
                pinLocations.Add(spawnLocation);
                continue;
            }

            while (true)
            {
                bool isValidPinDistance = true;
                workingSpawnLocation = GenerateSpawnLocation();
                List<float> distances = new List<float>();  

                foreach (UnityEngine.Vector2 existingPinLocation in pinLocations)
                {
                    distances.Add(Vector2.Distance(workingSpawnLocation, existingPinLocation));
                }

                foreach (float distance in distances)
                {
                    if (distance < minPinDistance)
                    {
                        isValidPinDistance = false;
                        break;
                    }
                }

                if (isValidPinDistance)
                {
                    break;
                }
            }

            Instantiate(PinTypes[0], workingSpawnLocation, Quaternion.identity);
            pinLocations.Add(workingSpawnLocation);
        }
    }
}
