public interface IInteractable
{
    void OnInteract(); // ��ȣ�ۿ� �� ȣ��� �޼ҵ�
    string GetInteractionMessage(); // ��ȣ�ۿ� �޽���
    void HandleTask(string taskKey); // �ļ� ó���� ���� �޼ҵ� �߰�
}