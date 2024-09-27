using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day1UI : MonoBehaviour
{
    public Button nextDay;
    public Button saveGameButton;
    public Button loadGameButton;
    public Button mapClickButton;
    public Button MoveshelterButton;

    private Day1Controller day1Controller;
    //public static event Action<string> OnButtonClicked;

    void Start()
    {
        day1Controller = (Day1Controller)StateManager.Instance.GetCurrentDayController();
        Debug.Log($"day1Controller: {day1Controller}");

        if (day1Controller == null)
        {
            Debug.LogError("Day1Controller를 찾을 수 없습니다.");
        }
        else
        {
            mapClickButton.onClick.AddListener(OnMapClickClicked);
        }
    }


    public void OnLoadGameButtonClicked()
    {
        GameManager.Instance.LoadGame();

    }
    public void OnMapClickClicked()
    {
        GameManager.Instance.currentTask="MapClick";
        OnCompleteTaskButtonClicked();
        //day1Controller = (Day1Controller)StateManager.Instance.GetCurrentDayController();

        //if (day1Controller == null)
        //{
        //    Debug.LogError("Day1Controller를 찾을 수 없습니다.");
        //}
        //else
        //{
        //    mapClickButton.onClick.AddListener(OnMapClickClicked);
        //    day1Controller.CompleteTask("MapClick");
        //}
    }
    public void OnMoveshelterClicked()
    {
        GameManager.Instance.currentTask = "Moveshelter";
        OnCompleteTaskButtonClicked();
    }

    public void OnCompleteTaskButtonClicked()
    {
        // Task 완료 처리
        GameManager.Instance.CompleteTask();
    }

    public void OnSaveGameButtonClicked()
    {
        // 게임 상태 저장
        Debug.Log("savebutton눌림");
        GameManager.Instance.SaveGame();
        Debug.Log("함수 실행됨");


    }
}
