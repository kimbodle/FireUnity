using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image defaultIcon; // 아이템 아이콘을 표시할 기본 UI 이미지
    private Item currentItem; // 현재 슬롯에 저장된 아이템
    private bool isSelected = false; // 아이템 선택 상태

    void Start()
    {
        if (defaultIcon == null)
        {
            defaultIcon = GetComponent<Image>();
        }
    }

    // 아이템을 슬롯에 추가하는 함수
    public void AddItem(Item item)
    {
        if (item != null)
        {
            currentItem = item;
            defaultIcon.sprite = item.itemIcon;
            defaultIcon.enabled = true;
        }
    }

    // 슬롯을 클릭했을 때 호출되는 함수
    public void OnSlotClick()
    {
        Debug.Log("테스트좀");
        if (currentItem != null)
        {
            
            if (!isSelected)
            {
                isSelected = true; // 선택 상태로 설정
                defaultIcon.color = Color.grey;
                Debug.Log($"{currentItem.itemName} 선택됨");
            }
            else
            {
                // 선택 상태에서 다시 클릭하면 상세보기
                InventoryUI.Instance.OnItemDoubleClick(currentItem);
                Debug.Log($"{currentItem.itemName} 상세보기");
                isSelected = false;
                defaultIcon.color = Color.white;
            }
        }
    }

    // 드래그 시작 시 호출
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSelected && currentItem != null)
        {
            DragHandler.Instance.StartDrag(currentItem, defaultIcon.sprite);
            defaultIcon.raycastTarget = false; // 드래그 중에는 클릭되지 않도록
        }
    }

    // 드래그 중일 때 호출
    public void OnDrag(PointerEventData eventData)
    {
        if (isSelected && currentItem != null)
        {
            DragHandler.Instance.OnDrag(eventData);
        }
    }

    // 드래그 종료 시 호출
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSelected && currentItem != null)
        {
            bool used = DragHandler.Instance.OnEndDrag(eventData);

            if (used)
            {
                Debug.Log($"{currentItem.itemName}이 사용되어 슬롯 초기화");
                InventoryUI.Instance.RemoveItem(currentItem);
                ClearSlot();
                
            }
            isSelected = false;
            defaultIcon.color = Color.white;
            defaultIcon.raycastTarget = true; // 드래그 종료 후 다시 클릭 가능하도록 설정
        }
    }

    // 슬롯을 비우는 함수
    public void ClearSlot()
    {
        currentItem = null;
        defaultIcon.sprite = null;
        defaultIcon.enabled = false;
        isSelected = false;
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
}
