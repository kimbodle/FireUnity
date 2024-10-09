using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static InventoryUI Instance { get; private set; } // �̱��� �ν��Ͻ�
    public GameObject inventoryPanel; // �κ��丮 UI �г�
    public GameObject slotPrefab; // ���� ������
    public List<InventorySlot> inventorySlots; // �κ��丮 ���� ����Ʈ
    public GameObject itemDetailPanel; // ������ �󼼺��� �г�
    public Image itemDetailImage; // �󼼺��� �г��� ������ �̹���
    public TMP_Text itemDetailText; // �󼼺��� �г��� �ؽ�Ʈ
    public Image draggableItemIcon; // �巡���� �������� ������
    public LayerMask dropLayerMask; // �������� ����� �� �ִ� �ݶ��̴��� ���� ���̾�

    private Item selectedItem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ������ Ŭ�� �� ȣ��Ǵ� �Լ� (�ڼ��� ���� �Ǵ� �巡�� ����)
    public void OnItemClick(Item item)
    {
        if (item.isUsable)
        {
            // �巡���� �������� ����
            selectedItem = item;
            draggableItemIcon.sprite = item.itemIcon;
            draggableItemIcon.gameObject.SetActive(true);
            Debug.Log("�巡�׿� ������ ������ Ȱ��");
        }
        else
        {
            // ������ �󼼺��� �г� Ȱ��ȭ �� ���� ����
            itemDetailPanel.SetActive(true);
            itemDetailImage.sprite = item.itemIcon;
            itemDetailText.text = item.ShowDetails();
        }
    }

    // �������� �巡���ϴ� ���� ȣ��Ǵ� �Լ�
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag�Լ� ȣ�� ��");
        if (draggableItemIcon.gameObject.activeSelf)
        {
            draggableItemIcon.transform.position = Input.mousePosition;
        }
    }

    // �巡�װ� ���� �� ȣ��Ǵ� �Լ� (��� ����)
    public void OnEndDrag(PointerEventData eventData)
    {
        if (selectedItem != null)
        {
            Vector2 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(dropPosition, dropLayerMask);

            if (hitCollider != null)
            {
                // ����� ��ġ�� �������� Ȯ�� �� ������ ���
                Debug.Log($"Dropped on: {hitCollider.name}");

                // ��: Ư�� �ݶ��̴��� Ȯ���Ͽ� ������ ��� ó��
                if (hitCollider.CompareTag("DropZone"))
                {
                    selectedItem.Use();
                }
            }

            ResetDrag();
        }
    }

    // �巡�װ� ������ �ʱ�ȭ
    private void ResetDrag()
    {
        selectedItem = null;
        draggableItemIcon.gameObject.SetActive(false);
    }

    // �� ������ ã�� �Լ�
    public InventorySlot GetEmptySlot()
    {
        return inventorySlots.Find(slot => slot.IsEmpty());
    }

    // ���ο� ������ �����ϴ� �Լ�
    public InventorySlot CreateNewSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);
        InventorySlot newInventorySlot = newSlot.GetComponent<InventorySlot>();
        inventorySlots.Add(newInventorySlot);
        return newInventorySlot;
    }

    // �κ��丮 â�� ���� �ݴ� �Լ�
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
