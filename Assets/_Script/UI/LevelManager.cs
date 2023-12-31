using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] private Transform levelButtonParent;
    [SerializeField] private bool[] levelOpen;
    private void Start()
    {
        PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1 );

        AssignLevelBooleans();

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if(!levelOpen[i])
                return;
            
            string sceneName = "Level " + i;
            //StartCoroutine(LoadLevel(sceneName));
            GameObject newButton = Instantiate(levelButton, levelButtonParent);
            newButton.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(LoadLevel(sceneName)));
            newButton.GetComponent<LevelButton>().UpdateTextInfo(i);
        }
    }

    private void AssignLevelBooleans()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if(unlocked)
                levelOpen[i] = true;
            else
                return;

            
        }    
    }
    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        AudioManager.instance.PlaySFX(4);
        GameManager.instance.SaveGameDifficulty();
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("end");
    
    
    }


    public void LoadNewGame()
    {
        AudioManager.instance.PlaySFX(4);
        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
            
            if(unlocked)
                PlayerPrefs.GetInt("Level" + i + "Unlocked", 0 );
            else
            {
                SceneManager.LoadScene("Level 1");
                return;
            }
        }
    }

    public void LoadContinueGame()
    {
        AudioManager.instance.PlaySFX(4);

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if(!unlocked)
            {
                SceneManager.LoadScene("Level " + (i-1));
                return;
            }
        }
    }

}
