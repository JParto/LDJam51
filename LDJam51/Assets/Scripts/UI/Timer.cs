using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _secondsText;
    [SerializeField] private TextMeshProUGUI _msText;
    private int _maxSeconds = 1000;
    private float _currentSeconds;
    private bool _stopUpdate;

    [SerializeField] private SO_VoidEventChannel _timeUpEventChannel;
    [SerializeField] private SO_BoolEventChannel _releaseLeftClickEventChannel;
    [SerializeField] private SO_FloatEventChannel _stopInteractForSecondsEventChannel;
    [SerializeField] private SO_VoidEventChannel _startEncounterEventChannel;

    void Start(){
        _currentSeconds = _maxSeconds;
    }

    void Update(){
        if(_stopUpdate){
            return;
        }
        UpdateSeconds();
        if (_currentSeconds <= 0f){
            NotifyTimeUp();
        }
    }

    private void UpdateSeconds(){
        _currentSeconds -= (Time.deltaTime * 100);

        _secondsText.text = FormatTime((int)_currentSeconds);
    }

    private string FormatTime(int seconds){
        int s = seconds / 100;
        int ms = seconds % 100;
        
        string milSecStr;
        if (ms < 10){
            milSecStr = string.Format("0{0}", ms);
        } else {
            milSecStr = ms.ToString();
        }
        
        string text = string.Format("{0}:{1}", s, milSecStr);
        return text;
    }

    private void NotifyTimeUp(){
        _timeUpEventChannel.RaiseEvent();
        _currentSeconds = 0f;
        
        ResetTimer(true);
        // _stopUpdate = true;
    }

    private void ResetTimer(bool val){
        if (!val)
            return;

        _currentSeconds = _maxSeconds;
        StartCoroutine(PauseForNextCharacter());

    }

    IEnumerator PauseForNextCharacter(){
        _stopUpdate = true;
        yield return new WaitForSeconds(2f);
        _stopUpdate = false;
        _startEncounterEventChannel.RaiseEvent();
    }
    private void OnEnable()
    {
        _releaseLeftClickEventChannel.onEventRaised += ResetTimer;
    }

    private void OnDestroy()
    {
        _releaseLeftClickEventChannel.onEventRaised -= ResetTimer;
    }
}
