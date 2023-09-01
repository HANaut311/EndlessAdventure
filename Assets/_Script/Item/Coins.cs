using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
public enum CoinsType
{
    coin,
    diamond
}

public class Coins : MonoBehaviour
{
    // [SerializeField] private Transform[] coinPosition;
    // [SerializeField] private GameObject coinPrefab;
    // [SerializeField] private bool randomCoin;
    [SerializeField] private Animator anim;
    public CoinsType  myCoinsType;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] coinImage;

    [SerializeField] private GameObject pickUpFX;
    
    private void OnTriggerEnter2D(Collider2D collison) 
    {
        if(collison.GetComponent<Player>() != null)
        {
            PlayerManager.instance.coins++;
            AudioManagerInGame.instance.PlaySFXInGame(9);
            if(pickUpFX != null)
            {
                GameObject newPickUpFX = Instantiate(pickUpFX, transform.position, transform.rotation);
                Destroy(newPickUpFX,.5f);
            }
            Destroy(gameObject);
        }
    }
    public void CoinSetup(int CoinIndex)
    {

        for(int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(CoinIndex, 1);
    }

}
