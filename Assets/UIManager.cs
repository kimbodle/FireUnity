using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }

    public GameObject LoginUI;

    [Space(10)]
    public GameObject mapUI; // ���� UI ������Ʈ
    public Button paperIconButton; // ���� ������ ��ư

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
        // ������ ���� ���� UI�� ��Ȱ��ȭ
        mapUI.SetActive(false);


        //���� �������� ������ �� ���� UI�� Ȱ��ȭ/��Ȱ��ȭ
        paperIconButton.onClick.AddListener(ToggleMapUI);
    }

    public void openLoginUI()
    {
        LoginUI.SetActive(true);
    }

    public void closeLoginUI()
    {
        LoginUI.SetActive(false);
    }
    public void ToggleMapUI()
    {
        Debug.Log("�� ���� ���Ⱦ��");
        // ���� UI�� Ȱ��ȭ�Ǿ� �ִٸ� ��Ȱ��ȭ, ��Ȱ��ȭ ���¸� Ȱ��ȭ
        mapUI.SetActive(!mapUI.activeSelf);
    }
}
