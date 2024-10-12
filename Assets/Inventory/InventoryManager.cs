using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; } // 싱글톤 인스턴스
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
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 파괴
        }
    }

    // 아이템을 인벤토리에 추가하는 함수
    public void AddItem(Item item)
    {
        InventorySlot emptySlot = inventoryUI.GetEmptySlot();
        if (emptySlot == null)
        {
            Debug.Log("슬롯 비었음");

            emptySlot = inventoryUI.CreateNewSlot();
        }
        Debug.Log("슬롯 있음");
        emptySlot.AddItem(item);
    }

    // 아이템을 인벤토리에서 제거하는 함수
    public void RemoveItem(Item item, InventoryUI inventoryUI)
    {
        InventorySlot slotToRemove = inventoryUI.inventorySlots.Find(slot => slot.HasItem(item));
        if (slotToRemove != null)
        {
            slotToRemove.ClearSlot();
        }
    }
}
