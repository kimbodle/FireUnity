using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject LoginUI;
    public Button completeTaskButton;
    public Button saveGameButton;
    public Button loadGameButton;

    void Start()
    {
        LoginUI.SetActive(false);

        // ��ư Ŭ�� �̺�Ʈ�� �޼��� ����
        completeTaskButton.onClick.AddListener(OnCompleteTaskButtonClicked);
        saveGameButton.onClick.AddListener(OnSaveGameButtonClicked);
        loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
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
        // Task �Ϸ� ó��
        GameManager.Instance.CompleteTask();
    }

    private void OnSaveGameButtonClicked()
    {
        // ���� ���� ����
        GameManager.Instance.SaveGame();
    }

    private void OnLoadGameButtonClicked()
    {
        // ���� ���� �ҷ�����
        GameManager.Instance.LoadGame();
    }
}
