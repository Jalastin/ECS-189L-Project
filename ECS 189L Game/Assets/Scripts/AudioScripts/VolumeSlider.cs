using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Resource: https://johnleonardfrench.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;
    [SerializeField] Slider AudioSlider;
    // These two variables are used to save the user's previous volume settings so that the visual aspect of the slider is consistent with the actual adjusted volume. 
    
    void Start()
    {
        // Update slider UI to the latest value
        if(GameManager.Instance.VolumeChanged)
        {
            AudioSlider.value = GameManager.Instance.CurrentVolume;
        }
    }
    public void SetVolume(float volumeValue)
    {
        Mixer.SetFloat("Volume", Mathf.Log10(volumeValue) * 20);
        GameManager.Instance.VolumeChanged = true;
        GameManager.Instance.CurrentVolume = volumeValue;
    }
}
