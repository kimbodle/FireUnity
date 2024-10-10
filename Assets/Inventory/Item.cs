using UnityEngine;

public abstract class Item: MonoBehaviour
{
    public string itemName; // 아이템 이름
    public Sprite itemIcon; // 아이템 아이콘
    public string description; // 아이템 설명
    public int itemID; // 아이템 ID
    public bool isUsable; // 아이템 사용 가능 여부

    // 아이템 사용 메서드 (각 아이템에 따라 구현)
    public abstract void Use();

    // 아이템 상세보기를 반환하는 메서드
    public abstract string ShowDetails();
}
