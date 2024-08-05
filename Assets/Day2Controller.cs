using UnityEngine;

public class Day2Controller : DayController
{
    public override void Initialize(int task)
    {
        // 초기화 로직
    }

    public override void CompleteTask(int task)
    {
        // Task 완료 시 로직
    }

    public override bool IsDayComplete(int task)
    {
        // Day2의 모든 Task가 완료되었는지 확인
        return task > 3; // 예: Day2는 3개의 Task가 있음
    }
}
