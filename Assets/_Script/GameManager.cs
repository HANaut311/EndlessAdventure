using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int difficulty;

    [Header("Timer")]
    public float timer;
    public bool startTime;

    [Header("Level Info")]
    public int levelNumber;
    public Image[] star;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    private void Start()
    {


        if(difficulty == 0)
            difficulty = PlayerPrefs.GetInt("GameDifficulty");

        PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime");

    }

    private void Update()
    {
        if(startTime)
            timer += Time.deltaTime;
    }




    public void SaveGameDifficulty()
    {
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
    }

    public void SaveBestTime()
    {
        startTime = false;
        
        float lastTime = PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime",999);

        if(timer < lastTime)
            PlayerPrefs.SetFloat("Level" + levelNumber + "BestTime", timer);

        timer = 0;
    }

    public void SaveCollectedCoins()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoinsCollected");

        int newTotalCoins  = totalCoins +  PlayerManager.instance.coins;

        PlayerPrefs.SetInt("TotalCoinsCollected", newTotalCoins);
        PlayerPrefs.SetInt("Level" + levelNumber + "CoinsCollected", PlayerManager.instance.coins);

        PlayerManager.instance.coins = 0;
    }

    public  void SaveLevelInfo()
    {
        int nextLevelNumber = levelNumber +1;
        PlayerPrefs.SetInt("Level" + nextLevelNumber + "Unlocked",1);
    }


    public void SaveStar()
    {
        int totalStars = PlayerPrefs.GetInt("TotalStarsCollected");
        int newTotalStars = totalStars + PlayerManager.instance.stars;

        PlayerPrefs.SetInt("TotalStarsCollected", newTotalStars);
        // PlayerPrefs.SetInt("Level" + levelNumber + "StartsCollected", PlayerManager.instance.stars);

        PlayerManager.instance.stars = 0;
    }
}
