using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day1Controller : DayController
{
    private TaskHandler taskHandler;

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        taskHandler = GetComponent<TaskHandler>();
    }

    public override void Initialize(string task)
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager is null");
            return;
        }

        // GameManager���� gameState�� ������ ���
        gameState = gameManager.gameState;

        // Task ���� ��Ȳ �ε�
        LoadProgress(task);
    }

    public override void CompleteTask(string task)
    {
        Debug.Log($"{task} �۾��� �����.");

        // Task �Ϸ� �� ���� ó��
        switch (task)
        {
            case "FindItem":
                MarkTaskComplete("FindItem");
                break;

            case "MapClick":
                MarkTaskComplete("MapClick");
                break;

            case "Moveshelter":
                MarkTaskComplete("Moveshelter");
                break;

            default:
                Debug.LogWarning($"Unknown task: {task}");
                break;
        }

        // Task �Ϸ� �� currentTask ������Ʈ
        UpdateCurrentTask(task);
    }

    public override bool IsDayComplete(string task)
    {
        // ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return task == "Moveshelter";
    }

    protected override void HandleTaskCompletion(string taskKey)
    {
        if (taskKey == "Start")
        {
            ReloadCurrentScene();
            return;
        }

        taskHandler.HandleTask(taskKey);
    }

    public void ReloadCurrentScene()
    {
        // ���� ���� �̸��� �����´�.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // ���� �ٽ� �ε��Ѵ�.
        SceneManager.LoadScene(currentSceneName);
    }
}
