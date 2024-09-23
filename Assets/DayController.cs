using System.Collections.Generic;
using UnityEngine;

public abstract class DayController : MonoBehaviour
{
    protected Dictionary<string, bool> gameState = new Dictionary<string, bool>();
    protected GameManager gameManager;

    public abstract void Initialize(string currentTask);
    public abstract void CompleteTask(string task);
    public abstract bool IsDayComplete(string currentTask);

    public Dictionary<string, bool> GetGameState()
    {
        return new Dictionary<string, bool>(gameState);
    }

    public void SetGameState(Dictionary<string, bool> state)
    {
        gameState = new Dictionary<string, bool>(state);
    }

    // Task 진행 상황 불러오기
    protected void LoadProgress(string currentTask)
    {
        Debug.Log($"LoadProgress: currentTask = {currentTask}");

        // 완료된 작업들을 저장할 리스트
        List<string> completedTasks = new List<string>();

        // gameState를 순회하며 완료된 작업 확인
        foreach (var taskKey in gameState.Keys)
        {
            if (gameState[taskKey])
            {
                Debug.Log($"{taskKey} task가 완료됨.");
                completedTasks.Add(taskKey);  // 완료된 작업을 리스트에 추가
            }
            else if (taskKey == currentTask)
            {
                Debug.Log($"현재 작업은 {currentTask}, 작업 진행 중...");
                completedTasks.Add(currentTask);  // 현재 진행 중인 작업 추가
            }
        }

        // 완료된 작업들 처리
        foreach (var taskKey in completedTasks)
        {
            HandleTaskCompletion(taskKey);
        }
    }

    // Task별 후속 작업 처리
    protected virtual void HandleTaskCompletion(string taskKey)
    {
        Debug.Log($"{taskKey}의 후속 작업 처리");
        // 기본 구현은 로그만 출력하고, 필요한 경우 하위 클래스에서 오버라이드할 수 있도록 함
    }
}
