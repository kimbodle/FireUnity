using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    public static DragHandler Instance { get; private set; }
    public Image draggableItemIcon; // 드래그할 아이템의 아이콘
    private Item selectedItem;
    //private Vector3 originalPosition; // 드래그 아이콘의 원래 위치

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

    public void StartDrag(Item item, Sprite itemSprite)
    {
        selectedItem = item;
        draggableItemIcon.sprite = itemSprite;
        draggableItemIcon.gameObject.SetActive(true);
        //originalPosition = draggableItemIcon.transform.position; // 원래 위치 저장
        draggableItemIcon.transform.position = Input.mousePosition;
        Debug.Log($"{selectedItem.itemName} 드래그 시작");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggableItemIcon.gameObject.activeSelf)
        {
            draggableItemIcon.transform.position = Input.mousePosition;
        }
    }

    public bool OnEndDrag(PointerEventData eventData)
    {
        Vector2 dropPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(dropPosition);

        if (hitCollider != null && hitCollider.CompareTag("DropZone"))
        {
            DropZone dropZone = hitCollider.GetComponent<DropZone>();

            // 선택한 아이템이 드롭존의 조건에 맞는지 확인
            if (selectedItem.allowedDropZones.Contains(dropZone.zoneID))
            {
                selectedItem.Use();
                Debug.Log($"{selectedItem.itemName} 사용됨: {dropZone.zoneID}에 드롭");
                ResetDrag();
                return true; // 아이템 사용 성공
            }
            else
            {
                Debug.Log($"아이템을 {dropZone.zoneID}에 사용할 수 없습니다.");
            }
        }

        ResetDrag();
        return false; // 아이템 사용 실패
    }

    private void ResetDrag()
    {
        selectedItem = null;
        draggableItemIcon.gameObject.SetActive(false);
        //draggableItemIcon.transform.position = originalPosition; // 아이콘 위치 초기화
    }
}
