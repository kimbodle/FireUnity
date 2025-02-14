public interface IInteractable
{
    void OnInteract(); // 상호작용 시 호출될 메소드
    string GetInteractionMessage(); // 상호작용 메시지
    void HandleTask(string taskKey); // 후속 처리를 위한 메소드 추가
}