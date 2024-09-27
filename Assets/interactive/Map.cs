using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour, IInteractable
{
    public string interactionMessage = "Press [E] to look.";
    Day1Controller day1Controller;

    Renderer map;


    private void OnEnable()
    {
        map = GetComponent<Renderer>();
        day1Controller = FindObjectOfType<Day1Controller>();
    }

    private void OnMouseDown() // ���콺 Ŭ�� �� ȣ��
    {
        OnInteract();
    }

    public void OnInteract()
    {
        // �Ź��� ���캸�� ���� ����
        map.material.color = Color.blue;
        day1Controller.CompleteTask("MapClick");
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }
    public void HandleTask(string taskKey)
    {
        if (taskKey == "MapClick")
        {
            map.material.color = Color.blue;
            Debug.Log("map �ļ� �۾� ó��");
            // �ļ� �۾� ���� �߰�
        }
    }
}
