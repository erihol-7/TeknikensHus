using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    [SerializeField]
    private float speed = 10f;   // this is the projectile's speed

    [SerializeField]
    private float lifespan = 10f; // this is the projectile's lifespan (in seconds)
    
    [SerializeField]
    private Color color = Color.blue;
    
    private new Rigidbody rigidbody;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        rigidbody.AddForce(rigidbody.transform.forward * speed);
        //transform.Rotate (90f, 0f, 0f); // rotate to face away from camera
        Destroy(gameObject, lifespan); // destroy after "lifespan" time
        gameObject.GetComponent<Renderer>().material.color = color; // set color to red
    }

    private void Update()
    {
        //Debug.Log(rigidbody.transform.position);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LandTile"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<FireTileScript>().ExtinguisheFire();
            
        }
    }
}
