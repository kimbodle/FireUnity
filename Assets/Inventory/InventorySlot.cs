using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image defaultIcon; // ������ �������� ǥ���� �⺻ UI �̹���
    private Item currentItem; // ���� ���Կ� ����� ������
    private bool isSelected = false; // ������ ���� ����

    void Start()
    {
        if (defaultIcon == null)
        {
            defaultIcon = GetComponent<Image>();
        }
    }

    // �������� ���Կ� �߰��ϴ� �Լ�
    public void AddItem(Item item)
    {
        if (item != null)
        {
            currentItem = item;
            defaultIcon.sprite = item.itemIcon;
            defaultIcon.enabled = true;
        }
    }

    // ������ Ŭ������ �� ȣ��Ǵ� �Լ�
    public void OnSlotClick()
    {
        Debug.Log("�׽�Ʈ��");
        if (currentItem != null)
        {
            
            if (!isSelected)
            {
                isSelected = true; // ���� ���·� ����
                defaultIcon.color = Color.grey;
                Debug.Log($"{currentItem.itemName} ���õ�");
            }
            else
            {
                // ���� ���¿��� �ٽ� Ŭ���ϸ� �󼼺���
                InventoryUI.Instance.OnItemDoubleClick(currentItem);
                Debug.Log($"{currentItem.itemName} �󼼺���");
                isSelected = false;
                defaultIcon.color = Color.white;
            }
        }
    }

    // �巡�� ���� �� ȣ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSelected && currentItem != null)
        {
            DragHandler.Instance.StartDrag(currentItem, defaultIcon.sprite);
            defaultIcon.raycastTarget = false; // �巡�� �߿��� Ŭ������ �ʵ���
        }
    }

    // �巡�� ���� �� ȣ��
    public void OnDrag(PointerEventData eventData)
    {
        if (isSelected && currentItem != null)
        {
            DragHandler.Instance.OnDrag(eventData);
        }
    }

    // �巡�� ���� �� ȣ��
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSelected && currentItem != null)
        {
            bool used = DragHandler.Instance.OnEndDrag(eventData);

            if (used)
            {
                Debug.Log($"{currentItem.itemName}�� ���Ǿ� ���� �ʱ�ȭ");
                InventoryUI.Instance.RemoveItem(currentItem);
                ClearSlot();
                
            }
            isSelected = false;
            defaultIcon.color = Color.white;
            defaultIcon.raycastTarget = true; // �巡�� ���� �� �ٽ� Ŭ�� �����ϵ��� ����
        }
    }

    // ������ ���� �Լ�
    public void ClearSlot()
    {
        currentItem = null;
        defaultIcon.sprite = null;
        defaultIcon.enabled = false;
        isSelected = false;
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
}
