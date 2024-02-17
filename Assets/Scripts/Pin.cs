using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pin : MonoBehaviour
{
    [SerializeField] private float soundEffectVolume = 1.0f;
    [SerializeField] private Light2D light2D;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) 
        {
            audioSource.PlayOneShot(audioSource.clip, soundEffectVolume);
            light2D.enabled = true;
            Debug.Log("Hit");
        }
    }
}
