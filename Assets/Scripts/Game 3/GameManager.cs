using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject[] allTiles;

    //variables
    public int waveNum = 0;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI FireText;
    public TextMeshProUGUI WaterText;
    public TextMeshProUGUI gameOverText;
    public Image goHomeButton;
    
    [SerializeField] private int tilesLeft;
    [SerializeField] private int tilesLeftGameoverNumber = 4;
    [SerializeField] private float numberOfFiresToSpawn = 1;
    [SerializeField] private float difficultyScaling = 1.1f;
    public float timeToDestroy = 30f; // time for the tile to burn up (disable)
    public float timeToSpreadFire = 20f; // time  for the fire to spread
    public bool gameover = false;
    public int tilesOnFire = 0;

    private GetWater getWater;
    
    [SerializeField] private AudioClip removeTileAudioClip;
    private AudioSource _audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        getWater = GameObject.Find("AR Camera").GetComponent<GetWater>();
        allTiles = GameObject.FindGameObjectsWithTag("LandTile");
        tilesLeft = allTiles.Length;
        ShuffleArray(allTiles);
        _audioSource = GameObject.Find("AR Session Origin").GetComponent<AudioSource>();
    }
    
    private static void ShuffleArray<T>(T[] arr) {
        for (int i = arr.Length - 1; i > 0; i--) {
            int r = Random.Range(0, i);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text ="Level:"  + waveNum.ToString();
        if ((tilesLeft - tilesOnFire - tilesLeftGameoverNumber) <= 0 )
        {
            FireText.text ="Mark som brinner: " + "Panik!";
        }
        else
        {
            FireText.text ="Mark som brinner: "  +  tilesOnFire.ToString() + "/" + tilesLeft.ToString();
        }
        WaterText.text = "Vatten:" + getWater.waterAmount + "/" + getWater.waterMaxAmount;
    }

    public void DifficultySetting(int difficulty)
    {
        switch (difficulty)
        {
            // Easy
            case 1:
                difficultyScaling = 1.05f;
                timeToDestroy = 30;
                timeToSpreadFire = 20;
                numberOfFiresToSpawn = 1;
                getWater.waterMaxAmount = 2;
                break;
            
            // Medel
            case 2:
                difficultyScaling = 1.1f;
                timeToDestroy = 30;
                timeToSpreadFire = 14;
                numberOfFiresToSpawn = 1;
                getWater.waterMaxAmount = 2;
                break;
            
            // Hard
            case 3:
                difficultyScaling = 1.1f;
                timeToDestroy = 30;
                timeToSpreadFire = 14;
                numberOfFiresToSpawn = 1;
                getWater.waterMaxAmount = 1;
                break;
        }

        NextWave(); // start game
    }
    
    [ContextMenu("next wave")]
    private void NextWave()
    {
        if (!gameover)
        {
            //Debug.Log(waveNum);
            // !!set new timeToDestroy, timeToSpreadFire and numberOfFiresToSpawn; to change difficulty!!
            timeToDestroy /= difficultyScaling;
            //Debug.Log($"timeToDestroy: {timeToDestroy}");
            timeToSpreadFire /= difficultyScaling;
            //Debug.Log($"timeToSpreadFire: {timeToSpreadFire}");
            numberOfFiresToSpawn *= difficultyScaling;
            //Debug.Log($"numberOfFiresToSpawn: {numberOfFiresToSpawn}");
    
            for (int i = 0; i < Mathf.RoundToInt(numberOfFiresToSpawn); i++)
            {
                var index = Random.Range(0, allTiles.Length - 1);
                var failsafe = 0; // used to avoid getting stuck in loop
                while (allTiles[index].gameObject.GetComponent<FireTileScript>().onFire && failsafe <= allTiles.Length)
                {
                    // if tile is on fire select the next one
                    //Debug.LogError($"Gameamnager: {allTiles[temp].name}");
                    index = ((index + 1) % allTiles.Length);
                    failsafe++;
                }
                //Debug.Log($"Gameamnager: {allTiles[temp].name}");
                allTiles[index].gameObject.GetComponent<FireTileScript>().SetOnFire();
            }
            waveNum++;
        }
    }

    public void DecreaseTiles()
    {
        tilesLeft = allTiles.Length;
        _audioSource.PlayOneShot(removeTileAudioClip, 1.0f);
        if (tilesLeft <= tilesLeftGameoverNumber && !gameover)
        {
            gameover = true;
            Debug.Log("Gameover");
            gameOverText.gameObject.SetActive(true);
            goHomeButton.gameObject.SetActive(true);
        }
    }

    public void DecreaseTilesOnFire()
    {
        tilesOnFire--;
        if (tilesOnFire <= 0 && !gameover)
        {
            //Debug.Log("next wave");
            // add countdown to next wave?
            StartCoroutine(NextWaveDelay(2));
        }
    }
    
    private IEnumerator NextWaveDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        NextWave();
    }

    public void RemoveFromArray(GameObject arrayGameObject)
    {
        allTiles = RemoveObjectArray(allTiles, arrayGameObject);
    }
    
    public static T[] RemoveObjectArray<T> ( T[] arr, T ObjToRemove) {  
        int numIdx = System.Array.IndexOf(arr, ObjToRemove);
        if (numIdx == -1) return arr;
        List<T> tmp = new List<T>(arr);
        tmp.RemoveAt(numIdx);
        return tmp.ToArray();
    }
    
    public void GoHome()
    {
        SceneManager.LoadScene("MenuCanvas", LoadSceneMode.Single);
    }
}
