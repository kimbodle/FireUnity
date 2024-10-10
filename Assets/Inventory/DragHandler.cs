using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Image draggableItemIcon; // �巡���� �������� ������
    private Item selectedItem;
    private Vector3 originalPosition; // �������� ���� ��ġ
    public LayerMask dropLayerMask;

    // �巡�� ���� �� ȣ��Ǵ� �޼���
    public void StartDrag(Item item)
    {
        selectedItem = item;
        draggableItemIcon.sprite = item.itemIcon;
        draggableItemIcon.gameObject.SetActive(true);
        originalPosition = draggableItemIcon.transform.position; // ���� ��ġ ����
        Debug.Log("�巡�� ����");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag �Լ� ȣ�� ��");
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
                InventoryUI.Instance.RemoveItem(selectedItem); // ������ ��� �� ����
                Debug.Log($"Dropped on: {hitCollider.name}");
            }
            else
            {
                // ����� �������� �� ���� ��ġ�� �ǵ�����
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
