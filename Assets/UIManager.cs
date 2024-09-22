using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject LoginUI;
    public Button completeTaskButton;
    public Button saveGameButton;
    public Button loadGameButton;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoginUI.SetActive(false);

        // 버튼 클릭 이벤트에 메서드 연결
        //completeTaskButton.onClick.AddListener(OnCompleteTaskButtonClicked);
        //saveGameButton.onClick.AddListener(OnSaveGameButtonClicked);
        //loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
    }

    public void openLoginUI()
    {
        LoginUI.SetActive(true);
    }

    public void closeLoginUI()
    {
        LoginUI.SetActive(false);
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

    private void OnLoadGameButtonClicked()
    {
        // 게임 상태 불러오기
        GameManager.Instance.LoadGame();
    }
}
