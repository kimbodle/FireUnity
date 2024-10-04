using UnityEngine;

public class Day2Controller : DayController
{
    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        MapManager.Instance.UpdateMapRegions();
    }
    public override void Initialize(string task)
    {
        // �ʱ�ȭ ����
    }

    public override void CompleteTask(string task)
    {
        // Task �Ϸ� �� ����
    }

    public override bool IsDayComplete(string task)
    {
        // Day2�� ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return task == ""; // ��: Day2�� 3���� Task�� ����
    }
}
