using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance { get; private set; }

    public GameObject LoginUI;

    [Space(10)] //����
    public GameObject mapCanvas; // ���� UI ������Ʈ
    public Button mapIconButton; // ���� ������ ��ư


    [Space(10)] //�κ��丮
    public GameObject inventoryCanvas; // �κ��丮 UI �г�
    //public InventoryUI inventoryUI;
    public Button inventoryUIButton;

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
        //inventoryUI = inventoryUI.GetComponent<InventoryUI>();
        inventoryCanvas.SetActive(false);
        LoginUI.SetActive(false);
        mapCanvas.SetActive(false);


        //onClick ����
        mapIconButton.onClick.AddListener(ToggleMapUI);
        inventoryUIButton.onClick.AddListener(ToggleInventoryUI);
    }

    public void OpenLoginUI()
    {
        LoginUI.SetActive(true);
    }

    public void CloseLoginUI()
    {
        LoginUI.SetActive(false);
    }
    public void ToggleMapUI()
    {
        mapCanvas.SetActive(!mapCanvas.activeSelf);
    }
    /*public void ToggleInventoryUI()
    {
        inventoryUI.ToggleInventory();
    }*/

    // �κ��丮 â�� ���� �ݴ� �Լ�
    public void ToggleInventoryUI()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }
}
