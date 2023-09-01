using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public static int health = 3;


    private int healthMax = 3;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    void Awake()
    {
        health = 3;
    }

    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
           hearts[i].sprite = fullHeart; 
        }
    }

    public void AddHealth()
    {
        health = healthMax;
        healthMax++;
        if(healthMax >3)
        {
            return;
        }
    }


}
