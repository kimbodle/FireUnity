using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; } // �̱��� �ν��Ͻ�
    public GameObject inventoryPanel; // �κ��丮 UI �г�
    public GameObject slotPrefab; // ���� ������
    public List<InventorySlot> inventorySlots; // �κ��丮 ���� ����Ʈ
    public GameObject itemDetailPanel; // ������ �󼼺��� �г�
    public Image itemDetailImage; // �󼼺��� �г��� ������ �̹���
    public TMP_Text itemDetailText; // �󼼺��� �г��� �ؽ�Ʈ

    public Button IventoryUiIcon;
    public Button itemDetailCloseButton;
    

    DragHandler dragHandler;

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

    private void Start()
    {
        dragHandler= FindObjectOfType<DragHandler>();
        //itemDetailCloseButton = GetComponentInChildren<Button>();
        IventoryUiIcon.onClick.AddListener(ToggleInventory);
        itemDetailCloseButton.onClick.AddListener(OncloseitemDetailPanel);
    }

    // ������ Ŭ�� �� ȣ��Ǵ� �Լ� (�ڼ��� ���� �Ǵ� �巡�� ����)
    public void OnItemDoubleClick(Item item)
    {
        // ������ �󼼺��� �г� Ȱ��ȭ �� ���� ����
        itemDetailPanel.SetActive(true);
        itemDetailImage.sprite = item.itemIcon;
        itemDetailText.text = item.ShowDetails();
    }

    public void OncloseitemDetailPanel()
    {
        itemDetailPanel.SetActive(false);
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

    public void RemoveItem(Item item)
    {
        InventorySlot slotToRemove = inventorySlots.Find(slot => slot.HasItem(item));
        if (slotToRemove != null)
        {
            slotToRemove.ClearSlot();
        }
    }

    // �κ��丮 â�� ���� �ݴ� �Լ�
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
