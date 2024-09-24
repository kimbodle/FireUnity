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

    // Task ���� ��Ȳ �ҷ�����
    protected void LoadProgress(string currentTask)
    {
        Debug.Log($"LoadProgress: currentTask = {currentTask}");

        // �Ϸ�� �۾����� ������ ����Ʈ
        List<string> completedTasks = new List<string>();

        // gameState�� ��ȸ�ϸ� �Ϸ�� �۾� Ȯ��
        foreach (var taskKey in gameState.Keys)
        {
            if (gameState[taskKey])
            {
                Debug.Log($"{taskKey} task�� �Ϸ��.");
                completedTasks.Add(taskKey);  // �Ϸ�� �۾��� ����Ʈ�� �߰�
            }
            else if (taskKey == currentTask)
            {
                Debug.Log($"���� �۾��� {currentTask}, �۾� ���� ��...");
                completedTasks.Add(currentTask);  // ���� ���� ���� �۾� �߰�
            }
        }

        // �Ϸ�� �۾��� ó��
        foreach (var taskKey in completedTasks)
        {
            HandleTaskCompletion(taskKey);
        }
    }

    // Task�� �ļ� �۾� ó��
    protected virtual void HandleTaskCompletion(string taskKey)
    {
        Debug.Log($"{taskKey}�� �ļ� �۾� ó��");
    }

    protected void MarkTaskComplete(string task)
    {
        if (gameState.ContainsKey(task))
        {
            gameState[task] = true;
            Debug.Log($"{task} �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"Task {task}�� gameState�� �������� �ʽ��ϴ�.");
        }
    }

    protected void UpdateCurrentTask(string task)
    {
        gameManager.currentTask = task;
        Debug.Log($"currentTask ������Ʈ: {task}");
    }
}
