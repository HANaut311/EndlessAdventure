using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StarsManager : MonoBehaviour
{
    public static StarsManager instance;

    public static int star = 0;

    [SerializeField] private Image[] Stars;
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;
    void Awake()
    {
        star = 0;
    }

    void Update()
    {
        foreach (Image img in Stars)
        {
            img.sprite = emptyStar;
        }
        for (int i = 0; i < star; i++)
        {
           Stars[i].sprite = fullStar; 
        }
    }   

}
