using System.Collections.Generic;
using UnityEngine;

public class TaskHandler : MonoBehaviour
{
    private Dictionary<string, IInteractable> taskMappings;

    private void OnEnable()
    {
        // ��ȣ�ۿ� ������ ��ü���� ã�Ƽ� Dictionary�� �����մϴ�.
        taskMappings = new Dictionary<string, IInteractable>
        {
            { "FindItem", FindObjectOfType<Newspaper>() },
            //{ "MapClick", FindObjectOfType<Map>() }, // Map Ŭ������ IInteractable�� �����ؾ� �մϴ�.
            //{ "Moveshelter", FindObjectOfType<Shelter>() } // Shelter Ŭ������ IInteractable�� �����ؾ� �մϴ�.
        };
    }

    public void HandleTask(string taskKey)
    {
        if (taskMappings.TryGetValue(taskKey, out var interactable))
        {
            interactable.HandleTask(taskKey);
        }
        else
        {
            Debug.LogWarning($"{taskKey}�� �������� �ʴ� �۾��Դϴ�.");
        }
    }
}
 