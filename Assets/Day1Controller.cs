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
        Debug.Log(task + "�۾��� �����.");
        // Task �Ϸ� �� ����
        if (task == 1)
        {
            FindItem();
        }
        if (task == 2)
        {
            Task2();
        }
        //�̷��� task == 3 �϶� day2��°�
    }

    private void FindItem()
    {
        gameState["itemFound"] = true;
        gameState["labVisible"] = true;
    }

    private void Task2()
    {
        gameState["Task2"] = true;
    }

    public override bool IsDayComplete(int task)
    {
        // Day1�� ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return task > 2; // ��: Day1�� 2���� Task�� ����
    }
}
