using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector] public int fruits;
    [HideInInspector] public int coins;
    [HideInInspector] public int stars;
    [HideInInspector] public Transform respawnPoint;
    [HideInInspector] public GameObject currentPlayer;
    [HideInInspector] public int choosenSkinId;

    public InGame_UI inGameUI;


    [Header("Player info")]
    

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject deathFx;

    [Header("CameraShakeFX")]
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector3 shakeDirection;
    [SerializeField] private float forceMultiplier;


    public void ScreenShake(int facingDir)
    {
        impulse.m_DefaultVelocity = new Vector3(shakeDirection.x * facingDir, shakeDirection.y) *forceMultiplier;
        impulse.GenerateImpulse();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    private void Update() 
    {
        // if(Input.GetKeyDown(KeyCode.R))
        // {
        //     RespawnPlayer();
        // }    
    }

    private bool HaveEnoughCoins()
    {
        if(coins > 0)
        {
            coins --;
            if(coins < 0)
                coins = 0;
            
            return  true;
        }

        return false;
    }
    private bool HaveEnoughStars()
    {
        if(stars > 0)
        {
            stars --;
            if(stars < 0)
                stars = 0;
            
            return  true;
        }

        return false;
    }


    public void OnTakingDamage()
    {
        if (HaveEnoughCoins())
        {
            KillPlayer();
            
            if(GameManager.instance.difficulty < 3)
                Invoke("RespawnPlayer",1);        

            else
            {   
                inGameUI.OnDeath();
            }
        
        
        }

    }



    public void OnFalling()
    {
        KillPlayer();

        int difficulty = GameManager.instance.difficulty;
        // if(HaveEnoughCoins())
        // {
        //     PearmanentDeath();
        // }

        // if(difficulty < 3)
        // {
        //     Invoke("RespawnPlayer",1);

        //     if(difficulty >1)
        //         HaveEnoughCoins();
        // }
        // else
        // {
            inGameUI.OnDeath();
        // }

    }

    public void RespawnPlayer()
    {
        if(currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
            inGameUI.AssignPlayerControlls(currentPlayer.GetComponent<Player>());
            //AudioManager.instance.PlaySFX(11);
            AudioManagerInGame.instance.PlaySFXInGame(10);
        }
    }

    public void KillPlayer()
    {
        AudioManagerInGame.instance.PlaySFXInGame(0);

        GameObject newDeathFx = Instantiate(deathFx, currentPlayer.transform.position, currentPlayer.transform.rotation);
        Destroy(newDeathFx, .4f);
        Destroy(currentPlayer);

    }
  

}
