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
        // 초기화 로직
    }

    public override void CompleteTask(string task)
    {
        // Task 완료 시 로직
    }

    public override bool IsDayComplete(string task)
    {
        // Day2의 모든 Task가 완료되었는지 확인
        return task == ""; // 예: Day2는 3개의 Task가 있음
    }
}
