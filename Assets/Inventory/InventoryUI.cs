using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static InventoryUI Instance { get; private set; } // 싱글톤 인스턴스
    public GameObject inventoryPanel; // 인벤토리 UI 패널
    public GameObject slotPrefab; // 슬롯 프리팹
    public List<InventorySlot> inventorySlots; // 인벤토리 슬롯 리스트
    public GameObject itemDetailPanel; // 아이템 상세보기 패널
    public Image itemDetailImage; // 상세보기 패널의 아이템 이미지
    public TMP_Text itemDetailText; // 상세보기 패널의 텍스트
    public Image draggableItemIcon; // 드래그할 아이템의 아이콘
    public LayerMask dropLayerMask; // 아이템을 드롭할 수 있는 콜라이더가 속한 레이어

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

    // 아이템 클릭 시 호출되는 함수 (자세히 보기 또는 드래그 시작)
    public void OnItemClick(Item item)
    {
        if (item.isUsable)
        {
            // 드래그할 아이템을 설정
            selectedItem = item;
            draggableItemIcon.sprite = item.itemIcon;
            draggableItemIcon.gameObject.SetActive(true);
            Debug.Log("드래그용 아이템 아이콘 활성");
        }
        else
        {
            // 아이템 상세보기 패널 활성화 및 정보 설정
            itemDetailPanel.SetActive(true);
            itemDetailImage.sprite = item.itemIcon;
            itemDetailText.text = item.ShowDetails();
        }
    }

    // 아이템을 드래그하는 동안 호출되는 함수
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag함수 호출 중");
        if (draggableItemIcon.gameObject.activeSelf)
        {
            draggableItemIcon.transform.position = Input.mousePosition;
        }
    }

    // 드래그가 끝날 때 호출되는 함수 (드롭 시점)
    public void OnEndDrag(PointerEventData eventData)
    {
        if (selectedItem != null)
        {
            Vector2 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(dropPosition, dropLayerMask);

            if (hitCollider != null)
            {
                // 드롭할 위치가 적합한지 확인 후 아이템 사용
                Debug.Log($"Dropped on: {hitCollider.name}");

                // 예: 특정 콜라이더를 확인하여 아이템 사용 처리
                if (hitCollider.CompareTag("DropZone"))
                {
                    selectedItem.Use();
                }
            }

            ResetDrag();
        }
    }

    // 드래그가 끝나면 초기화
    private void ResetDrag()
    {
        selectedItem = null;
        draggableItemIcon.gameObject.SetActive(false);
    }

    // 빈 슬롯을 찾는 함수
    public InventorySlot GetEmptySlot()
    {
        return inventorySlots.Find(slot => slot.IsEmpty());
    }

    // 새로운 슬롯을 생성하는 함수
    public InventorySlot CreateNewSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);
        InventorySlot newInventorySlot = newSlot.GetComponent<InventorySlot>();
        inventorySlots.Add(newInventorySlot);
        return newInventorySlot;
    }

    // 인벤토리 창을 열고 닫는 함수
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
