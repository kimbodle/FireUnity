using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day1UI : MonoBehaviour
{
    public Button completeTaskButton;
    public Button saveGameButton;
    public Button loadGameButton;
    // Start is called before the first frame update
    void Start()
    {
        completeTaskButton.onClick.AddListener(OnCompleteTaskButtonClicked);
        saveGameButton.onClick.AddListener(OnSaveGameButtonClicked);
        loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
    }

    private void OnLoadGameButtonClicked()
    {
        GameManager.Instance.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCompleteTaskButtonClicked()
    {
        // Task 완료 처리
        GameManager.Instance.CompleteTask();
    }

    private void OnSaveGameButtonClicked()
    {
        // 게임 상태 저장
        GameManager.Instance.SaveGame();
    }
}
