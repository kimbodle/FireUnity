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

    private void OnMouseDown() // 마우스 클릭 시 호출
    {
        OnInteract();
    }

    public void OnInteract()
    {
        // 신문을 살펴보는 동작 구현
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
            Debug.Log("map 후속 작업 처리");
            // 후속 작업 로직 추가
        }
    }
}
