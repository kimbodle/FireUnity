using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; } // 싱글톤 인스턴스
    public GameObject itemDetailPanel; // 아이템 상세보기 패널
    [Space(10)]
    public GameObject slotPrefab; // 슬롯 프리팹
    public List<InventorySlot> inventorySlots; // 인벤토리 슬롯 리스트
    [Space(10)]
    public Image itemDetailImage; // 상세보기 패널의 아이템 이미지
    public TMP_Text itemDetailText; // 상세보기 패널의 텍스트
    public Button itemDetailCloseButton; //상세보기 패널 닫기
    [Space(10)]
    [SerializeField] private Transform content;


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

    private void Start()
    {
        
        itemDetailCloseButton.onClick.AddListener(OncloseitemDetailPanel);
    }

    // 아이템 클릭 시 호출되는 함수 (자세히 보기 또는 드래그 시작)
    public void OnItemDoubleClick(Item item)
    {
        // 아이템 상세보기 패널 활성화 및 정보 설정
        itemDetailPanel.SetActive(true);
        itemDetailImage.sprite = item.itemIcon;
        itemDetailText.text = item.ShowDetails();
    }

    public void OncloseitemDetailPanel()
    {
        itemDetailPanel.SetActive(false);
    }

    // 빈 슬롯을 찾는 함수
    public InventorySlot GetEmptySlot()
    {
        return inventorySlots.Find(slot => slot.IsEmpty());
    }

    // 새로운 슬롯을 생성하는 함수
    public InventorySlot CreateNewSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, content);
        InventorySlot newInventorySlot = newSlot.GetComponent<InventorySlot>();
        inventorySlots.Add(newInventorySlot);
        return newInventorySlot;
    }

    public void RemoveItem(Item item)
    {
        InventorySlot slotToRemove = inventorySlots.Find(slot => slot.HasItem(item));
        if (slotToRemove != null)
        {
            slotToRemove.ClearSlot();
        }
    }


    // 인벤토리 슬롯을 모두 초기화하는 함수
    public void ClearAllSlots()
    {
        foreach (var slot in inventorySlots)
        {
            slot.ClearSlot();
        }
        inventorySlots.Clear(); // 리스트도 비웁니다.
    }

    public void DeselectAllSlots()
    {
        bool anySelected = inventorySlots.Exists(slot => slot.isSelected);
        if (anySelected)
        {
            foreach (var slot in inventorySlots)
            {
                slot.Deselect();
            }
        }
    }

}
