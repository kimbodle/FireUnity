using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemClick : MonoBehaviour
{
    private Item item; // ȹ���� ������ ����

    private void Start()
    {
        item = GetComponent<Item>();
    }

    // ���콺 Ŭ���� �������� �� ȣ��Ǵ� �޼���
    private void OnMouseDown()
    {
        // �κ��丮�� ������ �߰�
        InventoryManager.Instance.AddItem(item);
        Debug.Log($"{item.itemName}��(��) ȹ���߽��ϴ�!");
        

        // ������ ������Ʈ�� ��Ȱ��ȭ�ϰų� ����
        //Destroy(gameObject);
    }
}
