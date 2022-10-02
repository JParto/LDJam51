using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private SO_BoolEventChannel _matchEventChannel;
    [SerializeField] private SO_BoolEventChannel _singleMatchEventChannel;
    [SerializeField] private SO_BoolEventChannel _swipeRightEventChannel;
    [SerializeField] private SO_VoidEventChannel _timeUpEventChannel;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _endScoreText;

    [SerializeField] private Image[] _strikes;
    private int _currentStrikes;

    private int _pointAmount;
    private bool _swipeRight;
    bool ended = false;

    void Start(){
        _currentStrikes = 0;
    }

    void Update(){
        if (ended){
            return;
        }
        _scoreText.text = _pointAmount.ToString();
        _endScoreText.text = _pointAmount.ToString();
        if (_currentStrikes >= 3){
            ended = true;
            EndManager.instance.ShowEndScreen();
        }
    }

    private void AddPoints(int amount){
        _pointAmount += amount;
        _pointAmount = Mathf.Max(0, _pointAmount);
    }

    private void Swipe(bool right){
        _swipeRight = right;
    }

    private void Match(bool match){
        bool b = match == _swipeRight;
        int pointsToAdd = b ? 30 : -10;

        AddPoints(pointsToAdd);

        if (!b){
            AddStrike();
        }
    }

    public void AddStrike(){
        _strikes[_currentStrikes].gameObject.SetActive(true);
        _currentStrikes += 1;
    }

    private void SingleMatch(bool match){
        int pointsToAdd = match ? 10 : -5;

        AddPoints(pointsToAdd);
    }
    

    private void OnEnable()
    {
        _matchEventChannel.onEventRaised += Match;
        _singleMatchEventChannel.onEventRaised += SingleMatch;
        _swipeRightEventChannel.onEventRaised += Swipe;
        _timeUpEventChannel.onEventRaised += AddStrike;
    }

    private void OnDestroy()
    {
        _matchEventChannel.onEventRaised -= Match;
        _singleMatchEventChannel.onEventRaised -= SingleMatch;
        _swipeRightEventChannel.onEventRaised -= Swipe;
        _timeUpEventChannel.onEventRaised -= AddStrike;

    }
}
