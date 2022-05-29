using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleAudioIcon : MonoBehaviour
{
    [SerializeField] Sprite UnmutedAudioSprite;
    [SerializeField] Sprite MutedAudioSprite;
    [SerializeField] Button AudioButton;
    private bool isMuted = false;
    
    // Background music should always be playing when the game starts by default.
    void Start()
    {
        AudioListener.pause = false;
    }

    public void ChangeAudioImage()
    {
        if (!isMuted)
        {
            // Mute audio.
            AudioButton.image.sprite = MutedAudioSprite;
            this.isMuted = true;
            AudioListener.pause = true;
        } 
        else 
        {
            // Unmute audio.
            AudioButton.image.sprite = UnmutedAudioSprite;
            this.isMuted = false;
            AudioListener.pause = false;
        }
    }
}
