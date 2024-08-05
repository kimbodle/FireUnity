using UnityEngine;

public class Day1Controller : DayController
{
    public override void Initialize(int task)
    {
        // 초기화 로직
        if (task > 1)
        {
            // Task1 완료 상태로 시작
            FindItem();
        }

    }

    public override void CompleteTask(int task)
    {
        // Task 완료 시 로직
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
        // Day1의 모든 Task가 완료되었는지 확인
        return task > 2; // 예: Day1은 2개의 Task가 있음
    }
}
