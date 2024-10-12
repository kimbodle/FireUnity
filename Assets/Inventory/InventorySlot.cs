using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image defaultIcon; // 아이템 아이콘을 표시할 기본 UI 이미지
    [SerializeField]
    private Item currentItem; // 현재 슬롯에 저장된 아이템
    public bool isSelected = false; // 아이템 선택 상태

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
            // 슬롯에 같은 스크립트 추가
            Item newItem = item.GetComponent<Item>();
            currentItem = newItem;

            // 슬롯에 같은 타입의 스크립트 추가
            System.Type itemType = item.GetType();
            if (GetComponent(itemType) == null)
            {
                gameObject.AddComponent(itemType);
            }

            defaultIcon.sprite = item.itemIcon;
            defaultIcon.enabled = true;
        }
    }

    // 슬롯을 클릭했을 때 호출되는 함수
    public void OnSlotClick()
    {
        currentItem = GetComponent<Item>();
        Debug.Log("테스트중");
        if (currentItem != null)
        {
            if (!isSelected)
            {
                InventoryUI.Instance.DeselectAllSlots(); // 다른 슬롯 선택 해제
                isSelected = true;
                defaultIcon.color = Color.grey;
                Debug.Log($"{currentItem.itemName} 선택됨");
            }
            else
            {
                InventoryUI.Instance.OnItemDoubleClick(currentItem); // 아이템 상세 보기
                isSelected = false;
                defaultIcon.color = Color.white;
            }
        }
        else
        {
            Debug.Log("currentItem is null");
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

    public void Deselect()
    {
        isSelected = false;
        defaultIcon.color = Color.white;
    }
}
