using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCard : MonoBehaviour
{
    private int maxPrefAmount => Preferences.preferenceAmount;
    private readonly int easyAmount = 4;
    private readonly int mediumAmount = 7;
    private readonly int hardAmount = 10;

    private Animator _animator;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Sprite[] _spritePool;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Preferences _preference;
    
    [Header("For animation")]
    [SerializeField] private Transform _rightSideSwipePosition;
    [SerializeField] private Transform _leftSideSwipePosition;
    private bool _goRight;

    [Header("Invokes")]
    [SerializeField] private SO_EvaluateCompatibilityEventChannel _evaluateEventChannel;

    [Header("Listens to:")]
    [SerializeField] private SO_FloatEventChannel _aimMagnitudeEventChannel;
    [SerializeField] private SO_BoolEventChannel _swipeRightEventChannel;
    [SerializeField] private SO_BoolEventChannel _releaseLeftClickEventChannel;
    [SerializeField] private SO_VoidEventChannel _timeUpEventChannel;
    private List<int> _indices;

    void Awake(){
        _animator = GetComponent<Animator>();
    }

    void Start(){
        CleanSlate();
    }

    void CleanSlate(){
        SetRandomPreference();
        SetRandomSprite();
        ResetPosition();
    }

    public void SetRandomPreference(){
        // _preference = Preferences.RandomizePreference();
        _preference = new Preferences();
        Preferences.CopyPreferences(_playerManager.playerPreferences, _preference);
        // Preferences pref = _playerManager.playerPreferences;
        _indices = GetRandomIndices(easyAmount);

        // randomize only the indices
        foreach (int index in _indices)
        {
            _preference = Preferences.RandomizePreferencesAtIndex(index, _preference);
        }
    }

    public List<int> GetRandomIndices(int amount){
        List<int> indices = new List<int>();
        
        for (int i = 0; i < amount; i++)
        {
            int r = Random.Range(0, maxPrefAmount);

            while (indices.Contains(r)){
                r = Random.Range(0, maxPrefAmount);
            }

            indices.Add(r);
        }

        return indices;
    }

    public void SetRandomSprite(){
        // set a random sprite from a pool
        int i = Random.Range(0, _spritePool.Length);
        _sprite.sprite = _spritePool[i];

        // set a random color
        Color c = Random.ColorHSV(0f, 1f, 0f, 0.6f, 0.5f, 1f);
        _sprite.color = c;
    }

    private void WiggleWithAim(float val){
        if (_goRight){
            transform.position = Vector3.Lerp(Vector3.zero, _rightSideSwipePosition.position, val);
            transform.rotation = Quaternion.Lerp(Quaternion.identity, _rightSideSwipePosition.rotation, val);
        } else {
            transform.position = Vector3.Lerp(Vector3.zero, _leftSideSwipePosition.position, val);
            transform.rotation = Quaternion.Lerp(Quaternion.identity, _leftSideSwipePosition.rotation, val);

        }
    }

    private void GoRight(bool val){
        _goRight = val;
    }

    private void HandleEndSwipe(bool swipe){
        if (!swipe){
            // reset position
            ResetPosition();
        } else {
            // Throw away character
            if (_goRight){
                // Debug.Log("TODO: swipe like");
                _animator.SetTrigger("Like");
            } else {
                // Debug.Log("TODO: swipe dislike");
                _animator.SetTrigger("Dislike");
            }
        }
    }

    private void ResetPosition(){
        // reset the position
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

    }

    private void Leave(){
        ResetPosition();
        _animator.SetTrigger("Leave");
    }

    private void OnEnable()
    {
        _aimMagnitudeEventChannel.onEventRaised += WiggleWithAim;
        _swipeRightEventChannel.onEventRaised += GoRight;
        _releaseLeftClickEventChannel.onEventRaised += HandleEndSwipe;
        _timeUpEventChannel.onEventRaised += Leave;
    }

    private void OnDestroy()
    {
        _aimMagnitudeEventChannel.onEventRaised -= WiggleWithAim;
        _swipeRightEventChannel.onEventRaised -= GoRight;
        _releaseLeftClickEventChannel.onEventRaised -= HandleEndSwipe;
        _timeUpEventChannel.onEventRaised -= Leave;
    }

    #region TextBubbles
        public bool EvaluatePrefIndex(int index){
            return _playerManager.EvaluateSingle(index, _preference);
        }
    #endregion
}
