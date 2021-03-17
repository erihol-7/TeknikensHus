using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
   
    [SerializeField]
    private GameObject worldOrigin;
    
    [SerializeField]
    private GameObject arCamera;

    [SerializeField] 
    private GameObject projectile;
    
    [SerializeField] 
    private Heart PlayerHealth;


    public GameObject ScoreToSpawn;
    public AudioClip shootSound;
    private AudioSource bulletAudio;
    

    void Start()
    {

        bulletAudio = GameObject.Find("AR Session Origin").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

  
    
    public void Shoot()
    {
      
        if (!PlayerHealth.gameOver)
        {
            var bullet = Instantiate(projectile, GetPosition(), GetRotation());
            bullet.transform.parent = worldOrigin.transform;
            bulletAudio.PlayOneShot(shootSound, 0.5f);
        }
    
    }


    private Vector3 GetRelativePositionPosition()
    {
        Vector3 relativePosition = worldOrigin.transform.position - arCamera.transform.position; //Phone position relative image
        return relativePosition;
        /*
         * Fist value is left positive
         * Second is down positive
         * Third is back positive
         */
    }
    
    private Vector3 GetPosition()
    {
        Vector3 relativePosition = arCamera.transform.position; //Phone position
        return relativePosition;
        /*
         * Fist value is left positive
         * Second is down positive
         * Third is back positive
         */
    }

    private Quaternion GetRotation()
    {
        Quaternion relativeRotation = arCamera.transform.rotation; //Phone Rotation relative image
        return relativeRotation;
        /*
         * First is tilt down positive (X)
         * Second is turn right positive (Y)
         * Third is tilt left positive (Z)
         * Fourth is unknown
         *
         * Rotation reference image (Y and X is inverse)
         * https://www.mathworks.com/help/supportpkg/android/ref/simulinkandroidsupportpackage_galaxys4_gyroscopee0545c3dd6e0d54d37feccea60d134d9.png
         */
    }
    
}