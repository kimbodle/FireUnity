using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    public int healAmount; // 회복량

    // 아이템 사용 메서드
    public override void Use()
    {
        if (isUsable)
        {
            // 실제 체력 회복 로직 추가 필요 (예: 플레이어 체력 회복)
            Debug.Log("드래그");
        }
        else
        {
            Debug.Log($"{itemName}는 사용할 수 없습니다.");
        }
    }

    // 아이템 상세보기 메서드
    public override string ShowDetails()
    {
        string details = $"{itemName}: {description}, 체력 회복량: {healAmount}";
        Debug.Log(details);
        return details;
        // UI로 상세 정보를 표시하는 로직이 필요하다면 여기에 추가
        // 예: itemDetailPanel.GetComponentInChildren<Text>().text = details;
    }
}
