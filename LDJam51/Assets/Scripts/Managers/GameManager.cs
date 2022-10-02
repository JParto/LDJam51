using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ModularMotion;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PlayerInput _input;
    private readonly string _pauseActionName = "Pause";
    private InputAction _pauseAction;
    [SerializeField] private GameObject _pauseMenu;
    private readonly string _gameScene = "GameScene";
    private readonly string _prefScene = "Preferences Scene";
    [SerializeField] private UIMotion motion;
    
    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        _input = GetComponent<PlayerInput>();
        _pauseAction = _input.actions[_pauseActionName];
        _pauseAction.performed += ctx => Pause();
    }

    public void Pause(){
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
    }

    public void Button_Resume(){
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }

    public void StartPrefScene(){
        StartCoroutine(StartPref(2f));
    }

    IEnumerator StartPref(float time){
        motion.Play();
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(_prefScene);
        motion.Play();
        Time.timeScale = 1f;
    }
    IEnumerator StartGame(float time){
        motion.Play();
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(_gameScene);
        motion.Play();
    }

    public void StartGameScene(){
        StartCoroutine(StartGame(2f));
    }

    public void QuitGame(){
        Application.Quit();
    }
}
