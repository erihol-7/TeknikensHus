using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWater : MonoBehaviour
{
    public int waterAmount = 0;
    public int waterMaxAmount = 2; 
    private GameManager gameManager;
    private SoundManger soundManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("AR Session Origin").GetComponent<GameManager>();
        soundManager = GameObject.Find("AR Camera").GetComponent<SoundManger>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SeaTile"))
        {
            //Debug.Log("SeaTile");
            // play water fill sound
            waterAmount = waterMaxAmount;
        }

        if (other.CompareTag("UnderLevel"))
        {
            // Play under water sound and disable ambient water sound
            soundManager.Underwater();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UnderLevel"))
        {
            // Disable under water sound and play ambient water sound
            soundManager.Overwater();
        }
    }
}
