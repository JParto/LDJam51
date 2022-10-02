using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public static EndManager instance;
    [SerializeField] private GameObject EndScreen;

    void Awake(){
        instance = this;
    }

    public void ShowEndScreen(){
        Time.timeScale = 0f;
        EndScreen.SetActive(true);
    }

    public void Button_ReturnToStart(){
        GameManager.instance.StartPrefScene();
    }
}
