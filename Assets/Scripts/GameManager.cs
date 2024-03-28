using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    int nextLevel;
    int currentLevel;
    PlayerManager playerManager;
    PlayerMovement playerMovement;
    GoalObjectScript goalCollected;
    public bool shootingAllowed;

    public TextMeshProUGUI timerText;
    float levelTimer;
    float startTime;
    float currentTime;
    bool gamePaused;

    bool gameActive;

    public GameObject levelCompleteMenu;
    public GameObject levelRestartMenu;
    public GameObject escapeMenu;

    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        goalCollected = GameObject.FindGameObjectWithTag("Goal").GetComponent<GoalObjectScript>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        shootingAllowed = true;

        gameActive = true;
        startTime = Time.time;

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.isPlayerDead() == true)
        {
            levelRestartMenu.SetActive(true);
            escapeMenu.SetActive(false);
            MenuSettings();
            
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gamePaused)
            {
                escapeMenu.SetActive(false);
                GameSettings();
                gamePaused = false;
            }
            else
            {
                escapeMenu.SetActive(true);
                PauseSettings();
                gamePaused = true;
            }
            
        }

        if (goalCollected.Collected())
        {
            levelCompleteMenu.SetActive(true);
            escapeMenu.SetActive(false);
            MenuSettings();
        }

        if (gameActive == true)
        {
            levelTimer += Time.deltaTime;
            DisplayTime(levelTimer);
             
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        float Minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float Seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float Milliseconds = Mathf.FloorToInt((timeToDisplay % 1) * 100); // Get milliseconds without rounding

        timerText.text = string.Format("{0:0}:{1:00}:{2:00}", Minutes, Seconds, Milliseconds);
    }


    public void PlayGame()
    {
        
    }

    public void PlayNextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
120ibmw m1

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {

    }


    private void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }

    void MenuSettings()
    {
        playerMovement.movementAllowed = false;
        shootingAllowed = false;

        Time.timeScale = .3f;
        gameActive = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void PauseSettings()
    {
        playerMovement.movementAllowed = false;
        shootingAllowed = false;

        Time.timeScale = .3f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void GameSettings()
    {
        playerMovement.movementAllowed = true;
        shootingAllowed = true;

        Time.timeScale = 1f;
        gameActive = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
