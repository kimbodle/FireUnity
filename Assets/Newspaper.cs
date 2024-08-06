using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour, IInteractable
{
    public string interactionMessage = "Press [E] to look.";

    public void OnInteract()
    {
        // 신문을 살펴보는 동작 구현
        Debug.Log("신문을 살펴봅니다.");
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }
}
