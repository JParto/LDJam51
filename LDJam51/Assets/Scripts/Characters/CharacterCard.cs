using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCard : MonoBehaviour
{
    private int maxPrefAmount => Preferences.preferenceAmount;
    private readonly int easyAmount = 5;
    private readonly int mediumAmount = 7;
    private readonly int hardAmount = 11;

    private Animator _animator;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private CharacterCanvas _canvas;
    [SerializeField] private SO_CharacterSprite[] _spritePool;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _mouseRotationTransform;
    [SerializeField] private Transform _smoking;
    [SerializeField] private Transform _drinking;
    [SerializeField] private Transform _kids;
    [SerializeField] private Transform _marriage;
    [SerializeField] private Transform _humour;
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
    [SerializeField] private SO_VoidEventChannel _startEncounterEventChannel;
    private List<int> _indices;
    [SerializeField] private SO_Sentences _sentences;
    private List<List<List<string>>> _strings;

    void Awake(){
        _animator = GetComponent<Animator>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _canvas = FindObjectOfType<CharacterCanvas>();
        _strings = _sentences.Sentences();
    }

    void Start(){
        // CleanSlate();
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence(){
        yield return new WaitForSeconds(1f);
        StartEncounter();
    }

    void CleanSlate(){ // used in animation at the start of entering
        SetRandomPreference();
        SetRandomSprite();
        ResetPosition();
    }

    public void Animation_Evaluate(){
        _evaluateEventChannel.RaiseEvent(_preference);
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
        SO_CharacterSprite sprite = _spritePool[i];
        _sprite.sprite = sprite.sprite;

        // set the positions of the attributes
        _smoking.gameObject.SetActive(_preference.smoking);
        _smoking.localPosition = sprite.smokePosition;

        _drinking.gameObject.SetActive(_preference.drinking);
        _drinking.localPosition = sprite.drinkingPosition;

        _kids.gameObject.SetActive(_preference.kids);
        _kids.localPosition = sprite.kidsPosition;

        _marriage.gameObject.SetActive(_preference.marriage);
        _marriage.localPosition = sprite.MarriagePosition;

        _humour.gameObject.SetActive(_preference.humour);
        _humour.localPosition = sprite.humourPosition;

        // set a random color
        Color c = Random.ColorHSV(0f, 1f, 0f, 0.6f, 0.5f, 1f);
        _sprite.color = c;
    }

    private void WiggleWithAim(float val){
        if (_goRight){
            _mouseRotationTransform.position = Vector3.Lerp(Vector3.zero, _rightSideSwipePosition.position, val);
            _mouseRotationTransform.rotation = Quaternion.Lerp(Quaternion.identity, _rightSideSwipePosition.rotation, val);
        } else {
            _mouseRotationTransform.position = Vector3.Lerp(Vector3.zero, _leftSideSwipePosition.position, val);
            _mouseRotationTransform.rotation = Quaternion.Lerp(Quaternion.identity, _leftSideSwipePosition.rotation, val);

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
        _mouseRotationTransform.position = Vector3.zero;
        _mouseRotationTransform.rotation = Quaternion.identity;

    }

    private void StartEncounter(){
        foreach (int index in _indices)
        {
            if (index < 7){
                string s = GetRandomSentence(index);
                _canvas.SpawnTextBubble(index, s);
            } 
            // visuals for smoking etc get done at sprite selection
        }
    }


    private string GetRandomSentence(int index){
        int attributeIndex = 0;
        
        switch (index)
        {
            case 0:
                attributeIndex = (int)_preference.prefAnimal;
                break;
            case 1:
                attributeIndex = (int)_preference.prefColor;
                break;
            case 2:
                attributeIndex = (int)_preference.prefLifeStyle;
                break;
            case 3:
                attributeIndex = (int)_preference.prefMusic;
                break;
            case 4:
                attributeIndex = (int)_preference.prefSuperPower;
                break;
            case 5:
                attributeIndex = (int)_preference.prefSound;
                break;
            case 6:
                attributeIndex = (int)_preference.prefAge;
                break;
        }

        var sentenceList = _strings[index][attributeIndex];
        int r = Random.Range(0, sentenceList.Count);
        string finalSentence = sentenceList[r];

        return finalSentence;
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
        _startEncounterEventChannel.onEventRaised += StartEncounter;
    }

    private void OnDestroy()
    {
        _aimMagnitudeEventChannel.onEventRaised -= WiggleWithAim;
        _swipeRightEventChannel.onEventRaised -= GoRight;
        _releaseLeftClickEventChannel.onEventRaised -= HandleEndSwipe;
        _timeUpEventChannel.onEventRaised -= Leave;
        _startEncounterEventChannel.onEventRaised -= StartEncounter;
    }

    #region TextBubbles
        public bool EvaluatePrefIndex(int index){
            return _playerManager.EvaluateSingle(index, _preference);
        }
    #endregion
}
