using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    private void Start()
    {
        itemName = "heal"; // 아이템 이름 초기화
        // 코드에서 아이콘 설정
        //itemIcon = Resources.Load<Sprite>("Icons/HealIcon"); // Icons 폴더에 HealIcon이라는 이름의 스프라이트가 있어야 함
        description = "test"; // 설명 초기화
        itemID = 1;
        isUsable = true;
        allowedDropZones = new List<string> { "SafeZone" };
    } 
    
    // 아이템 사용 메서드
    public override void Use()
    {
        if (isUsable)
        {
            Debug.Log($"{itemName} 사용");
            // 아이템 사용 로직 추가 (예: 체력 회복 등)
            InventoryUI.Instance.RemoveItem(this); // 아이템 사용 후 제거
        }
        else
        {
            Debug.Log($"{itemName}는 사용할 수 없습니다.");
        }
    }


    // 아이템 상세보기 메서드
    public override string ShowDetails()
    {
        //string details = $"{itemName}: {description}, 체력 회복량: {healAmount}";
        string details = description;
        Debug.Log(details);
        return details;
        // UI로 상세 정보를 표시하는 로직이 필요하다면 여기에 추가
        // 예: itemDetailPanel.GetComponentInChildren<Text>().text = details;
    }
}
