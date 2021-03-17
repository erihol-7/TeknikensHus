using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private GameObject player;
    private Rigidbody enemyRb;
    private SpawnManager spawn;
    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Earth");
        spawn = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>();
        speed = spawn.speed;
        var spawnSphere = Instantiate(sphere, transform.position
            , sphere.transform.rotation);
        spawnSphere.transform.parent = transform;
        Debug.Log($"Speed: {speed}");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDir = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDir * speed);
        
    }

    public void IncreaseSpeed(int num)
    {
        speed += num;
    }
}