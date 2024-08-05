using UnityEngine;

public class Day1Controller : DayController
{
    public override void Initialize(int task)
    {
        // �ʱ�ȭ ����
        if (task > 1)
        {
            // Task1 �Ϸ� ���·� ����
            FindItem();
        }

    }

    public override void CompleteTask(int task)
    {
        // Task �Ϸ� �� ����
        if (task == 1)
        {
            FindItem();
        }
    }

    private void FindItem()
    {
        gameState["itemFound"] = true;
        gameState["labVisible"] = true;
    }

    public override bool IsDayComplete(int task)
    {
        // Day1�� ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return task > 2; // ��: Day1�� 2���� Task�� ����
    }
}
