using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    public static DragHandler Instance { get; private set; }
    public Image draggableItemIcon; // �巡���� �������� ������
    private Item selectedItem;
    //private Vector3 originalPosition; // �巡�� �������� ���� ��ġ

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
        //originalPosition = draggableItemIcon.transform.position; // ���� ��ġ ����
        draggableItemIcon.transform.position = Input.mousePosition;
        Debug.Log($"{selectedItem.itemName} �巡�� ����");
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

            // ������ �������� ������� ���ǿ� �´��� Ȯ��
            if (selectedItem.allowedDropZones.Contains(dropZone.zoneID))
            {
                selectedItem.Use();
                Debug.Log($"{selectedItem.itemName} ����: {dropZone.zoneID}�� ���");
                ResetDrag();
                return true; // ������ ��� ����
            }
            else
            {
                Debug.Log($"�������� {dropZone.zoneID}�� ����� �� �����ϴ�.");
            }
        }

        ResetDrag();
        return false; // ������ ��� ����
    }

    private void ResetDrag()
    {
        selectedItem = null;
        draggableItemIcon.gameObject.SetActive(false);
        //draggableItemIcon.transform.position = originalPosition; // ������ ��ġ �ʱ�ȭ
    }
}
