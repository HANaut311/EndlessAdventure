using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private GameObject pickUpFX;
    private void OnTriggerEnter2D(Collider2D collison) 
    {
        if(collison.GetComponent<Player>() != null)
        {   
            AudioManagerInGame.instance.PlaySFXInGame(8);
            if(HealthManager.health<3)
            {
                HealthManager.health++;
            }
            if(HealthManager.health >= 3)
            {
                HealthManager.health = 3;
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
