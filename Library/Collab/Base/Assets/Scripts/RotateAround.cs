using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotateAround : MonoBehaviour
{
    //Assign a GameObject in the Inspector to rotate around
    [SerializeField] private GameObject target;

    [SerializeField] private float distanceFromTarget = 2.0f;
    [SerializeField] private float spawnRange = 1.0f;
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private int spawnAngle;
    [SerializeField] private bool rotateX = true;
    [SerializeField] private bool rotateY;




    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Earth");
        }
        
        var targetPos = target.transform.position;
        if (rotateX)
        {
            transform.position =
                new Vector3(targetPos.x + Random.Range(-spawnRange, spawnRange), targetPos.y, targetPos.z + 1)
                    .normalized * distanceFromTarget;
            transform.RotateAround(target.transform.position, Vector3.left, spawnAngle);
        }
        else if (rotateY)
        {
            transform.position =
                new Vector3(targetPos.x + 1.5f, targetPos.y, targetPos.z + Random.Range(-spawnRange, spawnRange))
                    .normalized * distanceFromTarget;
            transform.RotateAround(target.transform.position, Vector3.forward, spawnAngle);
        }

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.

        if (rotateX)
        {
            transform.RotateAround(target.transform.position, Vector3.left, speed * Time.deltaTime);
            
            //Vector3 toVector = new Vector3(0,target.transform.position.y, target.transform.position.z) - new Vector3(0,transform.position.y, transform.position.z);
            //float angleToTarget = Vector3.Angle(toVector, target.transform.down);
            //Debug.Log(angleToTarget);
        }
        else if (rotateY)
        {
            transform.RotateAround(target.transform.position, Vector3.forward, speed * Time.deltaTime);
        }

    }

}
