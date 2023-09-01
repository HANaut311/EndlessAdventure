using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform resPoint;

    private void Awake() 
    {
    }

    private void Start()
    {
        PlayerManager.instance.respawnPoint = resPoint;
        PlayerManager.instance.RespawnPlayer();

        PlayerManager.instance.coins = 0;
        GameManager.instance.timer = 0;
        AudioManagerInGame.instance.PlayBGMInGame(4);
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)    
        {

            if(!GameManager.instance.startTime)
                GameManager.instance.startTime = true;
            
            if(collision.transform.position.x > transform.position.x)
                GetComponent<Animator>().SetTrigger("touch");

            if(collision.transform.position.x < transform.position.x)
                GetComponent<Animator>().SetTrigger("touch");
        }
    }
}
