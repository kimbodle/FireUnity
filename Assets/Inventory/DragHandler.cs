using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Image draggableItemIcon; // 드래그할 아이템의 아이콘
    private Item selectedItem;
    private Vector3 originalPosition; // 아이콘의 원래 위치
    public LayerMask dropLayerMask;

    // 드래그 시작 시 호출되는 메서드
    public void StartDrag(Item item)
    {
        selectedItem = item;
        draggableItemIcon.sprite = item.itemIcon;
        draggableItemIcon.gameObject.SetActive(true);
        originalPosition = draggableItemIcon.transform.position; // 원래 위치 저장
        Debug.Log("드래그 시작");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag 함수 호출 중");
        if (draggableItemIcon.gameObject.activeSelf)
        {
            draggableItemIcon.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (selectedItem != null)
        {
            Vector2 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(dropPosition, dropLayerMask);

            if (hitCollider != null && hitCollider.CompareTag("DropZone"))
            {
                Debug.Log(selectedItem.itemID);
                selectedItem.Use();
                InventoryUI.Instance.RemoveItem(selectedItem); // 아이템 사용 후 제거
                Debug.Log($"Dropped on: {hitCollider.name}");
            }
            else
            {
                // 드롭이 실패했을 때 원래 위치로 되돌리기
                draggableItemIcon.transform.position = originalPosition;
            }

            ResetDrag();
        }
    }

    private void ResetDrag()
    {
        selectedItem = null;
        draggableItemIcon.gameObject.SetActive(false);
    }
}
