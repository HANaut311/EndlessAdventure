using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    apple,
    banana,
    cherries,
    kiwi,
    melon,
    orange,
    pineapple,
    strawberry
}
public class Fruit_Item : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public  FruitType  myFruitType;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] fruitImage;

    private void OnTriggerEnter2D(Collider2D collison) 
    {
        if(collison.GetComponent<Player>() != null)
        {
            PlayerManager.instance.fruits++;
            Destroy(gameObject);
        }
    }

    public void FruitSetup(int fruitIndex)
    {

        for(int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(fruitIndex, 1);
    }
    // private void OnValidate() 
    // {
    //     sr.sprite = fruitImage[(int)myFruitType];
    // }

    
}
