using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public Heart healthObject;
    public TextMeshProUGUI scoreText;
    public GameObject scoreBoardUICanvas; //innehållar canvasen
    public int score =0  ;
    
    void Start()
    {
    
     
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int value)
    {

        score += value;
        scoreText.text = "Poäng: " + score.ToString();
        
    }
    
    
}
