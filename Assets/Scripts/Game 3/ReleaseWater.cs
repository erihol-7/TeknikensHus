using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseWater : MonoBehaviour
{
   
    [SerializeField]
    private GameObject worldOrigin;
    
    [SerializeField]
    private GameObject arCamera;

    [SerializeField] 
    private GameObject projectile;

    private GameManager gameManager;
    private GetWater getWater;
    
    void Start()
    {
       
        gameManager = GameObject.Find("AR Session Origin").GetComponent<GameManager>();
        getWater = GameObject.Find("AR Camera").GetComponent<GetWater>();
    }

    // Update is called once per frame
    void Update()
    {
        // if screen is clicked then.. Might be more efficient way to detect if so then change
        /*if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }*/
    }

  
    [ContextMenu("shoot")]
    public void Shoot()
    {
        if (!gameManager.gameover && getWater.waterAmount > 0)
        {
            var bullet = Instantiate(projectile, arCamera.transform.position, arCamera.transform.rotation);
            bullet.transform.parent = worldOrigin.transform;
            getWater.waterAmount--;
            // play shoot water sound
        }
    }
}
