using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    public int healAmount; // ȸ����

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
