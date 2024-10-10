using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image defalutIcon; // ������ �������� ǥ���� �⺻ UI �̹���
    private Item currentItem; // ���� ���Կ� ����� ������

    void Start()
    {
        if (defalutIcon == null)
        {
            defalutIcon = GetComponent<Image>();
        }
    }
    // �������� ���Կ� �߰��ϴ� �Լ�
    public void AddItem(Item item)
    {
        Debug.Log(item); // �߰��� �ڵ�
        if (item != null)
        {
            currentItem = item;
            defalutIcon.sprite = item.itemIcon;
            defalutIcon.enabled = true;
        }
        else
        {
            Debug.LogError("�������� null�Դϴ�.");
        }
    }

    // �������� ����ϴ� �Լ�
    public void UseItem()
    {
        if (currentItem != null)
        {
            currentItem.Use();
            InventoryUI.Instance.RemoveItem(currentItem); // ������ ��� �� �κ��丮���� ����
            ClearSlot(); // ���Կ��� ������ ����
        }
    }


    // ������ ���� �Լ�
    public void ClearSlot()
    {
        currentItem = null;
        defalutIcon.sprite = null;
        defalutIcon.enabled = false;
    }

    // ������ ������� Ȯ���ϴ� �Լ�
    public bool IsEmpty()
    {
        return currentItem == null;
    }

    // ���� ���Կ� �ִ� �������� ��ȯ
    public Item GetItem()
    {
        return currentItem;
    }

    // ���� �������� Ư�� ���������� Ȯ��
    public bool HasItem(Item item)
    {
        return currentItem == item;
    }

    // ������ Ŭ������ �� ȣ��Ǵ� �Լ�
    public void OnSlotClick()
    {
        if (currentItem != null)
        {
            InventoryUI.Instance.OnItemClick(currentItem);
        }
    }
}
