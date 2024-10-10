using UnityEngine;

public abstract class Item: MonoBehaviour
{
    public string itemName; // ������ �̸�
    public Sprite itemIcon; // ������ ������
    public string description; // ������ ����
    public int itemID; // ������ ID
    public bool isUsable; // ������ ��� ���� ����

    // ������ ��� �޼��� (�� �����ۿ� ���� ����)
    public abstract void Use();

    // ������ �󼼺��⸦ ��ȯ�ϴ� �޼���
    public abstract string ShowDetails();
}
