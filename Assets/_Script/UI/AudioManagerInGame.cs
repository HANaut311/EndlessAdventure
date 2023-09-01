using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerInGame : MonoBehaviour
{
    public static AudioManagerInGame instance;

    
    [SerializeField] private AudioSource[] sfxInGame;
    [SerializeField] private AudioSource[] bgmInGame;

    private int bgmInGameIndex;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);

        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }


    private void Update()
    {
        if(!bgmInGame[bgmInGameIndex].isPlaying)
            bgmInGame[bgmInGameIndex].Play();
    }
    public void PlaySFXInGame(int sfxToPlay)
    {
        if( sfxToPlay < sfxInGame.Length)
        {
            //sfxInGame[sfxToPlay].pitch = Random.Range(0.85f,1.15f);
            sfxInGame[sfxToPlay].Play();

        }

    }

    public void StopSFXInGame(int sfxToStop)
    {
        sfxInGame[sfxToStop].Stop();
    }


    public void PlayBGMInGame(int bgmToPlay)
    {
        
        for (int i = 0; i < bgmInGame.Length; i++)
        {
            bgmInGame[i].Stop();
        }

        bgmInGame[bgmToPlay].Play();
    }

    public void PlayRandomBGMInGame()
    {
        bgmInGameIndex = Random.Range(0, bgmInGame.Length);
   

        PlayBGMInGame(bgmInGameIndex);
    }


}
