using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles collisions with satellite and projectile (bullet), collision with asteroid is handled in another script */
public class SatelliteCollision : MonoBehaviour
{
   
    public Heart healthObject;
    private string bulletTag = "Bullet";
    public AudioClip expSound;
    public ParticleSystem explosion;
    private int isCollision = 0;
    private AudioSource audio;

    // Get components to reduce health upon impact, play explosion particle and sound.
    void Start()
    {
        healthObject = GameObject.Find("Player").GetComponent<Heart>();
        explosion = GameObject.FindWithTag("Explosion").GetComponent<ParticleSystem>();
        audio = GameObject.FindWithTag("Satellite").GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //Sentinel 
        if (isCollision == 0)
        {
            isCollision = 1;
            //If player shoots satellite, lose health destroy both objects, play explosion particle & sound
            if (other.tag == bulletTag)
            {
                healthObject.loseHeart();
                Destroy(gameObject);
                Destroy(other.gameObject);
                explosion.transform.position = gameObject.transform.position;
                explosion.Play();
                audio.PlayOneShot(expSound, 0.25f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Restore sentinel so new trigger event can occur.
        if (isCollision == 1)
        {
            isCollision = 0;
        }
    }
}