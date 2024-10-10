using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image defalutIcon; // 아이템 아이콘을 표시할 기본 UI 이미지
    private Item currentItem; // 현재 슬롯에 저장된 아이템

    void Start()
    {
        if (defalutIcon == null)
        {
            defalutIcon = GetComponent<Image>();
        }
    }
    // 아이템을 슬롯에 추가하는 함수
    public void AddItem(Item item)
    {
        Debug.Log(item); // 추가된 코드
        if (item != null)
        {
            currentItem = item;
            defalutIcon.sprite = item.itemIcon;
            defalutIcon.enabled = true;
        }
        else
        {
            Debug.LogError("아이템이 null입니다.");
        }
    }

    // 아이템을 사용하는 함수
    public void UseItem()
    {
        if (currentItem != null)
        {
            currentItem.Use();
            InventoryUI.Instance.RemoveItem(currentItem); // 아이템 사용 후 인벤토리에서 제거
            ClearSlot(); // 슬롯에서 아이템 제거
        }
    }


    // 슬롯을 비우는 함수
    public void ClearSlot()
    {
        currentItem = null;
        defalutIcon.sprite = null;
        defalutIcon.enabled = false;
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
