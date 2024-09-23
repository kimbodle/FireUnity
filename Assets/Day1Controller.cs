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

        // Task �Ϸ� �� currentTask ������Ʈ
        UpdateCurrentTask(task);
    }

    private void CompleteFindItem()
    {
        gameState["FindItem"] = true;
        Debug.Log("FindItem �Ϸ�");
    }

    private void CompleteMapClick()
    {
        gameState["MapClick"] = true;
        Debug.Log("MapClick �Ϸ�");
    }

    private void CompleteMoveShelter()
    {
        gameState["Moveshelter"] = true;
        Debug.Log("Moveshelter �Ϸ�");
    }

    private void UpdateCurrentTask(string task)
    {
        gameManager.currentTask = task;
        Debug.Log($"currentTask ������Ʈ: {task}");
    }

    public override bool IsDayComplete(string task)
    {
        // ��� Task�� �Ϸ�Ǿ����� Ȯ��
        return task == "Moveshelter";
    }

    protected override void HandleTaskCompletion(string taskKey)
    {
        switch (taskKey)
        {
            //�� �̵��� �� ���µ� �����ؾ��� ��
            case "FindItem":
                Debug.Log("FindItem �ļ� �۾� ó��");
                break;

            case "MapClick":
                Debug.Log("MapClick �ļ� �۾� ó��");
                break;

            case "Moveshelter":
                Debug.Log("Moveshelter �ļ� �۾� ó��");
                break;

            default:
                Debug.LogWarning($"{taskKey}�� �������� �ʴ� �۾��Դϴ�.");
                break;
        }
    }
}
