using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName; // 아이템 이름
    public Sprite itemIcon; // 아이템 아이콘
    public string description; // 아이템 설명
    public int itemID; // 아이템 ID
    public bool isUsable; // 아이템 사용 가능 여부
    public List<string> allowedDropZones; // 아이템을 사용할 수 있는 드롭존 ID 목록

    // 아이템 사용 메서드 (각 아이템에 따라 구현)
    public Action<ItemData> UseAction;
    // 아이템 상세보기를 반환하는 메서드
    public string ShowDetails()
    {
        return $"{itemName}: {description}";
    }

    // 사용 메서드 호출
    public void Use()
    {
        UseAction?.Invoke(this); // UseAction이 null이 아닐 경우 호출
    }
}
