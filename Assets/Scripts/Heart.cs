using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Heart : MonoBehaviour
{
    public int health = 5;

    private int numOfHearts = 5;

    public Image[] hearts;

    public Sprite fullHeart;

    public Sprite emptyHeart;

    public TextMeshProUGUI totalScore;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI gameOverText;
    public Image replayButton;

    public Image CrosshairButton;

    public Image shootButton;

    public bool gameOver= false;
    private ParticleSystem explosion;
    public AudioClip gameOverSound;
    private AudioSource _audioSource;
    private Scoring score;
   
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
       // Scoring.score = 0;
       score = GameObject.FindWithTag("Player").GetComponent<Scoring>();
        currentScore.gameObject.SetActive(true);
        totalScore.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(false);
        explosion = GameObject.FindWithTag("Explosion").GetComponent<ParticleSystem>();
        _audioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

   
    void Update()
    {

        for (int i = 0; i < hearts.Length; i++) //Updates heart interface
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
       
        
    }

    
    public void GameOver()  //Setup menu / buttons for gameover and freezes the game 
    {
       
        
        gameOver = true;
        totalScore.text ="Totala Poäng:" + score.score;
        totalScore.gameObject.SetActive(true);
        currentScore.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true); // Game Over text active
        CrosshairButton.gameObject.SetActive(false);
        shootButton.gameObject.SetActive(false);
       // StartCoroutine(playGameOverSound());
        _audioSource.PlayOneShot(gameOverSound, 0.5f);


    }

    IEnumerator playGameOverSound()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        _audioSource.PlayOneShot(gameOverSound, 0.5f);
    }

    

    public void GoHome()
    {
        SceneManager.LoadScene("MenuCanvas", LoadSceneMode.Single);
    }

    public void loseHeart()
    {
        health -= 1;
        
        if (health <= 0 && !gameOver)
        {
            GameOver();
        }
    }

    public void gainHeart()
    {
        if (health < 5)
        {
            health += 1;
        }
        
    }

   
  
}