using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName; // ������ �̸�
    public Sprite itemIcon; // ������ ������
    public string description; // ������ ����
    public int itemID; // ������ ID
    public bool isUsable; // ������ ��� ���� ����
    public List<string> allowedDropZones; // �������� ����� �� �ִ� ����� ID ���

    // ������ ��� �޼��� (�� �����ۿ� ���� ����)
    public Action<ItemData> UseAction;
    // ������ �󼼺��⸦ ��ȯ�ϴ� �޼���
    public string ShowDetails()
    {
        return $"{itemName}: {description}";
    }

    // ��� �޼��� ȣ��
    public void Use()
    {
        UseAction?.Invoke(this); // UseAction�� null�� �ƴ� ��� ȣ��
    }
}
