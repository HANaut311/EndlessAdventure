using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGame_UI : MonoBehaviour
{
    private bool gamePause;
    // private bool gameDead;

    [Header("Menu Game obj")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject defeatUI;    
    [SerializeField] private GameObject endLevelUI;
    [SerializeField] Animator transition;
    public float  transitionTime = 1f;
    
    [Header("Controlls")]
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Button jumpButton;


    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI  timerText;
    [SerializeField] private TextMeshProUGUI currentCoinAmount;

    [SerializeField] private TextMeshProUGUI endTimerText;
    [SerializeField] private TextMeshProUGUI endBestTimeText;
    [SerializeField] private TextMeshProUGUI endCoinsText;
    
    [Header("Volume")]
    [SerializeField] private VolumeControllerInGame_UI[] volumeControllerInGame; 


    private void Awake()
    {
        PlayerManager.instance.inGameUI = this;

    }
    private void Start()
    {
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale  = 1;
        SwitchUI(inGameUI);

        for (int i = 0; i < volumeControllerInGame.Length; i++)
        {
            volumeControllerInGame[i].GetComponent<VolumeControllerInGame_UI>().SetupVolumeInGameSlider();
        }
    }

    void Update()
    {
        
        UpdateInGameInfo();


        if(Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManagerInGame.instance.PlaySFXInGame(10);
            CheckIfNotPause();
        }


    }

    public void AssignPlayerControlls(Player player)
    {
        player.joystick = joystick;

        // jumpButton.onClick.RemoveAllListeners();
        // jumpButton.onClick.AddListener(player.JumpButton);   
    }

    public void PauseButton() => CheckIfNotPause();


    private bool CheckIfNotPause()
    {
        if(!gamePause)
        {
            gamePause = true;
            Time.timeScale = 0;
            SwitchUI(pauseUI);
            return true;
        }
        else
        {
            gamePause = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }

    }

    // private bool CheckIfNotDead()
    // {
    //     if(!gameDead)
    //     {
    //         gameDead = true;
    //         Time.timeScale = 0;
    //         SwitchUI(defeatUI);
    //         return true;
    //     }
    //     else
    //     {
    //         gameDead = false;
    //         Time.timeScale = 1;
    //         SwitchUI(inGameUI);
    //         return false;
    //     }

    // }

    public void OnDeath()
    {
        SwitchUI(defeatUI);
    }

    public void OnLevelFinished()
    {
        endCoinsText.text = "Coins: " + PlayerManager.instance.coins;
        endTimerText.text = "Your time: " + GameManager.instance.timer.ToString("00") +  " s";
        endBestTimeText.text = "Best time: " + PlayerPrefs.GetFloat("Level" + GameManager.instance.levelNumber + "BestTime",999).ToString("00") + " s";


        SwitchUI(endLevelUI);
    }
    private void UpdateInGameInfo()
    {
        timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " s";
        currentCoinAmount.text = PlayerManager.instance.coins.ToString();
    }

    public void SwitchUI(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);

        if(uiMenu == inGameUI)
        {
            // joystick.gameObject.SetActive(true);
            // jumpButton.gameObject.SetActive(true);
        }

    }

    public void LoadMainMenu()
    {
        AudioManagerInGame.instance.PlaySFXInGame(6);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadCurrentLevel()
    {

        AudioManagerInGame.instance.PlaySFXInGame(6);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void LoadNextLevel()
    {


        AudioManagerInGame.instance.PlaySFXInGame(6);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }




}
