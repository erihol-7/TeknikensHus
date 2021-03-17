using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    //Enemy = Asteroid variables
    private int numberOfEnemies;
    private float spawnRange = 10;
    public GameObject[] enemyPrefab = new GameObject[3];
    public int waveNum = 1;
    public int enemyCount;
    public GameObject sphere;
    public Heart healthObject;
    public bool startGame = false;
    private Vector3 pos;
    [SerializeField] private float spawnDistance = 20;

    public float speed;
    //Earth Variables
    public GameObject earth;
    private Vector3 addVector = new Vector3(10, 10, 10);

    public TextMeshProUGUI waveText;
   

    // Satellite variables
    public GameObject satellite;
    private int satelliteCount;
    private float satelliteTimer;
    private float satelliteWaitTime = 10.0f;

    //Health Variables
    [SerializeField] private Heart heart;

    //public GameObject powerUp;
    // Start is called before the first frame update
    void Start()
    {
        // asteroid = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        healthObject = GameObject.Find("Player").GetComponent<Heart>();
        SpawnSatellite();
        speed = 450f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            satelliteCount = GameObject.FindGameObjectsWithTag("Satellite").Length;
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount == 0 && !heart.gameOver)
            {
                SpawnEnemyWave(waveNum);
                //  Instantiate(powerUp, GenSpawnPos(), powerUp.transform.rotation);
            }

            satelliteTimer += Time.deltaTime;
            if (satelliteTimer > satelliteWaitTime)
            {
                SpawnSatellite();
                satelliteTimer -= satelliteWaitTime;
            }
        }
    }

    private Vector3 GenSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        float spawnPozY = Random.Range(-spawnRange, spawnRange);
        //Vector3 randomPos = new Vector3(spawnPosX, spawnPozY, spawnPosZ);

        float distanceFromTarget = Random.Range(5, 10);
        float spawnAngle = Random.Range(-180, 180);
        /* Vector3 randomPos = new Vector3(earth.transform.position.x + 1, 
             earth.transform.position.y + Random.Range(0.1f, spawnRange)
             , earth.transform.position.z) .normalized * distanceFromTarget;
         transform.RotateAround(earth.transform.position, Vector3.up, spawnAngle);
         */
        float planetRadius = 20.5f;
        Vector3 randomPos = Random.onUnitSphere * (planetRadius) + earth.transform.position;
        float test = Vector3.Distance(randomPos, earth.transform.position);
        if (test < 5)
        {
            randomPos += addVector;
            Debug.Log($"Distance = {test}");
        }

        randomPos.y = Mathf.Abs(randomPos.y/2);
        randomPos.z = Mathf.Abs(randomPos.z/2);
        return randomPos;
    }
    
    private Vector3 Spawnpos()
    {
        var spawn = transform;
        
        spawn.localPosition = spawn.parent.forward * spawnDistance;
        spawn.RotateAround( earth.transform.position, spawn.parent.up, Random.Range( -45.0f, 45.0f ) );
        spawn.RotateAround( earth.transform.position, spawn.parent.right , Random.Range( -45.0f, 0f ) );
        //Instantiate( astroid, spawnPoint.transform.position, Quaternion.identity );
        return spawn.position;
    }

    void SpawnEnemyWave(int numberOfEnemies)
    {
        waveText.text = "Nivå:" + waveNum;
        if (waveNum % 5 == 0)
        {
            speed += 100f;
            healthObject.gainHeart();
          
        }

        int asteroid = Random.Range(0, 2);
        if (numberOfEnemies == 1)
        {

            //pos = GenSpawnPos();
            pos = Spawnpos();
            Instantiate(enemyPrefab[asteroid], pos
                , enemyPrefab[asteroid].transform.rotation);
            
        }
        else
        {
            StartCoroutine(ExampleCoroutine(numberOfEnemies));
        }

        waveNum += 1;
    }

    IEnumerator ExampleCoroutine(int numberOfEnemies)
    {
        if (!heart.gameOver && startGame)
        {
            
            //public int varv = 1;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                //Invoke("SpawnEnemy", 4.0f);
                SpawnEnemy();
              yield return new WaitForSecondsRealtime(2);
            }
        }
    }

    void SpawnEnemy()
    {
        int asteroid = Random.Range(0, 2);
        //pos = GenSpawnPos();
        pos = Spawnpos();
        Instantiate(enemyPrefab[asteroid], pos
            , enemyPrefab[asteroid].transform.rotation);
        
    }

    void SpawnSatellite()
    {
        if (!heart.gameOver)
        {
            Instantiate(satellite, earth.transform.position, satellite.transform.rotation);
        }
    }

    public void StartGame()
    {
        startGame = true;
    }
   
}