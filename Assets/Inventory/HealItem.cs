using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    private void Start()
    {
        itemName = "heal"; // ������ �̸� �ʱ�ȭ
        // �ڵ忡�� ������ ����
        //itemIcon = Resources.Load<Sprite>("Icons/HealIcon"); // Icons ������ HealIcon�̶�� �̸��� ��������Ʈ�� �־�� ��
        description = "test"; // ���� �ʱ�ȭ
        itemID = 1;
        isUsable = true;
        allowedDropZones = new List<string> { "SafeZone" };
    } 
    
    // ������ ��� �޼���
    public override void Use()
    {
        if (isUsable)
        {
            Debug.Log($"{itemName} ���");
            // ������ ��� ���� �߰� (��: ü�� ȸ�� ��)
            InventoryUI.Instance.RemoveItem(this); // ������ ��� �� ����
        }
        else
        {
            Debug.Log($"{itemName}�� ����� �� �����ϴ�.");
        }
    }


    // ������ �󼼺��� �޼���
    public override string ShowDetails()
    {
        //string details = $"{itemName}: {description}, ü�� ȸ����: {healAmount}";
        string details = description;
        Debug.Log(details);
        return details;
        // UI�� �� ������ ǥ���ϴ� ������ �ʿ��ϴٸ� ���⿡ �߰�
        // ��: itemDetailPanel.GetComponentInChildren<Text>().text = details;
    }
}
