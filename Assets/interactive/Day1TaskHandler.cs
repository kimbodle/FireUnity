using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1TaskHandler : TaskHandler
{
    protected override void InitializeTaskMappings()
    {
        taskMappings = new Dictionary<string, IInteractable>();

        // FindObjectOfType�� ����� �� �ش� ��ü�� �������� ���� �� ������ ����ó��
        var newspaper = FindObjectOfType<Newspaper>();
        var map = FindObjectOfType<Map>();

        if (newspaper != null)
        {
            taskMappings.Add("FindItem", newspaper);
        }
        else
        {
            Debug.Log("Newspaper ��ü�� ã�� �� �����ϴ�.");
        }

        if (map != null)
        {
            taskMappings.Add("MapClick", map);
        }
        else
        {
            Debug.Log("Map ��ü�� ã�� �� �����ϴ�.");
        }

        // �ٸ� Task ���� �߰� ����
    }
}

