using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    // [SerializeField] private float healthValue;
    [SerializeField] private GameObject pickUpFX;

    private void Start()
    {
        int levelNumber = GameManager.instance.levelNumber;
        int totalAmountOfStars = PlayerPrefs.GetInt("Level" + levelNumber + "TotalStars");


    }

    private void OnTriggerEnter2D(Collider2D collison) 
    {
        if(collison.GetComponent<Player>() != null)
        {   
            AudioManagerInGame.instance.PlaySFXInGame(8);
            PlayerManager.instance.stars++;
            if(StarsManager.star<3)
            {
                StarsManager.star++;
                
            }
            if(pickUpFX != null)
            {
                GameObject newPickUpFX = Instantiate(pickUpFX, transform.position, transform.rotation);
                Destroy(newPickUpFX,.5f);
            }
            Destroy(gameObject);
        }
    }

}
