using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsMenu;

    public GameObject instructionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.FindWithTag("MainMenu");
        creditsMenu = GameObject.FindWithTag("CreditsMenu");
        instructionsMenu = GameObject.FindWithTag("InstructionsMenu");
        
        //MainMenuButton();
    }

    public void PlayNowButton()
    {
        // Play Now Button has been pressed
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game3");
    }

    public void CreditsButton()
    {
        // Show Credits Menu
        gameObject.SetActive(false);
       creditsMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        // Show Main Menu
       mainMenu.SetActive(true);
       creditsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}