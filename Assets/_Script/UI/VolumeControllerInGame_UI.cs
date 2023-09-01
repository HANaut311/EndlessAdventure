using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControllerInGame_UI : MonoBehaviour
{
    [SerializeField] private string mixerParametrInGame;
    [SerializeField] private AudioMixer audioMixerInGame;
    [SerializeField] private Slider sliderInGame;
    [SerializeField] private float sliderMultiplierInGame;


    public void SetupVolumeInGameSlider()
    {
        sliderInGame.onValueChanged.AddListener(SlideValueInGame);
        sliderInGame.minValue = .001f;
        sliderInGame.value = PlayerPrefs.GetFloat(mixerParametrInGame, sliderInGame.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(mixerParametrInGame, sliderInGame.value);
    }


    private void SlideValueInGame(float valueInGame)
    {
        audioMixerInGame.SetFloat(mixerParametrInGame,Mathf.Log10( valueInGame) * sliderMultiplierInGame);
    }


}
