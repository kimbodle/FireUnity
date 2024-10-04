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
    public GameObject mapUI; // 지도 UI 오브젝트
    public Button paperIconButton; // 종이 아이콘 버튼

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
        // 시작할 때는 지도 UI를 비활성화
        mapUI.SetActive(false);


        //종이 아이콘을 눌렀을 때 지도 UI를 활성화/비활성화
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
        Debug.Log("맵 버턴 눌렸어용");
        // 지도 UI가 활성화되어 있다면 비활성화, 비활성화 상태면 활성화
        mapUI.SetActive(!mapUI.activeSelf);
    }
}
