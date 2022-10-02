using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModularMotion;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio level channels")]
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change SFXs volume")]
    [SerializeField] private SO_FloatEventChannel _SFXVolumeEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Music volume")]
    [SerializeField] private SO_FloatEventChannel _musicVolumeEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Master volume")]
    [SerializeField] private SO_FloatEventChannel _masterVolumeEventChannel = default;
    [SerializeField] private UIMotion m;

    public void Slider_SetSFXVolume(float val){
        _SFXVolumeEventChannel.RaiseEvent(val);
    }
    public void Slider_SetMusicVolume(float val){
        _musicVolumeEventChannel.RaiseEvent(val);
    }
    public void Slider_SetMasterVolume(float val){
        _masterVolumeEventChannel.RaiseEvent(val);
    }

    public void Button_Play(){
        m.WrapMode = Wrap.Once;
        GameManager.instance.StartGameScene();
    }

    public void Button_Quit(){
        Application.Quit();
    }
}
