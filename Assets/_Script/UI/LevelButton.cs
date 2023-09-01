using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI bestTime;
    [SerializeField] private TextMeshProUGUI collectedCoins;
    [SerializeField] private TextMeshProUGUI totalCoins;
    
    // [SerializeField] private TextMeshProUGUI collectedStars;
    // [SerializeField] private Image[] totalStars;

    public void UpdateTextInfo(int levelNumber)
    {
        levelName.text = "Level " + levelNumber;
        bestTime.text = "Best time: " + PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime",999).ToString("00") + " ";
        collectedCoins.text = PlayerPrefs.GetInt("Level" + levelNumber + "CoinsCollected", PlayerManager.instance.coins).ToString();
        totalCoins.text = "/ " + PlayerPrefs.GetInt("Level" + levelNumber + "TotalCoins");
        // collectedStars.text = PlayerPrefs.GetInt("Level" + levelNumber + "StarsCollected", PlayerManager.instance.stars).ToString();

        // totalStars.text = " " + PlayerPrefs.GetInt("Level" + levelNumber + "TotalStars");



    }




}
