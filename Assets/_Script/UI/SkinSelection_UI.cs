using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class SkinSelection_UI : MonoBehaviour
{

    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private int[] priceForSkin;
    private int skin_Id;
    
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI bankText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Animator anim;

    // private void Start()
    // {
    //     PlayerPrefs.SetInt("TotalCoinsCollected", 1000);
    // }



    private void SetupSkinInfo()
    {
        skinPurchased[0] = true;
        skinPurchased[1] = true;
        skinPurchased[2] = true;
        skinPurchased[3] = true;

        for (int i = 1; i < skinPurchased.Length; i++)
        {
            bool skinUnlocked = PlayerPrefs.GetInt("skinPurchase" + i) == 1;

            if(skinUnlocked)
                skinPurchased[i] =  true;
        }

        bankText.text = " "+ PlayerPrefs.GetInt("TotalCoinsCollected").ToString();

        selectButton.SetActive(skinPurchased[skin_Id]);//true
        buyButton.SetActive(!skinPurchased[skin_Id]);//false
        
        if(!skinPurchased[skin_Id])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text ="Price: " + priceForSkin[skin_Id];

        anim.SetInteger("skinid",skin_Id);
    }

    public bool EnoughMoney()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoinsCollected");

        if(totalCoins >= priceForSkin[skin_Id]) 
        {
            totalCoins = totalCoins - priceForSkin[skin_Id];

            PlayerPrefs.SetInt("TotalCoinsCollected", totalCoins);
            
            AudioManager.instance.PlaySFX(2);
            return true;
        }

        AudioManager.instance.PlaySFX(0);

        return false;
    }


    public void NextSkin()
    {
        AudioManager.instance.PlaySFX(0);
        
        skin_Id ++;

        if(skin_Id > 3)
            skin_Id = 0;
        
        SetupSkinInfo();
    }

    public void PreviousSkin()
    {
        AudioManager.instance.PlaySFX(0);

        skin_Id --;

        if(skin_Id <0)
            skin_Id = 3;

        SetupSkinInfo();

    }

    public void Buy()
    {
        if(EnoughMoney())
        {
            PlayerPrefs.SetInt("skinPurchase" + skin_Id, 1);
            skinPurchased[skin_Id] = true;
            SetupSkinInfo();

        }
        else
            Debug.Log("Not Enogh Money");

    }

    public void Select()
    {
        AudioManager.instance.PlaySFX(5);
        PlayerManager.instance.choosenSkinId = skin_Id;


    }

    public void SwitchSelectButton(GameObject newButton)
    {
        selectButton = newButton;
    }
    private void OnEnable()
    {
        SetupSkinInfo();
    }
    
    private void OnDisable()
    {
        selectButton.SetActive(false);
    }


}
