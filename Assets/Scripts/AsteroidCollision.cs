using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private string satelliteTag = "Satellite";
    private string enemyTag = "Enemy";
    public Heart healthObject;
    private string bulletTag = "Bullet";
    private string earthTag = "Earth";
    public AudioClip expSound;
    public ParticleSystem explosion;
    private int isCollision = 0;
    private AudioSource audio;
    public Scoring score;

    public int incrementScore;
    // Get components to be able to reduce health if specific conditions is met, and play explosion & sound.
    void Start()
    {
        healthObject = GameObject.FindWithTag("Player").GetComponent<Heart>();
        score = GameObject.FindWithTag("Player").GetComponent<Scoring>();
        explosion = GameObject.FindWithTag("Explosion").GetComponent<ParticleSystem>();
        audio = GameObject.FindWithTag("ARCamera").GetComponent<AudioSource>();
    }
    

    void OnTriggerEnter(Collider other)
    {
        //Sentinel check
        if (isCollision == 0)
        {
            isCollision = 1;
            
            //If asteroid collides with earth, lose health & destroy satellite
            if (other.tag == earthTag)
            {
                healthObject.loseHeart();
                explosion.transform.position = gameObject.transform.position;
                explosion.Play();
                audio.PlayOneShot(expSound, 0.25f);
                Destroy(gameObject);
            }

            //If Asteroid collides with satellite, destroy both objects and lose health.
            if (other.tag == satelliteTag)
            {
                healthObject.loseHeart();

               
                explosion.transform.position = gameObject.transform.position;
                explosion.Play();
                audio.PlayOneShot(expSound, 0.25f);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }

            //If player shoots satellite, destroy both objects gain points
            if (other.tag == bulletTag && !healthObject.gameOver)
            {
                score.UpdateScore(incrementScore);
                explosion.transform.position = gameObject.transform.position;
                explosion.Play();
                audio.PlayOneShot(expSound, 0.25f);
                Destroy(gameObject);
                Destroy(other.gameObject);
              
            }

            if (other.tag == enemyTag && gameObject.tag == enemyTag)
            {
                explosion.transform.position = gameObject.transform.position;
                explosion.Play();
                audio.PlayOneShot(expSound, 0.25f);
                Destroy(gameObject);
                Destroy(other.gameObject);
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