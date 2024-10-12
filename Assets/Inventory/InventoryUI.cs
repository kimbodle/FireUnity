using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; } // �̱��� �ν��Ͻ�
    public GameObject itemDetailPanel; // ������ �󼼺��� �г�
    [Space(10)]
    public GameObject slotPrefab; // ���� ������
    public List<InventorySlot> inventorySlots; // �κ��丮 ���� ����Ʈ
    [Space(10)]
    public Image itemDetailImage; // �󼼺��� �г��� ������ �̹���
    public TMP_Text itemDetailText; // �󼼺��� �г��� �ؽ�Ʈ
    public Button itemDetailCloseButton; //�󼼺��� �г� �ݱ�
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

    // ������ Ŭ�� �� ȣ��Ǵ� �Լ� (�ڼ��� ���� �Ǵ� �巡�� ����)
    public void OnItemDoubleClick(Item item)
    {
        // ������ �󼼺��� �г� Ȱ��ȭ �� ���� ����
        itemDetailPanel.SetActive(true);
        itemDetailImage.sprite = item.itemIcon;
        itemDetailText.text = item.ShowDetails();
    }

    public void OncloseitemDetailPanel()
    {
        itemDetailPanel.SetActive(false);
    }

    // �� ������ ã�� �Լ�
    public InventorySlot GetEmptySlot()
    {
        return inventorySlots.Find(slot => slot.IsEmpty());
    }

    // ���ο� ������ �����ϴ� �Լ�
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


    // �κ��丮 ������ ��� �ʱ�ȭ�ϴ� �Լ�
    public void ClearAllSlots()
    {
        foreach (var slot in inventorySlots)
        {
            slot.ClearSlot();
        }
        inventorySlots.Clear(); // ����Ʈ�� ���ϴ�.
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
