using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMousePosition : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    private readonly string _mouseInput = "MousePosition";
    private readonly string _leftClickInput = "LeftClick";
    private readonly string _rightClickInput = "RightClick";
    private InputAction _mouse;
    private InputAction _leftClick;
    private InputAction _rightClick;

    [SerializeField] private float _maxMagnitude;
    private float _maxMagnitudeSquared { get {return _maxMagnitude * _maxMagnitude;}}
    private float _currentMagnitude;

    [SerializeField] private SO_FloatEventChannel _aimMagnitudeEventChannel;
    [SerializeField] private SO_BoolEventChannel _swipeRightEventChannel;
    [SerializeField] private SO_BoolEventChannel _releaseLeftClickEventChannel;
    [SerializeField] private SO_VoidEventChannel _timeUpEventChannel;

    private Vector2 mouseWorldPoint 
    { 
        get {
            Vector2 mousePos = _mouse.ReadValue<Vector2>();
            Vector2 worldPos = _input.camera.ScreenToWorldPoint(mousePos);
            return worldPos;
        }
    }

    private Vector2 _leftClickStartPoint;

    private bool _clickedLeft;

    void Awake(){
        _input = GetComponent<PlayerInput>();

        _mouse = _input.actions[_mouseInput];
        _leftClick = _input.actions[_leftClickInput];
        // _rightClick = _input.actions[_rightClickInput];

        _leftClick.performed += ctx => LeftClickStart();
        _leftClick.canceled  += ctx => LeftClickEnd();
    }

    void Update(){
        transform.position = mouseWorldPoint;

        if(!_clickedLeft){
            return;
        }

        Vector3 direction = mouseWorldPoint - _leftClickStartPoint;
        _currentMagnitude = Mathf.Abs(direction.x);
        float mag = Mathf.Clamp01(_currentMagnitude/_maxMagnitude);
        _aimMagnitudeEventChannel.RaiseEvent(mag);

        _swipeRightEventChannel.RaiseEvent(direction.x >= 0);
    }

    void LeftClickStart(){
        _leftClickStartPoint = mouseWorldPoint;
        _clickedLeft = true;
    }

    void LeftClickEnd(){
        _clickedLeft = false;

        if (!_input.enabled){
            return;
        }

        Vector2 leftClickEndPoint = mouseWorldPoint;

        Vector2 direction = leftClickEndPoint - _leftClickStartPoint;

        // Debug.Log(direction);
        bool swipe = direction.sqrMagnitude >= _maxMagnitudeSquared;
        _releaseLeftClickEventChannel.RaiseEvent(swipe);

        if (swipe)
            Restart();

    }

    IEnumerator PauseForNextCharacter(){
        _input.enabled = false;
        yield return new WaitForSeconds(2f);
        _input.enabled = true;
    }

    private void Restart(){
        StartCoroutine(PauseForNextCharacter());
    }

    private void OnEnable()
    {
        _timeUpEventChannel.onEventRaised += Restart;
    }

    private void OnDestroy()
    {
        _timeUpEventChannel.onEventRaised -= Restart;
    }
}
