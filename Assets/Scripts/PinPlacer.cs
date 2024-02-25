using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPlacer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Borders
    [SerializeField] private GameObject sideBorder;
    [SerializeField] private GameObject upperSpawnBorder;
    [SerializeField] private GameObject lowerSpawnBorder;

    // Pin values
    [SerializeField] private GameObject[] PinTypes;
    private int bluePinCount;
    private int redPinCount;

    // Positions
    private float sideBoundaryPos;
    private float upperHorizontalBoundaryPos;
    private float lowerHorizontalBoundaryPos;
    private List<Vector2> pinLocations = new List<Vector2>();
    private Vector2 workingSpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        sideBoundaryPos = sideBorder.transform.position.x;
        upperHorizontalBoundaryPos = upperSpawnBorder.transform.position.y;
        lowerHorizontalBoundaryPos = lowerSpawnBorder.transform.position.y;
        bluePinCount = gameManager.bluePinCount;
        redPinCount = gameManager.redPinCount;
        PinSpawner(PinTypes[0], bluePinCount);
        PinSpawner(PinTypes[1], redPinCount);
    }

    Vector2 GenerateSpawnLocation()
    {
        float xPositionPin = Random.Range(-sideBoundaryPos, sideBoundaryPos);
        float yPositionPin = Random.Range(lowerHorizontalBoundaryPos, upperHorizontalBoundaryPos);
        return new Vector2(xPositionPin, yPositionPin);
    }

    void PinSpawner(GameObject pinType, int pinAmount)
    {
        for (int i = 0; i < pinAmount; i++) 
        {
            // Initializes pinLocations list to permit the for loop
            if (pinLocations.Count == 0)
            {
                Vector2 spawnLocation = GenerateSpawnLocation();
                Instantiate(pinType, spawnLocation, Quaternion.identity);
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
                    if (distance < gameManager.minPinDistance)
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

            Instantiate(pinType, workingSpawnLocation, Quaternion.identity);
            pinLocations.Add(workingSpawnLocation);
        }
    }
}
