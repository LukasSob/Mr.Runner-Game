using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    int nextLevel;
    int currentLevel;

    [Header("Audio")]
    public AudioSource music;
    public float fadeDuration = 1.0f; // Duration for fade in
    public float musicVolume;

    // Additional private variables
    private float targetVolume;
    private bool fading;

    public GameObject uiFadeOut;
    public float uiFadeDuration;

    public Slider mouseSensSlider;

    PlayerManager playerManager;
    PlayerMovement playerMovement;
    GoalObjectScript goalCollected;
    public SceneInfo sceneInfo;

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

    [SerializeField] public GameObject level1Button;
    [SerializeField] public GameObject level2Button;
    [SerializeField] public GameObject level3Button;

    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        if (currentSceneIndex !=  0)
        {
            playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            goalCollected = GameObject.FindGameObjectWithTag("Goal").GetComponent<GoalObjectScript>();


            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            shootingAllowed = true;

            gameActive = true;
            startTime = Time.time;


        }

        if (currentSceneIndex == 0)
        {
            MainMenuSettings();
            uiFadeOut.SetActive(false);
            FadeInAudio();

            mouseSensSlider.value = sceneInfo.mouseSens; 

            Debug.Log("Menu");

            if (sceneInfo.GetLevelOneAccess() == true)
            {
                level1Button.SetActive(false);
            }
            if (sceneInfo.GetLevelTwoAccess() == true)
            {
                level2Button.SetActive(false);
            }
            if (sceneInfo.GetLevelThreeAcess() == true)
            {
                level3Button.SetActive(false);
            }
        }

        if (currentSceneIndex == 2)
        {
            sceneInfo.SetLevelOneAccessed(true);
        }
        else if(currentSceneIndex == 3)
        {
            sceneInfo.SetLevelTwoAccessed(true);
        }
        else if (currentSceneIndex == 4)
        {
            sceneInfo.SetLevelThreeAccessed(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (currentSceneIndex != 0)
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

    }

    private void DisplayTime(float timeToDisplay)
    {
        float Minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float Seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float Milliseconds = Mathf.FloorToInt((timeToDisplay % 1) * 100); // Get milliseconds without rounding

        timerText.text = string.Format("{0:0}:{1:00}:{2:00}", Minutes, Seconds, Milliseconds);
    }

    public void SetMouseSens()
    {
        sceneInfo.mouseSens = mouseSensSlider.value;
    }

    void FadeInAudio()
    {
        if (music != null)
        {
            targetVolume = musicVolume;
            music.volume = 0f;
            music.Play();
            fading = true;
        }
    }

    void FixedUpdate()
    {
        if (fading)
        {
            // Adjust volume gradually
            music.volume = Mathf.MoveTowards(music.volume, targetVolume, Time.fixedDeltaTime / fadeDuration);

            // Check if fading is complete
            if (Mathf.Approximately(music.volume, targetVolume))
            {
                fading = false;
            }
        }
    }


    public void PlayGame()
    {
        
    }

    public void PlayNextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void LoadLevel(int levelNum)
    {
        if (currentSceneIndex == 0)
        {
            uiFadeOut.SetActive(true);
            StartCoroutine(LoadLevelDelay(levelNum));           
        }
        else
        {
            SceneManager.LoadScene(levelNum);
        }
    }

    IEnumerator LoadLevelDelay(int levelNum)
    {
        yield return new WaitForSeconds(uiFadeDuration);

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

    void MainMenuSettings()
    {
        Time.timeScale = 1f;
        gameActive = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
