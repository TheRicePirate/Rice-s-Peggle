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

    [SerializeField] private float slowMotionScale;
    public int numPinballs;
    public int bluePinCount;
    public int redPinCount;
    public int playerScore;
    public bool canFirePinball = true;
    public bool inSuperMode = false;
    public bool isNearLastPin = false;


    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float zoomSpeed = 20f;
    public Pinball pinBall;
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

        if (isNearLastPin)
        {
            pinBall = GameObject.FindAnyObjectByType<Pinball>();
            Camera.main.orthographicSize -= zoomSpeed * Time.deltaTime * 35;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, cameraZoom, 5f);



            Vector3 targetPosition = pinBall.transform.position;
            targetPosition.z = defaultCameraPosition.z; // Keep camera's z position unchanged
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, pinBall.rb2d.velocity.magnitude * Time.deltaTime * 35);
        }
        else
        {
            Time.timeScale = 1;
            Camera.main.orthographicSize += zoomSpeed * Time.deltaTime * 35;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1f, 5);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, defaultCameraPosition, Time.deltaTime * 35);
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
