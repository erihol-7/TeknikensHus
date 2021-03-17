using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireTileScript : MonoBehaviour
{
    [SerializeField] private GameObject[] neighborTiles;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject water;
    [SerializeField] private float randomFireSpreadTime = 1f;
    public bool onFire = false;
    private GameObject tileFire;
    private GameObject tileWater;
    private GameManager gameManager;
    private IEnumerator coroutineSpreadFire;
    private IEnumerator coroutineOnFireTime;
    

    void Start()
    {
        coroutineSpreadFire = SpreadFire();
        coroutineOnFireTime = OnFireTime();
        ShuffleArray(neighborTiles);
        gameManager = GameObject.Find("AR Session Origin").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private static void ShuffleArray<T>(T[] arr) {
        // Shuffle Tile array randomly
        for (int i = arr.Length - 1; i > 0; i--) {
            int r = Random.Range(0, i);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
    
    private IEnumerator SpreadFire()
    {
        while (onFire) 
        {
            // spread fire at random time diff
            float timeToSpreadFire = gameManager.timeToSpreadFire; 
            yield return new WaitForSeconds(Random.Range(timeToSpreadFire - randomFireSpreadTime, timeToSpreadFire + randomFireSpreadTime));
            foreach (var neighbor in neighborTiles)
            {
                FireTileScript fireTileScript = neighbor.GetComponent<FireTileScript>();
                if (fireTileScript.onFire == false && neighbor.gameObject.activeSelf)
                {
                    // Sets neighbor on fire
                    fireTileScript.SetOnFire();
                    break;
                }
            }
        }
    }
    
    private IEnumerator OnFireTime()
    {
        // countdown to remove tile
        yield return new WaitForSeconds(gameManager.timeToDestroy);
        gameObject.SetActive(false); // disable tile
        Destroy(tileFire);
        gameManager.RemoveFromArray(gameObject);
        gameManager.DecreaseTiles();
        gameManager.DecreaseTilesOnFire();
    }
    
    [ContextMenu("SET ON FIRE")]
    public void SetOnFire()
    {
        onFire = true;
        tileFire = Instantiate(fire, transform.position, Quaternion.identity); // spawn fire
        tileFire.transform.parent = transform;
        tileFire.transform.localPosition = new Vector3(0,0,0.1f);
        StartCoroutine(coroutineOnFireTime); // starts OnFireTime routine
        StartCoroutine(coroutineSpreadFire); // starts SpreadFire routine
        gameManager.tilesOnFire++;
        // start fire sound
    }

    public void ExtinguisheFire()
    {
        tileWater = Instantiate(water, transform.position, Quaternion.identity); // spawn water splash
        tileWater.transform.parent = transform;
        tileWater.transform.localPosition = new Vector3(0,0,0.1f);
        Destroy(tileWater, 2); // destroy splash after "lifespan" time    
        // play water splash sound
        if (onFire)
        {
            onFire = false;
            Destroy(tileFire); // remove fire
            StopCoroutine(coroutineOnFireTime); // stop the tile disable countdown
            StopCoroutine(coroutineSpreadFire);
            coroutineSpreadFire = SpreadFire(); // reset coroutine
            coroutineOnFireTime = OnFireTime(); // reset coroutine
            gameManager.DecreaseTilesOnFire();
            //stop fire sound
        }
    }
}
