using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] private SO_EvaluateCompatibilityEventChannel _evaluateEventChannel;
    [SerializeField] private SO_BoolEventChannel _matchEventChannel;
    [SerializeField] private SO_BoolEventChannel _singleMatchEventChannel;
    private Preferences _playerPreferences;
    public Preferences playerPreferences {get {return _playerPreferences;}}

    public void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(gameObject);
        }
        Debug.LogWarning("Need to remove randomization of player preferences");
        // _playerPreferences = Preferences.RandomizePreference();
        _playerPreferences = new Preferences();
    }

    public void Set_PlayerPreferences(Preferences prefs){
        _playerPreferences = prefs;
    }

    private void Evaluate(Preferences charPrefs){
        // bool match = Preferences.SamePrefs(_playerPreferences, charPrefs);
        int compat = Preferences.Compatibility(_playerPreferences, charPrefs);

        bool match = compat <= 3;
        Debug.LogFormat("{0}:{1}", compat, match);

        _matchEventChannel.RaiseEvent(match);
    }

    public bool EvaluateSingle(int index, Preferences charPrefs){
        bool b = Preferences.EvaluatePrefIndex(index, _playerPreferences, charPrefs);

        _singleMatchEventChannel.RaiseEvent(b);

        return b;
    }

    private void OnEnable()
    {
        _evaluateEventChannel.onEventRaised += Evaluate;
    }

    private void OnDestroy()
    {
        _evaluateEventChannel.onEventRaised -= Evaluate;
    }
}
