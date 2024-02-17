using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float fireVolume = 1.0f;
    [SerializeField] private float shooterRotation;
    [SerializeField] private float initialVelocity = 12000.0f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject pinball;
    [SerializeField] private GameObject barrelEnd;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AimAtPlayerCursor();
        if (Input.GetMouseButtonDown(0))
        {
            FirePinball();
        }
    }

    void AimAtPlayerCursor()
    {
        Vector2 mousePosition = new Vector2();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float oppositeSide = mousePosition.x - transform.position.x;
        float adjacentSide = mousePosition.y - transform.position.y;

        if (mousePosition.y < transform.position.y)
        {
            shooterRotation = (Mathf.Atan(oppositeSide / adjacentSide) * Mathf.Rad2Deg) * -1;
            transform.rotation = Quaternion.Euler(Vector3.forward * shooterRotation);
        }
    }

    void FirePinball()
    {
        audioSource.PlayOneShot(audioSource.clip, fireVolume);
        GameObject firedPinball = Instantiate(pinball, barrelEnd.transform.position, barrelEnd.transform.rotation);
        Rigidbody2D pinballrb2d = firedPinball.GetComponent<Rigidbody2D>();
        Vector2 pinballDirection = barrelEnd.transform.right;
        pinballrb2d.AddForce(pinballDirection * initialVelocity, ForceMode2D.Impulse);
    }

}
