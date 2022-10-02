using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterTextBubble : MonoBehaviour
{
    CharacterCanvas _canvas;
    public int relatedPreferenceIndex;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color correctColor;
    [SerializeField] private Color incorrectColor;

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _dropOutDuration;
    private float _currentDuration = 0f;
    Vector2 direction;
    [Header("Listens to")]
    [SerializeField] private SO_BoolEventChannel _matchEventChannel;
    [SerializeField] private SO_VoidEventChannel _timeUpEventChannel;


    void Awake(){
        _canvas = transform.root.GetComponent<CharacterCanvas>();
        _text = GetComponent<TextMeshProUGUI>();
        direction = SetRandomPointToGoTo();
    }

    void Update(){
        if (_currentDuration < _dropOutDuration){
            _currentDuration += Time.deltaTime;
        }

        float t = _curve.Evaluate(_currentDuration/_dropOutDuration);

        transform.position = Vector2.Lerp(Vector2.zero, direction, t);
    }

    public void Spawn(int index, string text){
        relatedPreferenceIndex = index;
        _text.text = text;
    }

    public void EvaluateTrue(){
        bool b = _canvas.EvaluatePrefIndex(relatedPreferenceIndex);
        Color c = b ? correctColor : incorrectColor;
        _text.color = c;
    }

    public void EvaluateFalse(){
        _canvas.EvaluatePrefIndex(relatedPreferenceIndex);
    }

    private Vector2 SetRandomPointToGoTo(){
        Vector2 baseDirection = Random.value > 0.5f ? Vector2.right : Vector2.left;
        float angle = Random.Range(-30f, 30f);

        Vector2 dir = Quaternion.Euler(0, 0, angle) * baseDirection;
        float distance = Random.Range(_minDistance, _maxDistance);
        dir *= distance;
        return dir;
    }

    void Remove(bool v){
        Remove();
    }

    void Remove(){
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        _matchEventChannel.onEventRaised += Remove;
        _timeUpEventChannel.onEventRaised += Remove;
    }

    private void OnDestroy()
    {
        _matchEventChannel.onEventRaised -= Remove;
        _timeUpEventChannel.onEventRaised -= Remove;

    }
}
