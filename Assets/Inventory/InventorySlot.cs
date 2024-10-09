using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // 아이템 아이콘을 표시할 UI 이미지
    private Item currentItem; // 현재 슬롯에 저장된 아이템

    void Start()
    {
        if (icon == null)
        {
            icon = GetComponent<Image>();
        }
    }
    // 아이템을 슬롯에 추가하는 함수
    public void AddItem(Item item)
    {
        Debug.Log(item); // 추가된 코드
        if (item != null)
        {
            currentItem = item;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
        }
        else
        {
            Debug.LogError("아이템이 null입니다.");
        }
    }


    // 슬롯을 비우는 함수
    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    // 슬롯이 비었는지 확인하는 함수
    public bool IsEmpty()
    {
        return currentItem == null;
    }

    // 현재 슬롯에 있는 아이템을 반환
    public Item GetItem()
    {
        return currentItem;
    }

    // 현재 아이템이 특정 아이템인지 확인
    public bool HasItem(Item item)
    {
        return currentItem == item;
    }

    // 슬롯을 클릭했을 때 호출되는 함수
    public void OnSlotClick()
    {
        if (currentItem != null)
        {
            InventoryUI.Instance.OnItemClick(currentItem);
        }
    }
}
