using System.Collections.Generic;
using UnityEngine;

public abstract class Item: MonoBehaviour
{
    public string itemName; // ������ �̸�
    public Sprite itemIcon; // ������ ������
    public string description; // ������ ����
    public int itemID; // ������ ID
    public bool isUsable; // ������ ��� ���� ����
    public List<string> allowedDropZones; // �������� ����� �� �ִ� ����� ID ���

    // ������ ��� �޼��� (�� �����ۿ� ���� ����)
    public abstract void Use();

    // ������ �󼼺��⸦ ��ȯ�ϴ� �޼���
    public abstract string ShowDetails();
}
