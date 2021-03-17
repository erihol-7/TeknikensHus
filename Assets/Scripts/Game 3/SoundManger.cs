using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    
    [SerializeField] AudioSource[] water;
    [SerializeField] AudioSource underwater;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Underwater()
    {
        underwater.Play();
        foreach (var sound in water)
        {
            sound.Stop();
            //Debug.Log("Enter");
        }
    }

    public void Overwater()
    {
        foreach (var sound in water)
        {
            sound.Play();
        }
        underwater.Stop();
        //Debug.Log("Exit");
    }
}
