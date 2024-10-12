using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image defaultIcon; // ������ �������� ǥ���� �⺻ UI �̹���
    [SerializeField]
    private Item currentItem; // ���� ���Կ� ����� ������
    public bool isSelected = false; // ������ ���� ����

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
            // ���Կ� ���� ��ũ��Ʈ �߰�
            Item newItem = item.GetComponent<Item>();
            currentItem = newItem;

            // ���Կ� ���� Ÿ���� ��ũ��Ʈ �߰�
            System.Type itemType = item.GetType();
            if (GetComponent(itemType) == null)
            {
                gameObject.AddComponent(itemType);
            }

            defaultIcon.sprite = item.itemIcon;
            defaultIcon.enabled = true;
        }
    }

    // ������ Ŭ������ �� ȣ��Ǵ� �Լ�
    public void OnSlotClick()
    {
        currentItem = GetComponent<Item>();
        Debug.Log("�׽�Ʈ��");
        if (currentItem != null)
        {
            if (!isSelected)
            {
                InventoryUI.Instance.DeselectAllSlots(); // �ٸ� ���� ���� ����
                isSelected = true;
                defaultIcon.color = Color.grey;
                Debug.Log($"{currentItem.itemName} ���õ�");
            }
            else
            {
                InventoryUI.Instance.OnItemDoubleClick(currentItem); // ������ �� ����
                isSelected = false;
                defaultIcon.color = Color.white;
            }
        }
        else
        {
            Debug.Log("currentItem is null");
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

    public void Deselect()
    {
        isSelected = false;
        defaultIcon.color = Color.white;
    }
}
