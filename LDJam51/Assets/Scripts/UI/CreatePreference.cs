using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePreference : MonoBehaviour
{
    PlayerManager _playerManager;
    Preferences preference;
    [SerializeField] private GameObject _notAllSelectedWarning;
    [SerializeField] private Button _playButton;
    // [SerializeField] private GameObject _notAllSelectedWarning;
    bool be1;
    bool be2;
    bool be3;
    bool be4;
    bool be5;
    bool be6;
    bool be7;

    bool bb1;
    bool bb2;
    bool bb3;
    bool bb4;
    bool bb5;

    void Awake(){
        preference = new Preferences();
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    void Update(){
        if (be1 && be2 && be3 && be4 && be5 && be6 && be7){//} && bb1 && bb2 && bb3 && bb4 && bb5){
            _notAllSelectedWarning.SetActive(false);
            _playButton.interactable = true;
        }
    }

    public void SetEnums(int enumIndex, int index){
        switch (enumIndex)
        {
            case 0:
                preference.prefAnimal = (Preferences.Animal)index;
                be1 = true;
                break;
            case 1:
                preference.prefColor = (Preferences.Color)index;
                be2 = true;
                break;
            case 2:
                preference.prefLifeStyle = (Preferences.LifeStyle)index;
                be3 = true;
                break;
            case 3:
                preference.prefMusic = (Preferences.Music)index;
                be4 = true;
                break;
            case 4:
                preference.prefSuperPower = (Preferences.SuperPower)index;
                be5 = true;
                break;
            case 5:
                preference.prefSound = (Preferences.Sound)index;
                be6 = true;
                break;
            case 6:
                preference.prefAge = (Preferences.Age)index;
                be7 = true;
                break;
        }
    }

    public void Toggle_SetSmoking(bool val){
        preference.smoking = val;
        bb1 = true;
    }
    
    public void Toggle_SetDrinking(bool val){
        preference.drinking = val;
        bb2 = true;
    }

    public void Toggle_SetKids(bool val){
        preference.kids = val;
        bb3 = true;
    }

    public void Toggle_SetMarriage(bool val){
        preference.marriage = val;
        bb4 = true;
    }

    public void Toggle_SetHumour(bool val){
        preference.humour = val;
        bb5 = true;
    }

    public void Button_Return(){
        _playerManager.Set_PlayerPreferences(preference);
    }

}
