using System.Collections.Generic;
using UnityEngine;

public class Day1Controller : DayController
{
    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void Initialize(string task)
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager is null");
            return;
        }

        // GameManager에서 gameState를 가져와 사용
        gameState = gameManager.gameState;

        // Task 진행 상황 로드
        LoadProgress(task);
    }

    public override void CompleteTask(string task)
    {
        Debug.Log($"{task} 작업이 수행됨.");

        // Task 완료 시 로직 처리
        switch (task)
        {
            case "FindItem":
                CompleteFindItem();
                break;

            case "MapClick":
                CompleteMapClick();
                break;

            case "Moveshelter":
                CompleteMoveShelter();
                break;

            default:
                Debug.LogWarning($"Unknown task: {task}");
                break;
        }

        // Task 완료 후 currentTask 업데이트
        UpdateCurrentTask(task);
    }

    private void CompleteFindItem()
    {
        gameState["FindItem"] = true;
        Debug.Log("FindItem 완료");
    }

    private void CompleteMapClick()
    {
        gameState["MapClick"] = true;
        Debug.Log("MapClick 완료");
    }

    private void CompleteMoveShelter()
    {
        gameState["Moveshelter"] = true;
        Debug.Log("Moveshelter 완료");
    }

    private void UpdateCurrentTask(string task)
    {
        gameManager.currentTask = task;
        Debug.Log($"currentTask 업데이트: {task}");
    }

    public override bool IsDayComplete(string task)
    {
        // 모든 Task가 완료되었는지 확인
        return task == "Moveshelter";
    }

    protected override void HandleTaskCompletion(string taskKey)
    {
        switch (taskKey)
        {
            //씬 이동만 한 상태도 저장해야할 듯
            case "FindItem":
                Debug.Log("FindItem 후속 작업 처리");
                break;

            case "MapClick":
                Debug.Log("MapClick 후속 작업 처리");
                break;

            case "Moveshelter":
                Debug.Log("Moveshelter 후속 작업 처리");
                break;

            default:
                Debug.LogWarning($"{taskKey}는 지원되지 않는 작업입니다.");
                break;
        }
    }
}
