using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public bool isNearLastPin = false;

    // Dumb speed modifier for camera. Used to make things appear smoother.
    [SerializeField] private float globalSpeedModifier = 35f;

    public float slowMotionTimeScale = 0.25f;
    [SerializeField] private float zoomSpeed = 20f;
    [SerializeField] private float defaultCameraZoom = 5.0f;
    [SerializeField] float cameraZoom = 1.25f;
    private Vector3 defaultCameraPosition;


    // Start is called before the first frame update
    void Start()
    {
        defaultCameraPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
/*        if (redPinCount == 0)
        {
            RestartGame();
        }*/

        CameraSupermodeZoom();

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

    private void CameraSupermodeZoom()
    {
        if (isNearLastPin)
        {
            Pinball pinBall = GameObject.FindAnyObjectByType<Pinball>();
            Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime * globalSpeedModifier;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, cameraZoom, defaultCameraZoom);

            Vector3 targetPosition = pinBall.transform.position;
            targetPosition.z = defaultCameraPosition.z;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, pinBall.rb2d.velocity.magnitude * Time.deltaTime * globalSpeedModifier);
        }
        else
        {
            Time.timeScale = 1;
            Camera.main.orthographicSize += zoomSpeed * Time.deltaTime * globalSpeedModifier;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, cameraZoom, defaultCameraZoom);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, defaultCameraPosition, Time.deltaTime * globalSpeedModifier);
        }
    }
}
