using UnityEngine;

public class Day2Controller : DayController
{
    public override void Initialize(int task)
    {
        // �ʱ�ȭ ����
    }

    public override void CompleteTask(int task)
    {
        // Task �Ϸ� �� ����
    }

    public override bool IsDayComplete(int task)
    {
        // Day2�� ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return task > 3; // ��: Day2�� 3���� Task�� ����
    }
}
