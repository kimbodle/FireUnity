using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour, IInteractable
{
    public string interactionMessage = "Press [E] to look.";

    public void OnInteract()
    {
        // �Ź��� ���캸�� ���� ����
        Debug.Log("�Ź��� ���캾�ϴ�.");
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }
}
