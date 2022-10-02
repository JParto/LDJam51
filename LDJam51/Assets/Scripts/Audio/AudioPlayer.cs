using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioCuePlayer _audioCuePlayer;
    [SerializeField] private SO_BoolEventChannel _releaseLeftClickEventChannel;
    [SerializeField] private SO_BoolEventChannel _matchEventChannel;
    [SerializeField] private SO_BoolEventChannel _singleMatchEventChannel;
    [SerializeField] private SO_VoidEventChannel _timeUpEventChannel;

    private bool _swipeDirection;

    private void PlayMatchSound(bool match){
        string s = match == _swipeDirection ? "Love" : "Crash";
        _audioCuePlayer.PlaySound(s);
    }

    private void PlaySingleMatchSound(bool match){
        string s = match ? "Correct" : "Incorrect";
        _audioCuePlayer.PlaySound(s);
    }

    private void PlayTooLateSound(){
        _audioCuePlayer.PlaySound("Crash");
    }

    private void SwipeSound(bool match){
        _swipeDirection = match;
        if (match)
            _audioCuePlayer.PlaySound("Swipe");
    }

    private void OnEnable()
    {
        _matchEventChannel.onEventRaised += PlayMatchSound;
        _singleMatchEventChannel.onEventRaised += PlaySingleMatchSound;
        _releaseLeftClickEventChannel.onEventRaised += SwipeSound;
        _timeUpEventChannel.onEventRaised += PlayTooLateSound;

    }

    private void OnDestroy()
    {
        _matchEventChannel.onEventRaised -= PlayMatchSound;
        _singleMatchEventChannel.onEventRaised -= PlaySingleMatchSound;
        _releaseLeftClickEventChannel.onEventRaised -= SwipeSound;
        _timeUpEventChannel.onEventRaised -= PlayTooLateSound;
    }
}
