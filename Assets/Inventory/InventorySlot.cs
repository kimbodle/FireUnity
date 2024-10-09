using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // ������ �������� ǥ���� UI �̹���
    private Item currentItem; // ���� ���Կ� ����� ������

    void Start()
    {
        if (icon == null)
        {
            icon = GetComponent<Image>();
        }
    }
    // �������� ���Կ� �߰��ϴ� �Լ�
    public void AddItem(Item item)
    {
        Debug.Log(item); // �߰��� �ڵ�
        if (item != null)
        {
            currentItem = item;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
        }
        else
        {
            Debug.LogError("�������� null�Դϴ�.");
        }
    }


    // ������ ���� �Լ�
    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
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
