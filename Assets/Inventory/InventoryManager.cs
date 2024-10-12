using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; } // �̱��� �ν��Ͻ�
    private InventoryUI inventoryUI;

    private List<Item> items = new List<Item>();

    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ı�
        }
    }

    // �������� �κ��丮�� �߰��ϴ� �Լ�
    public void AddItem(Item item)
    {
        InventorySlot emptySlot = inventoryUI.GetEmptySlot();
        if (emptySlot == null)
        {
            Debug.Log("���� �����");

            emptySlot = inventoryUI.CreateNewSlot();
        }
        Debug.Log("���� ����");
        emptySlot.AddItem(item);
    }

    // �������� �κ��丮���� �����ϴ� �Լ�
    public void RemoveItem(Item item, InventoryUI inventoryUI)
    {
        InventorySlot slotToRemove = inventoryUI.inventorySlots.Find(slot => slot.HasItem(item));
        if (slotToRemove != null)
        {
            slotToRemove.ClearSlot();
        }
    }
}
