using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItem : MonoBehaviour, IInteractable
{
    public string interactionMessage = "Press [E] to look.";

    private void OnMouseDown()
    {
        CompletCollectItem();
    }

    private void CompletCollectItem()
    {
        GameObject player = GameObject.FindWithTag("Player"); // �÷��̾� ã��
        if (player != null && player.GetComponent<ItemCollector>() != null)
        {
            player.GetComponent<ItemCollector>().CollectItem();
            Destroy(gameObject); // �������� �����ϰ� �ı�
        }
    }

    public void OnInteract()
    {
        CompletCollectItem();
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void HandleTask(string taskKey)
    {
        
    }
}
